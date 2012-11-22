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
            //TODO
            // konstruktor, kus saame osa asju otse findatast kopeerida DcfDatasse, näiteks kuupäev, revenue, ebitda, ebit jne
            // see konstruktor on juba olnud kvartalite Dcf andmete jaoks
        }

        public DcfData(DateTime kuupev)
        {
            //TODO
            // konstruktor, luuakse prognoosi Dcf andmed, ette saame määrata kuupäeva ja selle, et _isPrognosis=true - ehk tegemist on tulevikuprognoosiga
        }

        private DateTime kuupaev;
        private bool _isPrognosis; // kas on prognoos
        private double? _totalAssets; // FinDatast, kommentaarid olemasolevate kvartalite arvutamise kohta
        private double? _totalAssetsPrcRevenue; // total assets/Revenue
        private double? _totalAssetsChange; // muut eelmise kvartaliga
        private double? _totalLiabilities; // Findatast
        private double? _totalLiabilitiesPrcRevenue; 
        private double? _totalLiabilitiesChange;
        private double? _capex; // arvutatakse assets ja liabilities muutude vahena
        private double? _totalCurrentAssets; // sama loogika, mis eelnevatel
        private double? _totalCurrentAssetsPrcRevenue;
        private double? _totalCurrentLiabilities;
        private double? _totalCurrentLiabilitiesPrcRevenue;
        private double? _netWorkingCapital;
        private double? _netWorkingCapitalChange; // sama, mis Net Noncash working capital

        private double? _revenue; // FinDatast
        private double? _revenueGrowth; // arvutada välja 4 kvartalit tagasi revenue järgi
        private double? _allCosts; // summeerida Findatast kõik kulude read
        private double? _allCostsPrcRevenue;
        private double? _ebitda; // Findatas olemas
        private double? _ebitdaPrcRevenue;
        private double? _depreciation; // sama mis Depreciation&Amortization (D&A), Findatas olemas
        private double? _depreciationPrcRevenue;
        private double? _ebit; // findatas olemas
        private double? _ebitPrcRevenue; // täiendav väli
        private double? _ebiat; // ebit*(1-tax rate)

        public DateTime Kuupaev
        {
            get { return kuupaev; }
            set { kuupaev = value; }
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
