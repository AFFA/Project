using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AFFA.Mudelid;


namespace AFFA.Scraperid
{
    static class XmlScraper
    {

        public static void GetData(string symbol, string fileName, FinDataDao dao)
        {
            try
            {
                XDocument xdoc = XDocument.Load(fileName);
                GetData(symbol, xdoc, dao);
            }
            catch (XmlException)
            {
                System.Windows.MessageBox.Show("Error reading XML");
            }
        }

        public static void GetData(string symbol, XDocument xdoc, FinDataDao dao)
        {
            try
            {
                var query = from x in xdoc.Descendants("table") select new FinData(x);
                foreach (var item in query)
                {
                    dao.AddFinData(item);
                }
            }
            catch (XmlException)
            {
                System.Windows.MessageBox.Show("Error reading XML");
            }
        }
    }
}
