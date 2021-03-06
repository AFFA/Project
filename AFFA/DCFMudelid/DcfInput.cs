﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    /// <summary>
    /// Objekt, mis hoiab ettevõtte väärtuse arvutusteks vajalike eelduste andmeid.
    /// </summary>
    public class DcfInput : INotifyPropertyChanged
    {
        // default values:

        private double _growthRatePrognosis = 0.03;
        private double _taxRate = 0.1;
        private double _costOfDebt = 0.05;
        private double _riskFreeRate = 0.02;
        private double _marketRiskPremium = 0.05;
        private double _continuousGrowth = 0.02;
        private double _totalAssetsPrcRevenue = 7.72;
        private double _totalLiabilitiesPrcRevenue = 3.48;
        private double _totalCurrentAssetsPrcRevenue = 1.01;
        private double _totalCurrentLiabilitiesPrcRevenue = 1.48;
        private double _allCostsPrcRevenue = 0.7605;
        private double _ebitdaPrcRevenue = 0.24;
        private double _depreciationPrcRevenue = 0.01;
        private double _ebitPrcRevenue = 0.23;
        private double _wacc = 0.072;
        private double _beta = 1.05;
        private double _costOfEquity = 0.12;
        private double _sharesOutstanding;

        private double _totalAssetsBeta = 0;
        private double _totalAssetsAlpha = 0;

        private double _totalLiabilitiesBeta = 0;
        private double _totalLiabilitiesAlpha = 0;
        private double _totalCurrentAssetsBeta = 0;
        private double _totalCurrentAssetsAlpha = 0;
        private double _totalCurrentLiabilitiesBeta = 0;
        private double _totalCurrentLiabilitiesAlpha = 0;

        private ForecastingMethod _forecastMethod = ForecastingMethod.LinearRegression;
        private bool _linearRegression = true;
        private bool _averageMargins = false;

        public enum ForecastingMethod
        {
            LinearRegression,
            AverageMargins
        }

        public double SharesOutstanding
        {
            get { return _sharesOutstanding; }
            set { _sharesOutstanding = value; RaisePropertyChanged(); }
        }

        public double TotalCurrentAssetsBeta
        {
            get { return _totalCurrentAssetsBeta; }
            set { _totalCurrentAssetsBeta = value; RaisePropertyChanged(); }
        }


        public double TotalAssetsBeta
        {
            get { return _totalAssetsBeta; }
            set { _totalAssetsBeta = value; RaisePropertyChanged(); }
        }

        public double TotalAssetsAlpha
        {
            get { return _totalAssetsAlpha; }
            set { _totalAssetsAlpha = value; RaisePropertyChanged(); }
        }

        public double TotalLiabilitiesBeta
        {
            get { return _totalLiabilitiesBeta; }
            set { _totalLiabilitiesBeta = value; RaisePropertyChanged(); }
        }

        public double TotalLiabilitiesAlpha
        {
            get { return _totalLiabilitiesAlpha; }
            set { _totalLiabilitiesAlpha = value; RaisePropertyChanged(); }
        }

        public double TotalCurrentLiabilitiesBeta
        {
            get { return _totalCurrentLiabilitiesBeta; }
            set { _totalCurrentLiabilitiesBeta = value; RaisePropertyChanged(); }
        }

        public double TotalCurrentLiabilitiesAlpha
        {
            get { return _totalCurrentLiabilitiesAlpha; }
            set { _totalCurrentLiabilitiesAlpha = value; RaisePropertyChanged(); }
        }



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



        public double TotalCurrentAssetsAlpha
        {
            get { return _totalCurrentAssetsAlpha; }
            set { _totalCurrentAssetsAlpha = value; }
        }

        public bool LinearRegression
        {
            get { return _linearRegression; }
            set { _linearRegression = value; }
        }

        public ForecastingMethod ForecastMethod
        {
            get { return _forecastMethod; }
            set { _forecastMethod = value; }
        }

        public bool AverageMargins
        {
            get { return _averageMargins; }
            set { _averageMargins = value; }
        }


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
