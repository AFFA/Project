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
using AFFA.Mudelid;
using AFFA.Vaatemudelid;

namespace AFFA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAvafail_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                MessageBoxResult result = MessageBox.Show(dialog.FileName);
                FinDataAdapter finDataAdapter = new FinDataAdapter("csco", FinDataAdapter.DataSource.XML, dialog.FileName);
                finDataAdapter.PrepareData();
                //InputVM vm = new InputVM();
                InputVM vm = new InputVM(finDataAdapter.FinDataDao.FinDatas);
                int columnIndex = 0;

                for (int i = 0; i < vm.ShowTable[0].GetSize(); i++)
                {
                    dataGrid.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = vm.ColumnHeader[columnIndex],
                            //Header = "veerg" + columnIndex,
                            Binding = new Binding(
                                string.Format("Values[{0}]", columnIndex++))
                        });
                }
                gridMainWindow.DataContext = vm;

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
