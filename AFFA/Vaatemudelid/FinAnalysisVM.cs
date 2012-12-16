using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using AFFA.Mudelid;
using System.Windows;

namespace AFFA.Vaatemudelid
{
    /// <summary>
    /// Vaatemudel Financial analysis tabi jaoks
    /// </summary>
    public class FinAnalysisVM
    {
        private ObservableCollection<FinAnalysisData> _showTable;
        private int maxColumns = 20;
        //private List<KeyValuePair<string, string>> _rowMapping;
        private List<Rowmapping.RowConf> _rowMapping;
        private IList<String> _columnHeader;
        private DataGrid _dataGrid;
        private bool _columnsPrepared = false;

        #region getterid, setterid
        public ObservableCollection<FinAnalysisData> ShowTable
        {
            get { return _showTable; }
        }

        public IList<string> ColumnHeader
        {
            get { return _columnHeader; }
        }
        #endregion

        #region konstruktorid
        public FinAnalysisVM(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
            _rowMapping = Rowmapping.EnglishRows();
            _showTable = new ObservableCollection<FinAnalysisData>();
            _columnHeader = new List<string>();
        }
        #endregion

        /// <summary>
        /// Puhasta tabel
        /// </summary>
        public void ClearTable()
        {
            _showTable.Clear();
            _dataGrid.Columns.Clear();

            //_showTable = new ObservableCollection<FinAnalysisData>(); 
            //_dataGrid.DataContext=null;
        }


        /// <summary>
        /// Veergude pealkirjade loomine (hetkel seal enamasti kuupäevad või % märgid)
        /// </summary>
        private void GenerateColumnHeaders()
        {
            int columnIndex = 0;

            try
            {
                for (int i = 0; i < ColumnHeader.Count; i++)
                {
                    //MessageBox.Show(ColumnHeader[i]);
                    _dataGrid.Columns.Add(
                        new DataGridTextColumn
                        {
                            Header = ColumnHeader[i],
                            Binding = new Binding(
                                    string.Format("Values[{0}]", i))

                        });


                }
            }
            catch (ArgumentOutOfRangeException) { }
        }



        /// <summary>
        /// Public meetod tabeli valmistamiseks
        /// </summary>
        /// <param name="finDatas"></param>
        public void PrepareTable(List<FinData> finDatas)
        {
            ClearTable();
            GenerateTableData(finDatas);
            GenerateColumnHeaders();

        }

        /// <summary>
        /// Meetodis toimub sisuline tabeli sisu (read, veerud) valmistamine
        /// </summary>
        /// <param name="finDatas"></param>
        private void GenerateTableData(List<FinData> finDatas)
        {
            _columnHeader = new List<string>();
            for (int j = 0; j < _rowMapping.Count; j++) // tekitame ridu nii palju, kui on rowMapping'us antud
            {
                FinAnalysisData row = new FinAnalysisData(); // iga rida on seda tüüpi klass ehk sisuliselt stringide list
                row.AddData(_rowMapping[j].Label); // igasse ritta paneme esimesele kohale (veergu) rea nime
                if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                {
                    _columnHeader.Add(""); // esimese veeru pealkiri
                }
                int k = 0; // seame muutuja, mis loeb, mitu kvartalit on sisestatud
                //MessageBox.Show(finDatas.Count.ToString());
                for (int i = finDatas.Count - 1; i >= 0; i--) // tekitame veerge
                {
                    if (k < maxColumns) // piirame ära, mitu kvartalit tabelisse kirjutatakse  
                    {
                        if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                        {
                            _columnHeader.Add(finDatas[i].Kuupaev.ToShortDateString());
                        }
                        PropertyInfo pi = finDatas[i].GetType().GetProperty(_rowMapping[j].Propery); // sellega saame kätte finData property, mille nimi on defineeritud _rowMapping klassis
                        double? currentQ = (double?)pi.GetValue(finDatas[i]); // kuigi property on meil käes, tuleb selle property väärtuse kättesaamiseks alati anda ka objekt ise parameetrina
                        String formatNumber = "{0:0}"; // tavaolukorras formateerime selliselt
                        if (_rowMapping[j].Decimals.Equals(Rowmapping.RowFormat.Decimal2)) // valida read, mille puhul numbriformaat on erinev (kaks nulli peale koma)
                        {
                            formatNumber = "{0:0.00}";
                        }
                        else if (_rowMapping[j].Decimals.Equals(Rowmapping.RowFormat.Prc1))
                        {
                            formatNumber = "{0:P1}";
                        }
                        else if (_rowMapping[j].Decimals.Equals(Rowmapping.RowFormat.Prc2))
                        {
                            formatNumber = "{0:P2}";
                        }
                        row.AddData(String.Format(formatNumber, currentQ)); // lisame ritta numbrilise väärtuse
                        // iga järgmine veergu on % muudu veerg ning siin arvutame välja % muudu, kui see on ette nähtud
                        if (i >= 4) // kui meil pole piisavalt andmeid, siis ei saa % muutu arvutada ja i-4 annab errori. 
                        {
                            if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                            {
                                _columnHeader.Add("%"); // veeru pealkiri on alati %
                            }
                            double? divisor = (double?)pi.GetValue(finDatas[i - 4]); // leiame sama property väärtuse 4 kvartalit tagasi, et % muutu arvutada
                            if (_rowMapping[j].Propery.Equals("FrAdjPrice")) // hind on erandjuhtum, kus leiame väärtuse vaid 1 kvartal tagasi
                            {
                                divisor = (double?)pi.GetValue(finDatas[i - 1]);
                            }
                            if (divisor != null && divisor != 0.0 && _rowMapping[j].PrcChange) // kontrollida, et kui ei saa % muutu välja arvutada või pole % arvutamist ette nähtud
                            {
                                row.AddData(String.Format("{0:0.0}%", (currentQ / divisor - 1) * 100));
                            }
                            else
                            {
                                row.AddData("");
                            }
                        }
                        else
                        {
                            row.AddData("");
                        }
                    }

                    k++; // suurendame kvartalite loendurit

                } // veergude if lopp
                _showTable.Add(row); // lisame rea tabelisse
            } // ridade if lopp
        }
    }
}
