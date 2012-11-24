using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.Mudelid;
using System.Windows.Forms;

namespace AFFA.DCFMudelid
{
    public static class DcfCalculator
    {

        public static void GenerateDcfData(List<FinData> finDatas, DcfDataDao dcfDataDao)
        {
            // meetodile antakse findata list ette
            // nüüd tuleb võtta findatast viimase nt 5 aasta andmed ehk 20 kvartalit
            // ja nende põhjal genereerida olemasolevate kvartalite DcfData objektid

            for (int i = (finDatas.Count - 21); i < finDatas.Count; i++)
            {
                // siin täidame findata andmetega loodavad DcfData objektid
                //MessageBox.Show(finDatas[i].Kuupaev.ToString());
                dcfDataDao.AddDcfData(new DcfData(finDatas[i]));


            }

            // nüüd oleks vaja luua ka tulevikuprognooside DcfData objektid
            // loome need näiteks veel 5 aasta kohta ehk 20 kvartalit

            DateTime futureDate = new DateTime();
            futureDate = finDatas[finDatas.Count-1].Kuupaev;

            for (int i = 0; i < 5; i++)
            {
                // kõigepealt tuleks genereerida vastavad kuupäevad järgneva 20 kvartali lõpu jaoks
                // ja siis luua tühjad DcfData objektid
 
                // futureDate = futureDate + 3 kuud
                dcfDataDao.AddDcfData(new DcfData(futureDate)); 

            }
        }

        public static void Calculate(List<DcfData> dcfDatas, DcfInput dcfInput)
        {


            for (int i = 0; i < dcfDatas.Count; i++)
            {
                
                // hakata välja arvutama DcfData välju, mis varem täidetud ei ole
                // olenevalt sellest, kas IsPrognosis on true või false, tuleb erinevalt arvutada
                // kui IsPrognosis=true, siis tuleb kasutada eelduste klassis DcfInput olevat infot, juba toimunud kvartalite puhul tavaliselt mitte 
                // võimalik, et on vaja vahemuutujaid vms
                if (!dcfDatas[i].IsPrognosis)
                {

                    dcfDatas[i].NetWorkingCapital = dcfDatas[i].TotalCurrentAssets - dcfDatas[i].TotalCurrentLiabilities;                
                    dcfDatas[i].Ebiat = dcfDatas[i].Ebit *(1- dcfInput.TaxRate); // ebit*(1-tax rate)
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
                        dcfDatas[i].RevenueGrowth = dcfDatas[i].Revenue / dcfDatas[i-1].Revenue - 1;
                    
                    }
                }
                else 
                {
                    dcfDatas[i].TaxRate = dcfInput.TaxRate;
                    dcfDatas[i].Revenue = dcfDatas[i - 1].Revenue * (1+dcfInput.GrowthRatePrognosis);
                    dcfDatas[i].TotalAssets = dcfDatas[i-1].Revenue * dcfInput.TotalAssetsPrcRevenue;
                    dcfDatas[i].TotalCurrentAssets = dcfDatas[i - 1].Revenue * dcfInput.TotalCurrentAssetsPrcRevenue;
                    dcfDatas[i].TotalLiabilities = dcfDatas[i-1].Revenue * dcfInput.TotalLiabilitiesPrcRevenue;
                    dcfDatas[i].TotalCurrentLiabilities = dcfDatas[i - 1].Revenue * dcfInput.TotalCurrentLiabilitiesPrcRevenue;                   
                    dcfDatas[i].NetWorkingCapital = dcfDatas[i].TotalCurrentAssets - dcfDatas[i].TotalCurrentLiabilities;
                    dcfDatas[i].AllCosts = dcfDatas[i - 1].Revenue * dcfInput.AllCostsPrcRevenue;
                    dcfDatas[i].Depreciation = dcfDatas[i - 1].Revenue * dcfInput.DepreciationPrcRevenue; 
                    dcfDatas[i].Ebit = dcfDatas[i - 1].Revenue * dcfInput.EbitPrcRevenue;
                    dcfDatas[i].Ebiat = dcfDatas[i].Ebit * (1 - dcfInput.TaxRate); // ebit*(1-tax rate)
                    dcfDatas[i].Ebitda = dcfDatas[i - 1].Revenue * dcfInput.EbitdaPrcRevenue;
                    dcfDatas[i].AllCostsEbitda = dcfDatas[i].AllCosts - dcfDatas[i].Ebitda;
                   
                   

                    if (i > 0)
                    {
                       
                        dcfDatas[i].NetWorkingCapitalChange = dcfDatas[i].NetWorkingCapital - dcfDatas[i - 1].NetWorkingCapital;
                        dcfDatas[i].TotalAssetsChange = dcfDatas[i].TotalAssets - dcfDatas[i - 1].TotalAssets;
                        dcfDatas[i].TotalLiabilitiesChange = dcfDatas[i].TotalLiabilities - dcfDatas[i - 1].TotalLiabilities;
                        dcfDatas[i].Capex = dcfDatas[i].TotalAssetsChange - dcfDatas[i].TotalLiabilitiesChange; // arvutatakse assets ja liabilities muutude vahena
                        dcfDatas[i].RevenueGrowth = dcfInput.GrowthRatePrognosis;
                        dcfDatas[i].Capexdepreciation = dcfDatas[i].Capex - dcfDatas[i].Depreciation;
                        dcfDatas[i].Fcff = dcfDatas[i].Ebiat - dcfDatas[i].Capexdepreciation - dcfDatas[i].NetWorkingCapitalChange;
                    }
                     
                }





            }
        }
    }
}
