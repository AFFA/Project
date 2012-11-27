using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AFFA.Mudelid
{
    public static class Rowmapping
    {

        public struct RowConf
        {
            public string Label;
            public string Propery;
            public RowFormat Decimals;
            public bool PrcChange;

            public RowConf(string label, string propery, RowFormat decimals, bool prc)
            {
                Propery = propery;
                Decimals = decimals;
                Label = label;
                PrcChange = prc;
            }
        }

        public enum RowFormat
        {
            Decimal0, // pole komakohti
            Decimal2, // 2 komakohta
            Prc2, // teisendab protsendiks 2 komakohaga
            Prc1, // teisendab protsendiks 1 komakohaga
            Prc0, // teisendab protsendiks 1 komakohaga
            Txt // tekst
        }
        private static List<RowConf> _rowMapping;

        public static List<RowConf> EnglishRows()
        {
            _rowMapping = new List<RowConf>();
            /*_rowMapping.Add(new RowConf("Revenue", "IsRevenue", RowFormat.Decimal0,true));
            _rowMapping.Add(new RowConf("Cost of revenue", "IsCostOfRevenue", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Gross profit", "IsGrossProfit", RowFormat.Decimal0,true));
            // kui siia lisada veel propertyte mappinguid, kus esimesel kohal on rea nimi ja teisel kohal vastav FinData property, 
            // siis tekib automaatselt tabelisse ridu juurde, nt küll isEpsDiluted puhul veel mingi viga kuskil sees
            _rowMapping.Add(new RowConf("Net Income", "IsNetIncome", RowFormat.Decimal0,true));
            _rowMapping.Add(new RowConf("EPS", "IsEpsDiluted", RowFormat.Decimal2,true));
            _rowMapping.Add(new RowConf("Return on Assets", "FrReturnOnAssets", RowFormat.Prc1,false));*/
            _rowMapping.Add(new RowConf("Price", "FrAdjPrice", RowFormat.Decimal2, true));
            _rowMapping.Add(new RowConf("Revenue", "IsRevenue", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Cost Of Revenue", "IsCostOfRevenue", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Gross Profit", "IsGrossProfit", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Gross Profit Margin", "FrGrossProfitMargin", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Rd Expense", "IsRdExpense", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Selling General Admin Expense", "IsSellingGeneralAdminExpense", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Depreciation Amortization", "IsDepreciationAmortization", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Operating Income", "IsOperatingIncome", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Operating Margin", "FrOperatingMargin", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Non Operating Income", "IsNonOperatingIncome", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Pretax Income", "IsPretaxIncome", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Income Before Disc Operations", "IsIncomeBeforeDiscOperations", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Ebitda", "FrEbitda", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Ebitda Margin", "FrEbitdaMarginTtm", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Ebit", "FrEbit", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Ebit Margin", "FrEbitPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Net Income", "IsNetIncome", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Profit Margin", "FrProfitMargin", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Eps Basic", "IsEpsBasic", RowFormat.Decimal2, true));
            _rowMapping.Add(new RowConf("Eps Diluted", "IsEpsDiluted", RowFormat.Decimal2, true));
            _rowMapping.Add(new RowConf("Cash Conversion Cycle", "FrCashConversionCycle", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Days Inventory Outstanding", "FrDaysInventoryOutstanding", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Days Sales Outstanding", "FrDaysSalesOutstanding", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Days Payable Outstanding", "FrDaysPayableOutstanding", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Cash Short Term Investments", "BsCashShortTermInvestments", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Cash Prc", "FrCashPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Receivables", "BsReceivables", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Inventory", "BsInventory", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Working Capital", "FrWorkingCapital", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Total Current Assets", "BsTotalCurrentAssets", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Current Prc", "FrCurrentPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Net Property Plant Equipment", "BsNetPropertyPlantEquipment", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Long Term Investments", "BsLongTermInvestments", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Goodwill Intangibles", "BsGoodwillIntangibles", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Goodwill Prc", "FrGoodwillPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Total Long Term Assets", "BsTotalLongTermAssets", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Total Assets", "BsTotalAssets", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Current Portion Of Debt", "BsCurrentPortionOfLongTermDebt", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Accounts Payable", "BsAccountsPayable", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Other Current Liabilities", "BsOtherCurrentLiabilities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Total Current Liabilities", "BsTotalCurrentLiabilities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Current LT Liab Prc", "FrCurrentLPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Total Long Term Debt", "BsTotalLongTermDebt", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Debt Prc", "FrDebtPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Total LT Liabilities", "BsTotalLongTermLiabilities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Liabilities Prc", "FrLiabilitiesPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Total Liabilities", "BsTotalLiabilities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Retained Earnings", "BsRetainedEarnings", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Shares Outstanding", "BsCommonSharesOutstanding", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Shareholders Equity", "BsShareholdersEquity1", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Eq Prc", "FrEqPrc", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Total Liabilities", "BsTotalLiabilitiesShareholdersEquity", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Div Payout Ratio Ttm", "FrCashDivPayoutRatioTtm", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Dividend Yield", "FrDividendYield", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Earnings Yield", "FrEarningsYield", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Operating Earnings Yield", "FrOperatingEarningsYield", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Free Cash Flow Yield", "FrFreeCashFlowYield", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Return On Assets", "FrReturnOnAssets", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Return On Equity", "FrReturnOnEquity", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Ev Ebit", "FrEvEbit", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Ev Ebitda", "FrEvEbitda", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Ev Free Cash Flow", "FrEvFreeCashFlow", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Ev Revenues", "FrEvRevenues", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Pe Ratio", "FrPeRatio", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Peg Ratio", "FrPegRatio", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Price Book Value", "FrPriceBookValue", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Price Tang. Book Value", "FrPriceTangibleBookValue", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Price Sales Ratio", "FrPriceSalesRatio", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Effective Tax Rate Ttm", "FrEffectiveTaxRateTtm", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("Current Ratio", "FrCurrentRatio", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Debt/Equity Ratio", "FrDebtToEquityRatio", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Debt/Ebitda", "FrDebtEbitda", RowFormat.Decimal2, false));
            _rowMapping.Add(new RowConf("Net Operating Cashflow", "CfsNetCashFromOperatingActivities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Capital Expenditures", "CfsCapitalExpenditures", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Debt Issued", "CfsDebtIssued", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Equity Issued", "CfsEquityIssued", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Dividends Paid", "CfsDividendsPaid", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Net Financing Cashflow", "CfsNetCashFromFinancingActivities", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Net Change In Cash", "CfsNetChangeInCashEquivalents", RowFormat.Decimal0, false));
            _rowMapping.Add(new RowConf("Cash End Of Period", "CfsCashEndOfPeriod", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Free Cash Flow", "FrFreeCashFlow", RowFormat.Decimal0, true));
            
            
            return _rowMapping;
        }

        public static List<RowConf> DcfRows()
        {
            _rowMapping = new List<RowConf>();

            _rowMapping.Add(new RowConf("Revenue", "Revenue", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Revenue Growth", "RevenueGrowth", RowFormat.Prc1, false));
           
            //_rowMapping.Add(new RowConf("All Costs", "AllCosts", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("All Costs ex ITDA", "AllCostsEbitda", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("EBITTDA", "Ebitda", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Depreciation", "Depreciation", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("EBIT", "Ebit", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Tax Rate", "TaxRate", RowFormat.Prc1, false));
            _rowMapping.Add(new RowConf("EBIAT", "Ebiat", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Capex", "Capex", RowFormat.Decimal0, true));
            //_rowMapping.Add(new RowConf("Capex-D&A", "Capexdepreciation", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("Net working capital", "NetWorkingCapitalChange", RowFormat.Decimal0, true));
            _rowMapping.Add(new RowConf("FCFF", "Fcff", RowFormat.Decimal0, true));

            //_rowMapping.Add(new RowConf("Total Current Assets", "TotalCurrentAssets", RowFormat.Decimal0, true));
            //_rowMapping.Add(new RowConf("Total Assets", "TotalAssets", RowFormat.Decimal0, true));            
            //_rowMapping.Add(new RowConf("Total Assets Change", "TotalAssetsChange", RowFormat.Decimal0, false));

            //_rowMapping.Add(new RowConf("Total Current Liabilities", "TotalCurrentLiabilities", RowFormat.Decimal0, true));
            //_rowMapping.Add(new RowConf("Total Liabilities", "TotalLiabilities", RowFormat.Decimal0, true));            
            //_rowMapping.Add(new RowConf("Total Liabilities Change", "TotalLiabilitiesChange", RowFormat.Decimal0, false));
            
            return _rowMapping;
        }
    }
}
