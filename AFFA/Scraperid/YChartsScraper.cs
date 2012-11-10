using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFFA.Scraperid
{
    public class YChartsScraper
    {
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
                                   {"email", ""},
                                   {"password", ""},
                                   {"remember_me", "on"},
                                   {"next", "/"},
                                   {"auth_submit", "Sign In"},
                                   
                               };
                    client.Headers.Add("Referer: https://ycharts.com/login?next=");
                    var response2 = client.UploadValues("https://ycharts.com/login", data);
                    //MessageBox.Show(Encoding.Default.GetString(response2));
                    //var response3 = client.DownloadString("http://ycharts.com/accounts/my_account");
                    //MessageBox.Show(response3);




                }
                
            }
        }
    }
}
