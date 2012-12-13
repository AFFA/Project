using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using AFFA.Mudelid;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;

namespace AFFA.Scraperid
{
    public class YChartsExcelScraperTest
    {
        Dictionary<KeyValuePair<string, string>, int> _dataNameRowVariableMappings = new Dictionary<KeyValuePair<string, string>, int>();
        int _dataHeaderColumnNum = 0;


        public XDocument GetData(byte[] dbytes, string symbol)
        {
            try
            {
               
                    ByteArrayInputStream bais = new ByteArrayInputStream(dbytes);
                    XDocument xDoc = new XDocument();
                    HSSFWorkbook hwb = new HSSFWorkbook(bais);
                    ISheet sheet = hwb.GetSheetAt(0);
                    AddDataNameRowVariableMappingsCurDoc(sheet.SheetName);
                    //MessageBox.Show(IsNameRowMappingValid(sheet).ToString());

                    IRow quartersDataRow = findQuartersDataRow(sheet);
                    if (quartersDataRow != null)
                    {
                        int[] quarterColumnIndexes = FindQuarterColumnIndexes(quartersDataRow);
                        
                        if (quarterColumnIndexes.Count() > 0)
                        {
                            
                            XElement xTable;
                            XElement xColumn;
                            XAttribute curAttribute;
                            ICell curCell;
                            string curData;
                            XElement xDatabase = new XElement("database");
                            for (int i = 0; i < quarterColumnIndexes.Count(); i++)
                            {
                                xTable = new XElement("table");
                                xColumn = new XElement("column", symbol);
                                curAttribute = new XAttribute("name", "is_symbol");
                                xColumn.Add(curAttribute);
                                xTable.Add(xColumn);
                                for (int j = 0; j < _dataNameRowVariableMappings.Count; j++)
                                {
                                    curCell = sheet.GetRow(_dataNameRowVariableMappings.ElementAt(j).Value).GetCell(quarterColumnIndexes[i]);
                                    curCell.SetCellType(CellType.STRING);
                                    if (String.IsNullOrEmpty(curCell.StringCellValue))
                                    {
                                        curData = "NULL";
                                    }
                                    else
                                    {
                                        curData = curCell.StringCellValue;
                                    }
                                    xColumn = new XElement("column", curData);
                                    curAttribute = new XAttribute("name", _dataNameRowVariableMappings.ElementAt(j).Key.Value);
                                    xColumn.Add(curAttribute);
                                    xTable.Add(xColumn);
                                }
                                xDatabase.Add(xTable);
                            }
                            xDoc.Add(xDatabase);
                            //DateTime dt = DateTime.Now;
                            //string directoryName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                            //                       "/AFFA";
                            //if (!Directory.Exists(directoryName))
                            //{
                            //    Directory.CreateDirectory(directoryName);
                            //}
                            //xDoc.Save(directoryName + "/" + symbol + "_" + dt.ToString("yyMMdd-HHmmss") + ".xml");
                            return xDoc;
                        }
                    }
                    return null;
                
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        public IRow findQuartersDataRow(ISheet s)
        {
            System.Collections.IEnumerator rows = s.GetRowEnumerator();
            ICell curCell;
            IRow curRow;
            while (rows.MoveNext())
            {
                curRow = (HSSFRow)rows.Current;
                for (int i = 0; i < curRow.LastCellNum; i++)
                {
                    curCell = curRow.GetCell(i);
                    if (curCell.StringCellValue.Equals("Cash Flow Statement") || curCell.StringCellValue.Equals("Balance Sheet") || curCell.StringCellValue.Equals("Income Statement"))
                    {
                        return curRow;
                    }
                }
            }
            return null;
        }

        public int[] FindQuarterColumnIndexes(IRow row)
        {
            List<int> quarterColumnIndexes = new List<int>();
            DateTime curItem;
            ICell curCell;
            for (int i = 0; i < row.LastCellNum; i++)
            {
                curCell = row.GetCell(i);
                if (curCell != null)
                {
                    if (DateTime.TryParse(curCell.StringCellValue, out curItem))
                    {
                        quarterColumnIndexes.Add(i);
                    }
                }
            }
            if (quarterColumnIndexes.Count == 0)
            {
                return null;
            }
            return quarterColumnIndexes.ToArray();
        }

        public string[] FindRowHeaderNames(ISheet s)
        {
            string[] result;
            List<string> rowHeaderNames = new List<string>();
            System.Collections.IEnumerator rows = s.GetRowEnumerator();
            ICell curCell;
            IRow curRow;
            while (rows.MoveNext())
            {
                curRow = (HSSFRow)rows.Current;
                curCell = curRow.GetCell(0);
                if (curCell != null)
                {
                    rowHeaderNames.Add(curCell.StringCellValue);
                }
            }
            if (rowHeaderNames.Count == 0)
            {
                return null;
            }
            result = rowHeaderNames.ToArray();
            Array.Sort(result);
            return result;
        }

        public Boolean IsNameRowMappingValid(ISheet s)
        {
            ICell curCell;
            KeyValuePair<KeyValuePair<string, string>, int> curKVP;
            for (int i = 0; i < _dataNameRowVariableMappings.Count; i++)
            {
                curKVP = _dataNameRowVariableMappings.ElementAt(i);
                if (s.GetRow(curKVP.Value) == null)
                {
                    MessageBox.Show("Staatilises andmetabelis vastavale reanumbrile ei leidu tabelis elementi.");
                    return false;
                }
                curCell = s.GetRow(curKVP.Value).GetCell(_dataHeaderColumnNum);
                if (curCell == null)
                {
                    MessageBox.Show("Staatilises andmetabelis vastavale lahtrinumbrile ei leidu tabelis elementi.");
                    return false;
                }
                else if (curCell.StringCellValue.Equals(curKVP.Key.Key) == false)
                {
                    MessageBox.Show("Staatilises andmetabelis vastavale nimetusele ei vasta tabelis õige nimetus.");
                    return false;
                }
            }
            return true;
        }

        public void AddDataNameRowVariableMappingsCurDoc(string sheetName)
        {
            if (sheetName.Equals("Quarterly Cash Flow"))
            {
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Cash Flow Statement", "is_kuupaev"), 3);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Income", "cfs_net_income"), 6);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Depreciation, Depletion, Amortization", "cfs_depreciation_depletion_amortization"), 7);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Non-Cash Items", "cfs_other_non_cash_items"), 8);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Non-Cash Items", "cfs_total_non_cash_items"), 9);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Deferred Income Taxes", "cfs_deferred_income_taxes"), 10);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Changes in Assets/Liabilities", "cfs_total_changes_in_assets_liabilities"), 11);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Operating Activities", "cfs_other_operating_activities"), 12);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Cash from Operating Activities", "cfs_net_cash_from_operating_activities"), 13);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Capital Expenditures", "cfs_capital_expenditures"), 16);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Acquisitions, Divestitures", "cfs_acquisitions_divestitures"), 17);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Investments", "cfs_investments"), 18);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Investing Activities", "cfs_other_investing_activities"), 19);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Cash from Investing Activities", "cfs_net_cash_from_investing_activities"), 20);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Debt Issued", "cfs_debt_issued"), 23);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Equity Issued", "cfs_equity_issued"), 24);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Dividends Paid", "cfs_dividends_paid"), 25);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Financing Activities", "cfs_other_financing_activities"), 26);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Cash from Financing Activities", "cfs_net_cash_from_financing_activities"), 27);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Foreign Exchange Effects", "cfs_foreign_exchange_effects"), 28);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Change in Cash & Cash Equivalents", "cfs_net_change_in_cash_equivalents"), 30);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Cash at beginning of period", "cfs_cash_beginning_of_period"), 31);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Cash at end of period", "cfs_cash_end_of_period"), 32);
            }
            else if (sheetName.Equals("Quarterly Balance Sheet"))
            {
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Balance Sheet", "is_kuupaev"), 3);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Cash & Short Term Investments", "bs_cash_short_term_investments"), 6);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Receivables", "bs_receivables"), 7);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Inventory", "bs_inventory"), 8);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Prepaid Expenses", "bs_prepaid_expenses"), 9);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Current Assets", "bs_other_current_assets"), 10);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Current Assets", "bs_total_current_assets"), 11);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Gross Property, Plant & Equipment", "bs_gross_property_plant_equipment"), 12);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Accumulated Depreciation", "bs_accumulated_depreciation"), 13);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Property, Plant & Equipment", "bs_net_property_plant_equipment"), 14);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Long Term Investments", "bs_long_term_investments"), 15);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Goodwill & Intangibles", "bs_goodwill_intangibles"), 16);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Long Term Assets", "bs_other_long_term_assets"), 17);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Long Term Assets", "bs_total_long_term_assets"), 18);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Assets", "bs_total_assets"), 19);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Current Portion of Long Term Debt", "bs_current_portion_of_long_term_debt"), 22);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Accounts Payable ", "bs_accounts_payable"), 23);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Accrued Expenses", "bs_accrued_expenses"), 24);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Deferred Revenues", "bs_deferred_revenues"), 25);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Current Liabilities", "bs_other_current_liabilities"), 26);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Current Liabilities", "bs_total_current_liabilities"), 27);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Long Term Debt", "bs_total_long_term_debt"), 28);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Deferred Income Tax", "bs_deferred_income_tax"), 29);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Minority Interest", "bs_minority_interest"), 42);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Long Term Liabilities", "bs_other_long_term_liabilities"), 30);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Long Term Liabilities", "bs_total_long_term_liabilities"), 31);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Liabilities", "bs_total_liabilities"), 32);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Common Shares Outstanding", "bs_common_shares_outstanding"), 35);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Preferred Stock", "bs_preferred_stock"), 36);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Common Stock, Net", "bs_common_stock_net"), 37);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Additional Paid-in Capital", "bs_additional_paid_in_capital"), 38);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Retained Earnings", "bs_retained_earnings"), 39);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Treasury Stock", "bs_treasury_stock"), 40);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Shareholder's Equity", "bs_other_shareholders_equity"), 41);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Shareholder's Equity", "bs_shareholders_equity1"), 43);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Liabilities & Shareholder's Equity", "bs_total_liabilities_shareholders_equity"), 44);
            }
            else if (sheetName.Equals("Quarterly Income Statements"))
            {
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Income Statement", "is_kuupaev"), 3);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Revenue", "is_revenue"), 6);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Cost of Revenue", "is_cost_of_revenue"), 7);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Gross Profit", "is_gross_profit"), 8);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Research & Development Expense", "is_rd_expense"), 9);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Selling, General, & Admin. Expense", "is_selling_general_admin_expense"), 10);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Depreciation  & Amortization", "is_depreciation_amortization"), 11);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Operating Interest Expense", "is_operating_interest_expense"), 12);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Operating Income (Expense)", "is_other_operating_income_expense"), 13);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Total Operating Expenses", "is_total_operating_expenses"), 14);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Operating Income", "is_operating_income"), 15);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Non-Operating Income", "is_non_operating_income"), 16);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Pretax Income", "is_pretax_income"), 17);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Provision for Income Taxes", "is_provision_for_income_taxes"), 18);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Income after Tax", "is_income_after_tax"), 19);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Minority Interest", "is_minority_interest1"), 20);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Equity In Affiliates", "is_equity_in_affiliates"), 21);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Income Before Extraordinaries & Disc. Operations", "is_income_before_disc_operations"), 22);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Investment Gains/Losses", "is_investment_gains_losses"), 23);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Other Income/Charges", "is_other_income_charges"), 24);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Income from Discontinued Operations", "is_income_from_disc_operations"), 25);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Net Income", "is_net_income"), 26);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Average Shares to compute diluted EPS", "is_average_shares_diluted_eps"), 28);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("Average Shares to compute basic EPS", "is_average_shares_basic_eps"), 29);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("EPS - Basic net", "is_eps_basic"), 30);
                _dataNameRowVariableMappings.Add(new KeyValuePair<string, string>("EPS - Diluted net", "is_eps_diluted"), 31);
            }
        }
    }
}
