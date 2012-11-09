using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AFFA.Mudelid;
using AFFA.Scraperid;

namespace AFFA.Vaatemudelid
{
    public class InputVM
    {

        private PriceDataDao _priceDataDao;
        private string _industry;
        private string _sector;
        private string _employees;
        
        public InputVM()
        {
            _priceDataDao=new PriceDataDao();
                    
        }

        public PriceDataDao PriceDataDao
        {
            get { return _priceDataDao; }
        }

        public string Industry
        {
            get { return _industry; }
            set { _industry = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }


        public void LaeAndmed(string symbol)
        {
            YahooFScraper yh = new YahooFScraper(this);
            yh.GetPriceData(symbol);
            yh.GetProfileData(symbol);



        }



    }
}
