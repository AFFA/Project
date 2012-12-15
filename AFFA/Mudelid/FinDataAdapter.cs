using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using AFFA.Scraperid;
using AFFA.Vaatemudelid;
using AFFA.DCFMudelid;

namespace AFFA.Mudelid
{
    /// <summary>
    /// Adapter, mis annab ligipääsu kõigile finantsandmetele ning hoolitseb erinevatest allikatest pärit andmete standardsele kujule viimise eest.
    /// </summary>
    public class FinDataAdapter
    {
        private FinDataDao _finDataDao;
        private DataSource _dataSource;
        private string _symbol;
        private string _xmlFile;
        private XDocument _xmlDocument;
        private PriceDataDao _priceDataDao;
        private FinAnalysisVM _finAnalysisVm;
        private InputVM _inputVm;
        private DcfDataDao _dcfDataDao;
        private DcfInput _dcfInput;
        private DcfOutput _dcfOutput;
        private bool _passwordSet = false;
        private MainWindow _mainWindow;

        #region konstruktorid
        public FinDataAdapter(FinAnalysisVM finAnalysisVm, string symbol, DataSource dataSource)
        {
            _finDataDao = new FinDataDao();
            _priceDataDao = new PriceDataDao();
            _dataSource = dataSource;
            _symbol = symbol;
            _finAnalysisVm = finAnalysisVm;
            _dcfDataDao = new DcfDataDao();

        }

        public FinDataAdapter(InputVM inputVm, FinAnalysisVM finAnalysisVm, string symbol, DataSource dataSource, string fileName)
            : this(finAnalysisVm, symbol, dataSource)
        {
            _xmlFile = fileName;
            _inputVm = inputVm;
        }

        public FinDataAdapter(FinAnalysisVM finAnalysisVm, string symbol, DataSource dataSource, XDocument file)
            : this(finAnalysisVm, symbol, dataSource)
        {
            _xmlDocument = file;
        }
        #endregion

        #region konstruktoritele lisaks
        public void AddDcfDataDao(DcfDataDao dc)
        {
            _dcfDataDao = dc;
        }

        public void AddDcfInput(DcfInput di)
        {
            _dcfInput = di;
        }

        public void AddDcfOutput(DcfOutput dco)
        {
            _dcfOutput = dco;
        }

        public void AddMainWindow(MainWindow mw)
        {
            _mainWindow = mw;
        }

        #endregion


        public enum DataSource
        {
            XML,
            XLS
        }


        #region getterid, setterid
        public DcfDataDao DcfDataDao
        {
            get { return _dcfDataDao; }
            set { _dcfDataDao = value; }
        }

        public FinDataDao FinDataDao
        {
            get { return _finDataDao; }
        }

        public PriceDataDao PriceDataDao
        {
            get { return _priceDataDao; }
        }

        public DcfInput DcfInput
        {
            get { return _dcfInput; }
            set { _dcfInput = value; }
        }

        public DcfOutput DcfOutput
        {
            get { return _dcfOutput; }
            set { _dcfOutput = value; }
        }

        public bool PasswordSet
        {
            get { return _passwordSet; }
            set { _passwordSet = value; }
        }

        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set { _mainWindow = value; }
        }
        #endregion

        /// <summary>
        /// Kui XML andmed saavad failist loetud, valmistatakse finantsandmete tabel ja saadetakse päringud turuhindade osas.
        /// </summary>
        public void XmlDataReady()
        {
            FinDataDao.SortFinDatas();
            if (FinDataDao.FinDatas.Count > 0)
            {
                YahooFScraper yh = new YahooFScraper(this);
                yh.GetPriceData(_finDataDao.FinDatas[0].BsSymbol);
                yh.GetIndexData("SPY");
                _inputVm.LaeAndmed(_finDataDao.FinDatas[0].BsSymbol);
                RatioCalculator.Calculate(_finDataDao.FinDatas);
                _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
                if (_mainWindow != null)
                {
                    _mainWindow.XmlReady(_xmlFile);
                }
            }
        }

        /// <summary>
        /// Peamine (universaalne) meetod, mida kutsuda andmete valmistamiseks olenemata allikast, kust andmed pärit on
        /// </summary>
        public void PrepareData()
        {
            YahooFScraper yh = new YahooFScraper(this);
            _finAnalysisVm.ClearTable();
            if (_dataSource == DataSource.XML)
            {
                XmlScraper xmlScraper = new XmlScraper();
                xmlScraper.GetData(_xmlFile, _finDataDao, this);
                //FinDataDao.SortFinDatas();
                //if (FinDataDao.FinDatas.Count > 0)
                //{
                //    yh.GetPriceData(_finDataDao.FinDatas[0].BsSymbol);
                //    yh.GetIndexData("SPY");
                //    _inputVm.LaeAndmed(_finDataDao.FinDatas[0].BsSymbol);
                //    RatioCalculator.Calculate(_finDataDao.FinDatas);
                //    _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
                //}
            }
            if (_dataSource == DataSource.XLS)
            {
                yh.GetPriceData(_symbol);
                yh.GetIndexData("SPY");
                FinDataDao.SortFinDatas();
                RatioCalculator.Calculate(_finDataDao.FinDatas);
                _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
                if (_mainWindow != null)
                {
                    _mainWindow.YchartsReady();
                }

            }
        }

        /// <summary>
        /// YCharts.com pärit andmete valmistamistamiseks vajalik meetod
        /// </summary>
        /// <param name="user">YCharts kasutajanimi</param>
        /// <param name="psw">YCharts parool</param>
        /// <param name="mv">Viide põhiaknale</param>
        public void PrepareDataXLS(string user, string psw, MainWindow mv)
        {
            _mainWindow = mv;
            YChartsScraper ys = new YChartsScraper(this, _symbol.ToUpper());
            ys.getData(user, psw);
        }

        /// <summary>
        /// Kui analüüsitava aktsia hinna andmed on saabunud, tuleb teha uued finantsnäitajate arvutused ja uuendada tabelit
        /// </summary>
        public void PriceDataReady()
        {

            UpdateFinDataPrice();
            RatioCalculator.Calculate(_finDataDao.FinDatas);
            _finAnalysisVm.ClearTable();
            _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
            //MessageBox.Show("arvutan inputi");
            DcfInputCalculator.CalculateInput(this, _dcfInput);
        }

        /// <summary>
        /// Kui võrdlusindeksi andmed on saabunud, saab arvutada eelduste jaoks vajaliku beta
        /// </summary>
        public void IndexDataReady()
        {
            // arvutada beta
            //MessageBox.Show("arvutan beta");
            DcfInputCalculator.CalculateBeta(this, _dcfInput);
        }

        /// <summary>
        /// Uuendatakse finantsandmeid hinnainfoga
        /// </summary>
        public void UpdateFinDataPrice()
        {
            for (int i = _finDataDao.FinDatas.Count - 1; i >= 0; i--)
            {
                double?[] price = _priceDataDao.GetClosePrice(_finDataDao.FinDatas[i].Kuupaev, _priceDataDao.PriceDatas);
                _finDataDao.FinDatas[i].FrPrice = price[0];
                _finDataDao.FinDatas[i].FrAdjPrice = price[1];
                //string tekst="Date Findata: "+_finDataDao.FinDatas[i].Kuupaev+", Date PriceData , Price "+_priceDataDao.GetClosePrice(_finDataDao.FinDatas[i].Kuupaev);
                //MessageBox.Show(tekst);

            }
        }


    }
}
