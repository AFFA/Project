using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    public static class RatioCalculator
    {
        public static void Calculate(List<FinData> finDatas)
        {


            for (int i = 0; i < finDatas.Count; i++)
            {

                FinData yandmed = finDatas[i];


                double? workingcapital = null;
                if (yandmed.BsTotalCurrentAssets != null && yandmed.BsTotalCurrentLiabilities != null)
                {
                    workingcapital = (yandmed.BsTotalCurrentAssets - yandmed.BsTotalCurrentLiabilities);
                }



                /*
                 * ttm arvutused:
                 */
                double? revenuettm = null;
                double? netincomettm = null;
                double? dividendspaidttm = null;
                double? cashflowfinancingttm = null;
                double? cashflowinvestingttm = null;
                double? cashflowoperationsttm = null;
                double? dividendttm = null;
                double? sharesttm = null;
                double? ebitttm = null;
                double? ebitdattm = null;
                double? freecashflowttm = null;
                double? epsttm = null;
                double? incometaxesttm = null;
                double? pretaxincomettm = null;
                double? expensesttm = null;
                double? grossprofitttm = null;
                double? contopsepsttm = null;
                double? operatingprofitttm = null;
                double? bookvaluepersharettm = null;
                double? tangiblebookvaluepersharettm = null;
                double? costrevenuettm = null;
                double? interestexpensesttm = null;
                double? debtttm = null;
                double? marketcap = null;
                double? price = yandmed.FrPrice;

                if (i >= 3)
                {
                    FinData yandmedT1 = finDatas[i - 1];
                    FinData yandmedT2 = finDatas[i - 2];
                    FinData yandmedT3 = finDatas[i - 3];

                    if (yandmed.BsCommonSharesOutstanding == null)
                    {
                        yandmed.BsCommonSharesOutstanding = yandmedT1.BsCommonSharesOutstanding;
                        if (yandmed.BsCommonSharesOutstanding == null)
                        {
                            yandmed.BsCommonSharesOutstanding = yandmedT2.BsCommonSharesOutstanding;
                            if (yandmed.BsCommonSharesOutstanding == null)
                            {
                                yandmed.BsCommonSharesOutstanding = yandmedT3.BsCommonSharesOutstanding;
                            }
                        }
                    }

                    try
                    {
                        revenuettm = yandmed.IsRevenue + yandmedT1.IsRevenue + yandmedT2.IsRevenue + yandmedT3.IsRevenue;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        netincomettm = yandmed.IsNetIncome + yandmedT1.IsNetIncome + yandmedT2.IsNetIncome +
                                       yandmedT3.IsNetIncome;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        dividendspaidttm = yandmed.CfsDividendsPaid + yandmedT1.CfsDividendsPaid +
                                           yandmedT2.CfsDividendsPaid + yandmedT3.CfsDividendsPaid;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        cashflowfinancingttm = yandmed.CfsNetCashFromFinancingActivities +
                                               yandmedT1.CfsNetCashFromFinancingActivities +
                                               yandmedT2.CfsNetCashFromFinancingActivities +
                                               yandmedT3.CfsNetCashFromFinancingActivities;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        cashflowinvestingttm = yandmed.CfsNetCashFromInvestingActivities +
                                               yandmedT1.CfsNetCashFromInvestingActivities +
                                               yandmedT2.CfsNetCashFromInvestingActivities +
                                               yandmedT3.CfsNetCashFromInvestingActivities;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        cashflowoperationsttm = yandmed.CfsNetCashFromOperatingActivities +
                                                yandmedT1.CfsNetCashFromOperatingActivities +
                                                yandmedT2.CfsNetCashFromOperatingActivities +
                                                yandmedT3.CfsNetCashFromOperatingActivities;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        dividendttm = yandmed.CfsDividendsPaid + yandmedT1.CfsDividendsPaid + yandmedT2.CfsDividendsPaid +
                                      yandmedT3.CfsDividendsPaid;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        sharesttm = (yandmed.BsCommonSharesOutstanding + yandmedT1.BsCommonSharesOutstanding +
                                     yandmedT2.BsCommonSharesOutstanding + yandmedT3.BsCommonSharesOutstanding) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        ebitttm = yandmed.IsNetIncome - ((yandmed.IsOperatingInterestExpense == null) ? 0 : yandmed.IsOperatingInterestExpense) -
                           ((yandmed.IsProvisionForIncomeTaxes == null) ? 0 : yandmed.IsProvisionForIncomeTaxes) +
                             yandmedT1.IsNetIncome - ((yandmedT1.IsOperatingInterestExpense == null) ? 0 : yandmedT1.IsOperatingInterestExpense) -
                           ((yandmedT1.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT1.IsProvisionForIncomeTaxes) +
                             yandmedT2.IsNetIncome - ((yandmedT2.IsOperatingInterestExpense == null) ? 0 : yandmedT2.IsOperatingInterestExpense) -
                           ((yandmedT2.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT2.IsProvisionForIncomeTaxes) +
                             yandmedT3.IsNetIncome - ((yandmedT3.IsOperatingInterestExpense == null) ? 0 : yandmedT3.IsOperatingInterestExpense) -
                           ((yandmedT3.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT3.IsProvisionForIncomeTaxes);
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        ebitdattm = yandmed.IsNetIncome - ((yandmed.IsOperatingInterestExpense == null) ? 0 : yandmed.IsOperatingInterestExpense) -
                           ((yandmed.IsProvisionForIncomeTaxes == null) ? 0 : yandmed.IsProvisionForIncomeTaxes) -
                             ((yandmed.IsDepreciationAmortization == null) ? 0 : yandmed.IsDepreciationAmortization) +
                             yandmedT1.IsNetIncome - ((yandmedT1.IsOperatingInterestExpense == null) ? 0 : yandmedT1.IsOperatingInterestExpense) -
                           ((yandmedT1.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT1.IsProvisionForIncomeTaxes) -
                             ((yandmedT1.IsDepreciationAmortization == null) ? 0 : yandmedT1.IsDepreciationAmortization) +
                             yandmedT2.IsNetIncome - ((yandmedT2.IsOperatingInterestExpense == null) ? 0 : yandmedT2.IsOperatingInterestExpense) -
                           ((yandmedT2.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT2.IsProvisionForIncomeTaxes) -
                             ((yandmedT2.IsDepreciationAmortization == null) ? 0 : yandmedT2.IsDepreciationAmortization) +
                             yandmedT3.IsNetIncome - ((yandmedT3.IsOperatingInterestExpense == null) ? 0 : yandmedT3.IsOperatingInterestExpense) -
                           ((yandmedT3.IsProvisionForIncomeTaxes == null) ? 0 : yandmedT3.IsProvisionForIncomeTaxes) -
                             ((yandmedT3.IsDepreciationAmortization == null) ? 0 : yandmedT3.IsDepreciationAmortization);


                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        freecashflowttm = yandmed.CfsNetCashFromOperatingActivities - yandmed.CfsCapitalExpenditures
                                          + yandmedT1.CfsNetCashFromOperatingActivities -
                                          yandmedT1.CfsCapitalExpenditures
                                          + yandmedT2.CfsNetCashFromOperatingActivities -
                                          yandmedT2.CfsCapitalExpenditures
                                          + yandmedT3.CfsNetCashFromOperatingActivities -
                                          yandmedT3.CfsCapitalExpenditures;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        epsttm = yandmed.IsEpsDiluted
                                 + yandmedT1.IsEpsDiluted
                                 + yandmedT2.IsEpsDiluted
                                 + yandmedT3.IsEpsDiluted;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        incometaxesttm = yandmed.IsProvisionForIncomeTaxes
                                         + yandmedT1.IsProvisionForIncomeTaxes
                                         + yandmedT2.IsProvisionForIncomeTaxes
                                         + yandmedT3.IsProvisionForIncomeTaxes;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        pretaxincomettm = yandmed.IsPretaxIncome
                                          + yandmedT1.IsPretaxIncome
                                          + yandmedT2.IsPretaxIncome
                                          + yandmedT3.IsPretaxIncome;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        expensesttm = yandmed.IsRevenue - yandmed.IsNetIncome
                                      + yandmedT1.IsRevenue - yandmedT1.IsNetIncome
                                      + yandmedT2.IsRevenue - yandmedT2.IsNetIncome
                                      + yandmedT3.IsRevenue - yandmedT3.IsNetIncome;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        grossprofitttm = yandmed.IsGrossProfit
                                         + yandmedT1.IsGrossProfit
                                         + yandmedT2.IsGrossProfit
                                         + yandmedT3.IsGrossProfit;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        contopsepsttm = yandmed.IsIncomeBeforeDiscOperations / yandmed.IsAverageSharesBasicEps
                                        + yandmedT1.IsIncomeBeforeDiscOperations / yandmedT1.IsAverageSharesBasicEps
                                        + yandmedT2.IsIncomeBeforeDiscOperations / yandmedT2.IsAverageSharesBasicEps
                                        + yandmedT3.IsIncomeBeforeDiscOperations / yandmedT3.IsAverageSharesBasicEps;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        operatingprofitttm = yandmed.IsOperatingIncome
                                             + yandmedT1.IsOperatingIncome
                                             + yandmedT2.IsOperatingIncome
                                             + yandmedT3.IsOperatingIncome;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        bookvaluepersharettm = yandmed.BsShareholdersEquity1 / yandmed.BsCommonSharesOutstanding
                                               + yandmedT1.BsShareholdersEquity1 / yandmedT1.BsCommonSharesOutstanding
                                               + yandmedT2.BsShareholdersEquity1 / yandmedT2.BsCommonSharesOutstanding
                                               + yandmedT3.BsShareholdersEquity1 / yandmedT3.BsCommonSharesOutstanding;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        tangiblebookvaluepersharettm = (yandmed.BsShareholdersEquity1 - yandmed.BsGoodwillIntangibles) /
                                                       yandmed.BsCommonSharesOutstanding
                                                       +
                                                       (yandmedT1.BsShareholdersEquity1 -
                                                        yandmedT1.BsGoodwillIntangibles) /
                                                       yandmedT1.BsCommonSharesOutstanding
                                                       +
                                                       (yandmedT2.BsShareholdersEquity1 -
                                                        yandmedT2.BsGoodwillIntangibles) /
                                                       yandmedT2.BsCommonSharesOutstanding
                                                       +
                                                       (yandmedT3.BsShareholdersEquity1 -
                                                        yandmedT3.BsGoodwillIntangibles) /
                                                       yandmedT3.BsCommonSharesOutstanding;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        costrevenuettm = yandmed.IsCostOfRevenue
                                         + yandmedT1.IsCostOfRevenue
                                         + yandmedT2.IsCostOfRevenue
                                         + yandmedT3.IsCostOfRevenue;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        interestexpensesttm = yandmed.IsOperatingInterestExpense
                                              + yandmedT1.IsOperatingInterestExpense
                                              + yandmedT2.IsOperatingInterestExpense
                                              + yandmedT3.IsOperatingInterestExpense;

                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        debtttm = (yandmed.BsCurrentPortionOfLongTermDebt + yandmed.BsTotalLongTermDebt
                                   + yandmedT1.BsCurrentPortionOfLongTermDebt + yandmedT1.BsTotalLongTermDebt
                                   + yandmedT2.BsCurrentPortionOfLongTermDebt + yandmedT2.BsTotalLongTermDebt
                                   + yandmedT3.BsCurrentPortionOfLongTermDebt + yandmedT3.BsTotalLongTermDebt) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }






                }
                /*
                 * ttm arvutuste lopp
                 */

                /*
                 * 4q ago arvutuste algus
                 */
                double? eps4ago = null;
                double? retainedearnings4ago = null;
                double? revenue4ago = null;
                if (i >= 4)
                {
                    FinData yandmedT4 = finDatas[i - 4];
                    eps4ago = yandmedT4.IsEpsDiluted;
                    retainedearnings4ago = yandmedT4.BsRetainedEarnings;
                    revenue4ago = yandmedT4.IsRevenue;
                }
                /*
                 * 4q ago arvutuste lopp
                 */






                double? avgassets5 = yandmed.BsTotalAssets;
                double? avginventory5 = yandmed.BsInventory;
                double? avgreceivables5 = yandmed.BsReceivables;
                double? avgpayables5 = yandmed.BsAccountsPayable;

                double? avgequity5 = yandmed.BsShareholdersEquity1;
                double? avginvestedcapital5 = null;
                try
                {
                    avginvestedcapital5 = yandmed.BsShareholdersEquity1 + yandmed.BsTotalLongTermDebt +
                                          yandmed.BsCurrentPortionOfLongTermDebt;
                }
                catch (InvalidOperationException)
                {
                }
                if (i >= 4)
                {
                    FinData yandmedT1 = finDatas[i - 1];
                    FinData yandmedT2 = finDatas[i - 2];
                    FinData yandmedT3 = finDatas[i - 3];
                    FinData yandmedT4 = finDatas[i - 4];
                    try
                    {
                        avgassets5 = (yandmed.BsTotalAssets + yandmedT1.BsTotalAssets + yandmedT2.BsTotalAssets +
                                      yandmedT3.BsTotalAssets + yandmedT4.BsTotalAssets) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginventory5 = (yandmed.BsInventory + yandmedT1.BsInventory + yandmedT2.BsInventory +
                                         yandmedT3.BsInventory + yandmedT4.BsInventory) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgreceivables5 = (yandmed.BsReceivables + yandmedT1.BsReceivables + yandmedT2.BsReceivables +
                                           yandmedT3.BsReceivables + yandmedT4.BsReceivables) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgpayables5 = (yandmed.BsAccountsPayable + yandmedT1.BsAccountsPayable +
                                        yandmedT2.BsAccountsPayable + yandmedT3.BsAccountsPayable +
                                        yandmedT4.BsAccountsPayable) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {

                        avgequity5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                      yandmedT2.BsShareholdersEquity1 + yandmedT3.BsShareholdersEquity1 +
                                      yandmedT4.BsShareholdersEquity1) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginvestedcapital5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                               yandmedT2.BsShareholdersEquity1 + yandmedT3.BsShareholdersEquity1 +
                                               yandmedT4.BsShareholdersEquity1
                                               + yandmed.BsTotalLongTermDebt + yandmedT1.BsTotalLongTermDebt +
                                               yandmedT2.BsTotalLongTermDebt + yandmedT3.BsTotalLongTermDebt +
                                               yandmedT4.BsTotalLongTermDebt
                                               + yandmed.BsCurrentPortionOfLongTermDebt +
                                               yandmedT1.BsCurrentPortionOfLongTermDebt +
                                               yandmedT2.BsCurrentPortionOfLongTermDebt +
                                               yandmedT3.BsCurrentPortionOfLongTermDebt +
                                               yandmedT4.BsCurrentPortionOfLongTermDebt) / 5;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                }
                else if (i >= 3)
                {
                    FinData yandmedT1 = finDatas[i - 1];
                    FinData yandmedT2 = finDatas[i - 2];
                    FinData yandmedT3 = finDatas[i - 3];

                    try
                    {
                        avgassets5 = (yandmed.BsTotalAssets + yandmedT1.BsTotalAssets + yandmedT2.BsTotalAssets +
                                      yandmedT3.BsTotalAssets) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {

                        avginventory5 = (yandmed.BsInventory + yandmedT1.BsInventory + yandmedT2.BsInventory +
                                         yandmedT3.BsInventory) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgreceivables5 = (yandmed.BsReceivables + yandmedT1.BsReceivables + yandmedT2.BsReceivables +
                                           yandmedT3.BsReceivables) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgpayables5 = (yandmed.BsAccountsPayable + yandmedT1.BsAccountsPayable +
                                        yandmedT2.BsAccountsPayable + yandmedT3.BsAccountsPayable) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {

                        avgequity5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                      yandmedT2.BsShareholdersEquity1 + yandmedT3.BsShareholdersEquity1) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginvestedcapital5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                               yandmedT2.BsShareholdersEquity1 + yandmedT3.BsShareholdersEquity1
                                               + yandmed.BsTotalLongTermDebt + yandmedT1.BsTotalLongTermDebt +
                                               yandmedT2.BsTotalLongTermDebt + yandmedT3.BsTotalLongTermDebt
                                               + yandmed.BsCurrentPortionOfLongTermDebt +
                                               yandmedT1.BsCurrentPortionOfLongTermDebt +
                                               yandmedT2.BsCurrentPortionOfLongTermDebt +
                                               yandmedT3.BsCurrentPortionOfLongTermDebt) / 4;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                }
                else if (i >= 2)
                {
                    FinData yandmedT1 = finDatas[i - 1];
                    FinData yandmedT2 = finDatas[i - 2];

                    try
                    {
                        avgassets5 = (yandmed.BsTotalAssets + yandmedT1.BsTotalAssets + yandmedT2.BsTotalAssets) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {

                        avginventory5 = (yandmed.BsInventory + yandmedT1.BsInventory + yandmedT2.BsInventory) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgreceivables5 = (yandmed.BsReceivables + yandmedT1.BsReceivables + yandmedT2.BsReceivables) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgpayables5 = (yandmed.BsAccountsPayable + yandmedT1.BsAccountsPayable +
                                        yandmedT2.BsAccountsPayable) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {

                        avgequity5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                      yandmedT2.BsShareholdersEquity1) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginvestedcapital5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1 +
                                               yandmedT2.BsShareholdersEquity1
                                               + yandmed.BsTotalLongTermDebt + yandmedT1.BsTotalLongTermDebt +
                                               yandmedT2.BsTotalLongTermDebt
                                               + yandmed.BsCurrentPortionOfLongTermDebt +
                                               yandmedT1.BsCurrentPortionOfLongTermDebt +
                                               yandmedT2.BsCurrentPortionOfLongTermDebt) / 3;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                }
                else if (i >= 1)
                {
                    FinData yandmedT1 = finDatas[i - 1];

                    try
                    {
                        avgassets5 = (yandmed.BsTotalAssets + yandmedT1.BsTotalAssets) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginventory5 = (yandmed.BsInventory + yandmedT1.BsInventory) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgreceivables5 = (yandmed.BsReceivables + yandmedT1.BsReceivables) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgpayables5 = (yandmed.BsAccountsPayable + yandmedT1.BsAccountsPayable) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgequity5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avginvestedcapital5 = (yandmed.BsShareholdersEquity1 + yandmedT1.BsShareholdersEquity1
                                               + yandmed.BsTotalLongTermDebt + yandmedT1.BsTotalLongTermDebt
                                               + yandmed.BsCurrentPortionOfLongTermDebt +
                                               yandmedT1.BsCurrentPortionOfLongTermDebt) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                }

                double? avginventory2 = yandmed.BsInventory;
                double? avgreceivables2 = yandmed.BsReceivables;
                double? avgpayables2 = yandmed.BsAccountsPayable;
                double? enterprisevalue = null;
                try
                {
                    marketcap = price * yandmed.BsCommonSharesOutstanding;
                }
                catch (InvalidOperationException)
                {
                }
                if (i >= 1)
                {


                    FinData yandmedT1 = finDatas[i - 1];

                    try
                    {
                        avginventory2 = (yandmed.BsInventory + yandmedT1.BsInventory) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    try
                    {
                        avgreceivables2 = (yandmed.BsReceivables + yandmedT1.BsReceivables) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }
                    try
                    {
                        avgpayables2 = (yandmed.BsAccountsPayable + yandmedT1.BsAccountsPayable) / 2;
                    }
                    catch (InvalidOperationException)
                    {
                    }

                }





                double? ebit = null;
                double? ebitda = null;

                double? freecashflow = null;
                double? interestprc = null;



                try
                {
                    ebit = yandmed.IsNetIncome - ((yandmed.IsOperatingInterestExpense == null) ? 0 : yandmed.IsOperatingInterestExpense) -
                           ((yandmed.IsProvisionForIncomeTaxes == null) ? 0 : yandmed.IsProvisionForIncomeTaxes);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    ebitda = yandmed.IsNetIncome - ((yandmed.IsOperatingInterestExpense == null) ? 0 : yandmed.IsOperatingInterestExpense) -
                           ((yandmed.IsProvisionForIncomeTaxes == null) ? 0 : yandmed.IsProvisionForIncomeTaxes) -
                             ((yandmed.IsDepreciationAmortization == null) ? 0 : yandmed.IsDepreciationAmortization);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {

                    enterprisevalue = marketcap +
                        ((yandmed.BsCurrentPortionOfLongTermDebt == null) ? 0 : yandmed.BsCurrentPortionOfLongTermDebt) +
                        ((yandmed.BsTotalLongTermDebt == null) ? 0 : yandmed.BsTotalLongTermDebt) +
                        ((yandmed.BsPreferredStock == null) ? 0 : yandmed.BsPreferredStock) +
                        ((yandmed.BsMinorityInterest == null) ? 0 : yandmed.BsMinorityInterest);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    freecashflow = yandmed.CfsNetCashFromOperatingActivities - yandmed.CfsCapitalExpenditures;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    interestprc = interestexpensesttm / debtttm;
                }
                catch (InvalidOperationException)
                {
                }



                /*
                 * helper arvude lopp, alati jätta sisse see osa suhtarvude
                 * arvutamiseks:
                 */





                try
                {
                    yandmed.FrAccruals = yandmed.IsNetIncome - yandmed.CfsNetCashFromOperatingActivities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrAltmanZScore = 1.2 * (workingcapital / yandmed.BsTotalAssets)
                                             + 1.4 * (yandmed.BsRetainedEarnings / yandmed.BsTotalAssets)
                                             + 3.3 * (ebit / yandmed.BsTotalAssets)
                                             + 0.6 * ((marketcap + yandmed.BsPreferredStock) / yandmed.BsTotalLiabilities)
                                             + 1.0 * (yandmed.IsRevenue / yandmed.BsTotalAssets);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrAssetUtilization = revenuettm / avgassets5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrBookValue = yandmed.BsTotalAssets - yandmed.BsGoodwillIntangibles -
                                          yandmed.BsTotalCurrentLiabilities - yandmed.BsTotalLongTermDebt;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrBookValuePerShare = yandmed.BsShareholdersEquity1 / yandmed.BsCommonSharesOutstanding;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCapitalExpenditures = yandmed.CfsCapitalExpenditures;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashConversionCycle = 91.5 *
                                                    ((avginventory2 / yandmed.IsCostOfRevenue) +
                                                     (avgreceivables2 / yandmed.IsRevenue) -
                                                     (avgpayables2 / yandmed.IsCostOfRevenue));
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashDivPayoutRatioTtm = dividendspaidttm / netincomettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashFinancing = yandmed.CfsNetCashFromFinancingActivities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashFinancingTtm = cashflowfinancingttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashInvesting = yandmed.CfsNetCashFromInvestingActivities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashInvestingTtm = cashflowinvestingttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashOperations = yandmed.CfsNetCashFromOperatingActivities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashOperationsTtm = cashflowoperationsttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashAndStInvestments = yandmed.BsCashShortTermInvestments;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrInterestExpense = yandmed.IsOperatingInterestExpense;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrInventories = yandmed.BsInventory;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRDExpense = yandmed.IsRdExpense;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRetainedEarnings = yandmed.BsRetainedEarnings;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRevenues = yandmed.IsRevenue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrShareholdersEquity = yandmed.BsShareholdersEquity1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrSharesOutstanding = yandmed.BsCommonSharesOutstanding;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrSgAExpense = yandmed.IsSellingGeneralAdminExpense;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrTotalAssets = yandmed.BsTotalAssets;
                }
                catch (InvalidOperationException)
                {
                }




                try
                {
                    yandmed.FrCurrentRatio = yandmed.BsTotalCurrentAssets / yandmed.BsTotalCurrentLiabilities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDaysInventoryOutstanding = 91.5 * (avginventory2 / yandmed.IsCostOfRevenue);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDaysPayableOutstanding = 91.5 * (avgpayables2 / yandmed.IsCostOfRevenue);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDaysSalesOutstanding = 91.5 * (avgreceivables2 / yandmed.IsRevenue);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDebtToEquityRatio = (yandmed.BsCurrentPortionOfLongTermDebt + yandmed.BsTotalLongTermDebt) /
                                                  yandmed.BsShareholdersEquity1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDividend = yandmed.CfsDividendsPaid / yandmed.BsCommonSharesOutstanding;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDividendYield = (dividendttm / sharesttm) / price;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEbitdaMarginTtm = ebitdattm / revenuettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEbitdaTtm = ebitdattm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEvEbit = enterprisevalue / ebitttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEvEbitda = enterprisevalue / ebitdattm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEvFreeCashFlow = enterprisevalue / freecashflowttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEvRevenues = enterprisevalue / revenuettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEarningsPerShare = yandmed.IsEpsDiluted;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEarningsPerShareGrowth = (yandmed.IsEpsDiluted / eps4ago) - 1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEarningsPerShareTtm = epsttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEarningsYield = epsttm / price;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEffectiveTaxRateTtm = (yandmed.IsPretaxIncome < 0) ? null : incometaxesttm / pretaxincomettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEnterpriseValue = enterprisevalue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrExpenses = yandmed.IsRevenue - yandmed.IsNetIncome;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrExpensesTtm = expensesttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrFreeCashFlow = freecashflow;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrFreeCashFlowTtm = freecashflowttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrFreeCashFlowYield = freecashflowttm / marketcap;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrGrossProfitMargin = yandmed.IsGrossProfit / yandmed.IsRevenue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrGrossProfitTtm = grossprofitttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrIncomeFromContOps = yandmed.IsIncomeBeforeDiscOperations;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrInventoryTurnover = costrevenuettm / avginventory5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrLiabilities = yandmed.BsTotalLiabilities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrLongTermDebt = yandmed.BsTotalLongTermDebt + yandmed.BsCurrentPortionOfLongTermDebt;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrMarketCap = marketcap;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrNetIncome = yandmed.IsNetIncome;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrNetIncomeTtm = netincomettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrNetPpE = yandmed.BsNetPropertyPlantEquipment;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrOperatingEarningsYield = contopsepsttm / price;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrOperatingMargin = yandmed.IsOperatingIncome / yandmed.IsRevenue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrOperatingMarginTtm = operatingprofitttm / revenuettm;
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    yandmed.FrPeRatio = price / epsttm;
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    yandmed.FrPegRatio = (price / epsttm) / ((yandmed.IsEpsDiluted / eps4ago) - 1);
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    yandmed.FrPayoutRatioTtm = dividendttm / netincomettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrPlowbackRatio = 1 - (dividendttm / netincomettm);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrPrice = price;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrPriceBookValue = price / bookvaluepersharettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrPriceSalesRatio = price / revenuettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrPriceTangibleBookValue = price / tangiblebookvaluepersharettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrProfitMargin = yandmed.IsNetIncome / yandmed.IsRevenue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrReceivablesTurnover = revenuettm / avgreceivables5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRetainedEarningsGrowth = (yandmed.BsRetainedEarnings / retainedearnings4ago) - 1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrReturnOnAssets = netincomettm / avgassets5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrReturnOnEquity = netincomettm / avgequity5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrReturnOnInvestedCapital = netincomettm / avginvestedcapital5;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRevenueGrowth = (yandmed.IsRevenue / revenue4ago) - 1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRevenuePerShareTtm = revenuettm / sharesttm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRevenuesTtm = revenuettm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrStockBuybacks = yandmed.CfsEquityIssued * -1;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrTangibleBookValue = yandmed.BsShareholdersEquity1 - yandmed.BsGoodwillIntangibles;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrTangibleBookValuePerShare = (yandmed.BsShareholdersEquity1 - yandmed.BsGoodwillIntangibles) /
                                                          yandmed.BsCommonSharesOutstanding;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrTangibleCommonEquityRatio = (yandmed.BsShareholdersEquity1 - yandmed.BsGoodwillIntangibles -
                                                           yandmed.BsPreferredStock)
                                                          / (yandmed.BsTotalAssets - yandmed.BsGoodwillIntangibles);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrTimesInterestEarnedTtm = ebitttm / interestexpensesttm;
                }
                catch (InvalidOperationException)
                {
                }


                try
                {
                    yandmed.FrWorkingCapital = workingcapital;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrRoicGrowthRate = (netincomettm / avginvestedcapital5) * (1 - (dividendttm / netincomettm));
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrQuickRatio = (yandmed.BsTotalCurrentAssets - yandmed.BsInventory) /
                                           yandmed.BsTotalCurrentLiabilities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrAssetCoverage = ((yandmed.BsTotalAssets - yandmed.BsGoodwillIntangibles)
                                               -
                                               (yandmed.BsTotalCurrentLiabilities -
                                                yandmed.BsCurrentPortionOfLongTermDebt))
                                              / (yandmed.BsCurrentPortionOfLongTermDebt + yandmed.BsTotalLongTermDebt);
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDscr = (ebitdattm - incometaxesttm) /
                                     (interestprc * (yandmed.BsCurrentPortionOfLongTermDebt + yandmed.BsTotalLongTermDebt))
                                     / (1 - Math.Pow((1 + interestprc.Value), -10));
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDebtEbitda = (yandmed.BsCurrentPortionOfLongTermDebt + yandmed.BsTotalLongTermDebt) /
                                           ebitdattm;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEqPrc = yandmed.BsShareholdersEquity1 / yandmed.BsTotalAssets;
                }
                catch (InvalidOperationException)
                {
                }

                try
                {
                    yandmed.FrBookToMarket = yandmed.BsShareholdersEquity1 / marketcap;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrEarningsToPriceRatio = operatingprofitttm / marketcap;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashFlowToPriceRatio = yandmed.CfsNetCashFromOperatingActivities / marketcap;
                }
                catch (InvalidOperationException)
                {
                }


                try
                {
                    yandmed.FrEbitPrc = ebit / yandmed.IsRevenue;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCashPrc = yandmed.BsCashShortTermInvestments / yandmed.BsTotalAssets;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCurrentPrc = yandmed.BsTotalCurrentAssets / yandmed.BsTotalAssets;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrGoodwillPrc = yandmed.BsGoodwillIntangibles / yandmed.BsTotalAssets;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrCurrentLPrc = yandmed.BsTotalCurrentLiabilities / yandmed.BsTotalLiabilities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrDebtPrc = yandmed.BsTotalLongTermDebt / yandmed.BsTotalLiabilities;
                }
                catch (InvalidOperationException)
                {
                }
                try
                {
                    yandmed.FrLiabilitiesPrc = yandmed.BsTotalLongTermLiabilities / yandmed.BsTotalLiabilities;
                }
                catch (InvalidOperationException)
                {
                }


                yandmed.FrNetIncomeTtm = netincomettm;
                yandmed.FrDividendsPaidTtm = dividendspaidttm;
                yandmed.FrDividendTtm = dividendttm;
                yandmed.FrSharesTtm = sharesttm;
                yandmed.FrEbitTtm = ebitttm;
                yandmed.FrIncomeTaxesTtm = incometaxesttm;
                yandmed.FrPreTaxIncomeTtm = pretaxincomettm;
                yandmed.FrContOpsEpsTtm = contopsepsttm;
                yandmed.FrOperatingProfitTtm = operatingprofitttm;
                yandmed.FrBookValuePerShareTtm = bookvaluepersharettm;
                yandmed.FrTangibleBookValuePerShareTtm = tangiblebookvaluepersharettm;
                yandmed.FrCostRevenueTtm = costrevenuettm;
                yandmed.FrInterestExpensesTtm = interestexpensesttm;
                yandmed.FrDebtTtm = debtttm;
                yandmed.FrEbit = ebit;
                yandmed.FrEbitda = ebitda;
                yandmed.FrInterestPrc = interestprc;
                yandmed.FrAvgPayables2 = avgpayables2;
                yandmed.FrAvgReceivables2 = avgreceivables2;
                yandmed.FrAvgInventory2 = avginventory2;
                yandmed.FrAvgAssets5 = avgassets5;
                yandmed.FrAvgInventory5 = avginventory5;
                yandmed.FrAvgReceivables5 = avgreceivables5;
                yandmed.FrAvgPayables5 = avgpayables5;
                yandmed.FrAvgEquity5 = avgequity5;
                yandmed.FrAvgInvestedCapital5 = avginvestedcapital5;
                yandmed.FrEps4Ago = eps4ago;
                yandmed.FrRetainedEarnings4Ago = retainedearnings4ago;
                yandmed.FrRevenue4Ago = revenue4ago;
            }


        }
    }
}
