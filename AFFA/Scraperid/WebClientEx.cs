using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Scraperid
{
    public class WebClientEx : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        public CookieContainer CookieContainer
        {
            get { return _cookieContainer; }
            set { _cookieContainer = value; }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            this.Headers.Add("User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.0; et; rv:1.9.2.16) Gecko/20110319 Firefox/3.6.16 ( .NET CLR 3.5.30729; .NET4.0C)");
            this.Headers.Add("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            this.Headers.Add("Accept-Language: et,et-ee;q=0.5");
            this.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            this.Headers.Add("Keep-Alive: 115");
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = CookieContainer;
            }
            return request;
        }

        public WebClientEx clone()
        {
            WebClientEx webClientEx=new WebClientEx();
            webClientEx._cookieContainer = this._cookieContainer;
            return webClientEx;
        }
    }
}
