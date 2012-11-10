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
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.0; et; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16 ( .NET CLR 3.5.30729; .NET4.0C)");
                client.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                client.Headers.Add("Accept-Language: et,et-ee;q=0.5");
                client.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                client.Headers.Add("Keep-Alive: 115");

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
                    client.Headers.Add("User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.0; et; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16 ( .NET CLR 3.5.30729; .NET4.0C)");
                    client.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    client.Headers.Add("Accept-Language: et,et-ee;q=0.5");
                    client.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                    client.Headers.Add("Keep-Alive: 115");
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
