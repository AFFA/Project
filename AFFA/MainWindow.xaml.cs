using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AFFA.Mudelid;
using AFFA.Vaatemudelid;
using AFFA.Scraperid;
using AFFA.Graafikud;
using AFFA.DCFMudelid;
using System.Globalization;
using System.Threading.Tasks;

namespace AFFA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Boolean muutuja, mida hiljem kasutatakse MainWindow õigeks sättimisel vastavalt defaultiks valitud seadetele.
        private bool _akenOnLaetud = false;
        private InputVM _inputVm;
        private FinDataAdapter _finDataAdapter;
        private DcfInput _dci = new DcfInput();
        private DcfVM _dcfVM;
        private bool _passwordSet = false;
        private string _user;
        private string _password;

        public MainWindow()
        {
            InitializeComponent();
        }

        public bool PasswordSet
        {
            get { return _passwordSet; }
            set { _passwordSet = value; }
        }

        #region Evendid
        // Kui MainWindow sisu on renderdatud, siis saab kujunduselemente sättida, ilma et tekiks probleeme.
        // Näiteks ComboBoxis, kui default item on valitud, siis see tekitab akna laadimisel SelectionChanged eventi.
        public void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            _akenOnLaetud = true;
            ComboBoxItem cbi = (comboDataSource.SelectedItem as ComboBoxItem);
            comboDataSource.SelectedItem = null;
            comboDataSource.SelectedItem = cbi;

            _inputVm = new InputVM();
            _inputVm.LoadCompanyData();
            listViewCompanyDetails.DataContext = _inputVm;

            
            _dcfVM = new DcfVM(_dci);
            elementUserInputDatas.DataContext = _dcfVM;
        }

        private void btnAvaXMLFail_Click(object sender, RoutedEventArgs e)
        {
            //_inputVm.LaeAndmed("csco");
            labelProgrammiStaatus.Content = "Loading XML data ...";
            btnCalculateForecast.IsEnabled = false;
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                panelDcfOutput.DataContext = null;
                txtBoxAndmeteAllikas.IsEnabled = true;
                txtBoxAndmeteAllikas.Text = dialog.FileName;
                //txtBoxAndmeteAllikas.Foreground = Brushes.Black;
                //txtBoxAndmeteAllikas.CaretIndex = txtBoxAndmeteAllikas.Text.Length;
                //labelProgrammiStaatus.Content = "Data loaded from XML file (" + dialog.SafeFileName + ").";
                //var rect = txtBoxAndmeteAllikas.GetRectFromCharacterIndex(txtBoxAndmeteAllikas.CaretIndex);
                //txtBoxAndmeteAllikas.ScrollToHorizontalOffset(rect.Right);
                FinAnalysisVM finAnalysisVm = new FinAnalysisVM(dataGrid);
                _finDataAdapter = new FinDataAdapter(_inputVm, finAnalysisVm, "", FinDataAdapter.DataSource.XML, dialog.FileName);
                // testimiseks:
                _finDataAdapter.AddMainWindow(this);
                _finDataAdapter.AddDcfInput(_dci);

                _finDataAdapter.PrepareData();
                panelQuarterlyData.DataContext = finAnalysisVm;
                //btnCalculateForecast.IsEnabled = true;
            }
        }

        public void YchartsReady()
        {
            labelProgrammiStaatus.Content = "Data retrieved from YCharts.com.";
            btnCalculateForecast.IsEnabled = true;
        }

        public void XmlReady(string filename)
        {
            txtBoxAndmeteAllikas.Foreground = Brushes.Black;
            txtBoxAndmeteAllikas.CaretIndex = txtBoxAndmeteAllikas.Text.Length;
            labelProgrammiStaatus.Content = "Data loaded from XML file (" + filename + ").";
            var rect = txtBoxAndmeteAllikas.GetRectFromCharacterIndex(txtBoxAndmeteAllikas.CaretIndex);
            txtBoxAndmeteAllikas.ScrollToHorizontalOffset(rect.Right);
            btnCalculateForecast.IsEnabled = true;
        }

        private void btnRetrieveYCharts_Click(object sender, RoutedEventArgs e)
        {
            if (comboDataSource.SelectedIndex == 0)
            {
                labelProgrammiStaatus.Content = "Retrieving data from YCharts.com ...";
                btnCalculateForecast.IsEnabled = false;
                string symbol = txtBoxAndmeteAllikas.Text;
                if (string.IsNullOrEmpty(symbol) || symbol.Equals("input company ticker here") || symbol.Contains("."))
                {
                    MessageBox.Show("Illegal symbol or no symbol specified.");
                    return;
                }
                _inputVm.LaeAndmed(symbol);

                if (!PasswordSet)
                {
                    string[] promptValue = Prompt.ShowDialog("Enter YCharts.com Username");

                    string user = promptValue[0];
                    string psw = promptValue[1];
                    _user = user;
                    _password = psw;

                }
                if (!string.IsNullOrEmpty(_user) && !string.IsNullOrEmpty(_password))
                {
                    panelDcfOutput.DataContext = null;
                    FinAnalysisVM finAnalysisVm = new FinAnalysisVM(dataGrid);
                    _finDataAdapter = new FinDataAdapter(finAnalysisVm, symbol, FinDataAdapter.DataSource.XLS);
                    _finDataAdapter.AddDcfInput(_dci);
                    _finDataAdapter.PrepareDataXLS(_user, _password, this);
                    panelQuarterlyData.DataContext = finAnalysisVm;

                    // need peaks seatud saama alles siis, kui andmed saabuvad YCharts.com-st 
                    //labelProgrammiStaatus.Content = "Data retrieved from YCharts.com.";
                    //btnCalculateForecast.IsEnabled = true;
                }
                else
                {
                    PasswordSet = false;
                    MessageBox.Show("Enter username and password, fields cannot be empty.");

                }
            }
            else if (comboDataSource.SelectedIndex == 2)
            {
                btnLaeAndmed.IsEnabled = false;
                string programStatus = labelProgrammiStaatus.Content.ToString();
                labelProgrammiStaatus.Content = "Loading data from Google Finance.";
                GoogleFinanceScraper gfs = new GoogleFinanceScraper();
                var uiScheduler = TaskScheduler.FromCurrentSynchronizationContext(); //get UI thread context 
                string txtBoxAndmeteAllikasText = txtBoxAndmeteAllikas.Text;
                var task1 = Task.Factory.StartNew(() => gfs.GetData(txtBoxAndmeteAllikasText));
                task1.ContinueWith(task => {
                    if (gfs.DownloadedData == null)
                    {
                        MessageBox.Show("Failed to download data for this ticker (" + txtBoxAndmeteAllikasText + ").");
                        labelProgrammiStaatus.Content = programStatus;
                    }
                    else
                    {
                        panelDcfOutput.DataContext = null;
                        labelProgrammiStaatus.Content = "Data loaded from Google Finance.";
                        FinAnalysisVM finAnalysisVm = new FinAnalysisVM(dataGrid);
                        _finDataAdapter = new FinDataAdapter(_inputVm, finAnalysisVm, "", FinDataAdapter.DataSource.XML, gfs.XmlPath);
                        // testimiseks:
                        _finDataAdapter.AddMainWindow(this);
                        _finDataAdapter.AddDcfInput(_dci);

                        _finDataAdapter.PrepareData();
                        panelQuarterlyData.DataContext = finAnalysisVm;
                    }
                    btnLaeAndmed.IsEnabled = true;
                }, uiScheduler);
            }


        }



        private void ComboDataSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_akenOnLaetud)
            {
                if (comboDataSource.SelectedIndex == 1)
                {
                    btnLaeAndmed.Visibility = System.Windows.Visibility.Collapsed;
                    btnAvaXMLFail.Visibility = System.Windows.Visibility.Visible;
                    txtBoxAndmeteAllikas.IsEnabled = false;
                    txtBoxAndmeteAllikas.Text = "";
                    txtBoxAndmeteAllikas.CaretIndex = txtBoxAndmeteAllikas.Text.Length;
                    var rect = txtBoxAndmeteAllikas.GetRectFromCharacterIndex(txtBoxAndmeteAllikas.CaretIndex);
                    txtBoxAndmeteAllikas.ScrollToHorizontalOffset(rect.Right);
                    txtBoxAndmeteAllikas.IsReadOnly = true;
                    labelAndmeteAllikas.Content = "File location:";
                }
                else
                {
                    btnLaeAndmed.Visibility = System.Windows.Visibility.Visible;
                    btnAvaXMLFail.Visibility = System.Windows.Visibility.Collapsed;
                    txtBoxAndmeteAllikas.IsEnabled = true;
                    txtBoxAndmeteAllikas.Text = "input company ticker here";
                    txtBoxAndmeteAllikas.IsReadOnly = false;
                    labelAndmeteAllikas.Content = "Company ticker:";
                }
            }
        }

        private void btnSeaded_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Owner = this;
            sw.ShowDialog();
        }

        public void txtBoxAndmeteAllikas_GotFocus(object sender, EventArgs e)
        {
            if (txtBoxAndmeteAllikas.Text.Equals("input company ticker here"))
            {
                txtBoxAndmeteAllikas.Text = "";
            }
        }

        public void txtBoxAndmeteAllikas_LostFocus(object sender, EventArgs e)
        {
            if (txtBoxAndmeteAllikas.Text == null || txtBoxAndmeteAllikas.Text.Equals(""))
            {
                txtBoxAndmeteAllikas.Text = "input company ticker here";
            }
        }

        public void listViewCompanyDetails_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newSize = e.NewSize.Width;
            ValueColumn.Width = (int)newSize - 160;
        }

        private void btnCalculateForecast_Click(object sender, RoutedEventArgs e)
        {
            if (_finDataAdapter == null)
            {
                MessageBox.Show("Load data first.");
            }
            else
            {
                MessageBox.Show("Calculating forecast");
                //DcfDataDao dcfDataDao= new DcfDataDao();
                //_finDataAdapter.addDcfDataDao(dcfDataDao);
                //DcfCalculator.GenerateDcfData(_finDataAdapter.FinDataDao.FinDatas, dcfDataDao);
                //DcfInput dcfInput = new DcfInput();
                //_finDataAdapter.AddDcfInput(dcfInput);
                //DcfCalculator.CalculateQuaterlyForecasts(dcfDataDao.DcfDatas,dcfInput);
                if (_finDataAdapter.FinDataDao.FinDatas.Count > 0)
                {
                    DcfOutput dcfOutput = new DcfOutput();
                    _finDataAdapter.AddDcfOutput(dcfOutput);
                    _dcfVM.PrepareCalculations(dataGridForecast, _finDataAdapter.DcfDataDao, _dci,
                                               _finDataAdapter.FinDataDao, _finDataAdapter);
                    _dcfVM.GetDcf();
                    _dcfVM.ClearTable();
                    _dcfVM.PrepareTable(_finDataAdapter.DcfDataDao.DcfDatas);
                    panelDcfOutput.DataContext = _dcfVM;
                    panelForecast.DataContext = _dcfVM;
                }
                else
                {
                    MessageBox.Show("No data available. Please load new XML or retrieve data from the web.");
                }
        }
            /*YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData("CSCO");
            FinDataAdapter finDataAdapter = new FinDataAdapter("csco", FinDataAdapter.DataSource.XLS, data);
            finDataAdapter.PrepareData();
            FinAnalysisVM finAnalysisVm = new FinAnalysisVM(finDataAdapter.FinDataDao.FinDatas, dataGrid);
            panelQuarterlyData.DataContext = finAnalysisVm;*/
        }
        #endregion
        #region Graafikud



        private void Button_Click_EpsDiluted(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 1).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_Revenue(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 2).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_GrossProfitMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 3).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_OperatingMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 4).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_ProfitMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 5).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 6).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_BsTotalCurrentAssets_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 7).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_BsTotalCurrentLiabilities_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 8).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrEqPrc(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 9).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_ReturnOnEquity(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 10).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_IsNetIncome_Divided_IsPretaxIncome(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 11).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_IsPretaxIncome_Divided_FrEbit(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 12).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_FrOperatingMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 13).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_IsRevenue_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 14).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_BsTotalAssets_Divided_BsShareholdersEquity1(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 15).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrPeRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 16).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrPegRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 17).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrPriceBookValue(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 18).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrPriceSalesRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 19).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrEvEbitda(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 20).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrEvFreeCashFlow(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 21).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }

        private void Button_Click_FrCashConversionCycle(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 22).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_FrDaysInventoryOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 23).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_FrDaysSalesOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 24).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        private void Button_Click_FrDaysPayableOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 25).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load data first.");
            }
        }
        #endregion


        private void CheckPercentTextBoxInput(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            double arv;
            if (double.TryParse(txtBox.Text, NumberStyles.Any ^ NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("et-EE"), out arv))
            {
                if (arv < 0)
                {
                    ToolTip tooltip1 = new ToolTip();
                    tooltip1.Content = "Must be positive";
                    txtBox.ToolTip = tooltip1;
                }
            }
            else
            {
                ToolTip tooltip1 = new ToolTip();
                tooltip1.Content = "Must be a number";
                txtBox.ToolTip = tooltip1;

            }
        }

        private void ComboForecastingMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_akenOnLaetud)
            {
                if (ComboForecastingMethod.SelectedIndex == 0)
                {
                    Group4.IsEnabled = false;
                    _dci.ForecastMethod = DcfInput.ForecastingMethod.LinearRegression;
                }
                else if (ComboForecastingMethod.SelectedIndex == 1)
                {
                    Group4.IsEnabled = true;
                    _dci.ForecastMethod = DcfInput.ForecastingMethod.AverageMargins;
                }
            }
        }
    }

    public class DataItem
    {
        private string header;
        private string data;
        public string Header { get { return header; } set { header = value; } }
        public string Data { get { return data; } set { data = value; } }
    }
}
