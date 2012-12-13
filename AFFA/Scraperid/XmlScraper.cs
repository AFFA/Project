using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AFFA.Mudelid;


namespace AFFA.Scraperid
{
    public class XmlScraper
    {
        private FinDataDao _finDataDao;
        private FinDataAdapter _finDataAdapter;

        public void GetData(string fileName, FinDataDao dao, FinDataAdapter finDataAdapter)
        {
            _finDataDao = dao;
            _finDataAdapter = finDataAdapter;
            try
            {
                //XDocument xdoc = XDocument.Load(fileName);
                //GetData(xdoc, dao);
                LoadXMLData(fileName);
            }
            catch (XmlException)
            {
                System.Windows.MessageBox.Show("Error reading XML");
            }
        }

        private void LoadXMLData(string fileName)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync(@fileName);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // This will run on your UI thread when worker_DoWork returns
            try
            {
                XDocument xmlData = (XDocument) e.Result;
                GetData(xmlData, _finDataDao);
            }
            catch (InvalidCastException)
            {
                System.Windows.MessageBox.Show("Error reading XML.");
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) // is called by worker.RunWorkerAsync()
        {
            // Load data here
            XDocument xmlData = XDocument.Load(e.Argument.ToString());
            //object xmlData = null; // can use e.Argument => "c:\test.xml";

            e.Result = xmlData;
        }

        public void GetData(XDocument xdoc, FinDataDao dao)
        {
            try
            {
                var query = from x in xdoc.Descendants("table") select new FinData(x);
                foreach (var item in query)
                {
                    dao.AddFinData(item);
                }
                _finDataAdapter.XmlDataReady();
            }
            catch (XmlException)
            {
                System.Windows.MessageBox.Show("Error reading XML");
            }
        }
    }
}
