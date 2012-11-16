using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.Util;

namespace AFFA.Scraperid
{
    public class YChartsScraper
    {
        private string _user;
        private string _password;
        private string _symbol;

        public YChartsScraper(string symbol, string user, string password)
        {
            _symbol = symbol;
            _user = user;
            _password = password;
        }

        public byte[] getData()
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
                    //MessageBox.Show(Encoding.Default.GetString(response2));

                    //http://ycharts.com/financials/CSCO/income_statement/quarterly/export
                    
                    byte[] dbytes = client.DownloadData("http://ycharts.com/financials/CSCO/income_statement/quarterly/export");
                    //client.DownloadFile("http://ycharts.com/financials/CSCO/income_statement/quarterly/export","C://Data/test.xls");
                    //ByteArrayInputStream bais = new ByteArrayInputStream(dbytes);
                    //HSSFWorkbook hwb = new HSSFWorkbook(bais);
                    //var response3 = client.DownloadString("http://ycharts.com/accounts/my_account");
                    //MessageBox.Show(response3);
                    return dbytes;
                    //return null;


                }
                
            }
            return null;
        }
    }
}
