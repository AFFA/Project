using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    /// <summary>
    /// Objekt, mis hoiab väljundi (ettevõtte väärtuse arvutuse tulemuseD) jaoks vajalikke andmeid.
    /// </summary>
    public class DcfOutput : INotifyPropertyChanged
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
        private Recommendations _recommendation;

        public enum Recommendations
        {
            No_Data_To_Recommend,
            Strong_Buy,
            Buy,
            Strong_Sell,
            Sell,
            Hold
        }

        #region getters setters
        public double? TerminalFreeCashFlow
        {
            get { return _terminalFreeCashFlow; }
            set { _terminalFreeCashFlow = value; RaisePropertyChanged(); }
        }

        public double? TerminalValue
        {
            get { return _terminalValue; }
            set { _terminalValue = value; RaisePropertyChanged(); }
        }

        public double? PerpetuityGrowthRate
        {
            get { return _perpetuityGrowthRate; }
            set { _perpetuityGrowthRate = value; RaisePropertyChanged(); }
        }

        public double? Wacc
        {
            get { return _wacc; }
            set { _wacc = value; RaisePropertyChanged(); }
        }

        public double? PresentValueOfFreeCashFlow
        {
            get { return _presentValueOfFreeCashFlow; }
            set { _presentValueOfFreeCashFlow = value; RaisePropertyChanged(); }
        }

        public double? PresentValueOfTerminalValue
        {
            get { return _presentValueOfTerminalValue; }
            set { _presentValueOfTerminalValue = value; RaisePropertyChanged(); }
        }

        public double? EnterpriseValueWithoutCash
        {
            get { return _enterpriseValueWithoutCash; }
            set { _enterpriseValueWithoutCash = value; RaisePropertyChanged(); }
        }

        public double? CashAndCashEquivalents
        {
            get { return _cashAndCashEquivalents; }
            set { _cashAndCashEquivalents = value; RaisePropertyChanged(); }
        }

        public double? EnterpriseValue
        {
            get { return _enterpriseValue; }
            set { _enterpriseValue = value; RaisePropertyChanged(); }
        }

        public double? LessTotalDebt
        {
            get { return _lessTotalDebt; }
            set { _lessTotalDebt = value; RaisePropertyChanged(); }
        }

        public double? EquityValue
        {
            get { return _equityValue; }
            set { _equityValue = value; RaisePropertyChanged(); }
        }

        public double? OutstandingShares
        {
            get { return _outstandingShares; }
            set { _outstandingShares = value; RaisePropertyChanged(); }
        }

        public double? CurrentSharePrice
        {
            get { return _currentSharePrice; }
            set { _currentSharePrice = value; RaisePropertyChanged(); }
        }

        public double? ModelSharePrice
        {
            get { return _modelSharePrice; }
            set { _modelSharePrice = value; RaisePropertyChanged(); }
        }

        public Recommendations Recommendation
        {
            get { return _recommendation; }
            set { _recommendation = value; RaisePropertyChanged(); }
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
