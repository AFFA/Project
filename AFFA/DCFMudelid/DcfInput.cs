using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    public class DcfInput : INotifyPropertyChanged
    {
        // default values:
        #region Class Variables
        private double _growthRatePrognosis=0.03;
        private double _taxRate=0.1;
        private double _costOfDebt = 0.05;
        private double _riskFreeRate = 0.02;
        private double _marketRiskPremium = 0.05;
        private double _continuousGrowth = 0.02;
        private double _totalAssetsPrcRevenue=7.72;
        private double _totalLiabilitiesPrcRevenue=3.48;
        private double _totalCurrentAssetsPrcRevenue=1.01;
        private double _totalCurrentLiabilitiesPrcRevenue=1.48;
        private double _allCostsPrcRevenue=0.7605;
        private double _ebitdaPrcRevenue=0.24;
        private double _depreciationPrcRevenue=0.01;
        private double _ebitPrcRevenue=0.23;
        private double _wacc=0.072;
        private double _beta = 1.05;
        private double _costOfEquity = 0.12;
        #endregion

        #region Getters and Setters for Class Variables
        // NB! If renaming these methods, remember that DcfVM contains getter/setter methods with names equal to [method_name_here]+"Value".
        // If the new method name here is different than the specified format in DcfVM then databinding isn't automatically updated.
        public double GrowthRatePrognosis
        {
            get { return _growthRatePrognosis; }
            set { _growthRatePrognosis = value; RaisePropertyChanged(); }
        }

        public double TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; RaisePropertyChanged(); }
        }

        public double CostOfDebt
        {
            get { return _costOfDebt; }
            set { _costOfDebt = value; RaisePropertyChanged(); }
        }

        public double RiskFreeRate
        {
            get { return _riskFreeRate; }
            set { _riskFreeRate = value; RaisePropertyChanged(); }
        }

        public double MarketRiskPremium
        {
            get { return _marketRiskPremium; }
            set { _marketRiskPremium = value; RaisePropertyChanged(); }
        }

        public double ContinuousGrowth
        {
            get { return _continuousGrowth; }
            set { _continuousGrowth = value; RaisePropertyChanged(); }
        }

        public double TotalAssetsPrcRevenue
        {
            get { return _totalAssetsPrcRevenue; }
            set { _totalAssetsPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double TotalLiabilitiesPrcRevenue
        {
            get { return _totalLiabilitiesPrcRevenue; }
            set { _totalLiabilitiesPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double TotalCurrentAssetsPrcRevenue
        {
            get { return _totalCurrentAssetsPrcRevenue; }
            set { _totalCurrentAssetsPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double TotalCurrentLiabilitiesPrcRevenue
        {
            get { return _totalCurrentLiabilitiesPrcRevenue; }
            set { _totalCurrentLiabilitiesPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double AllCostsPrcRevenue
        {
            get { return _allCostsPrcRevenue; }
            set { _allCostsPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double EbitdaPrcRevenue
        {
            get { return _ebitdaPrcRevenue; }
            set { _ebitdaPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double DepreciationPrcRevenue
        {
            get { return _depreciationPrcRevenue; }
            set { _depreciationPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double EbitPrcRevenue
        {
            get { return _ebitPrcRevenue; }
            set { _ebitPrcRevenue = value; RaisePropertyChanged(); }
        }

        public double Wacc
        {
            get { return _wacc; }
            set { _wacc = value; RaisePropertyChanged(); }
        }

        public double Beta
        {
            get { return _beta; }
            set { _beta = value; RaisePropertyChanged(); }
        }

        public double CostOfEquity
        {
            get { return _costOfEquity; }
            set { _costOfEquity = value; RaisePropertyChanged(); }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
