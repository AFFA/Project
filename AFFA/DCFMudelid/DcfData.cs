using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.Mudelid;

namespace AFFA.DCFMudelid
{
    public class DcfData
    {
        public DcfData(FinData finData)
        {

            // konstruktor, kus saame osa asju otse findatast kopeerida DcfDatasse, näiteks kuupäev, revenue, ebitda, ebit jne
            // see konstruktor on juba olnud kvartalite Dcf andmete jaoks
            _kuupaev = finData.Kuupaev;
            _isPrognosis = false; // kas on prognoos
            if (finData.BsTotalAssets != null)
            {
                _totalAssets = (double)finData.BsTotalAssets; // FinDatast, kommentaarid olemasolevate kvartalite arvutamise kohta
            }


            try
            {
                if (finData.BsTotalAssets != null && finData.IsRevenue != null)
                {
                    _totalAssetsPrcRevenue = (double)(finData.BsTotalAssets / finData.IsRevenue); // total assets/Revenue
                }
            }
            catch (InvalidOperationException)
            {
            }
            if (finData.BsTotalLiabilities != null)
            {

                _totalLiabilities = (double) finData.BsTotalLiabilities; // Findatast
            }

            try
            {
                if (finData.BsTotalLiabilities != null && finData.IsRevenue != null)
                {
                    _totalLiabilitiesPrcRevenue = (double)(finData.BsTotalLiabilities / finData.IsRevenue);
                }
            }
            catch (InvalidOperationException)
            { }

            if (finData.BsTotalCurrentAssets != null)
            {
                _totalCurrentAssets = (double) finData.BsTotalCurrentAssets; // sama loogika, mis eelnevatel
            }
            try
            {
                if (finData.BsTotalCurrentAssets != null && finData.IsRevenue != null)
                {
                    _totalCurrentAssetsPrcRevenue =(double) (finData.BsTotalCurrentAssets/finData.IsRevenue);
                }
            }
            catch (InvalidOperationException)
            { }

            if (finData.BsTotalCurrentLiabilities != null)
            {
                _totalCurrentLiabilities = (double) finData.BsTotalCurrentLiabilities;
            }
            try
            {
                if (finData.BsTotalCurrentLiabilities != null && finData.IsRevenue != null)
                {
                    _totalCurrentLiabilitiesPrcRevenue = (double) (finData.BsTotalCurrentLiabilities/finData.IsRevenue);
                }
            }
            catch (InvalidOperationException)
            { }

            if (finData.IsRevenue != null)
            {
                _revenue = (double) finData.IsRevenue; // FinDatast
            }
            
                if (finData.IsTotalOperatingExpenses != null)
                {
                    _allCosts += (double) finData.IsTotalOperatingExpenses;
                        // summeerida Findatast kõik kulude read
                }
                if (finData.IsDepreciationAmortization != null)
                {
                    _allCosts -= (double) finData.IsDepreciationAmortization;
                }
            

            try
            {
                if (finData.IsRevenue != null)
                {
                    _allCostsPrcRevenue = _allCosts/(double) finData.IsRevenue;
                }
            }
            catch (InvalidOperationException)
            { }
            if (finData.FrEbitda != null)
            {
                _ebitda = (double) finData.FrEbitda; // Findatas olemas
            }
            try
            {
                if (finData.IsRevenue != null)
                {
                    _ebitdaPrcRevenue = _ebitda/(double) finData.IsRevenue;
                }
            }
            catch (InvalidOperationException)
            { }
            if (finData.IsDepreciationAmortization != null)
            {
                _depreciation = (double) finData.IsDepreciationAmortization;
                    // sama mis Depreciation&Amortization (D&A), Findatas olemas
            }
            try
            {
                if (finData.IsRevenue != null)
                {
                    _depreciationPrcRevenue = _depreciation / (double) finData.IsRevenue;
                }
            }
            catch (InvalidOperationException)
            { }
            if (finData.FrEbit != null)
            {
                _ebit = (double) finData.FrEbit; // findatas olemas
            }
            try
            {
                if (finData.IsRevenue != null)
                {
                    _ebitPrcRevenue = _ebit/(double) finData.IsRevenue; // täiendav väli
                }
            }
            catch (InvalidOperationException)
            { }





        }

        public DcfData(DateTime kuupev)
        {
            //TODO
            // konstruktor, luuakse prognoosi Dcf andmed, ette saame määrata kuupäeva ja selle, et _isPrognosis=true - ehk tegemist on tulevikuprognoosiga
            this._kuupaev = kuupev;
            this._isPrognosis = true;
        }

        private DateTime _kuupaev;
        private bool _isPrognosis = false; // kas on prognoos
        private double _totalAssets = 0; // FinDatast, kommentaarid olemasolevate kvartalite arvutamise kohta
        private double _totalAssetsPrcRevenue = 0; // total assets/Revenue
        private double _totalAssetsChange = 0; // muut eelmise kvartaliga
        private double _totalLiabilities = 0; // Findatast
        private double _totalLiabilitiesPrcRevenue = 0;
        private double _totalLiabilitiesChange = 0;
        private double _capex = 0; // arvutatakse assets ja liabilities muutude vahena
        private double _totalCurrentAssets = 0; // sama loogika, mis eelnevatel
        private double _totalCurrentAssetsPrcRevenue = 0;
        private double _totalCurrentLiabilities = 0;
        private double _totalCurrentLiabilitiesPrcRevenue = 0;
        private double _netWorkingCapital = 0;
        private double _netWorkingCapitalChange = 0; // sama, mis Net Noncash working capital

        private double _revenue = 0; // FinDatast
        private double _revenueGrowth = 0; // arvutada välja 4 kvartalit tagasi revenue järgi
        private double _allCosts = 0; // summeerida Findatast kõik kulude read
        private double _allCostsPrcRevenue = 0;
        private double _ebitda = 0; // Findatas olemas
        private double _ebitdaPrcRevenue = 0;
        private double _depreciation = 0; // sama mis Depreciation&Amortization (D&A), Findatas olemas
        private double _depreciationPrcRevenue = 0;
        private double _ebit = 0; // findatas olemas
        private double _ebitPrcRevenue = 0; // täiendav väli
        private double _ebiat = 0; // ebit*(1-tax rate)
        private double _capexdepreciation = 0;
        private double _taxRate = 0;
        private double _fcff = 0;
        private double _allCostsEbitda = 0;

        #region getters, setters
        public DateTime Kuupaev
        {
            get { return _kuupaev; }
            set { _kuupaev = value; }
        }

        public bool IsPrognosis
        {
            get { return _isPrognosis; }
            set { _isPrognosis = value; }
        }

        public double TotalAssets
        {
            get { return _totalAssets; }
            set { _totalAssets = value; }
        }

        public double TotalAssetsPrcRevenue
        {
            get { return _totalAssetsPrcRevenue; }
            set { _totalAssetsPrcRevenue = value; }
        }

        public double TotalAssetsChange
        {
            get { return _totalAssetsChange; }
            set { _totalAssetsChange = value; }
        }

        public double TotalLiabilities
        {
            get { return _totalLiabilities; }
            set { _totalLiabilities = value; }
        }

        public double TotalLiabilitiesPrcRevenue
        {
            get { return _totalLiabilitiesPrcRevenue; }
            set { _totalLiabilitiesPrcRevenue = value; }
        }

        public double Capex
        {
            get { return _capex; }
            set { _capex = value; }
        }

        public double TotalCurrentAssets
        {
            get { return _totalCurrentAssets; }
            set { _totalCurrentAssets = value; }
        }

        public double TotalLiabilitiesChange
        {
            get { return _totalLiabilitiesChange; }
            set { _totalLiabilitiesChange = value; }
        }

        public double TotalCurrentAssetsPrcRevenue
        {
            get { return _totalCurrentAssetsPrcRevenue; }
            set { _totalCurrentAssetsPrcRevenue = value; }
        }

        public double TotalCurrentLiabilities
        {
            get { return _totalCurrentLiabilities; }
            set { _totalCurrentLiabilities = value; }
        }

        public double NetWorkingCapital
        {
            get { return _netWorkingCapital; }
            set { _netWorkingCapital = value; }
        }

        public double TotalCurrentLiabilitiesPrcRevenue
        {
            get { return _totalCurrentLiabilitiesPrcRevenue; }
            set { _totalCurrentLiabilitiesPrcRevenue = value; }
        }

        public double NetWorkingCapitalChange
        {
            get { return _netWorkingCapitalChange; }
            set { _netWorkingCapitalChange = value; }
        }

        public double Revenue
        {
            get { return _revenue; }
            set { _revenue = value; }
        }

        public double RevenueGrowth
        {
            get { return _revenueGrowth; }
            set { _revenueGrowth = value; }
        }

        public double AllCosts
        {
            get { return _allCosts; }
            set { _allCosts = value; }
        }

        public double Ebitda
        {
            get { return _ebitda; }
            set { _ebitda = value; }
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

        public double Depreciation
        {
            get { return _depreciation; }
            set { _depreciation = value; }
        }

        public double DepreciationPrcRevenue
        {
            get { return _depreciationPrcRevenue; }
            set { _depreciationPrcRevenue = value; }
        }

        public double Ebit
        {
            get { return _ebit; }
            set { _ebit = value; }
        }

        public double EbitPrcRevenue
        {
            get { return _ebitPrcRevenue; }
            set { _ebitPrcRevenue = value; }
        }

        public double Ebiat
        {
            get { return _ebiat; }
            set { _ebiat = value; }
        }

        public double TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }

        public double Capexdepreciation
        {
            get { return _capexdepreciation; }
            set { _capexdepreciation = value; }
        }

        public double Fcff
        {
            get { return _fcff; }
            set { _fcff = value; }
        }

        public double AllCostsEbitda
        {
            get { return _allCostsEbitda; }
            set { _allCostsEbitda = value; }
        }

        #endregion
    }
}
