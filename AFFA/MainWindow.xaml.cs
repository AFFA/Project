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
        
        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void btnAvaXMLFail_Click(object sender, RoutedEventArgs e)
        {
            _inputVm.LaeAndmed("csco");
            
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
                FinDataAdapter finDataAdapter = new FinDataAdapter("csco", FinDataAdapter.DataSource.XML, dialog.FileName);
                finDataAdapter.PrepareData();
                FinAnalysisVM finAnalysisVm = new FinAnalysisVM(finDataAdapter.FinDataDao.FinDatas, dataGrid);
                panelQuarterlyData.DataContext = finAnalysisVm;
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

        private void btnLaeYChartsExcelData_Click(object sender, RoutedEventArgs e)
        {
            YChartsExcelScraper yExcel = new YChartsExcelScraper();
            XDocument data = yExcel.GetData("CSCO", "tulevikus failinimi", new FinDataDao());
            FinDataAdapter finDataAdapter = new FinDataAdapter("csco", FinDataAdapter.DataSource.XLS, data);
            finDataAdapter.PrepareData();
            FinAnalysisVM finAnalysisVm = new FinAnalysisVM(finDataAdapter.FinDataDao.FinDatas, dataGrid);
            panelQuarterlyData.DataContext = finAnalysisVm;
        }
        #endregion
    }

    public class DataItem
    {
        private string header;
        private string data;
        public string Header { get { return header; } set { header = value; } }
        public string Data { get { return data; } set { data = value; } }
    }
}
