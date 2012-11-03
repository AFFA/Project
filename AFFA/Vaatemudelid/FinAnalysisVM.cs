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

namespace AFFA.Vaatemudelid
{
    public class FinAnalysisVM
    {
         private ObservableCollection<FinAnalysisData> _showTable;
        private int maxColumns = 10;
        private List<KeyValuePair<string, string>> _rowMapping;
        private IList<String> _columnHeader;

        public ObservableCollection<FinAnalysisData> ShowTable
        {
            get { return _showTable; }
        }

        public IList<string> ColumnHeader
        {
            get { return _columnHeader; }
        }


        private void InitVars()
        {
            _rowMapping = new List<KeyValuePair<string, string>>();
            _rowMapping.Add(new KeyValuePair<string, string>("Revenue", "IsRevenue"));
            _rowMapping.Add(new KeyValuePair<string, string>("Cost of revenue", "IsCostOfRevenue"));
            _rowMapping.Add(new KeyValuePair<string, string>("Gross profit", "IsGrossProfit"));
            // kui siia lisada veel propertyte mappinguid, kus esimesel kohal on rea nimi ja teisel kohal vastav FinData property, 
            // siis tekib automaatselt tabelisse ridu juurde, nt küll isEpsDiluted puhul veel mingi viga kuskil sees
            _rowMapping.Add(new KeyValuePair<string, string>("Net Income", "IsNetIncome"));
            _rowMapping.Add(new KeyValuePair<string, string>("EPS", "IsEpsDiluted"));
        }

        private void GenerateColumnHeaders(DataGrid dataGrid)
        {
            int columnIndex = 0;

            for (int i = 0; i < ShowTable[0].GetSize(); i++)
            {
                dataGrid.Columns.Add(
                    new DataGridTextColumn
                    {
                        Header = ColumnHeader[columnIndex],
                        //Header = "veerg" + columnIndex,
                        Binding = new Binding(
                            string.Format("Values[{0}]", columnIndex++))
                    });
            }
        }



        public FinAnalysisVM(Collection<FinData> finDatas, DataGrid dataGrid)
        {
            InitVars();
            _showTable = new ObservableCollection<FinAnalysisData>();
            _columnHeader = new List<string>();
            GenerateTableData(finDatas);
            GenerateColumnHeaders(dataGrid);
        }

        private void GenerateTableData(Collection<FinData> finDatas)
        {
            for (int j = 0; j < _rowMapping.Count; j++) // tekitame ridu nii palju, kui on rowMapping'us antud
            {
                FinAnalysisData row = new FinAnalysisData();
                row.AddData(_rowMapping[j].Key);
                if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                {
                    _columnHeader.Add("");
                }
                int k = 0;
                for (int i = finDatas.Count - 1; i >= 0; i--) // tekitame veerge
                {
                    if (k < maxColumns) // piirame ära, mitu kvartalit tabelisse kirjutatakse  
                    {
                        if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                        {
                            _columnHeader.Add(finDatas[i].Kuupaev.ToShortDateString());
                        }
                        PropertyInfo pi = finDatas[i].GetType().GetProperty(_rowMapping[j].Value);
                        double? currentQ = (double?)pi.GetValue(finDatas[i]);
                        String formatNumber = "{0:0}";
                        if (_rowMapping[j].Value.Equals("IsEpsDiluted")) // valida read, mille puhul numbriformaat on erinev (kaks nulli peale koma)
                        {
                            formatNumber = "{0:0.00}";
                        }
                        row.AddData(String.Format(formatNumber, currentQ));
                        if (i >= 4) // kui meil pole piisavalt andmeid, siis ei saa % muutu arvutada ja i-4 annab errori. 
                        {
                            if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                            {
                                _columnHeader.Add("%");
                            }
                            pi = finDatas[i - 4].GetType().GetProperty(_rowMapping[j].Value);
                            double? divisor = (double?)pi.GetValue(finDatas[i - 4]);
                            if (divisor != null && divisor != 0.0) // kui ei saa %muutu välja arvutada
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

                    k++;

                } // veergude if lopp
                _showTable.Add(row);
            } // ridade if lopp
        }
    }
}
