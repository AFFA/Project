using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    public class DcfOutput
    {
        private double? _terminalFreeCashFlow;
        private double? _perpetuityGrowthRate;
        private double? _terminalValue;
        private double? _wacc;
        private double? _presentValueOfFreeCashFlow;
        private double? _presentValueOfTerminalValue;
        private double? _enterpriseValueWithoutCash;
        private double? _cashAndCashEquivalents;
        private double? _enterpriseValue;
        private double? _lessTotalDebt;
        private double? _equityValue;
        private double? _outstandingShares;
        private double? _currentSharePrice;
        private double? _modelSharePrice;
        private string _recommendation;



        #region getters setters
        public double? TerminalFreeCashFlow
        {
            get { return _terminalFreeCashFlow; }
            set { _terminalFreeCashFlow = value; }
        }

        public double? TerminalValue
        {
            get { return _terminalValue; }
            set { _terminalValue = value; }
        }

        public double? PerpetuityGrowthRate
        {
            get { return _perpetuityGrowthRate; }
            set { _perpetuityGrowthRate = value; }
        }

        public double? Wacc
        {
            get { return _wacc; }
            set { _wacc = value; }
        }

        public double? PresentValueOfFreeCashFlow
        {
            get { return _presentValueOfFreeCashFlow; }
            set { _presentValueOfFreeCashFlow = value; }
        }

        public double? PresentValueOfTerminalValue
        {
            get { return _presentValueOfTerminalValue; }
            set { _presentValueOfTerminalValue = value; }
        }

        public double? EnterpriseValueWithoutCash
        {
            get { return _enterpriseValueWithoutCash; }
            set { _enterpriseValueWithoutCash = value; }
        }

        public double? CashAndCashEquivalents
        {
            get { return _cashAndCashEquivalents; }
            set { _cashAndCashEquivalents = value; }
        }

        public double? EnterpriseValue
        {
            get { return _enterpriseValue; }
            set { _enterpriseValue = value; }
        }

        public double? LessTotalDebt
        {
            get { return _lessTotalDebt; }
            set { _lessTotalDebt = value; }
        }

        public double? EquityValue
        {
            get { return _equityValue; }
            set { _equityValue = value; }
        }

        public double? OutstandingShares
        {
            get { return _outstandingShares; }
            set { _outstandingShares = value; }
        }

        public double? CurrentSharePrice
        {
            get { return _currentSharePrice; }
            set { _currentSharePrice = value; }
        }

        public double? ModelSharePrice
        {
            get { return _modelSharePrice; }
            set { _modelSharePrice = value; }
        }

        public string Recommendation
        {
            get { return _recommendation; }
            set { _recommendation = value; }
        }

        #endregion

    }
}
