using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.Scraperid;

namespace AFFA.Mudelid
{
    public class FinDataAdapter
    {
        private FinDataDao _finDataDao;
        private DataSource _dataSource;
        private string _symbol;
        private string _xmlFile;

        public FinDataAdapter(string symbol, DataSource dataSource)
        {
            _finDataDao = new FinDataDao();
            _dataSource = dataSource;
            _symbol = symbol;
        }

        public FinDataAdapter(string symbol, DataSource dataSource, string fileName):this(symbol,dataSource)
        {
            _xmlFile = fileName;
        }


        public enum DataSource
        {
            XML
        };

        public FinDataDao FinDataDao
        {
            get { return _finDataDao; }
        }

        public void PrepareData()
        {
            if (_dataSource==DataSource.XML)
            {
                XmlScraper.GetData(_symbol, _xmlFile, FinDataDao);            
            }
        }


    }
}
