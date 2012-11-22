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
using AFFA.DCFMudelid;


namespace AFFA.Vaatemudelid
{
    public class DcfVM
    {
        private DcfDataDao _dcfDataDao;
        private FinDataDao _finDataDao;
        private ObservableCollection<ForecastData> _showForecastTable;
        private int maxColumns = 2;
        private List<Rowmapping.RowConf> _rowMapping;
        private IList<String> _columnHeader;
        private DataGrid _dataGrid;

        public ObservableCollection<ForecastData> ShowForecastTable
        {
            get { return _showForecastTable; }
            set { _showForecastTable = value; }
        }
        // TODO mingi observable collection, mis binditakse XAMLi

        public IList<string> ColumnHeader
        {
            get { return _columnHeader; }
        }

        public void ClearTable()
        {
            _showForecastTable.Clear();
            _dataGrid.Columns.Clear();

            //_showTable = new ObservableCollection<FinAnalysisData>(); 
            //_dataGrid.DataContext=null;
        }

        private void GenerateColumnHeaders()
        {
            int columnIndex = 0;

            for (int i = 0; i < _showForecastTable[0].GetSize(); i++)
            {
                _dataGrid.Columns.Add(
                    new DataGridTextColumn
                    {
                        Header = ColumnHeader[columnIndex],
                        //Header = "veerg" + columnIndex,
                        Binding = new Binding(
                            string.Format("Values[{0}]", columnIndex++))
                    });
            }
        }

        public DcfVM(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
            _rowMapping = Rowmapping.DcfRows();
           _showForecastTable = new ObservableCollection<ForecastData>();
            _columnHeader = new List<string>();
        }

        public void PrepareTable(List<DcfData> dcfDatas)
        {
            GenerateTableData(dcfDatas);
            GenerateColumnHeaders();
        }


        private void GenerateTableData(List<DcfData> dcfDatas)
        {
            for (int j = 0; j < _rowMapping.Count; j++) // tekitame ridu nii palju, kui on rowMapping'us antud
            {
                ForecastData row = new ForecastData(); // iga rida on seda tüüpi klass ehk sisuliselt stringide list
                row.AddData(_rowMapping[j].Label); // igasse ritta paneme esimesele kohale (veergu) rea nime
                if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                {
                    _columnHeader.Add(""); // esimese veeru pealkiri
                }
                int k = 0; // seame muutuja, mis loeb, mitu kvartalit on sisestatud
                for (int i = dcfDatas.Count - 1; i >= 0; i--) // tekitame veerge
                {
                    if (k < maxColumns) // piirame ära, mitu kvartalit tabelisse kirjutatakse  
                    {
                        if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                        {
                            _columnHeader.Add(dcfDatas[i].Kuupaev.ToShortDateString());
                        }
                        PropertyInfo pi = dcfDatas[i].GetType().GetProperty(_rowMapping[j].Propery); // sellega saame kätte finData property, mille nimi on defineeritud _rowMapping klassis
                        double? currentQ = (double?)pi.GetValue(dcfDatas[i]); // kuigi property on meil käes, tuleb selle property väärtuse kättesaamiseks alati anda ka objekt ise parameetrina
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
                            double? divisor = (double?)pi.GetValue(dcfDatas[i - 4]); // leiame sama property väärtuse 4 kvartalit tagasi, et % muutu arvutada
                            if (_rowMapping[j].Propery.Equals("FrAdjPrice")) // hind on erandjuhtum, kus leiame väärtuse vaid 1 kvartal tagasi
                            {
                                divisor = (double?)pi.GetValue(dcfDatas[i - 1]);
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
                _showForecastTable.Add(row); // lisame rea tabelisse
            } // ridade if lopp
        }













        public DcfDataDao DcfDataDao
        {
            get { return _dcfDataDao; }
            set { _dcfDataDao = value; }
        }




        public DcfVM(FinDataDao finDataDao)
        {
            _finDataDao = finDataDao; // anname konstruktoriga findata andmed
            _dcfDataDao=new DcfDataDao(); 

        }

        public void GetDcf()
        {
            // anname kalkulaatorile findata listi ja dcfdatadao, et calculaator paneks sinna genereeritavad dcf andmed
            DcfCalculator.GenerateDcfData(_finDataDao.FinDatas, _dcfDataDao);
            DcfCalculator.Calculate(_dcfDataDao.DcfDatas, new DcfInput());
            
        }

        private void GenerateTableData(List<FinData> finDatas)
        {
            // TODO tekitada FinAnalysisVM näitel sobival kujul observable collection
            // selles osas on lihtsam, et erinevalt FinAnalysisVM-st ei pea siin veergude muutude % eraldi arvutama
        }
    }
}
