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
        _kuupaev=finData.Kuupaev;
        _isPrognosis=false; // kas on prognoos
        _totalAssets = finData.BsTotalAssets; // FinDatast, kommentaarid olemasolevate kvartalite arvutamise kohta
        
        try
        {
            _totalAssetsPrcRevenue=finData.BsTotalAssets/finData.IsRevenue; // total assets/Revenue
        }
        catch (InvalidOperationException)
        { }
        
        _totalLiabilities=finData.BsTotalLiabilities; // Findatast
       
        try
        {
            _totalLiabilitiesPrcRevenue = finData.BsTotalLiabilities / finData.IsRevenue;
        }
        catch (InvalidOperationException)
        { }
        _totalCurrentAssets=finData.BsTotalCurrentAssets; // sama loogika, mis eelnevatel
        try
        {
        _totalCurrentAssetsPrcRevenue = finData.BsTotalCurrentAssets / finData.IsRevenue;
        }
        catch (InvalidOperationException)
        { }
        
        _totalCurrentLiabilities=finData.BsTotalCurrentLiabilities;
        
        try
        {
            _totalCurrentLiabilitiesPrcRevenue = finData.BsTotalCurrentLiabilities / finData.IsRevenue;   
        }
        catch (InvalidOperationException)
        { }

        _revenue=finData.IsRevenue; // FinDatast
        try
        {
            _allCosts = finData.IsTotalOperatingExpenses - finData.IsDepreciationAmortization; // summeerida Findatast kõik kulude read
        }
        catch (InvalidOperationException)
        { }
        
        try
        {
            _allCostsPrcRevenue = _allCosts / finData.IsRevenue;
        }
        catch (InvalidOperationException)
        { }
        
        _ebitda=finData.FrEbitda; // Findatas olemas
        try
        {
            _ebitdaPrcRevenue = _ebitda / finData.IsRevenue;
        }
        catch (InvalidOperationException)
        { }
        
        _depreciation=finData.IsDepreciationAmortization; // sama mis Depreciation&Amortization (D&A), Findatas olemas
        try
        {
            _depreciationPrcRevenue = finData.IsDepreciationAmortization / finData.IsRevenue;
        }
        catch (InvalidOperationException)
        { }
        
        _ebit=finData.FrEbit; // findatas olemas
        try
        {
            _ebitPrcRevenue = _ebit / finData.IsRevenue; // täiendav väli
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
        private bool _isPrognosis=false; // kas on prognoos
        private double? _totalAssets=null; // FinDatast, kommentaarid olemasolevate kvartalite arvutamise kohta
        private double? _totalAssetsPrcRevenue = null; // total assets/Revenue
        private double? _totalAssetsChange = null; // muut eelmise kvartaliga
        private double? _totalLiabilities = null; // Findatast
        private double? _totalLiabilitiesPrcRevenue = null;
        private double? _totalLiabilitiesChange = null;
        private double? _capex = null; // arvutatakse assets ja liabilities muutude vahena
        private double? _totalCurrentAssets = null; // sama loogika, mis eelnevatel
        private double? _totalCurrentAssetsPrcRevenue = null;
        private double? _totalCurrentLiabilities = null;
        private double? _totalCurrentLiabilitiesPrcRevenue = null;
        private double? _netWorkingCapital = null;
        private double? _netWorkingCapitalChange = null; // sama, mis Net Noncash working capital

        private double? _revenue = null; // FinDatast
        private double? _revenueGrowth = null; // arvutada välja 4 kvartalit tagasi revenue järgi
        private double? _allCosts = null; // summeerida Findatast kõik kulude read
        private double? _allCostsPrcRevenue = null;
        private double? _ebitda = null; // Findatas olemas
        private double? _ebitdaPrcRevenue = null;
        private double? _depreciation = null; // sama mis Depreciation&Amortization (D&A), Findatas olemas
        private double? _depreciationPrcRevenue = null;
        private double? _ebit = null; // findatas olemas
        private double? _ebitPrcRevenue = null; // täiendav väli
        private double? _ebiat = null; // ebit*(1-tax rate)
        private double? _capexdepreciation = null;
        private double? _taxRate = null;
        private double? _fcff = null;
        private double? _allCostsEbitda = null;

        public double? AllCostsEbitda
        {
            get { return _allCostsEbitda; }
            set { _allCostsEbitda = value; }
        }

        public double? Fcff
        {
            get { return _fcff; }
            set { _fcff = value; }
        }

        public double? TaxRate
        {
            get { return _taxRate; }
            set { _taxRate = value; }
        }
       

        public double? Capexdepreciation
        {
            get { return _capexdepreciation; }
            set { _capexdepreciation = value; }
        }


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

        public double? TotalAssets
        {
            get { return _totalAssets; }
            set { _totalAssets = value; }
        }

        public double? TotalAssetsPrcRevenue
        {
            get { return _totalAssetsPrcRevenue; }
            set { _totalAssetsPrcRevenue = value; }
        }

        public double? TotalAssetsChange
        {
            get { return _totalAssetsChange; }
            set { _totalAssetsChange = value; }
        }

        public double? TotalLiabilities
        {
            get { return _totalLiabilities; }
            set { _totalLiabilities = value; }
        }

        public double? TotalLiabilitiesPrcRevenue
        {
            get { return _totalLiabilitiesPrcRevenue; }
            set { _totalLiabilitiesPrcRevenue = value; }
        }

        public double? Capex
        {
            get { return _capex; }
            set { _capex = value; }
        }

        public double? TotalLiabilitiesChange
        {
            get { return _totalLiabilitiesChange; }
            set { _totalLiabilitiesChange = value; }
        }

        public double? TotalCurrentAssets
        {
            get { return _totalCurrentAssets; }
            set { _totalCurrentAssets = value; }
        }

        public double? TotalCurrentAssetsPrcRevenue
        {
            get { return _totalCurrentAssetsPrcRevenue; }
            set { _totalCurrentAssetsPrcRevenue = value; }
        }

        public double? TotalCurrentLiabilities
        {
            get { return _totalCurrentLiabilities; }
            set { _totalCurrentLiabilities = value; }
        }

        public double? TotalCurrentLiabilitiesPrcRevenue
        {
            get { return _totalCurrentLiabilitiesPrcRevenue; }
            set { _totalCurrentLiabilitiesPrcRevenue = value; }
        }

        public double? NetWorkingCapital
        {
            get { return _netWorkingCapital; }
            set { _netWorkingCapital = value; }
        }

        public double? NetWorkingCapitalChange
        {
            get { return _netWorkingCapitalChange; }
            set { _netWorkingCapitalChange = value; }
        }

        public double? Revenue
        {
            get { return _revenue; }
            set { _revenue = value; }
        }

        public double? RevenueGrowth
        {
            get { return _revenueGrowth; }
            set { _revenueGrowth = value; }
        }

        public double? AllCosts
        {
            get { return _allCosts; }
            set { _allCosts = value; }
        }

        public double? AllCostsPrcRevenue
        {
            get { return _allCostsPrcRevenue; }
            set { _allCostsPrcRevenue = value; }
        }

        public double? Ebitda
        {
            get { return _ebitda; }
            set { _ebitda = value; }
        }

        public double? EbitdaPrcRevenue
        {
            get { return _ebitdaPrcRevenue; }
            set { _ebitdaPrcRevenue = value; }
        }

        public double? Depreciation
        {
            get { return _depreciation; }
            set { _depreciation = value; }
        }

        public double? DepreciationPrcRevenue
        {
            get { return _depreciationPrcRevenue; }
            set { _depreciationPrcRevenue = value; }
        }

        public double? Ebit
        {
            get { return _ebit; }
            set { _ebit = value; }
        }

        public double? EbitPrcRevenue
        {
            get { return _ebitPrcRevenue; }
            set { _ebitPrcRevenue = value; }
        }

        public double? Ebiat
        {
            get { return _ebiat; }
            set { _ebiat = value; }
        }



    }
}
