using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHelpers;
using AFFA.Mudelid;
using AFFA.Vaatemudelid;

namespace AFFA.Scraperid
{
    public class YahooFScraper
    {
        private FinDataAdapter _finDataAdapter;
        private InputVM _inputVm;
        private bool _priceReady = false;
        private bool _indexReady = false;

        public YahooFScraper(InputVM inputVm)
        {
            _inputVm = inputVm;
        }

        public YahooFScraper(FinDataAdapter finDataAdapter)
        {
            _finDataAdapter = finDataAdapter;
        }

        public YahooFScraper()
        {
        }

        private string YahooUrl(string symbol)
        {
            return "http://ichart.finance.yahoo.com/table.csv?s=" + symbol + "&d=" + (DateTime.Today.Month - 1) + "&e=" + DateTime.Today.Day + "&f=" + DateTime.Today.ToString("yyyy") + "&g=d&a=2&b=26&c=1990&ignore=.csv";
        }

        public void GetPriceData(string symbol)
        {
            string url = YahooUrl(symbol);
            //MessageBox.Show(url);
            WebClient klient = new WebClient();
            klient.Encoding = Encoding.UTF8;
            klient.DownloadStringCompleted += klient_DownloadStringCompleted;
            klient.DownloadStringAsync(new Uri(url));
        }

        public void GetIndexData(string symbol)
        {
            string url = YahooUrl(symbol);
            //MessageBox.Show(url);
            WebClient klient = new WebClient();
            klient.Encoding = Encoding.UTF8;
            klient.DownloadStringCompleted += klient_IndexDownloadStringCompleted;
            klient.DownloadStringAsync(new Uri(url));
        }

        void klient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            PriceData[] res = readFile(e.Result);
            _finDataAdapter.PriceDataDao.AddData(res);
            _finDataAdapter.PriceDataReady();
            DownloadCompleted(1, sender);

        }
        void klient_IndexDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            PriceData[] res=readFile(e.Result);
            _finDataAdapter.PriceDataDao.AddIndexData(res);
            DownloadCompleted(2, sender);
        }

        private PriceData[] readFile(string file)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(PriceData));
            PriceData[] res = engine.ReadString(file) as PriceData[];
            return res;
        }

        public void GetProfileData(string symbol)
        {
            string url = "http://finance.yahoo.com/q/pr?s="+symbol.ToUpper()+"+Profile";
            //MessageBox.Show(url);
            WebClient klient = new WebClient();
            klient.Encoding = Encoding.UTF8;
            klient.DownloadStringCompleted += profile_DownloadStringCompleted;
            klient.DownloadStringAsync(new Uri(url));
        }
        void profile_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            ParseProfile(e.Result);
        }

        void ParseProfile(string file)
        {
            Regex rSector = new Regex(">Sector:<.*?<a.*?>(.*?)<");
            Match match = rSector.Match(file);
            if (match.Success)
            {
                _inputVm.Sector = match.Groups[1].Value.Replace("&amp;","&");
                //MessageBox.Show(_inputVm.Sector);
            }
            Regex rIndustry = new Regex(">Industry:<.*?<a.*?>(.*?)<");
            match = rIndustry.Match(file);
            if (match.Success)
            {
                _inputVm.Industry = match.Groups[1].Value.Replace("&amp;", "&");
                //MessageBox.Show(_inputVm.Industry);
            }
            Regex rEmployees = new Regex(">Full Time Employees:<.*?<td.*?>(.*?)<");
            match = rEmployees.Match(file);
            if (match.Success)
            {
                _inputVm.Employees = match.Groups[1].Value;
                //MessageBox.Show(_inputVm.Employees);
            }
            Regex rName = new Regex("<div class=\"title\"><h2>(.*?)<");
            match = rName.Match(file);
            if (match.Success)
            {
                _inputVm.Name = match.Groups[1].Value;
                //MessageBox.Show(_inputVm.Employees);
            }

       
            _inputVm.LoadCompanyData();
        }

        private void DownloadCompleted(int id, object lockThis)
        {
            lock (lockThis)
            {

                switch (id)
                {
                    case 1:
                        _priceReady = true;
                        break;
                    case 2:
                        _indexReady = true;
                        break;

                }
                if (_priceReady && _indexReady)
                {
                    //MessageBox.Show("prepare data");
                    _finDataAdapter.IndexDataReady();
                }
            }
        }

    }
}
