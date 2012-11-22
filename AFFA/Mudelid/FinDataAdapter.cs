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

        public FinDataAdapter(FinAnalysisVM finAnalysisVm, string symbol, DataSource dataSource)
        {
            _finDataDao = new FinDataDao();
            _priceDataDao = new PriceDataDao();
            _dataSource = dataSource;
            _symbol = symbol;
            _finAnalysisVm = finAnalysisVm;

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

        public void addDcfDataDao(DcfDataDao dc){
            _dcfDataDao = dc;
        }


        public enum DataSource
        {
            XML,
            XLS
        }

        public FinDataDao FinDataDao
        {
            get { return _finDataDao; }
        }

        public PriceDataDao PriceDataDao
        {
            get { return _priceDataDao; }
        }

        public void PrepareData()
        {
            YahooFScraper yh = new YahooFScraper(this);
            _finAnalysisVm.ClearTable();
            if (_dataSource == DataSource.XML)
            {
                XmlScraper.GetData(_xmlFile, _finDataDao);
                FinDataDao.SortFinDatas();
                yh.GetPriceData(_finDataDao.FinDatas[0].BsSymbol);
                _inputVm.LaeAndmed(_finDataDao.FinDatas[0].BsSymbol);
                RatioCalculator.Calculate(_finDataDao.FinDatas);
                _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
            }
            if (_dataSource == DataSource.XLS)
            {
                yh.GetPriceData(_symbol);
                FinDataDao.SortFinDatas();
                RatioCalculator.Calculate(_finDataDao.FinDatas);
                _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
            }
        }

        public void PrepareDataXLS(string user, string psw)
        {
            YChartsScraper ys = new YChartsScraper(this, _symbol.ToUpper(), user, psw);
            ys.getData();
        }

        public void PriceDataReady()
        {
            UpdateFinDataPrice();
            RatioCalculator.Calculate(_finDataDao.FinDatas);
            _finAnalysisVm.ClearTable();
            _finAnalysisVm.PrepareTable(_finDataDao.FinDatas);
        }

        public void UpdateFinDataPrice()
        {
            for (int i = _finDataDao.FinDatas.Count - 1; i >= 0; i--)
            {
                double?[] price = _priceDataDao.GetClosePrice(_finDataDao.FinDatas[i].Kuupaev);
                _finDataDao.FinDatas[i].FrPrice = price[0];
                _finDataDao.FinDatas[i].FrAdjPrice = price[1];
                //string tekst="Date Findata: "+_finDataDao.FinDatas[i].Kuupaev+", Date PriceData , Price "+_priceDataDao.GetClosePrice(_finDataDao.FinDatas[i].Kuupaev);
                //MessageBox.Show(tekst);

            }
        }


    }
}
