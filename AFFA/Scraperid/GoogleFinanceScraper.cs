using AFFA.Mudelid;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace AFFA.Scraperid
{
    class GoogleFinanceScraper
    {
        XDocument _downloadedData;

        public XDocument DownloadedData
        {
            get { return this._downloadedData; }
            set { this._downloadedData = value; }
        }

        public void GetData(string ticker)
        {
            FinDataDao curDatas = new FinDataDao();
            string url = "http://www.google.com/finance?q=" + ticker.ToUpper() + "&fstype=ii";

            //Load Google Finance with the specified URL.
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);

            //List of Elements containing data tables in Google Finance.
            Dictionary<string, Dictionary<string, string>> elementNames = new Dictionary<string, Dictionary<string, string>>();
            elementNames.Add("incinterimdiv", IsMappings());
            elementNames.Add("balinterimdiv", BsMappings());
            elementNames.Add("casinterimdiv", CfsMappings());

            HtmlNode curElement;
            List<XElement> datas = new List<XElement>();
            Dictionary<int, int> columnToDataMapping;
            DateTime dt;
            HtmlNode newNode;
            XElement newEl;
            string kuupaev;
            string curDataName;
            foreach (var element in elementNames)
            {
                curElement = document.GetElementbyId(element.Key);
                Dictionary<string, string> mappings = element.Value;

                if (curElement == null)
                {
                    continue;
                }
                columnToDataMapping = new Dictionary<int, int>();
                var columnHeaders = from lnks in curElement.Descendants() where lnks.Name.Equals("th") && lnks.InnerText.Trim().Length > 0 select new { Text = lnks.InnerText };
                for (int i = 0; i < columnHeaders.Count(); i++)
                {
                    var columnHeader = columnHeaders.ElementAt(i);
                    if (DateTime.TryParse(columnHeader.Text.Trim().Substring(columnHeader.Text.Trim().Length - 11), out dt))
                    {
                        if (HasQuarter(datas, dt) >= 0)
                        {
                            columnToDataMapping.Add(i, HasQuarter(datas, dt));
                        }
                        else
                        {
                            kuupaev = String.Format("{0}-{1}-{2}", dt.Year, dt.Month, dt.Day);
                            newEl = new XElement("table");
                            newEl.Add(new XAttribute("is_kuupaev", kuupaev));
                            XElement dateEl = new XElement("column", kuupaev);
                            dateEl.Add(new XAttribute("name", "is_kuupaev"));
                            newEl.Add(dateEl);
                            columnToDataMapping.Add(i, datas.Count());
                            datas.Add(newEl);
                        }
                    }
                }

                var rows = from lnks in curElement.Descendants() where lnks.Name.Equals("tr") select new { InnerHtml = lnks.OuterHtml };
                foreach (var row in rows)
                {
                    newNode = HtmlNode.CreateNode(row.InnerHtml);
                    var tdd = from td in newNode.Descendants() where td.Name.Equals("td") select new { Data = td.InnerText };
                    if (tdd.Count() > 0)
                    {
                        if (mappings.TryGetValue(tdd.ElementAt(0).Data.Trim(), out curDataName) && tdd.Count() > 1)
                        {
                            for (int i = 1; i < tdd.Count(); i++)
                            {
                                int j = 0;
                                if (columnToDataMapping.TryGetValue(i, out j))
                                {
                                    double num;
                                    NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                                    if (Double.TryParse(tdd.ElementAt(i).Data.Trim(), style, culture, out num))
                                    {
                                        newEl = new XElement("column", num.ToString());
                                    }
                                    else
                                    {
                                        newEl = new XElement("column", "NULL");
                                    }
                                    newEl.Add(new XAttribute("name", curDataName));
                                    datas.ElementAt(j).Add(newEl);
                                }
                            }
                        }
                    }
                }
            }

            if (datas.Count() > 0)
            {
                _downloadedData = new XDocument();
                XElement database = new XElement("database");
                foreach (XElement e in datas)
                {
                    database.Add(e);
                }
                _downloadedData.Add(database);
            }
            else
            {
                _downloadedData = null;
            }
        }

        public int HasQuarter(List<XElement> xel, DateTime kp)
        {
            DateTime elemDate;
            XElement e;
            for (int i = 0; i < xel.Count(); i++)
            {
                e = xel.ElementAt(i);
                elemDate = DateTime.TryParse(e.Attribute("is_kuupaev").Value, out elemDate) ? elemDate : DateTime.Now;
                if (elemDate.Equals(kp))
                {
                    return i;
                }
            }
            return -1;
        }

        public Dictionary<string, string> BsMappings()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            // Balance sheet
            mappings.Add("Cash and Short Term Investments", "bs_cash_short_term_investments");
            mappings.Add("Total Receivables, Net", "bs_receivables");
            mappings.Add("Total Inventory ", "bs_inventory");
            mappings.Add("Prepaid Expenses ", "bs_prepaid_expenses");
            mappings.Add("Other Current Assets, Total", "bs_other_current_assets");
            mappings.Add("Total Current Assets", "bs_total_current_assets");
            mappings.Add("Property/Plant/Equipment, Total - Gross", "bs_gross_property_plant_equipment");
            mappings.Add("Accumulated Depreciation, Total", "bs_accumulated_depreciation");

            //(Property/Plant/Equipment, Total - Gross) + (Accumulated Depreciation, Total)
            //mappings.Add("", "BsNetPropertyPlantEquipment");

            mappings.Add("Long Term Investments ", "bs_long_term_investments");

            //(Goodwill, Net) + (Intangibles, Net)
            //mappings.Add("", "BsGoodwillIntangibles");

            mappings.Add("Other Long Term Assets, Total", "bs_other_long_term_assets");

            //(Total Assets) - (Total Current Assets)
            //mappings.Add("", "BsTotalLongTermAssets");

            mappings.Add("Total Assets", "bs_total_assets");
            mappings.Add("Current Port. of LT Debt/Capital Leases", "bs_current_portion_of_long_term_debt");
            mappings.Add("Accounts Payable", "bs_accounts_payable");
            mappings.Add("Accrued Expenses", "bs_accrued_expenses");
            mappings.Add("Other Current liabilities, Total", "bs_other_current_liabilities");
            mappings.Add("Total Current Liabilities", "bs_total_current_liabilities");
            mappings.Add("Total Long Term Debt", "bs_total_long_term_debt");
            mappings.Add("Deferred Income Tax", "bs_deferred_income_tax");
            mappings.Add("Minority Interest", "bs_minority_interest");
            mappings.Add("Other Liabilities, Total", "bs_other_long_term_liabilities");

            //(Total Liabilities) - (Total Current Liabilities)
            //mappings.Add("", "BsTotalLongTermLiabilities");

            mappings.Add("Total Liabilities", "bs_total_liabilities");
            mappings.Add("Total Common Shares Outstanding", "bs_common_shares_outstanding");
            mappings.Add("Preferred Stock - Non Redeemable, Net", "bs_preferred_stock");
            mappings.Add("Common Stock, Total", "bs_common_stock_net");
            mappings.Add("Additional Paid-In Capital", "bs_additional_paid_in_capital");
            mappings.Add("Retained Earnings (Accumulated Deficit)", "bs_retained_earnings");
            mappings.Add("Treasury Stock - Common", "bs_treasury_stock");
            mappings.Add("Other Equity, Total", "bs_other_shareholders_equity");
            mappings.Add("Total Equity", "bs_shareholders_equity1");
            mappings.Add("Total Liabilities &amp; Shareholders' Equity", "bs_total_liabilities_shareholders_equity");
            return mappings;
        }

        public Dictionary<string, string> IsMappings()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();

            // Income statement
            mappings.Add("Total Revenue", "is_revenue");
            mappings.Add("Cost of Revenue, Total", "is_cost_of_revenue");
            mappings.Add("Gross Profit", "is_gross_profit");
            mappings.Add("Research &amp; Development", "is_rd_expense");
            mappings.Add("Selling/General/Admin. Expenses, Total ", "is_selling_general_admin_expense");
            mappings.Add("Depreciation/Amortization", "is_depreciation_amortization");
            mappings.Add("Interest Expense(Income) - Net Operating", "is_operating_interest_expense");
            mappings.Add("Other Operating Expenses, Total ", "is_other_operating_income_expense");
            mappings.Add("Total Operating Expense", "is_total_operating_expenses");
            mappings.Add("Operating Income", "is_operating_income");
            mappings.Add("Other, Net", "is_non_operating_income");
            mappings.Add("Income Before Tax", "is_pretax_income");
            mappings.Add("Income After Tax", "is_income_after_tax");
            mappings.Add("Minority Interest", "is_minority_interest");
            mappings.Add("Equity In Affiliates ", "is_equity_in_affiliates");
            mappings.Add("Net Income Before Extra. Items", "is_income_before_disc_operations");
            mappings.Add("Accounting Change", "is_other_income_charges");
            mappings.Add("Discontinued Operations", "is_income_from_disc_operations");
            mappings.Add("Net Income", "is_net_income");
            mappings.Add("Diluted Weighted Average Shares", "is_average_shares_diluted_eps");
            mappings.Add("Basic Weighted Average Shares", "is_average_shares_basic_eps");
            mappings.Add("Basic EPS Excluding Extraordinary Items ", "is_eps_basic");
            mappings.Add("Diluted EPS Excluding Extraordinary Items", "is_eps_diluted");
            return mappings;
        }

        public Dictionary<string, string> CfsMappings()
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();

            //Cash flow statement
            //mappings.Add("Net Income/Starting Line", "CfsNetIncome");

            //(Depreciation/Depletion) + (Amortization)
            //mappings.Add("", "CfsDepreciationDepletionAmortization");

            mappings.Add("Non-Cash Items", "cfs_total_non_cash_items");
            mappings.Add("Deferred Taxes", "cfs_deferred_income_taxes");
            mappings.Add("Changes in Working Capital", "cfs_total_changes_in_assets_liabilities");
            mappings.Add("Cash from Operating Activities", "cfs_net_cash_from_operating_activities");
            mappings.Add("Cash from Investing Activities", "cfs_cash_flow_investing");
            mappings.Add("Capital Expenditures", "cfs_capital_expenditures");
            mappings.Add("Other Investing Cash Flow Items, Total", "cfs_other_investing_activities");
            mappings.Add("Issuance (Retirement) of Debt, Net", "cfs_debt_issued");
            mappings.Add("Issuance (Retirement) of Stock, Net", "cfs_equity_issued");
            mappings.Add("Total Cash Dividends Paid", "cfs_dividends_paid");
            mappings.Add("Cash from Financing Activities", "cfs_net_cash_from_financing_activities");
            mappings.Add("Foreign Exchange Effects", "cfs_foreign_exchange_effects");
            mappings.Add("Net Change in Cash", "cfs_net_change_in_cash_equivalents");
            return mappings;
        }
    }
}
