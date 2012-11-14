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
using System.ComponentModel;
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
        private ObservableCollection<CompanyData> _companyDatas;
        
        public InputVM()
        {
            _companyDatas = new ObservableCollection<CompanyData>();
            _priceDataDao=new PriceDataDao(); 
        }

        public ObservableCollection<CompanyData> CompanyDatas
        {
            get { return _companyDatas; }
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
            LoadCompanyData();
        }

        public void LoadCompanyData()
        {
            _companyDatas.Clear();
            _companyDatas.Add(new CompanyData("Industry:", this._industry));
            _companyDatas.Add(new CompanyData("Sector:", this._sector));
            _companyDatas.Add(new CompanyData("Employees:", this._employees));
        }
    }
}
