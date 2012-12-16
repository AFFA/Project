using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AFFA.Mudelid;
using AFFA.Vaatemudelid;
using NPOI.HSSF.UserModel;
using NPOI.Util;

namespace AFFA.Scraperid
{
    /// <summary>
    /// YCharts.com andmete tõmbamine
    /// </summary>
    public class YChartsScraper
    {
        private string _user;
        private string _password;
        private string _symbol;
        private FinDataAdapter _finDataAdapter;
        private bool _file1 = false;
        private bool _file2 = false;
        private bool _file3 = false;
        private WebClientEx _webClientEx;
        private XDocument _isData;
        private XDocument _bsData;
        private XDocument _cfsData;
        private XmlScraper _xmlScraper;



        public YChartsScraper(FinDataAdapter finDataAdapter, string symbol)
        {
            _finDataAdapter = finDataAdapter;
            _symbol = symbol;
            _xmlScraper = new XmlScraper(finDataAdapter);
        }

        /// <summary>
        /// Avalik meetod andmete kättesaamiseks, tõmmatava aktsia sümbol pannakse paika konstruktoriga. 
        /// Siin meetodis toimub sisselogimise ettevalmistamine
        /// </summary>
        /// <param name="user">Kasutajanimi</param>
        /// <param name="psw">Parool</param>
        public void getData(string user, string psw)
        {

            _user = user;
            _password = psw;

            _webClientEx = new WebClientEx();
            _webClientEx.DownloadStringCompleted += client_DownloadStringCompleted;
            _webClientEx.DownloadStringAsync(new Uri("https://ycharts.com/login?next="));


        }

        /// <summary>
        /// Sisselogimise lehelt leitakse serveri poolt seadistatud token ja kasutatakse seda sisselogimiseks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            Regex rSector = new Regex("csrfmiddlewaretoken' value='(.*?)'");
            Match match = rSector.Match(e.Result);
            if (match.Success)
            {

                string token = match.Groups[1].Value;
                var data = new NameValueCollection
                               {
                                   {"csrfmiddlewaretoken", token},
                                   {"email", _user},
                                   {"password", _password},
                                   {"remember_me", "on"},
                                   {"next", "/"},
                                   {"auth_submit", "Sign In"},
                                   
                               };
                using (var clientA = _webClientEx.clone())
                {
                    clientA.Headers.Add("Referer: https://ycharts.com/login?next=");

                    clientA.UploadValuesCompleted += client_UploadValuesCompleted;
                    clientA.UploadValuesAsync(new Uri("https://ycharts.com/login"), data);
                    //clientA.UploadValues("https://ycharts.com/login", data);
                    _webClientEx = clientA.clone();
                    //clientA.DownloadStringCompleted += _webClientEx_DownloadStringCompleted;
                    //clientA.DownloadStringAsync(new Uri("https://ycharts.com/"));

                }


            }
        }

        /// <summary>
        /// Kui sisselogimise andmed on saadetud, avatakse avaleht
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_UploadValuesCompleted(object sender, System.Net.UploadValuesCompletedEventArgs e)
        {
            using (var clientB = _webClientEx.clone())
            {
                clientB.DownloadStringCompleted += _webClientEx_DownloadStringCompleted;
                clientB.DownloadStringAsync(new Uri("https://ycharts.com/"));
                _webClientEx = clientB.clone();
            }
        }

        /// <summary>
        /// Kui avaleht on avatud, kontrollitakse, kas sisselogimine on õnnestunud ja kui on, siis hakatakse eraldi async tõmbama
        /// kolme erinevat exceli faili
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _webClientEx_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {

            Regex loginR = new Regex(">Sign Out<");
            //MessageBox.Show(e.Result);
            Match match = loginR.Match(e.Result);
            if (match.Success)
            {
                _finDataAdapter.MainWindow.PasswordSet = true;
                string isUrl = "http://ycharts.com/financials/" + _symbol.ToUpper() + "/income_statement/quarterly/export";
                string bsUrl = "http://ycharts.com/financials/" + _symbol.ToUpper() + "/balance_sheet/quarterly/export";
                string cfsUrl = "http://ycharts.com/financials/" + _symbol.ToUpper() + "/cash_flow_statement/quarterly/export";


                using (WebClientEx client0 = _webClientEx.clone())
                {
                    client0.DownloadDataCompleted += is_DownloadDataCompleted;
                    client0.DownloadDataAsync(new Uri(isUrl));
                }
                using (WebClientEx client1 = _webClientEx.clone())
                {
                    client1.DownloadDataCompleted += bs_DownloadDataCompleted;
                    client1.DownloadDataAsync(new Uri(bsUrl));
                }
                using (WebClientEx client2 = _webClientEx.clone())
                {
                    client2.DownloadDataCompleted += cfs_DownloadDataCompleted;
                    client2.DownloadDataAsync(new Uri(cfsUrl));
                }
                _webClientEx.Dispose();
            }
            else
            {
                _finDataAdapter.MainWindow.PasswordSet = false;
                MessageBox.Show("Login to YCharts.com failed.");
            }
        }

        /// <summary>
        /// Kui Income Statement on valmis, saadetakse see läbi Exceli parseri ja siis XML parseri salvestama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void is_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            try
            {
                byte[] dbytes = e.Result;
                YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
                _isData = yExcel.GetData(dbytes, _symbol);
                _xmlScraper.GetData(_isData, _finDataAdapter.FinDataDao);
                PrepareData(1, sender);
            }
            catch (System.Reflection.TargetInvocationException)
            {
                MessageBox.Show("Invalid symbol or no data.");
            }
        }

        /// <summary>
        /// Kui Balance Sheet on valmis, saadetakse see läbi Exceli parseri ja siis XML parseri salvestama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            try
            {
                byte[] dbytes = e.Result;
                YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
                _bsData = yExcel.GetData(dbytes, _symbol);
                _xmlScraper.GetData(_bsData, _finDataAdapter.FinDataDao);
                PrepareData(2, sender);
            }
            catch (System.Reflection.TargetInvocationException) { }
        }

        /// <summary>
        /// Kui Cash Flow Statement on valmis, saadetakse see läbi Exceli parseri ja siis XML parseri salvestama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cfs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            try
            {
                byte[] dbytes = e.Result;
                YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
                _cfsData = yExcel.GetData(dbytes, _symbol);
                _xmlScraper.GetData(_cfsData, _finDataAdapter.FinDataDao);
                PrepareData(3, sender);
            }
            catch (System.Reflection.TargetInvocationException) { }
        }

        /// <summary>
        /// Thread safe meetod teada saamaks, kui kõik aruanded on valmis, siis salvestatakse nendest loodud XML
        /// fail desktopile AFFA folderisse. Lisaks teavitatakse Adapterit, et kõik andmed on kätte saadud.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lockThis"></param>
        private void PrepareData(int id, object lockThis)
        {
            lock (lockThis)
            {

                switch (id)
                {
                    case 1:
                        _file1 = true;
                        break;
                    case 2:
                        _file2 = true;
                        break;
                    case 3:
                        _file3 = true;
                        break;

                }
                if (_file1 && _file2 && _file3)
                {
                    var xDoc = _isData;
                    try
                    {
                        xDoc.Root.Add(_bsData.Root.Elements());
                    }
                    catch (NullReferenceException) { }
                    try
                    {
                        xDoc.Root.Add(_cfsData.Root.Elements());
                    }
                    catch (NullReferenceException) { }

                    DateTime dt = DateTime.Now;
                    string directoryName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                                           "/AFFA";
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    xDoc.Save(directoryName + "/" + _symbol + "_" + dt.ToString("yyMMdd-HHmmss") + ".xml");
                    //MessageBox.Show("prepare data");
                    _finDataAdapter.PrepareData();
                }
            }
        }


    }
}
