using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    public class DcfInput
    {
        private double _growthRatePrognosis=0.03;
        private double _taxRate=0.8;
        private double _costOfDebt = 0.05;
        private double _riskFreeRate = 0.02;
        private double _marketRiskPremium = 0.08;
        private double _continuousGrowth = 0.02;
        private double _totalAssetsPrcRevenue=6.72;
        private double _totalLiabilitiesPrcRevenue=2.88;
        private double _totalCurrentAssetsPrcRevenue=1.01;
        private double _totalCurrentLiabilitiesPrcRevenue=1.48;
        private double _allCostsPrcRevenue=0.7605;
        private double _ebitdaPrcRevenue=0.24;
        private double _depreciationPrcRevenue=0.8;
        private double _ebitPrcRevenue=0.23; 

        public double GrowthRatePrognosis
        {
            get { return _growthRatePrognosis; }
            set { _growthRatePrognosis = value; }
        }

        public double TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        public double CostOfDebt
        {
            get { return _costOfDebt; }
            set { _costOfDebt = value; }
        }

        public double RiskFreeRate
        {
            get { return _riskFreeRate; }
            set { _riskFreeRate = value; }
        }

        public double MarketRiskPremium
        {
            get { return _marketRiskPremium; }
            set { _marketRiskPremium = value; }
        }

        public double ContinuousGrowth
        {
            get { return _continuousGrowth; }
            set { _continuousGrowth = value; }
        }

        public double TotalAssetsPrcRevenue
        {
            get { return _totalAssetsPrcRevenue; }
            set { _totalAssetsPrcRevenue = value; }
        }

        public double TotalLiabilitiesPrcRevenue
        {
            get { return _totalLiabilitiesPrcRevenue; }
            set { _totalLiabilitiesPrcRevenue = value; }
        }

        public double TotalCurrentAssetsPrcRevenue
        {
            get { return _totalCurrentAssetsPrcRevenue; }
            set { _totalCurrentAssetsPrcRevenue = value; }
        }

        public double TotalCurrentLiabilitiesPrcRevenue
        {
            get { return _totalCurrentLiabilitiesPrcRevenue; }
            set { _totalCurrentLiabilitiesPrcRevenue = value; }
        }

        public double AllCostsPrcRevenue
        {
            get { return _allCostsPrcRevenue; }
            set { _allCostsPrcRevenue = value; }
        }

        public double EbitdaPrcRevenue
        {
            get { return _ebitdaPrcRevenue; }
            set { _ebitdaPrcRevenue = value; }
        }

        public double DepreciationPrcRevenue
        {
            get { return _depreciationPrcRevenue; }
            set { _depreciationPrcRevenue = value; }
        }

        public double EbitPrcRevenue
        {
            get { return _ebitPrcRevenue; }
            set { _ebitPrcRevenue = value; }
        }
    }
}
