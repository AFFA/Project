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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace AFFA.Vaatemudelid
{
    /// <summary>
    /// Vaatemudel Forecast tabi jaoks
    /// </summary>
    public class DcfVM : INotifyPropertyChanged
    {
        private DcfDataDao _dcfDataDao;
        private FinDataDao _finDataDao;
        private ObservableCollection<ForecastData> _showForecastTable;
        private int maxColumns = 40;
        private List<Rowmapping.RowConf> _rowMapping;
        private IList<String> _columnHeader;
        private DataGrid _dataGrid;
        private DcfInput _dcfInput;
        private FinDataAdapter _finDataAdapter;

        #region getterid, setterid tabeli jaoks
        public ObservableCollection<ForecastData> ShowForecastTable
        {
            get { return _showForecastTable; }
            set { _showForecastTable = value; }
        }

        public IList<string> ColumnHeader
        {
            get { return _columnHeader; }
        }

        public DcfDataDao DcfDataDao
        {
            get { return _dcfDataDao; }
            set { _dcfDataDao = value; }
        }
        #endregion

        #region konstruktorid

        public DcfVM(DcfInput dcfInput)
        {
            _dcfInput = dcfInput;

            _dcfInput.PropertyChanged += MyEventHandler;
        }

        public DcfVM(DataGrid dataGrid, DcfDataDao dcfDataDao, DcfInput dcfInput, FinDataDao finDataDao, FinDataAdapter finDataAdapter)
        {
            _dataGrid = dataGrid;
            _rowMapping = Rowmapping.DcfRows();
            _showForecastTable = new ObservableCollection<ForecastData>();
            _columnHeader = new List<string>();
            _dcfDataDao = dcfDataDao;
            _dcfInput = dcfInput;
            _finDataDao = finDataDao;
            _finDataAdapter = finDataAdapter;

            _finDataAdapter.DcfOutput.PropertyChanged += OutputChangedEventHandler;
        }
        #endregion

        /// <summary>
        /// Tühejnda tabel tervikuna
        /// </summary>
        public void ClearTable()
        {
            _showForecastTable.Clear();
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

        /// <summary>
        /// Andmete valmistamine, andes kõik vajalikud andmete hoidjad kaasa
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="dcfDataDao"></param>
        /// <param name="dcfInput"></param>
        /// <param name="finDataDao"></param>
        /// <param name="finDataAdapter"></param>
        public void PrepareCalculations(DataGrid dataGrid, DcfDataDao dcfDataDao, DcfInput dcfInput, FinDataDao finDataDao, FinDataAdapter finDataAdapter)
        {
            _dataGrid = dataGrid;
            _rowMapping = Rowmapping.DcfRows();
            _showForecastTable = new ObservableCollection<ForecastData>();
            _columnHeader = new List<string>();
            _dcfDataDao = dcfDataDao;
            _dcfInput = dcfInput;
            _finDataDao = finDataDao;
            _finDataAdapter = finDataAdapter;
            _finDataAdapter.DcfOutput.PropertyChanged += OutputChangedEventHandler;
        }

        /// <summary>
        /// Public meetod tabeli valmistamiseks DCFData listist
        /// </summary>
        /// <param name="dcfDatas"></param>
        public void PrepareTable(List<DcfData> dcfDatas)
        {
            GenerateTableData(dcfDatas);
            GenerateColumnHeaders();
        }

        /// <summary>
        /// Meetodis toimub sisuline tabeli sisu (read, veerud) valmistamine DCFData listist
        /// </summary>
        /// <param name="dcfDatas"></param>
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
                        else if (_rowMapping[j].Decimals.Equals(Rowmapping.RowFormat.Prc0))
                        {
                            formatNumber = "{0:P1}";
                        }
                        row.AddData(String.Format(formatNumber, currentQ)); // lisame ritta numbrilise väärtuse
                        // iga järgmine veergu on % muudu veerg ning siin arvutame välja % muudu, kui see on ette nähtud

                        //if (i >= 4) // kui meil pole piisavalt andmeid, siis ei saa % muutu arvutada ja i-4 annab errori. 
                        //{
                        //    if (j == 0) // esimese rea läbikäimise korral seame paika veergude nimed
                        //    {
                        //        _columnHeader.Add("%"); // veeru pealkiri on alati %
                        //    }
                        //    double? divisor = (double?)pi.GetValue(dcfDatas[i - 4]); // leiame sama property väärtuse 4 kvartalit tagasi, et % muutu arvutada
                        //    if (_rowMapping[j].Propery.Equals("FrAdjPrice")) // hind on erandjuhtum, kus leiame väärtuse vaid 1 kvartal tagasi
                        //    {
                        //        divisor = (double?)pi.GetValue(dcfDatas[i - 1]);
                        //    }
                        //    if (divisor != null && divisor != 0.0 && _rowMapping[j].PrcChange) // kontrollida, et kui ei saa % muutu välja arvutada või pole % arvutamist ette nähtud
                        //    {
                        //        row.AddData(String.Format("{0:0.0}%", (currentQ / divisor - 1) * 100));
                        //    }
                        //    else
                        //    {
                        //        row.AddData("");
                        //    }
                        //}
                        //else
                        //{
                        //    row.AddData("");
                        //}
                    }

                    k++; // suurendame kvartalite loendurit

                } // veergude if lopp
                _showForecastTable.Add(row); // lisame rea tabelisse
            } // ridade if lopp
        }



        //public DcfVM(FinDataDao finDataDao)
        //{
        //    _finDataDao = finDataDao; // anname konstruktoriga findata andmed
        //    //_dcfDataDao=new DcfDataDao(); 

        //}


        /// <summary>
        /// Meetod Forecast genereerimiseks, arvutamiseks ning lõpuks ettevõtte väärtuse leidmiseks
        /// </summary>
        public void GetDcf()
        {
            // anname kalkulaatorile findata listi ja dcfdatadao, et calculaator paneks sinna genereeritavad dcf andmed
            DcfCalculator.GenerateDcfData(_finDataDao.FinDatas, _dcfDataDao);
            DcfCalculator.CalculateQuaterlyForecasts(_dcfDataDao.DcfDatas, _dcfInput);
            DcfCalculator.CalculateTerminal(_finDataAdapter);

        }

        #region Getters and Setters for DcfOutput Data
        public double? TerminalFreeCashFlowDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.TerminalFreeCashFlow; }
        }

        public double? TerminalValueDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.TerminalValue; }
        }

        public double? PerpetuityGrowthRateDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.PerpetuityGrowthRate; }
        }

        public double? WaccDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.Wacc; }
        }

        public double? PresentValueOfFreeCashFlowDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.PresentValueOfFreeCashFlow; }
        }

        public double? PresentValueOfTerminalValueDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.PresentValueOfTerminalValue; }
        }

        public double? EnterpriseValueWithoutCashDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.EnterpriseValueWithoutCash; }
        }

        public double? CashAndCashEquivalentsDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.CashAndCashEquivalents; }
        }

        public double? EnterpriseValueDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.EnterpriseValue; }
        }

        public double? LessTotalDebtDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.LessTotalDebt; }
        }

        public double? EquityValueDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.EquityValue; }
        }

        public double? OutstandingSharesDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.OutstandingShares; }
        }

        public double? CurrentSharePriceDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.CurrentSharePrice; }
        }

        public double? ModelSharePriceDcfOutput
        {
            get { return _finDataAdapter.DcfOutput.ModelSharePrice; }
        }

        public string RecommendationDcfOutput
        {
            get
            {
                if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.Buy)
                {
                    return "Buy";
                }
                else if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.Hold)
                {
                    return "Hold";
                }
                else if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.No_Data_To_Recommend)
                {
                    return "Not enough data";
                }
                else if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.Sell)
                {
                    return "Sell";
                }
                else if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.Strong_Buy)
                {
                    return "Strong Buy";
                }
                else if (_finDataAdapter.DcfOutput.Recommendation == DcfOutput.Recommendations.Strong_Sell)
                {
                    return "Strong Sell";
                }
                else
                {
                    return "";
                }
            }
        }
        #endregion

        #region Getters and Setters for DcfInput Data
        public double GrowthRatePrognosisValue
        {
            get { return _dcfInput.GrowthRatePrognosis * 100; }
            set
            {
                if (value / 100 != _dcfInput.GrowthRatePrognosis)
                {
                    _dcfInput.GrowthRatePrognosis = value / 100;
                }
            }
        }

        public double TaxRateValue
        {
            get { return _dcfInput.TaxRate * 100; }
            set
            {
                if (value / 100 != _dcfInput.TaxRate)
                {
                    //MessageBox.Show("Vana väärtus: " + _dcfInput.TaxRate + " Uus väärtus: " + value);
                    _dcfInput.TaxRate = value / 100;
                }
            }
        }

        public double CostOfDebtValue
        {
            get { return _dcfInput.CostOfDebt * 100; }
            set
            {
                if (value / 100 != _dcfInput.CostOfDebt)
                {
                    _dcfInput.CostOfDebt = value / 100;
                }
            }
        }

        public double RiskFreeRateValue
        {
            get { return _dcfInput.RiskFreeRate * 100; }
            set
            {
                if (value / 100 != _dcfInput.RiskFreeRate)
                {
                    _dcfInput.RiskFreeRate = value / 100;
                }
            }
        }

        public double MarketRiskPremiumValue
        {
            get { return _dcfInput.MarketRiskPremium * 100; }
            set
            {
                if (value / 100 != _dcfInput.MarketRiskPremium)
                {
                    _dcfInput.MarketRiskPremium = value / 100;
                }
            }
        }

        public double ContinuousGrowthValue
        {
            get { return _dcfInput.ContinuousGrowth * 100; }
            set
            {
                if (value / 100 != _dcfInput.ContinuousGrowth)
                {
                    _dcfInput.ContinuousGrowth = value / 100;
                }
            }
        }

        public double TotalAssetsPrcRevenueValue
        {
            get { return _dcfInput.TotalAssetsPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.TotalAssetsPrcRevenue)
                {
                    _dcfInput.TotalAssetsPrcRevenue = value / 100;
                }
            }
        }

        public double TotalLiabilitiesPrcRevenueValue
        {
            get { return _dcfInput.TotalLiabilitiesPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.TotalLiabilitiesPrcRevenue)
                {
                    _dcfInput.TotalLiabilitiesPrcRevenue = value / 100;
                }
            }
        }

        public double TotalCurrentAssetsPrcRevenueValue
        {
            get { return _dcfInput.TotalCurrentAssetsPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.TotalCurrentAssetsPrcRevenue)
                {
                    _dcfInput.TotalCurrentAssetsPrcRevenue = value / 100;
                }
            }
        }

        public double TotalCurrentLiabilitiesPrcRevenueValue
        {
            get { return _dcfInput.TotalCurrentLiabilitiesPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.TotalCurrentLiabilitiesPrcRevenue)
                {
                    _dcfInput.TotalCurrentLiabilitiesPrcRevenue = value / 100;
                }
            }
        }

        public double AllCostsPrcRevenueValue
        {
            get { return _dcfInput.AllCostsPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.AllCostsPrcRevenue)
                {
                    _dcfInput.AllCostsPrcRevenue = value / 100;
                }
            }
        }

        public double EbitdaPrcRevenueValue
        {
            get { return _dcfInput.EbitdaPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.EbitdaPrcRevenue)
                {
                    _dcfInput.EbitdaPrcRevenue = value / 100;
                }
            }
        }

        public double DepreciationPrcRevenueValue
        {
            get { return _dcfInput.DepreciationPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.DepreciationPrcRevenue)
                {
                    _dcfInput.DepreciationPrcRevenue = value / 100;
                }
            }
        }

        public double EbitPrcRevenueValue
        {
            get { return _dcfInput.EbitPrcRevenue * 100; }
            set
            {
                if (value / 100 != _dcfInput.EbitPrcRevenue)
                {
                    _dcfInput.EbitPrcRevenue = value / 100;
                }
            }
        }

        public double WaccValue
        {
            get { return _dcfInput.Wacc * 100; }
            set
            {
                if (value / 100 != _dcfInput.Wacc)
                {
                    _dcfInput.Wacc = value / 100;
                }
            }
        }

        public double BetaValue
        {
            get { return _dcfInput.Beta; }
            set
            {
                if (value != _dcfInput.Beta)
                {
                    _dcfInput.Beta = value;
                }
            }
        }

        public double CostOfEquityValue
        {
            get { return _dcfInput.CostOfEquity * 100; }
            set
            {
                if (value / 100 != _dcfInput.CostOfEquity)
                {
                    _dcfInput.CostOfEquity = value / 100;
                }
            }
        }

        public double SharesOutstandingValue
        {
            get { return _dcfInput.SharesOutstanding; }
            set
            {
                if (value != _dcfInput.SharesOutstanding)
                {
                    _dcfInput.SharesOutstanding = value;
                }
            }
        }
        #endregion

        #region Event Handlers
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        private void MyEventHandler(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged(args.PropertyName + "Value");
        }

        private void OutputChangedEventHandler(object sender, PropertyChangedEventArgs args)
        {
            RaisePropertyChanged(args.PropertyName + "DcfOutput");
        }
        #endregion
    }
}
