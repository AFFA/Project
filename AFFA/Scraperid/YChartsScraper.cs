﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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




        public YChartsScraper(FinDataAdapter finDataAdapter, string symbol)
        {
            _finDataAdapter = finDataAdapter;
            _symbol = symbol;

        }

        public void getData(string user, string psw)
        {

                _user = user;
                _password = psw;

 
                
                _webClientEx = new WebClientEx();
                _webClientEx.DownloadStringCompleted += client_DownloadStringCompleted;
                _webClientEx.DownloadStringAsync(new Uri("https://ycharts.com/login?next="));

          
        }

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

        void client_UploadValuesCompleted(object sender, System.Net.UploadValuesCompletedEventArgs e)
        {
            using (var clientB = _webClientEx.clone())
            {
                clientB.DownloadStringCompleted += _webClientEx_DownloadStringCompleted;
                clientB.DownloadStringAsync(new Uri("https://ycharts.com/"));
                _webClientEx = clientB.clone();
            }
        }

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

        void is_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes, _symbol);
            XmlScraper.GetData(data, _finDataAdapter.FinDataDao);
            PrepareData(1, sender);
        }
        void bs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes, _symbol);
            XmlScraper.GetData(data, _finDataAdapter.FinDataDao);
            PrepareData(2, sender);
        }
        void cfs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes, _symbol);
            XmlScraper.GetData(data, _finDataAdapter.FinDataDao);
            PrepareData(3, sender);
        }

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
                    //MessageBox.Show("prepare data");
                    _finDataAdapter.PrepareData();
                }
            }
        }


    }
}
