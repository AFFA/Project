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
    /// <summary>
    /// Yahoo Finance lehelt hinnainfo ja ettevõtte üldinfo tõmbamise funktsionaalsus.
    /// </summary>
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

        /// <summary>
        /// Yahoo Finance hindade tõmbamise URL-i koostamine
        /// </summary>
        /// <param name="symbol">Aktsiasümbol</param>
        /// <returns>URL</returns>
        private string YahooUrl(string symbol)
        {
            return "http://ichart.finance.yahoo.com/table.csv?s=" + symbol + "&d=" + (DateTime.Today.Month - 1) + "&e=" + DateTime.Today.Day + "&f=" + DateTime.Today.ToString("yyyy") + "&g=d&a=2&b=26&c=1990&ignore=.csv";
        }

        /// <summary>
        /// Async hindade tõmbamine
        /// </summary>
        /// <param name="symbol">Aktsiasümbol</param>
        public void GetPriceData(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                string url = YahooUrl(symbol);
                //MessageBox.Show(url);
                WebClient klient = new WebClient();
                klient.Encoding = Encoding.UTF8;
                klient.DownloadStringCompleted += klient_DownloadStringCompleted;
                klient.DownloadStringAsync(new Uri(url));
            }
            else
            {
                MessageBox.Show("Yahoo Finance price data failed, symbol not specified");
            }
        }

        /// <summary>
        /// Async indeksi hindade tõmbamine
        /// </summary>
        /// <param name="symbol">Sümbol</param>
        public void GetIndexData(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                string url = YahooUrl(symbol);
                //MessageBox.Show(url);
                WebClient klient = new WebClient();
                klient.Encoding = Encoding.UTF8;
                klient.DownloadStringCompleted += klient_IndexDownloadStringCompleted;
                klient.DownloadStringAsync(new Uri(url));
            }
            else
            {
                MessageBox.Show("Yahoo Finance index data failed, symbol not specified");
            }
        }

        /// <summary>
        /// Aktsia hinnad on kätte saadud, toimub nende salvestamine vastavatesse objektidesse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void klient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            PriceData[] res = readFile(e.Result);
            _finDataAdapter.PriceDataDao.AddData(res);
            _finDataAdapter.PriceDataReady();
            DownloadCompleted(1, sender);

        }

        /// <summary>
        /// Indeksi hinnad on kätte saadud, toimub nende salvestamine vastavatesse objektidesse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void klient_IndexDownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            PriceData[] res = readFile(e.Result);
            _finDataAdapter.PriceDataDao.AddIndexData(res);
            DownloadCompleted(2, sender);
        }

        /// <summary>
        /// CSV andmete sisselugemine FileHelperEngine dll abiga.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private PriceData[] readFile(string file)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(PriceData));
            PriceData[] res = engine.ReadString(file) as PriceData[];
            return res;
        }

        /// <summary>
        /// Ettevõtte profiili andmete tõmbamine, async
        /// </summary>
        /// <param name="symbol">Aktsiasümbol</param>
        public void GetProfileData(string symbol)
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                string url = "http://finance.yahoo.com/q/pr?s=" + symbol.ToUpper() + "+Profile";
                //MessageBox.Show(url);
                WebClient klient = new WebClient();
                klient.Encoding = Encoding.UTF8;
                klient.DownloadStringCompleted += profile_DownloadStringCompleted;
                klient.DownloadStringAsync(new Uri(url));
            }
            else
            {
                MessageBox.Show("Yahoo Finance profile data failed, symbol not specified");
            }
        }

        /// <summary>
        /// Profiiliandmete tõmbamine lõpetatud, saadetakse andmed puhastusse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void profile_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            ParseProfile(e.Result);
        }

        /// <summary>
        /// Tõmmatud profiili lehelt saadud andmetest vajalike väljade leidmine ja salvestamine
        /// </summary>
        /// <param name="file">Fail HTML formaadis</param>
        void ParseProfile(string file)
        {
            Regex rSector = new Regex(">Sector:<.*?<a.*?>(.*?)<");
            Match match = rSector.Match(file);
            if (match.Success)
            {
                _inputVm.Sector = match.Groups[1].Value.Replace("&amp;", "&");
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

        /// <summary>
        /// Thread safe meetod tagamaks, et FinDataAdapterit teavitatakse andmete kättesaamisest alles siis,
        /// kui nii aktsia kui indeksi hinna andmed on kohale jõudnud, sest FinDataAdapter saab siis beta (vms) välja arvutada,
        /// mida enne ei saa arvutada.
        /// </summary>
        /// <param name="id">Hinna või ideksi identifikaator</param>
        /// <param name="lockThis">Lukk</param>
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
