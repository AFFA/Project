using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AFFA.Mudelid;
using Meta.Numerics.Statistics;

namespace AFFA.DCFMudelid
{
    public static class DcfInputCalculator
    {
        public static void CalculateInput(FinDataAdapter finDataAdapter, DcfInput dcfInput)
        {
            CalculateCorrelation(finDataAdapter,  dcfInput);
            int requiredPeriodsForMean = 1;
            List<FinData> finDatas = finDataAdapter.FinDataDao.FinDatas;
            int k = 0;
            Sample sampleRevenue = new Sample();
            Sample sampleTax = new Sample();
            Sample sampleInterest = new Sample();

            Sample sampleTotalAssets = new Sample();
            Sample sampleTotalLiabilities = new Sample();
            Sample sampleTotalCurrentAssets = new Sample();
            Sample sampleTotalCurrentLiabilities = new Sample();
            Sample sampleAllCosts = new Sample();
            Sample sampleEbitda = new Sample();
            Sample sampleDepreciation = new Sample();
            Sample sampleEbit = new Sample();

            for (int i = finDatas.Count - 1; i >= 4; i--)
            {
                if (k < 16)
                {
                    if (k == 0)
                    {
                        dcfInput.SharesOutstanding = (double) finDatas[i].BsCommonSharesOutstanding;
                    }
                    try
                    {
                        double RevenueGrowth = (double)(finDatas[i].IsRevenue / finDatas[i - 4].IsRevenue) - 1;
                        sampleRevenue.Add(RevenueGrowth);
                    }
                    catch (InvalidOperationException) { }

                    try
                    {
                        double taxRate = (double)((finDatas[i].IsPretaxIncome - finDatas[i].IsIncomeAfterTax) / finDatas[i].IsPretaxIncome);
                        sampleTax.Add(taxRate);
                    }
                    catch (InvalidOperationException) { }

                    double debt = 0;
                    if (finDatas[i].BsCurrentPortionOfLongTermDebt != null)
                    {
                        debt += (double)finDatas[i].BsCurrentPortionOfLongTermDebt;
                    }
                    if (finDatas[i].BsTotalLongTermDebt != null)
                    {
                        debt += (double)finDatas[i].BsTotalLongTermDebt;
                    }
                    if (finDatas[i].FrInterestExpense != null && debt != 0.0)
                    {
                        double interest = (double)(finDatas[i].FrInterestExpense) / debt;
                        if (interest < 0.2)
                        {
                            sampleInterest.Add(interest);
                        }
                    }

                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].BsTotalAssets != null)
                    {
                        double temp = (double)(finDatas[i].BsTotalAssets / finDatas[i].IsRevenue);
                        if (temp > 0.5 && temp < 50)
                        {
                            sampleTotalAssets.Add(temp);
                        }
                    }
                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].BsTotalLiabilities != null)
                    {
                        double temp = (double)(finDatas[i].BsTotalLiabilities / finDatas[i].IsRevenue);
                        if (temp > 0.5 && temp < 50)
                        {
                            sampleTotalLiabilities.Add(temp);
                        }
                    }

                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].BsTotalCurrentAssets != null)
                    {
                        double temp = (double)(finDatas[i].BsTotalCurrentAssets / finDatas[i].IsRevenue);
                        if (temp > 0.5 && temp < 50)
                        {
                            sampleTotalCurrentAssets.Add(temp);
                        }
                    }
                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].BsTotalCurrentLiabilities != null)
                    {
                        double temp = (double)(finDatas[i].BsTotalCurrentLiabilities / finDatas[i].IsRevenue);
                        if (temp > 0.5 && temp < 50)
                        {
                            sampleTotalCurrentLiabilities.Add(temp);
                        }
                    }

                    double allCosts = 0.0;
                    if (finDatas[i].IsTotalOperatingExpenses != null)
                    {
                        allCosts += (double)finDatas[i].IsTotalOperatingExpenses;
                    }
                    if (finDatas[i].IsDepreciationAmortization != null)
                    {
                        allCosts -= (double)finDatas[i].IsDepreciationAmortization;
                    }
                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && allCosts != 0.0)
                    {
                        double temp = (double)(allCosts / finDatas[i].IsRevenue);
                        if (temp > 0.1 && temp < 1)
                        {
                            sampleAllCosts.Add(temp);
                        }
                    }


                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].FrEbitda != null)
                    {
                        double temp = (double)(finDatas[i].FrEbitda / finDatas[i].IsRevenue);
                        if (temp > -1.0 && temp < 1.0)
                        {
                            sampleEbitda.Add(temp);
                        }
                    }


                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].IsDepreciationAmortization != null)
                    {
                        double temp = (double)(finDatas[i].IsDepreciationAmortization / finDatas[i].IsRevenue);
                        if (temp > -1.0 && temp < 1.0)
                        {
                            sampleDepreciation.Add(temp);
                        }
                    }
                    if (finDatas[i].IsRevenue != null && finDatas[i].IsRevenue != 0.0 && finDatas[i].FrEbit != null)
                    {
                        double temp = (double)(finDatas[i].FrEbit / finDatas[i].IsRevenue);
                        if (temp > -1.0 && temp < 1.0)
                        {
                            sampleEbit.Add(temp);
                        }
                    }



                }


                k++;
            }

            if (sampleRevenue.Count >= requiredPeriodsForMean)
            {
                dcfInput.GrowthRatePrognosis = sampleRevenue.Mean;
            }
            if (sampleTax.Count >= requiredPeriodsForMean)
            {
                dcfInput.TaxRate = sampleTax.Mean;
            }
            if (sampleInterest.Count >= requiredPeriodsForMean)
            {
                dcfInput.CostOfDebt = sampleInterest.Mean;
            }

            if (sampleTotalAssets.Count >= requiredPeriodsForMean)
            {
                dcfInput.TotalAssetsPrcRevenue = sampleTotalAssets.Mean;
            }
            if (sampleTotalLiabilities.Count >= requiredPeriodsForMean)
            {
                dcfInput.TotalLiabilitiesPrcRevenue = sampleTotalLiabilities.Mean;
            }
            if (sampleTotalCurrentAssets.Count >= requiredPeriodsForMean)
            {
                dcfInput.TotalCurrentAssetsPrcRevenue = sampleTotalCurrentAssets.Mean;
            }
            if (sampleTotalCurrentLiabilities.Count >= requiredPeriodsForMean)
            {
                dcfInput.TotalCurrentLiabilitiesPrcRevenue = sampleTotalCurrentLiabilities.Mean;
            }
            if (sampleAllCosts.Count >= requiredPeriodsForMean)
            {
                dcfInput.AllCostsPrcRevenue = sampleAllCosts.Mean;
            }
            if (sampleEbitda.Count >= requiredPeriodsForMean)
            {
                dcfInput.EbitdaPrcRevenue = sampleEbitda.Mean;
            }
            if (sampleDepreciation.Count >= requiredPeriodsForMean)
            {
                dcfInput.DepreciationPrcRevenue = sampleDepreciation.Mean;
            }
            if (sampleEbit.Count >= requiredPeriodsForMean)
            {
                dcfInput.EbitPrcRevenue = sampleEbit.Mean;
            }


        }

        public static void CalculateBeta(FinDataAdapter finDataAdapter, DcfInput dcfInput)
        {
            BivariateSample bivariate = new BivariateSample();
            MultivariateSample mv = new MultivariateSample(2);
            decimal prevPrice = 0;
            double? prevIndex = null;
            decimal curPrice = 0;
            double? curIndex = null;
            int k = 0;
            for (int i = 0; i < finDataAdapter.PriceDataDao.PriceDatas.Count; i = i + 22)
            {
                if (k < 36)
                {
                    PriceData pd = finDataAdapter.PriceDataDao.PriceDatas[i];
                    curPrice = pd.AdjClose;
                    curIndex = finDataAdapter.PriceDataDao.GetClosePrice(pd.PriceDate, finDataAdapter.PriceDataDao.IndexDatas)[0];
                    if (curPrice != 0 && curIndex != null && prevPrice != 0 && prevIndex != null)
                    {
                        //MessageBox.Show("s:" + ((double)(prevPrice / curPrice) - 1));
                        //MessageBox.Show("i:" + ((double)(prevIndex / curIndex) - 1));
                        ////bivariate.Add((double) (prevPrice/curPrice)-1,(double) (prevIndex/curIndex)-1);
                        double[] db = new double[2];
                        db[0] = ((double)(prevPrice / curPrice) - 1);
                        db[1] = ((double)(prevIndex / curIndex) - 1);
                        mv.Add(db);
                    }
                    prevPrice = curPrice;
                    prevIndex = curIndex;

                    //DateTime dt = finDataAdapter.PriceDataDao.PriceDatas[i].PriceDate;

                    //MessageBox.Show(finDataAdapter.PriceDataDao.PriceDatas[i].AdjClose + " " +
                    //                dt.ToShortDateString());
                    //MessageBox.Show(finDataAdapter.PriceDataDao.GetClosePrice(dt, finDataAdapter.PriceDataDao.IndexDatas)[0].ToString());
                }
                k++;
            }

            if (mv.Count > 10)
            {
                //FitResult fitResult = bivariate.LinearRegression();
                FitResult fitResult = mv.LinearRegression(0);
                dcfInput.Beta = fitResult.Parameter(1).Value;
                List<FinData> finDatas = finDataAdapter.FinDataDao.FinDatas;

                dcfInput.CostOfEquity = dcfInput.RiskFreeRate + dcfInput.Beta * dcfInput.MarketRiskPremium;
                double debt = 0;
                if (finDatas[finDatas.Count - 1].BsCurrentPortionOfLongTermDebt != null)
                {
                    debt += (double)finDatas[finDatas.Count - 1].BsCurrentPortionOfLongTermDebt;
                }
                if (finDatas[finDatas.Count - 1].BsTotalLongTermDebt != null)
                {
                    debt += (double)finDatas[finDatas.Count - 1].BsTotalLongTermDebt;
                }
                double total = 0.0;
                if (finDatas[finDatas.Count - 1].BsShareholdersEquity1 != null)
                {
                    total+=(double) (finDatas[finDatas.Count - 1].BsShareholdersEquity1);
                }
                total+= debt;

                try
                {
                    dcfInput.Wacc = dcfInput.CostOfEquity *
                                    (double)(finDatas[finDatas.Count - 1].BsShareholdersEquity1 / total) +
                                    dcfInput.CostOfDebt * (double) (debt / total) * (1 - dcfInput.TaxRate);
                }
                catch (InvalidOperationException) { }
                //MessageBox.Show("beta: "+fitResult.Parameter(1).Value.ToString());
                //double[] pars = fitResult.Parameters();
                //foreach (var par in pars)
                //{
                //    MessageBox.Show(par.ToString());

                //}

                //MessageBox.Show(fitResult.CorrelationCoefficient(0,1).ToString());
                //double[] gfit = fitResult.
                //MessageBox.Show(fitResult.);

                //MessageBox.Show(fitResult.Parameter(2).ToString());
            }

        }


        public static void CalculateCorrelation(FinDataAdapter finDataAdapter, DcfInput dcfInput)
        {
            MultivariateSample mvTA = new MultivariateSample(2);
            MultivariateSample mvTL = new MultivariateSample(2);
            MultivariateSample mvCA = new MultivariateSample(2);
            MultivariateSample mvCL = new MultivariateSample(2);
            int k = 0;
            List<FinData> finDatas=finDataAdapter.FinDataDao.FinDatas;
            for (int i = finDataAdapter.FinDataDao.FinDatas.Count-1; i >=0; i --)
            {
                if (k < 20)
                {
                    double revChange = 0;
                    try
                    {
                        //revChange = (double) (finDatas[i].IsRevenue/finDatas[i - 5].IsRevenue);
                        revChange = (double)finDatas[i].IsRevenue;
                    } catch(InvalidOperationException){}
                    double caChange = 0;
                    try
                    {
                        //secChange = (double)(finDatas[i].BsTotalCurrentAssets / finDatas[i - 5].BsTotalCurrentAssets);
                        caChange = (double) finDatas[i].BsTotalCurrentAssets;
                    }
                    catch (InvalidOperationException) { }

                    double clChange = 0;
                    try
                    {
                        //secChange = (double)(finDatas[i].BsTotalCurrentAssets / finDatas[i - 5].BsTotalCurrentAssets);
                        clChange = (double)finDatas[i].BsTotalCurrentLiabilities;
                    }
                    catch (InvalidOperationException) { }
                    double taChange = 0;
                    try
                    {
                        //secChange = (double)(finDatas[i].BsTotalCurrentAssets / finDatas[i - 5].BsTotalCurrentAssets);
                        taChange = (double)finDatas[i].BsTotalAssets;
                    }
                    catch (InvalidOperationException) { }
                    double tlChange = 0;
                    try
                    {
                        //secChange = (double)(finDatas[i].BsTotalCurrentAssets / finDatas[i - 5].BsTotalCurrentAssets);
                        tlChange = (double)finDatas[i].BsTotalLiabilities;
                    }
                    catch (InvalidOperationException) { }
                    


                    
                        //MessageBox.Show("s:" + ((double)(prevPrice / curPrice) - 1));
                        //MessageBox.Show("i:" + ((double)(prevIndex / curIndex) - 1));
                        ////bivariate.Add((double) (prevPrice/curPrice)-1,(double) (prevIndex/curIndex)-1);
                        double[] db = new double[2];
                        db[0] = caChange;
                        db[1] = revChange;
                        mvCA.Add(db);

                        db = new double[2];
                        db[0] = clChange;
                        db[1] = revChange;
                        mvCL.Add(db);

                        db = new double[2];
                        db[0] = taChange;
                        db[1] = revChange;
                        mvTA.Add(db);

                        db = new double[2];
                        db[0] = tlChange;
                        db[1] = revChange;
                        mvTL.Add(db);
                   

                    //DateTime dt = finDataAdapter.PriceDataDao.PriceDatas[i].PriceDate;

                    //MessageBox.Show(finDataAdapter.PriceDataDao.PriceDatas[i].AdjClose + " " +
                    //                dt.ToShortDateString());
                    //MessageBox.Show(finDataAdapter.PriceDataDao.GetClosePrice(dt, finDataAdapter.PriceDataDao.IndexDatas)[0].ToString());
                }
                k++;
            }

            if (mvCA.Count > 2)
            {
                //FitResult fitResult = bivariate.LinearRegression();
                FitResult fitResult = mvCA.LinearRegression(0);
                dcfInput.TotalCurrentAssetsBeta = fitResult.Parameter(1).Value;
                dcfInput.TotalCurrentAssetsAlpha = fitResult.Parameter(0).Value;

                fitResult = mvCL.LinearRegression(0);
                dcfInput.TotalCurrentLiabilitiesBeta = fitResult.Parameter(1).Value;
                dcfInput.TotalCurrentLiabilitiesAlpha = fitResult.Parameter(0).Value;

                fitResult = mvTA.LinearRegression(0);
                dcfInput.TotalAssetsBeta = fitResult.Parameter(1).Value;
                dcfInput.TotalAssetsAlpha = fitResult.Parameter(0).Value;

                fitResult = mvTL.LinearRegression(0);
                dcfInput.TotalLiabilitiesBeta = fitResult.Parameter(1).Value;
                dcfInput.TotalLiabilitiesAlpha = fitResult.Parameter(0).Value;




                //MessageBox.Show("alfa: " + fitResult.Parameter(0).Value.ToString());
                //MessageBox.Show("beta: "+fitResult.Parameter(1).Value.ToString());
                //double[] pars = fitResult.Parameters();
                //foreach (var par in pars)
                //{
                //    MessageBox.Show(par.ToString());

                //}

                //MessageBox.Show(fitResult.CorrelationCoefficient(0,1).ToString());
                //double[] gfit = fitResult.
                //MessageBox.Show(fitResult.);

                //MessageBox.Show(fitResult.Parameter(2).ToString());
            }

        }

    }
}
