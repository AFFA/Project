﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AFFA.Mudelid;
using AFFA.Vaatemudelid;
using FileHelpers;
using System.Windows.Forms;

namespace AFFA.Scraperid
{
    public class YahooFScraper
    {
        private InputVM _inputVm;

        public YahooFScraper(InputVM inputVM)
        {
            _inputVm = inputVM;
        }

        public void GetPriceData(string symbol)
        {
            string url = "http://ichart.finance.yahoo.com/table.csv?s=" + symbol + "&d=" + (DateTime.Today.Month-1) + "&e=" + DateTime.Today.Day + "&f=" + DateTime.Today.ToString("yyyy") + "&g=d&a=2&b=26&c=1990&ignore=.csv";
            //MessageBox.Show(url);
            WebClient klient = new WebClient();
            klient.Encoding = Encoding.UTF8;
            klient.DownloadStringCompleted += klient_DownloadStringCompleted;
            klient.DownloadStringAsync(new Uri(url));
        }

        void klient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            readFile(e.Result);
        }
        void readFile(string file)
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(PriceData));
            PriceData[] res = engine.ReadString(file) as PriceData[];
            _inputVm.PriceDataDao.AddData(res);
        }

        public void GetProfileData(string symbol)
        {
            string url = "http://finance.yahoo.com/q/pr?s="+symbol.ToUpper()+"+Profile";
            //MessageBox.Show(url);
            WebClient klient = new WebClient();
            klient.Encoding = Encoding.UTF8;
            klient.DownloadStringCompleted += profile_DownloadStringCompleted;
            klient.DownloadStringAsync(new Uri(url));
        }
        void profile_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            ParseProfile(e.Result);
        }

        void ParseProfile(string file)
        {
            Regex rSector = new Regex(">Sector:<.*?<a.*?>(.*?)<");
            Match match = rSector.Match(file);
            if (match.Success)
            {
                _inputVm.Sector = match.Groups[1].Value.Replace("&amp;","&");
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
        }

    }
}
