using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using AFFA.Mudelid;
using AFFA.Vaatemudelid;
using AFFA.Scraperid;
using AFFA.Graafikud;
using AFFA.DCFMudelid;

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
        private DcfInput _dci;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Evendid
        // Kui MainWindow sisu on renderdatud, siis saab kujunduselemente sättida, ilma et tekiks probleeme.
        // Näiteks ComboBoxis, kui default item on valitud, siis see tekitab akna laadimisel SelectionChanged eventi.

        public void SetInputs()
        {
            txtBox_DcfInput_TaxRate.Text = (_dci.TaxRate * 100).ToString();
            txtBox_DcfInput_CostOfDebt.Text = (_dci.CostOfDebt * 100).ToString();
            txtBox_DcfInput_RiskFreeRate.Text = (_dci.RiskFreeRate * 100).ToString();
            txtBox_DcfInput_MarketRiskPremium.Text = (_dci.MarketRiskPremium * 100).ToString();
            txtBox_DcfInput_GrowthRatePrognosis.Text = (_dci.GrowthRatePrognosis * 100).ToString();
            txtBox_DcfInput_ContinuousGrowth.Text = (_dci.ContinuousGrowth * 100).ToString();
            txtBox_DcfInput_AllCostsPrcRevenue.Text = (_dci.AllCostsPrcRevenue * 100).ToString();
            txtBox_DcfInput_EbitdaPrcRevenue.Text = (_dci.EbitdaPrcRevenue * 100).ToString();
            txtBox_DcfInput_EbitPrcRevenue.Text = (_dci.EbitPrcRevenue * 100).ToString();
            txtBox_DcfInput_TotalAssetsPrcRevenue.Text = (_dci.TotalAssetsPrcRevenue * 100).ToString();
            txtBox_DcfInput_TotalCurrentAssetsPrcRevenue.Text = (_dci.TotalCurrentAssetsPrcRevenue * 100).ToString();
            txtBox_DcfInput_TotalCurrentLiabilitiesPrcRevenue.Text = (_dci.TotalCurrentLiabilitiesPrcRevenue * 100).ToString();
            txtBox_DcfInput_TotalLiabilitiesPrcRevenue.Text = (_dci.TotalLiabilitiesPrcRevenue * 100).ToString();
        }

        public void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            _akenOnLaetud = true;
            ComboBoxItem cbi = (comboDataSource.SelectedItem as ComboBoxItem);
            comboDataSource.SelectedItem = null;
            comboDataSource.SelectedItem = cbi;

            _inputVm = new InputVM();
            _inputVm.LoadCompanyData();
            listViewCompanyDetails.DataContext = _inputVm;

            _dci = new DcfInput();
            elementUserInputDatas.DataContext = _dci;
            SetInputs();
        }

        private void btnAvaXMLFail_Click(object sender, RoutedEventArgs e)
        {
            //_inputVm.LaeAndmed("csco");
           
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtBoxAndmeteAllikas.IsEnabled = true;
                txtBoxAndmeteAllikas.Text = dialog.FileName;
                txtBoxAndmeteAllikas.Foreground = Brushes.Black;
                txtBoxAndmeteAllikas.CaretIndex = txtBoxAndmeteAllikas.Text.Length;
                labelProgrammiStaatus.Content = "Data loaded from XML file ("+dialog.SafeFileName+").";
                var rect = txtBoxAndmeteAllikas.GetRectFromCharacterIndex(txtBoxAndmeteAllikas.CaretIndex);
                txtBoxAndmeteAllikas.ScrollToHorizontalOffset(rect.Right);
                FinAnalysisVM finAnalysisVm = new FinAnalysisVM(dataGrid);
                _finDataAdapter = new FinDataAdapter(_inputVm, finAnalysisVm, "", FinDataAdapter.DataSource.XML, dialog.FileName);
                // testimiseks:
                _finDataAdapter.AddMainWindow(this);
                _finDataAdapter.AddDcfInput(_dci);

                _finDataAdapter.PrepareData();
                panelQuarterlyData.DataContext = finAnalysisVm;
                btnCalculateForecast.IsEnabled = true;
            }
        }

        private void btnRetrieveYCharts_Click(object sender, RoutedEventArgs e)
        {
            string symbol = txtBoxAndmeteAllikas.Text;
            if (string.IsNullOrEmpty(symbol) || symbol.Equals("input company ticker here") || symbol.Contains("."))
            {
                MessageBox.Show("Illegal symbol or no symbol specified.");
                return;
            }
            _inputVm.LaeAndmed(symbol);
            string[] promptValue = Prompt.ShowDialog("Enter YCharts.com Username");

            string user =promptValue[0];
            string psw = promptValue[1];
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(psw))
            {
                labelProgrammiStaatus.Content = "Data retrieved from YCharts.com.";
                FinAnalysisVM finAnalysisVm = new FinAnalysisVM(dataGrid);
                _finDataAdapter = new FinDataAdapter(finAnalysisVm, symbol, FinDataAdapter.DataSource.XLS);
                _finDataAdapter.AddDcfInput(_dci);
                _finDataAdapter.PrepareDataXLS(user, psw);
                panelQuarterlyData.DataContext = finAnalysisVm;
                btnCalculateForecast.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Enter username and password, cannot be empty.");
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
            ValueColumn.Width = (int)newSize-160;
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
                double curDouble;
                if (!string.IsNullOrEmpty(txtBox_DcfInput_TaxRate.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_TaxRate.Text, out curDouble))
                    {
                        _dci.TaxRate = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_CostOfDebt.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_CostOfDebt.Text, out curDouble))
                    {
                        _dci.CostOfDebt = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_RiskFreeRate.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_RiskFreeRate.Text, out curDouble))
                    {
                        _dci.RiskFreeRate = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_MarketRiskPremium.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_MarketRiskPremium.Text, out curDouble))
                    {
                        _dci.MarketRiskPremium = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_GrowthRatePrognosis.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_GrowthRatePrognosis.Text, out curDouble))
                    {
                        _dci.GrowthRatePrognosis = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_ContinuousGrowth.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_ContinuousGrowth.Text, out curDouble))
                    {
                        _dci.ContinuousGrowth = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_AllCostsPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_AllCostsPrcRevenue.Text, out curDouble))
                    {
                        _dci.AllCostsPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_EbitdaPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_EbitdaPrcRevenue.Text, out curDouble))
                    {
                        _dci.EbitdaPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_DepreciationPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_DepreciationPrcRevenue.Text, out curDouble))
                    {
                        _dci.DepreciationPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_EbitPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_EbitPrcRevenue.Text, out curDouble))
                    {
                        _dci.EbitPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_TotalAssetsPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_TotalAssetsPrcRevenue.Text, out curDouble))
                    {
                        _dci.TotalAssetsPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_TotalLiabilitiesPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_TotalLiabilitiesPrcRevenue.Text, out curDouble))
                    {
                        _dci.TotalLiabilitiesPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_TotalCurrentAssetsPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_TotalCurrentAssetsPrcRevenue.Text, out curDouble))
                    {
                        _dci.TotalCurrentAssetsPrcRevenue = curDouble / 100;
                    }
                }
                if (!string.IsNullOrEmpty(txtBox_DcfInput_TotalCurrentLiabilitiesPrcRevenue.Text))
                {
                    if (double.TryParse(txtBox_DcfInput_TotalCurrentLiabilitiesPrcRevenue.Text, out curDouble))
                    {
                        _dci.TotalCurrentLiabilitiesPrcRevenue = curDouble / 100;
                    }
                }
                //DcfCalculator.CalculateQuaterlyForecasts(dcfDataDao.DcfDatas,dcfInput);
                DcfOutput dcfOutput=new DcfOutput();
                _finDataAdapter.AddDcfOutput(dcfOutput);
                DcfVM dcfVM = new DcfVM(dataGridForecast, _finDataAdapter.DcfDataDao, _dci, _finDataAdapter.FinDataDao, _finDataAdapter);
                dcfVM.GetDcf();
                dcfVM.ClearTable();
                dcfVM.PrepareTable(_finDataAdapter.DcfDataDao.DcfDatas);
                panelDcfOutput.DataContext = dcfOutput;
                panelForecast.DataContext = dcfVM;
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
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_Revenue(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 2).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_GrossProfitMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 3).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_OperatingMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 4).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_ProfitMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 5).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 6).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_BsTotalCurrentAssets_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 7).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_BsTotalCurrentLiabilities_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 8).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrEqPrc(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 9).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_ReturnOnEquity(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 10).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_IsNetIncome_Divided_IsPretaxIncome(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 11).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_IsPretaxIncome_Divided_FrEbit(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 12).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_FrOperatingMargin(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 13).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_IsRevenue_Divided_BsTotalAssets(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 14).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_BsTotalAssets_Divided_BsShareholdersEquity1(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 15).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrPeRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 16).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrPegRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 17).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrPriceBookValue(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 18).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrPriceSalesRatio(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 19).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrEvEbitda(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 20).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrEvFreeCashFlow(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 21).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }

        private void Button_Click_FrCashConversionCycle(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 22).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_FrDaysInventoryOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 23).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_FrDaysSalesOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 24).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        private void Button_Click_FrDaysPayableOutstanding(object sender, RoutedEventArgs e)
        {
            try { new Graafik(_finDataAdapter.FinDataDao.FinDatas, 25).Show(); }
            catch (NullReferenceException)
            {
                MessageBox.Show("Lae andmed!");
            }
        }
        #endregion


        private void CheckPercentTextBoxInput(object sender, RoutedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            double arv;
            if (double.TryParse(txtBox.Text, out arv))
            {
                if (arv < 0)
                {
                    MessageBox.Show("Must be positive");
                    txtBox_DcfInput_MarketRiskPremium.Text = null;
                }
            }
            else
            {
                MessageBox.Show("Must be a number");
                txtBox_DcfInput_MarketRiskPremium.Text = null;
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
