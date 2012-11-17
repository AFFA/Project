﻿using System;
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
        //private List<KeyValuePair<string, string>> _rowMapping;
        private List<Rowmapping.RowConf> _rowMapping;
        private IList<String> _columnHeader;
        private DataGrid _dataGrid;

        public ObservableCollection<FinAnalysisData> ShowTable
        {
            get { return _showTable; }
        }

        public IList<string> ColumnHeader
        {
            get { return _columnHeader; }
        }

 
        private void GenerateColumnHeaders()
        {
            int columnIndex = 0;

            for (int i = 0; i < ShowTable[0].GetSize(); i++)
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



        public FinAnalysisVM(DataGrid dataGrid)
        {
            _dataGrid = dataGrid;
           _rowMapping=Rowmapping.EnglishRows();
            _showTable = new ObservableCollection<FinAnalysisData>();
            _columnHeader = new List<string>();
        }

        public void PrepareTable(List<FinData> finDatas)
        {
            GenerateTableData(finDatas);
            GenerateColumnHeaders();
        }

        private void GenerateTableData(List<FinData> finDatas)
        {
            for (int j = 0; j < _rowMapping.Count; j++) // tekitame ridu nii palju, kui on rowMapping'us antud
            {
                FinAnalysisData row = new FinAnalysisData();
                row.AddData(_rowMapping[j].Label);
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
                        PropertyInfo pi = finDatas[i].GetType().GetProperty(_rowMapping[j].Propery);
                        double? currentQ = (double?)pi.GetValue(finDatas[i]);
                        String formatNumber = "{0:0}";
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
                        row.AddData(String.Format(formatNumber, currentQ));
                        if (i >= 4) // kui meil pole piisavalt andmeid, siis ei saa % muutu arvutada ja i-4 annab errori. 
                        {
                            if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                            {
                                _columnHeader.Add("%");
                            }
                            pi = finDatas[i - 4].GetType().GetProperty(_rowMapping[j].Propery);
                            double? divisor = (double?)pi.GetValue(finDatas[i - 4]);
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

                    k++;

                } // veergude if lopp
                _showTable.Add(row);
            } // ridade if lopp
        }
    }
}
