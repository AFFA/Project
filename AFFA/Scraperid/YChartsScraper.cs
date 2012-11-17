using System;
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
        private FinAnalysisVM _finAnalysisVm;


        public YChartsScraper(FinDataAdapter finDataAdapter, FinAnalysisVM finAnalysisVm, string symbol, string user, string password)
        {
            _finDataAdapter = finDataAdapter;
            _finAnalysisVm = finAnalysisVm;
            _symbol = symbol;
            _user = user;
            _password = password;
        }

        public void getData()
        {
            using (var client = new WebClientEx())
            {

                var response1 = client.DownloadString("https://ycharts.com/login?next=");
                Regex rSector = new Regex("csrfmiddlewaretoken' value='(.*?)'");
                Match match = rSector.Match(response1);
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
                    client.Headers.Add("Referer: https://ycharts.com/login?next=");
                    var response2 = client.UploadValues("https://ycharts.com/login", data);

                    string isUrl = "http://ycharts.com/financials/CSCO/income_statement/quarterly/export";
                    string bsUrl = "http://ycharts.com/financials/CSCO/balance_sheet/quarterly/export";
                    string cfsUrl = "http://ycharts.com/financials/CSCO/cash_flow_statement/quarterly/export";


                    client.DownloadDataCompleted += is_DownloadDataCompleted;
                    client.DownloadDataAsync(new Uri(isUrl));

                    using (WebClientEx client1 = client.clone())
                    {
                        client1.DownloadDataCompleted += bs_DownloadDataCompleted;
                        client1.DownloadDataAsync(new Uri(bsUrl));
                    }
                    using (WebClientEx client2 = client.clone())
                    {
                        client2.DownloadDataCompleted += cfs_DownloadDataCompleted;
                        client2.DownloadDataAsync(new Uri(cfsUrl));
                    }

                }

            }
        }

        void is_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes);
            XmlScraper.GetData(_symbol, data, _finDataAdapter.FinDataDao);
            PrepareData(1, sender);
        }
        void bs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes);
            XmlScraper.GetData(_symbol, data, _finDataAdapter.FinDataDao);
            PrepareData(2, sender);
        }
        void cfs_DownloadDataCompleted(object sender, System.Net.DownloadDataCompletedEventArgs e)
        {
            byte[] dbytes = e.Result;
            YChartsExcelScraperTest yExcel = new YChartsExcelScraperTest();
            XDocument data = yExcel.GetData(dbytes);
            XmlScraper.GetData(_symbol, data, _finDataAdapter.FinDataDao);
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
                    MessageBox.Show("prepare data");
                    _finDataAdapter.PrepareData();
                    _finAnalysisVm.PrepareTable(_finDataAdapter.FinDataDao.FinDatas);
                }
            }
        }


    }
}
