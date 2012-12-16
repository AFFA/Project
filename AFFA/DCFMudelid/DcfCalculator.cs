using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.Mudelid;
using System.Windows.Forms;

namespace AFFA.DCFMudelid
{
    /// <summary>
    /// Static objekt, mis arvutab välja ettevõtte väärtuse hinnangu jaoks vajalikke näitajaid.
    /// </summary>
    public static class DcfCalculator
    {
        /// <summary>
        /// Genereerib algsed rahavoogude prognoosid. Osa nendest on ajaloolised, osa tulevikuprognoosid. Ajaloolistest andmetest
        /// saadakse otse DCFData objektid (CalculateQuaterlyForecasts meetodiga küll neid täiendatakse veel), tulevikuprognoosid tuleb eraldi täita.
        /// </summary>
        /// <param name="finDatas">Ajaloolised andmed</param>
        /// <param name="dcfDataDao">Genereeritavate andmete hoidja</param>

        public static void GenerateDcfData(List<FinData> finDatas, DcfDataDao dcfDataDao)
        {
            // meetodile antakse findata list ette
            // nüüd tuleb võtta findatast viimase nt 5 aasta andmed ehk 20 kvartalit
            // ja nende põhjal genereerida olemasolevate kvartalite DcfData objektid
            dcfDataDao.ClearDcfData();
            int min = 0;
            if (finDatas.Count - 26 > 0)
            {
                min = finDatas.Count - 26;
            }
            for (int i = min; i < finDatas.Count; i++)
            {
                // siin täidame findata andmetega loodavad DcfData objektid
                dcfDataDao.AddDcfData(new DcfData(finDatas[i]));


            }

            // nüüd oleks vaja luua ka tulevikuprognooside DcfData objektid
            // loome need näiteks veel 5 aasta kohta ehk 20 kvartalit

            //DateTime futureDate = new DateTime();

            //futureDate.AddMonths(3);
            for (int i = 0; i < 20; i++)
            {
                // kõigepealt tuleks genereerida vastavad kuupäevad järgneva 20 kvartali lõpu jaoks
                // ja siis luua tühjad DcfData objektid

                // futureDate = futureDate + 3 kuud
                DateTime futureDate = dcfDataDao.DcfDatas[dcfDataDao.DcfDatas.Count - 1].Kuupaev.AddMonths(3);
                dcfDataDao.AddDcfData(new DcfData(futureDate));

            }
        }

        /// <summary>
        /// Siin täidetakse juba loodud DCFData objekte täiendavate andmetega ajalooliste andmete puhul ja kõigi andmetega 
        /// tuleviku prognoosi objektide puhul.
        /// </summary>
        /// <param name="dcfDatas">DCFData list</param>
        /// <param name="dcfInput">Arvutuste tegemiseks sisendi objekt</param>
        public static void CalculateQuaterlyForecasts(List<DcfData> dcfDatas, DcfInput dcfInput)
        {

            for (int i = 0; i < dcfDatas.Count; i++)
            {

                // hakata välja arvutama DcfData välju, mis varem täidetud ei ole
                // olenevalt sellest, kas IsPrognosis on true või false, tuleb erinevalt arvutada
                // kui IsPrognosis=true, siis tuleb kasutada eelduste klassis DcfInput olevat infot, juba toimunud kvartalite puhul tavaliselt mitte 


                // tulevikuprognoosi spetsiifiline arvutus:
                if (dcfDatas[i].IsPrognosis)
                {


                    dcfDatas[i].Revenue = dcfDatas[i - 4].Revenue * (1 + dcfInput.GrowthRatePrognosis);
                    if (dcfInput.TotalAssetsAlpha != 0 && dcfInput.TotalAssetsBeta != 0 && dcfInput.LinearRegression)
                    {
                        dcfDatas[i].TotalCurrentAssets = dcfInput.TotalAssetsAlpha +
                                                         dcfInput.TotalAssetsBeta * dcfDatas[i].Revenue;
                    }
                    else
                    {
                        dcfDatas[i].TotalAssets = dcfDatas[i - 1].Revenue * dcfInput.TotalAssetsPrcRevenue;
                    }

                    if (dcfInput.TotalCurrentAssetsAlpha != 0 && dcfInput.TotalCurrentAssetsBeta != 0 && dcfInput.LinearRegression)
                    {
                        dcfDatas[i].TotalCurrentAssets = dcfInput.TotalCurrentAssetsAlpha +
                                                         dcfInput.TotalCurrentAssetsBeta * dcfDatas[i].Revenue;
                    }
                    else
                    {
                        dcfDatas[i].TotalCurrentAssets = dcfDatas[i - 1].Revenue * dcfInput.TotalCurrentAssetsPrcRevenue;
                    }

                    if (dcfInput.TotalLiabilitiesAlpha != 0 && dcfInput.TotalLiabilitiesBeta != 0 && dcfInput.LinearRegression)
                    {
                        dcfDatas[i].TotalCurrentAssets = dcfInput.TotalLiabilitiesAlpha +
                                                         dcfInput.TotalLiabilitiesBeta * dcfDatas[i].Revenue;
                    }
                    else
                    {
                        dcfDatas[i].TotalLiabilities = dcfDatas[i - 1].Revenue * dcfInput.TotalLiabilitiesPrcRevenue;
                    }
                    if (dcfInput.TotalCurrentLiabilitiesAlpha != 0 && dcfInput.TotalCurrentLiabilitiesBeta != 0 && dcfInput.LinearRegression)
                    {
                        dcfDatas[i].TotalCurrentAssets = dcfInput.TotalCurrentLiabilitiesAlpha +
                                                         dcfInput.TotalCurrentLiabilitiesBeta * dcfDatas[i].Revenue;
                    }
                    else
                    {
                        dcfDatas[i].TotalCurrentLiabilities = dcfDatas[i - 1].Revenue *
                                                              dcfInput.TotalCurrentLiabilitiesPrcRevenue;
                    }

                    dcfDatas[i].AllCosts = dcfDatas[i - 1].Revenue * dcfInput.AllCostsPrcRevenue;
                    dcfDatas[i].Depreciation = dcfDatas[i - 1].Revenue * dcfInput.DepreciationPrcRevenue;
                    dcfDatas[i].Ebit = dcfDatas[i - 1].Revenue * dcfInput.EbitPrcRevenue;
                    dcfDatas[i].Ebitda = dcfDatas[i - 1].Revenue * dcfInput.EbitdaPrcRevenue;

                }


                // koigil samasugune arvutus
                dcfDatas[i].NetWorkingCapital = dcfDatas[i].TotalCurrentAssets - dcfDatas[i].TotalCurrentLiabilities;
                dcfDatas[i].Ebiat = dcfDatas[i].Ebit * (1 - dcfInput.TaxRate); // ebit*(1-tax rate)
                dcfDatas[i].TaxRate = dcfInput.TaxRate;
                dcfDatas[i].AllCostsEbitda = dcfDatas[i].AllCosts - dcfDatas[i].Ebitda;


                if (i > 0)
                {

                    dcfDatas[i].NetWorkingCapitalChange = dcfDatas[i].NetWorkingCapital - dcfDatas[i - 1].NetWorkingCapital;
                    dcfDatas[i].TotalAssetsChange = dcfDatas[i].TotalAssets - dcfDatas[i - 1].TotalAssets;
                    dcfDatas[i].TotalLiabilitiesChange = dcfDatas[i].TotalLiabilities - dcfDatas[i - 1].TotalLiabilities;
                    dcfDatas[i].Capex = dcfDatas[i].TotalAssetsChange - dcfDatas[i].TotalLiabilitiesChange; // arvutatakse assets ja liabilities muutude vahena
                    dcfDatas[i].Capexdepreciation = dcfDatas[i].TotalAssetsChange - dcfDatas[i].TotalLiabilitiesChange - dcfDatas[i].Depreciation;
                    dcfDatas[i].Fcff = dcfDatas[i].Ebiat - dcfDatas[i].Capexdepreciation - dcfDatas[i].NetWorkingCapitalChange;
                    if (i >= 4)
                    {
                        try
                        {
                            dcfDatas[i].RevenueGrowth = dcfDatas[i].Revenue / dcfDatas[i - 4].Revenue - 1;
                        }
                        catch (InvalidOperationException) { }
                    }
                }

            }
        }


        /// <summary>
        /// Arvutab ettevõtte koguväärtuse
        /// </summary>
        /// <param name="finDataAdapter">Adapter annab ligipääsu kõigile kogutud andmetele.</param>
        public static void CalculateTerminal(FinDataAdapter finDataAdapter)
        {
            List<DcfData> dcfDatas = finDataAdapter.DcfDataDao.DcfDatas;
            DcfInput dcfInput = finDataAdapter.DcfInput;
            DcfOutput dcfOutput = finDataAdapter.DcfOutput;
            List<FinData> finDatas = finDataAdapter.FinDataDao.FinDatas;

            // algselt väärtus
            double presentValueOfFcff = 0.0;
            // jätkuväärtus
            double terminalFCFF = 0.0;
            int j = 1;
            int finalDiscountFactor = 1;

            // käime läbi DCFData tuleviku objektid ja diskonteerime sealt FCFF väärtused tänapäeva
            for (int i = 0; i < dcfDatas.Count; i++)
            {
                if (dcfDatas[i].IsPrognosis)
                {
                    finalDiscountFactor = i;
                    try
                    {
                        presentValueOfFcff += (double)dcfDatas[i].Fcff / Math.Pow(1 + dcfInput.Wacc / 4, j);
                        if (i > dcfDatas.Count - 4)
                        {
                            terminalFCFF += (double)dcfDatas[i].Fcff;
                        }

                    }
                    catch (InvalidOperationException) { }
                    j++;

                }
            }
            dcfOutput.PerpetuityGrowthRate = dcfInput.ContinuousGrowth;
            dcfOutput.Wacc = dcfInput.Wacc;
            dcfOutput.TerminalFreeCashFlow = terminalFCFF;
            // arvutame jätkuväärtuse
            dcfOutput.TerminalValue = dcfOutput.TerminalFreeCashFlow * dcfInput.ContinuousGrowth / (dcfInput.Wacc - dcfInput.ContinuousGrowth);


            // edasi toimuvad leitud väärtuste korrigeerimised võla, sularaha jms-ga
            dcfOutput.PresentValueOfFreeCashFlow = presentValueOfFcff;
            dcfOutput.PresentValueOfTerminalValue = dcfOutput.TerminalValue / Math.Pow(1 + dcfInput.Wacc, finalDiscountFactor / 4);
            dcfOutput.EnterpriseValueWithoutCash = dcfOutput.PresentValueOfTerminalValue + dcfOutput.PresentValueOfFreeCashFlow;
            dcfOutput.CashAndCashEquivalents = finDatas[finDatas.Count - 1].BsCashShortTermInvestments;
            dcfOutput.EnterpriseValue = dcfOutput.EnterpriseValueWithoutCash + dcfOutput.CashAndCashEquivalents;
            dcfOutput.LessTotalDebt = 0;
            if (finDatas[finDatas.Count - 1].BsCurrentPortionOfLongTermDebt != null)
            {
                dcfOutput.LessTotalDebt += finDatas[finDatas.Count - 1].BsCurrentPortionOfLongTermDebt;
            }
            if (finDatas[finDatas.Count - 1].BsTotalLongTermDebt != null)
            {
                dcfOutput.LessTotalDebt += finDatas[finDatas.Count - 1].BsTotalLongTermDebt;
            }


            dcfOutput.EquityValue = dcfOutput.EnterpriseValue - dcfOutput.LessTotalDebt;
            dcfOutput.OutstandingShares = dcfInput.SharesOutstanding;
            dcfOutput.CurrentSharePrice = finDatas[finDatas.Count - 1].FrPrice;
            dcfOutput.ModelSharePrice = dcfOutput.EquityValue / dcfOutput.OutstandingShares;
            // võrdleme mudelit leitud hinda reaalse turuhinnaga ja anname vastavalt sellele ostu/müügi soovituse
            double? priceDifference = dcfOutput.ModelSharePrice / dcfOutput.CurrentSharePrice - 1;
            if (priceDifference == null)
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.No_Data_To_Recommend;
            }
            else if (priceDifference > 0.4)
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.Strong_Buy;
            }
            else if (priceDifference > 0.2)
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.Buy;
            }
            else if (priceDifference < -0.4)
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.Strong_Sell;
            }
            else if (priceDifference < -0.2)
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.Sell;
            }
            else
            {
                dcfOutput.Recommendation = DcfOutput.Recommendations.Hold;
            }
        }
    }
}
