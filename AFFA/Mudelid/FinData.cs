using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AFFA.Mudelid
{
    public class FinData
    {

        private DateTime kuupaev;
        private String bs_symbol;
        private Double? bs_cash_short_term_investments = null;
        private Double? bs_receivables = null;
        private Double? bs_inventory = null;
        private Double? bs_prepaid_expenses = null;
        private Double? bs_other_current_assets = null;
        private Double? bs_total_current_assets = null;
        private Double? bs_gross_property_plant_equipment = null;
        private Double? bs_accumulated_depreciation = null;
        private Double? bs_net_property_plant_equipment = null;
        private Double? bs_long_term_investments = null;
        private Double? bs_goodwill_intangibles = null;
        private Double? bs_other_long_term_assets = null;
        private Double? bs_total_long_term_assets = null;
        private Double? bs_total_assets = null;
        private Double? bs_liabilities = null;
        private Double? bs_current_portion_of_long_term_debt = null;
        private Double? bs_accounts_payable = null;
        private Double? bs_accrued_expenses = null;
        private Double? bs_deferred_revenues = null;
        private Double? bs_other_current_liabilities = null;
        private Double? bs_total_current_liabilities = null;
        private Double? bs_total_long_term_debt = null;
        private Double? bs_shareholders_equity = null;
        private Double? bs_deferred_income_tax = null;
        private Double? bs_minority_interest = null;
        private Double? bs_other_long_term_liabilities = null;
        private Double? bs_total_long_term_liabilities = null;
        private Double? bs_total_liabilities = null;
        private Double? bs_common_shares_outstanding = null;
        private Double? bs_preferred_stock = null;
        private Double? bs_common_stock_net = null;
        private Double? bs_additional_paid_in_capital = null;
        private Double? bs_retained_earnings = null;
        private Double? bs_treasury_stock = null;
        private Double? bs_other_shareholders_equity = null;
        private Double? bs_shareholders_equity1 = null;
        private Double? bs_total_liabilities_shareholders_equity = null;
        private Double? is_revenue = null;
        private Double? is_cost_of_revenue = null;
        private Double? is_gross_profit = null;
        private Double? is_rd_expense = null;
        private Double? is_selling_general_admin_expense = null;
        private Double? is_depreciation_amortization = null;
        private Double? is_operating_interest_expense = null;
        private Double? is_other_operating_income_expense = null;
        private Double? is_total_operating_expenses = null;
        private Double? is_operating_income = null;
        private Double? is_non_operating_income = null;
        private Double? is_pretax_income = null;
        private Double? is_provision_for_income_taxes = null;
        private Double? is_income_after_tax = null;
        private Double? is_minority_interest = null;
        private Double? is_minority_interest1 = null;
        private Double? is_equity_in_affiliates = null;
        private Double? is_income_before_disc_operations = null;
        private Double? is_investment_gains_losses = null;
        private Double? is_other_income_charges = null;
        private Double? is_income_from_disc_operations = null;
        private Double? is_net_income = null;
        private Double? is_earnings_per_share_data = null;
        private Double? is_average_shares_diluted_eps = null;
        private Double? is_average_shares_basic_eps = null;
        private Double? is_eps_basic = null;
        private Double? is_eps_diluted = null;
        private Double? cfs_net_income = null;
        private Double? cfs_depreciation_depletion_amortization = null;
        private Double? cfs_other_non_cash_items = null;
        private Double? cfs_total_non_cash_items = null;
        private Double? cfs_deferred_income_taxes = null;
        private Double? cfs_total_changes_in_assets_liabilities = null;
        private Double? cfs_other_operating_activities = null;
        private Double? cfs_net_cash_from_operating_activities = null;
        private Double? cfs_cash_flow_investing = null;
        private Double? cfs_capital_expenditures = null;
        private Double? cfs_acquisitions_divestitures = null;
        private Double? cfs_investments = null;
        private Double? cfs_other_investing_activities = null;
        private Double? cfs_cash_flow_financing = null;
        private Double? cfs_net_cash_from_investing_activities = null;
        private Double? cfs_debt_issued = null;
        private Double? cfs_equity_issued = null;
        private Double? cfs_dividends_paid = null;
        private Double? cfs_other_financing_activities = null;
        private Double? cfs_net_cash_from_financing_activities = null;
        private Double? cfs_foreign_exchange_effects = null;
        private Double? cfs_net_change_in_cash_equivalents = null;
        private Double? cfs_cash_beginning_of_period = null;
        private Double? cfs_cash_end_of_period = null;
        private Double? fr_accruals = null;
        private Double? fr_altman_z_score = null;
        private Double? fr_asset_utilization = null;
        private Double? fr_beneish_m_score = null;
        private Double? fr_beta = null;
        private Double? fr_book_value = null;
        private Double? fr_book_value_per_share = null;
        private Double? fr_capital_expenditures = null;
        private Double? fr_cash_conversion_cycle = null;
        private Double? fr_cash_div_payout_ratio_ttm = null;
        private Double? fr_cash_financing = null;
        private Double? fr_cash_financing_ttm = null;
        private Double? fr_cash_investing = null;
        private Double? fr_cash_investing_ttm = null;
        private Double? fr_cash_operations = null;
        private Double? fr_cash_operations_ttm = null;
        private Double? fr_cash_and_equivalents = null;
        private Double? fr_cash_and_st_investments = null;
        private Double? fr_current_ratio = null;
        private Double? fr_days_inventory_outstanding = null;
        private Double? fr_days_payable_outstanding = null;
        private Double? fr_days_sales_outstanding = null;
        private Double? fr_debt_to_equity_ratio = null;
        private Double? fr_dividend = null;
        private Double? fr_dividend_yield = null;
        private Double? fr_ebitda_margin_ttm = null;
        private Double? fr_ebitda_ttm = null;
        private Double? fr_ev_ebit = null;
        private Double? fr_ev_ebitda = null;
        private Double? fr_ev_free_cash_flow = null;
        private Double? fr_ev_revenues = null;
        private Double? fr_earnings_per_share = null;
        private Double? fr_earnings_per_share_growth = null;
        private Double? fr_earnings_per_share_ttm = null;
        private Double? fr_earnings_yield = null;
        private Double? fr_effective_tax_rate_ttm = null;
        private Double? fr_enterprise_value = null;
        private Double? fr_expenses = null;
        private Double? fr_expenses_ttm = null;
        private Double? fr_free_cash_flow = null;
        private Double? fr_free_cash_flow_ttm = null;
        private Double? fr_free_cash_flow_yield = null;
        private Double? fr_fundamental_score = null;
        private Double? fr_gross_profit_margin = null;
        private Double? fr_gross_profit_ttm = null;
        private Double? fr_income_from_cont_ops = null;
        private Double? fr_interest_expense = null;
        private Double? fr_interest_income = null;
        private Double? fr_inventories = null;
        private Double? fr_inventory_turnover = null;
        private Double? fr_kz_index = null;
        private Double? fr_liabilities = null;
        private Double? fr_long_term_debt = null;
        private Double? fr_market_cap = null;
        private Double? fr_net_income = null;
        private Double? fr_net_income_ttm = null;
        private Double? fr_net_pp_e = null;
        private Double? fr_operating_earnings_yield = null;
        private Double? fr_operating_margin = null;
        private Double? fr_operating_margin_ttm = null;
        private Double? fr_operating_pe_ratio = null;
        private Double? fr_other_comprehensive_income = null;
        private Double? fr_pe_10 = null;
        private Double? fr_pe_ratio = null;
        private Double? fr_pe_value = null;
        private Double? fr_peg_ratio = null;
        private Double? fr_ps_value = null;
        private Double? fr_payout_ratio_ttm = null;
        private Double? fr_plowback_ratio = null;
        private Double? fr_price = null;
        private Double? fr_adjprice = null;
        private Double? fr_price_book_value = null;
        private Double? fr_price_sales_ratio = null;
        private Double? fr_price_tangible_book_value = null;
        private Double? fr_profit_margin = null;
        private Double? fr_r_d_expense = null;
        private Double? fr_receivables_turnover = null;
        private Double? fr_retained_earnings = null;
        private Double? fr_retained_earnings_growth = null;
        private Double? fr_return_on_assets = null;
        private Double? fr_return_on_equity = null;
        private Double? fr_return_on_invested_capital = null;
        private Double? fr_revenue_growth = null;
        private Double? fr_revenue_per_share_ttm = null;
        private Double? fr_revenues = null;
        private Double? fr_revenues_ttm = null;
        private Double? fr_sg_a_expense = null;
        private Double? fr_shareholders_equity = null;
        private Double? fr_shares_outstanding = null;
        private Double? fr_stock_buybacks = null;
        private Double? fr_tangible_book_value = null;
        private Double? fr_tangible_book_value_per_share = null;
        private Double? fr_tangible_common_equity_ratio = null;
        private Double? fr_times_interest_earned_ttm = null;
        private Double? fr_total_assets = null;
        private Double? fr_total_return_price = null;
        private Double? fr_valuation_historical_mult = null;
        private Double? fr_valuation_percentage = null;
        private Double? fr_value_score = null;
        private Double? fr_working_capital = null;
        private Double? fr_standard_deviation = null;
        private Double? fr_roic_growth_rate = null;
        private Double? fr_quick_ratio = null;
        private Double? fr_asset_coverage = null;
        private Double? fr_dscr = null;
        private Double? fr_debt_EBITDA = null;
        private Double? fr_eq_prc = null;
        private Double? fr_cash_flow_volatility = null;
        private Double? fr_turnover_ratio = null;
        private Double? fr_book_to_market = null;
        private Double? fr_earnings_to_price_ratio = null;
        private Double? fr_cash_flow_to_price_ratio = null;
        private Double? fr_sales_growth_ratio = null;
        private Double? fr_netIncomeTTM = null;
        private Double? fr_dividendsPaidTTM = null;
        private Double? fr_dividendTTM = null;
        private Double? fr_sharesTTM = null;
        private Double? fr_ebitTTM = null;
        private Double? fr_incomeTaxesTTM = null;
        private Double? fr_preTaxIncomeTTM = null;
        private Double? fr_contOpsEpsTTM = null;
        private Double? fr_operatingProfitTTM = null;
        private Double? fr_bookValuePerShareTTM = null;
        private Double? fr_tangibleBookValuePerShareTTM = null;
        private Double? fr_costRevenueTTM = null;
        private Double? fr_interestExpensesTTM = null;
        private Double? fr_debtTTM = null;
        private Double? fr_ebit = null;
        private Double? fr_ebitda = null;
        private Double? fr_interestPrc = null;
        private Double? fr_avgPayables2 = null;
        private Double? fr_avgReceivables2 = null;
        private Double? fr_avgInventory2 = null;
        private Double? fr_avgAssets5 = null;
        private Double? fr_avgInventory5 = null;
        private Double? fr_avgReceivables5 = null;
        private Double? fr_avgPayables5 = null;
        private Double? fr_avgEquity5 = null;
        private Double? fr_avgInvestedCapital5 = null;
        private Double? fr_stdCashFlowOperationsTTM = null;
        private Double? fr_cashFlowOperationsTTM1 = null;
        private Double? fr_cashFlowOperationsTTM2 = null;
        private Double? fr_cashFlowOperationsTTM3 = null;
        private Double? fr_cashFlowOperationsTTM4 = null;
        private Double? fr_cashFlowOperationsTTM5 = null;
        private Double? fr_avgEps10yTTM = null;
        private Double? fr_eps4ago = null;
        private Double? fr_retainedEarnings4ago = null;
        private Double? fr_revenue4ago = null;

        private Double? fr_ebitPrc = null;
        private Double? fr_cashPrc = null;
        private Double? fr_currentPrc = null;
        private Double? fr_goodwillPrc = null;
        private Double? fr_CurrentLPrc = null;
        private Double? fr_debtPrc = null;
        private Double? fr_liabilitiesPrc = null;

        public double? FrLiabilitiesPrc
        {
            get { return fr_liabilitiesPrc; }
            set { fr_liabilitiesPrc = value; }
        }

        public double? FrDebtPrc
        {
            get { return fr_debtPrc; }
            set { fr_debtPrc = value; }
        }

        public double? FrCurrentLPrc
        {
            get { return fr_CurrentLPrc; }
            set { fr_CurrentLPrc = value; }
        }

        public double? FrGoodwillPrc
        {
            get { return fr_goodwillPrc; }
            set { fr_goodwillPrc = value; }
        }

        public double? FrCurrentPrc
        {
            get { return fr_currentPrc; }
            set { fr_currentPrc = value; }
        }

        public double? FrCashPrc
        {
            get { return fr_cashPrc; }
            set { fr_cashPrc = value; }
        }

        public double? FrEbitPrc
        {
            get { return fr_ebitPrc; }
            set { fr_ebitPrc = value; }
        }


        public DateTime Kuupaev
        {
            get { return kuupaev; }
            set { kuupaev = value; }
        }

        public string BsSymbol
        {
            get { return bs_symbol; }
            set { bs_symbol = value; }
        }

        public double? BsCashShortTermInvestments
        {
            get { return bs_cash_short_term_investments; }
            set { bs_cash_short_term_investments = value; }
        }

        public double? BsReceivables
        {
            get { return bs_receivables; }
            set { bs_receivables = value; }
        }

        public double? BsPrepaidExpenses
        {
            get { return bs_prepaid_expenses; }
            set { bs_prepaid_expenses = value; }
        }

        public double? BsInventory
        {
            get { return bs_inventory; }
            set { bs_inventory = value; }
        }

        public double? BsOtherCurrentAssets
        {
            get { return bs_other_current_assets; }
            set { bs_other_current_assets = value; }
        }

        public double? BsTotalCurrentAssets
        {
            get { return bs_total_current_assets; }
            set { bs_total_current_assets = value; }
        }

        public double? BsGrossPropertyPlantEquipment
        {
            get { return bs_gross_property_plant_equipment; }
            set { bs_gross_property_plant_equipment = value; }
        }

        public double? BsAccumulatedDepreciation
        {
            get { return bs_accumulated_depreciation; }
            set { bs_accumulated_depreciation = value; }
        }

        public double? BsNetPropertyPlantEquipment
        {
            get { return bs_net_property_plant_equipment; }
            set { bs_net_property_plant_equipment = value; }
        }

        public double? BsLongTermInvestments
        {
            get { return bs_long_term_investments; }
            set { bs_long_term_investments = value; }
        }

        public double? BsGoodwillIntangibles
        {
            get { return bs_goodwill_intangibles; }
            set { bs_goodwill_intangibles = value; }
        }

        public double? BsOtherLongTermAssets
        {
            get { return bs_other_long_term_assets; }
            set { bs_other_long_term_assets = value; }
        }

        public double? BsTotalLongTermAssets
        {
            get { return bs_total_long_term_assets; }
            set { bs_total_long_term_assets = value; }
        }

        public double? BsTotalAssets
        {
            get { return bs_total_assets; }
            set { bs_total_assets = value; }
        }

        public double? BsLiabilities
        {
            get { return bs_liabilities; }
            set { bs_liabilities = value; }
        }

        public double? BsCurrentPortionOfLongTermDebt
        {
            get { return bs_current_portion_of_long_term_debt; }
            set { bs_current_portion_of_long_term_debt = value; }
        }

        public double? BsAccountsPayable
        {
            get { return bs_accounts_payable; }
            set { bs_accounts_payable = value; }
        }

        public double? BsAccruedExpenses
        {
            get { return bs_accrued_expenses; }
            set { bs_accrued_expenses = value; }
        }

        public double? BsDeferredRevenues
        {
            get { return bs_deferred_revenues; }
            set { bs_deferred_revenues = value; }
        }

        public double? BsOtherCurrentLiabilities
        {
            get { return bs_other_current_liabilities; }
            set { bs_other_current_liabilities = value; }
        }

        public double? BsTotalCurrentLiabilities
        {
            get { return bs_total_current_liabilities; }
            set { bs_total_current_liabilities = value; }
        }

        public double? BsTotalLongTermDebt
        {
            get { return bs_total_long_term_debt; }
            set { bs_total_long_term_debt = value; }
        }

        public double? BsShareholdersEquity
        {
            get { return bs_shareholders_equity; }
            set { bs_shareholders_equity = value; }
        }

        public double? BsDeferredIncomeTax
        {
            get { return bs_deferred_income_tax; }
            set { bs_deferred_income_tax = value; }
        }

        public double? BsMinorityInterest
        {
            get { return bs_minority_interest; }
            set { bs_minority_interest = value; }
        }

        public double? BsOtherLongTermLiabilities
        {
            get { return bs_other_long_term_liabilities; }
            set { bs_other_long_term_liabilities = value; }
        }

        public double? BsTotalLiabilities
        {
            get { return bs_total_liabilities; }
            set { bs_total_liabilities = value; }
        }

        public double? BsTotalLongTermLiabilities
        {
            get { return bs_total_long_term_liabilities; }
            set { bs_total_long_term_liabilities = value; }
        }

        public double? BsCommonSharesOutstanding
        {
            get { return bs_common_shares_outstanding; }
            set { bs_common_shares_outstanding = value; }
        }

        public double? BsPreferredStock
        {
            get { return bs_preferred_stock; }
            set { bs_preferred_stock = value; }
        }

        public double? BsCommonStockNet
        {
            get { return bs_common_stock_net; }
            set { bs_common_stock_net = value; }
        }

        public double? BsAdditionalPaidInCapital
        {
            get { return bs_additional_paid_in_capital; }
            set { bs_additional_paid_in_capital = value; }
        }

        public double? BsRetainedEarnings
        {
            get { return bs_retained_earnings; }
            set { bs_retained_earnings = value; }
        }

        public double? BsTreasuryStock
        {
            get { return bs_treasury_stock; }
            set { bs_treasury_stock = value; }
        }

        public double? BsOtherShareholdersEquity
        {
            get { return bs_other_shareholders_equity; }
            set { bs_other_shareholders_equity = value; }
        }

        public double? BsShareholdersEquity1
        {
            get { return bs_shareholders_equity1; }
            set { bs_shareholders_equity1 = value; }
        }

        public double? BsTotalLiabilitiesShareholdersEquity
        {
            get { return bs_total_liabilities_shareholders_equity; }
            set { bs_total_liabilities_shareholders_equity = value; }
        }

        public double? IsRevenue
        {
            get { return is_revenue; }
            set { is_revenue = value; }
        }

        public double? IsCostOfRevenue
        {
            get { return is_cost_of_revenue; }
            set { is_cost_of_revenue = value; }
        }

        public double? IsGrossProfit
        {
            get { return is_gross_profit; }
            set { is_gross_profit = value; }
        }

        public double? IsRdExpense
        {
            get { return is_rd_expense; }
            set { is_rd_expense = value; }
        }

        public double? IsSellingGeneralAdminExpense
        {
            get { return is_selling_general_admin_expense; }
            set { is_selling_general_admin_expense = value; }
        }

        public double? IsDepreciationAmortization
        {
            get { return is_depreciation_amortization; }
            set { is_depreciation_amortization = value; }
        }

        public double? IsOperatingInterestExpense
        {
            get { return is_operating_interest_expense; }
            set { is_operating_interest_expense = value; }
        }

        public double? IsOtherOperatingIncomeExpense
        {
            get { return is_other_operating_income_expense; }
            set { is_other_operating_income_expense = value; }
        }

        public double? IsTotalOperatingExpenses
        {
            get { return is_total_operating_expenses; }
            set { is_total_operating_expenses = value; }
        }

        public double? IsOperatingIncome
        {
            get { return is_operating_income; }
            set { is_operating_income = value; }
        }

        public double? IsNonOperatingIncome
        {
            get { return is_non_operating_income; }
            set { is_non_operating_income = value; }
        }

        public double? IsPretaxIncome
        {
            get { return is_pretax_income; }
            set { is_pretax_income = value; }
        }

        public double? IsProvisionForIncomeTaxes
        {
            get { return is_provision_for_income_taxes; }
            set { is_provision_for_income_taxes = value; }
        }

        public double? IsIncomeAfterTax
        {
            get { return is_income_after_tax; }
            set { is_income_after_tax = value; }
        }

        public double? IsMinorityInterest
        {
            get { return is_minority_interest; }
            set { is_minority_interest = value; }
        }

        public double? IsMinorityInterest1
        {
            get { return is_minority_interest1; }
            set { is_minority_interest1 = value; }
        }

        public double? IsEquityInAffiliates
        {
            get { return is_equity_in_affiliates; }
            set { is_equity_in_affiliates = value; }
        }

        public double? IsIncomeBeforeDiscOperations
        {
            get { return is_income_before_disc_operations; }
            set { is_income_before_disc_operations = value; }
        }

        public double? IsInvestmentGainsLosses
        {
            get { return is_investment_gains_losses; }
            set { is_investment_gains_losses = value; }
        }

        public double? IsOtherIncomeCharges
        {
            get { return is_other_income_charges; }
            set { is_other_income_charges = value; }
        }

        public double? IsIncomeFromDiscOperations
        {
            get { return is_income_from_disc_operations; }
            set { is_income_from_disc_operations = value; }
        }

        public double? IsNetIncome
        {
            get { return is_net_income; }
            set { is_net_income = value; }
        }

        public double? IsEarningsPerShareData
        {
            get { return is_earnings_per_share_data; }
            set { is_earnings_per_share_data = value; }
        }

        public double? IsAverageSharesDilutedEps
        {
            get { return is_average_shares_diluted_eps; }
            set { is_average_shares_diluted_eps = value; }
        }

        public double? IsAverageSharesBasicEps
        {
            get { return is_average_shares_basic_eps; }
            set { is_average_shares_basic_eps = value; }
        }

        public double? IsEpsBasic
        {
            get { return is_eps_basic; }
            set { is_eps_basic = value; }
        }

        public double? IsEpsDiluted
        {
            get { return is_eps_diluted; }
            set { is_eps_diluted = value; }
        }

        public double? CfsNetIncome
        {
            get { return cfs_net_income; }
            set { cfs_net_income = value; }
        }

        public double? CfsDepreciationDepletionAmortization
        {
            get { return cfs_depreciation_depletion_amortization; }
            set { cfs_depreciation_depletion_amortization = value; }
        }

        public double? CfsOtherNonCashItems
        {
            get { return cfs_other_non_cash_items; }
            set { cfs_other_non_cash_items = value; }
        }

        public double? CfsTotalNonCashItems
        {
            get { return cfs_total_non_cash_items; }
            set { cfs_total_non_cash_items = value; }
        }

        public double? CfsDeferredIncomeTaxes
        {
            get { return cfs_deferred_income_taxes; }
            set { cfs_deferred_income_taxes = value; }
        }

        public double? CfsTotalChangesInAssetsLiabilities
        {
            get { return cfs_total_changes_in_assets_liabilities; }
            set { cfs_total_changes_in_assets_liabilities = value; }
        }

        public double? CfsOtherOperatingActivities
        {
            get { return cfs_other_operating_activities; }
            set { cfs_other_operating_activities = value; }
        }

        public double? CfsNetCashFromOperatingActivities
        {
            get { return cfs_net_cash_from_operating_activities; }
            set { cfs_net_cash_from_operating_activities = value; }
        }

        public double? CfsCashFlowInvesting
        {
            get { return cfs_cash_flow_investing; }
            set { cfs_cash_flow_investing = value; }
        }

        public double? CfsAcquisitionsDivestitures
        {
            get { return cfs_acquisitions_divestitures; }
            set { cfs_acquisitions_divestitures = value; }
        }

        public double? CfsCapitalExpenditures
        {
            get { return cfs_capital_expenditures; }
            set { cfs_capital_expenditures = value; }
        }

        public double? CfsInvestments
        {
            get { return cfs_investments; }
            set { cfs_investments = value; }
        }

        public double? CfsOtherInvestingActivities
        {
            get { return cfs_other_investing_activities; }
            set { cfs_other_investing_activities = value; }
        }

        public double? CfsCashFlowFinancing
        {
            get { return cfs_cash_flow_financing; }
            set { cfs_cash_flow_financing = value; }
        }

        public double? CfsNetCashFromInvestingActivities
        {
            get { return cfs_net_cash_from_investing_activities; }
            set { cfs_net_cash_from_investing_activities = value; }
        }

        public double? CfsDebtIssued
        {
            get { return cfs_debt_issued; }
            set { cfs_debt_issued = value; }
        }

        public double? CfsEquityIssued
        {
            get { return cfs_equity_issued; }
            set { cfs_equity_issued = value; }
        }

        public double? CfsDividendsPaid
        {
            get { return cfs_dividends_paid; }
            set { cfs_dividends_paid = value; }
        }

        public double? CfsNetCashFromFinancingActivities
        {
            get { return cfs_net_cash_from_financing_activities; }
            set { cfs_net_cash_from_financing_activities = value; }
        }

        public double? CfsOtherFinancingActivities
        {
            get { return cfs_other_financing_activities; }
            set { cfs_other_financing_activities = value; }
        }

        public double? CfsForeignExchangeEffects
        {
            get { return cfs_foreign_exchange_effects; }
            set { cfs_foreign_exchange_effects = value; }
        }

        public double? CfsNetChangeInCashEquivalents
        {
            get { return cfs_net_change_in_cash_equivalents; }
            set { cfs_net_change_in_cash_equivalents = value; }
        }

        public double? CfsCashBeginningOfPeriod
        {
            get { return cfs_cash_beginning_of_period; }
            set { cfs_cash_beginning_of_period = value; }
        }

        public double? CfsCashEndOfPeriod
        {
            get { return cfs_cash_end_of_period; }
            set { cfs_cash_end_of_period = value; }
        }

        public double? FrAccruals
        {
            get { return fr_accruals; }
            set { fr_accruals = value; }
        }

        public double? FrAltmanZScore
        {
            get { return fr_altman_z_score; }
            set { fr_altman_z_score = value; }
        }

        public double? FrAssetUtilization
        {
            get { return fr_asset_utilization; }
            set { fr_asset_utilization = value; }
        }

        public double? FrBeneishMScore
        {
            get { return fr_beneish_m_score; }
            set { fr_beneish_m_score = value; }
        }

        public double? FrBeta
        {
            get { return fr_beta; }
            set { fr_beta = value; }
        }

        public double? FrBookValue
        {
            get { return fr_book_value; }
            set { fr_book_value = value; }
        }

        public double? FrBookValuePerShare
        {
            get { return fr_book_value_per_share; }
            set { fr_book_value_per_share = value; }
        }

        public double? FrCapitalExpenditures
        {
            get { return fr_capital_expenditures; }
            set { fr_capital_expenditures = value; }
        }

        public double? FrCashConversionCycle
        {
            get { return fr_cash_conversion_cycle; }
            set { fr_cash_conversion_cycle = value; }
        }

        public double? FrCashDivPayoutRatioTtm
        {
            get { return fr_cash_div_payout_ratio_ttm; }
            set { fr_cash_div_payout_ratio_ttm = value; }
        }

        public double? FrCashFinancing
        {
            get { return fr_cash_financing; }
            set { fr_cash_financing = value; }
        }

        public double? FrCashFinancingTtm
        {
            get { return fr_cash_financing_ttm; }
            set { fr_cash_financing_ttm = value; }
        }

        public double? FrCashInvesting
        {
            get { return fr_cash_investing; }
            set { fr_cash_investing = value; }
        }

        public double? FrCashInvestingTtm
        {
            get { return fr_cash_investing_ttm; }
            set { fr_cash_investing_ttm = value; }
        }

        public double? FrCashOperations
        {
            get { return fr_cash_operations; }
            set { fr_cash_operations = value; }
        }

        public double? FrCashOperationsTtm
        {
            get { return fr_cash_operations_ttm; }
            set { fr_cash_operations_ttm = value; }
        }

        public double? FrCashAndEquivalents
        {
            get { return fr_cash_and_equivalents; }
            set { fr_cash_and_equivalents = value; }
        }

        public double? FrCashAndStInvestments
        {
            get { return fr_cash_and_st_investments; }
            set { fr_cash_and_st_investments = value; }
        }

        public double? FrCurrentRatio
        {
            get { return fr_current_ratio; }
            set { fr_current_ratio = value; }
        }

        public double? FrDaysInventoryOutstanding
        {
            get { return fr_days_inventory_outstanding; }
            set { fr_days_inventory_outstanding = value; }
        }

        public double? FrDaysPayableOutstanding
        {
            get { return fr_days_payable_outstanding; }
            set { fr_days_payable_outstanding = value; }
        }

        public double? FrDaysSalesOutstanding
        {
            get { return fr_days_sales_outstanding; }
            set { fr_days_sales_outstanding = value; }
        }

        public double? FrDebtToEquityRatio
        {
            get { return fr_debt_to_equity_ratio; }
            set { fr_debt_to_equity_ratio = value; }
        }

        public double? FrDividend
        {
            get { return fr_dividend; }
            set { fr_dividend = value; }
        }

        public double? FrDividendYield
        {
            get { return fr_dividend_yield; }
            set { fr_dividend_yield = value; }
        }

        public double? FrEbitdaMarginTtm
        {
            get { return fr_ebitda_margin_ttm; }
            set { fr_ebitda_margin_ttm = value; }
        }

        public double? FrEvEbit
        {
            get { return fr_ev_ebit; }
            set { fr_ev_ebit = value; }
        }

        public double? FrEbitdaTtm
        {
            get { return fr_ebitda_ttm; }
            set { fr_ebitda_ttm = value; }
        }

        public double? FrEvEbitda
        {
            get { return fr_ev_ebitda; }
            set { fr_ev_ebitda = value; }
        }

        public double? FrEvFreeCashFlow
        {
            get { return fr_ev_free_cash_flow; }
            set { fr_ev_free_cash_flow = value; }
        }

        public double? FrEvRevenues
        {
            get { return fr_ev_revenues; }
            set { fr_ev_revenues = value; }
        }

        public double? FrEarningsPerShare
        {
            get { return fr_earnings_per_share; }
            set { fr_earnings_per_share = value; }
        }

        public double? FrEarningsPerShareGrowth
        {
            get { return fr_earnings_per_share_growth; }
            set { fr_earnings_per_share_growth = value; }
        }

        public double? FrEarningsYield
        {
            get { return fr_earnings_yield; }
            set { fr_earnings_yield = value; }
        }

        public double? FrEarningsPerShareTtm
        {
            get { return fr_earnings_per_share_ttm; }
            set { fr_earnings_per_share_ttm = value; }
        }

        public double? FrEffectiveTaxRateTtm
        {
            get { return fr_effective_tax_rate_ttm; }
            set { fr_effective_tax_rate_ttm = value; }
        }

        public double? FrEnterpriseValue
        {
            get { return fr_enterprise_value; }
            set { fr_enterprise_value = value; }
        }

        public double? FrExpenses
        {
            get { return fr_expenses; }
            set { fr_expenses = value; }
        }

        public double? FrExpensesTtm
        {
            get { return fr_expenses_ttm; }
            set { fr_expenses_ttm = value; }
        }

        public double? FrFreeCashFlow
        {
            get { return fr_free_cash_flow; }
            set { fr_free_cash_flow = value; }
        }

        public double? FrFreeCashFlowTtm
        {
            get { return fr_free_cash_flow_ttm; }
            set { fr_free_cash_flow_ttm = value; }
        }

        public double? FrFreeCashFlowYield
        {
            get { return fr_free_cash_flow_yield; }
            set { fr_free_cash_flow_yield = value; }
        }

        public double? FrFundamentalScore
        {
            get { return fr_fundamental_score; }
            set { fr_fundamental_score = value; }
        }

        public double? FrGrossProfitMargin
        {
            get { return fr_gross_profit_margin; }
            set { fr_gross_profit_margin = value; }
        }

        public double? FrGrossProfitTtm
        {
            get { return fr_gross_profit_ttm; }
            set { fr_gross_profit_ttm = value; }
        }

        public double? FrIncomeFromContOps
        {
            get { return fr_income_from_cont_ops; }
            set { fr_income_from_cont_ops = value; }
        }

        public double? FrInterestExpense
        {
            get { return fr_interest_expense; }
            set { fr_interest_expense = value; }
        }

        public double? FrInterestIncome
        {
            get { return fr_interest_income; }
            set { fr_interest_income = value; }
        }

        public double? FrInventories
        {
            get { return fr_inventories; }
            set { fr_inventories = value; }
        }

        public double? FrInventoryTurnover
        {
            get { return fr_inventory_turnover; }
            set { fr_inventory_turnover = value; }
        }

        public double? FrKzIndex
        {
            get { return fr_kz_index; }
            set { fr_kz_index = value; }
        }

        public double? FrLiabilities
        {
            get { return fr_liabilities; }
            set { fr_liabilities = value; }
        }

        public double? FrLongTermDebt
        {
            get { return fr_long_term_debt; }
            set { fr_long_term_debt = value; }
        }

        public double? FrMarketCap
        {
            get { return fr_market_cap; }
            set { fr_market_cap = value; }
        }

        public double? FrNetIncome
        {
            get { return fr_net_income; }
            set { fr_net_income = value; }
        }

        public double? FrNetIncomeTtm
        {
            get { return fr_net_income_ttm; }
            set { fr_net_income_ttm = value; }
        }

        public double? FrNetPpE
        {
            get { return fr_net_pp_e; }
            set { fr_net_pp_e = value; }
        }

        public double? FrOperatingEarningsYield
        {
            get { return fr_operating_earnings_yield; }
            set { fr_operating_earnings_yield = value; }
        }

        public double? FrOperatingMargin
        {
            get { return fr_operating_margin; }
            set { fr_operating_margin = value; }
        }

        public double? FrOperatingMarginTtm
        {
            get { return fr_operating_margin_ttm; }
            set { fr_operating_margin_ttm = value; }
        }

        public double? FrOperatingPeRatio
        {
            get { return fr_operating_pe_ratio; }
            set { fr_operating_pe_ratio = value; }
        }

        public double? FrOtherComprehensiveIncome
        {
            get { return fr_other_comprehensive_income; }
            set { fr_other_comprehensive_income = value; }
        }

        public double? FrPe10
        {
            get { return fr_pe_10; }
            set { fr_pe_10 = value; }
        }

        public double? FrPeRatio
        {
            get { return fr_pe_ratio; }
            set { fr_pe_ratio = value; }
        }

        public double? FrPeValue
        {
            get { return fr_pe_value; }
            set { fr_pe_value = value; }
        }

        public double? FrPegRatio
        {
            get { return fr_peg_ratio; }
            set { fr_peg_ratio = value; }
        }

        public double? FrPsValue
        {
            get { return fr_ps_value; }
            set { fr_ps_value = value; }
        }

        public double? FrPayoutRatioTtm
        {
            get { return fr_payout_ratio_ttm; }
            set { fr_payout_ratio_ttm = value; }
        }

        public double? FrPlowbackRatio
        {
            get { return fr_plowback_ratio; }
            set { fr_plowback_ratio = value; }
        }

        public double? FrPrice
        {
            get { return fr_price; }
            set { fr_price = value; }
        }

        public double? FrAdjPrice
        {
            get { return fr_adjprice; }
            set { fr_adjprice = value; }
        }

        public double? FrPriceBookValue
        {
            get { return fr_price_book_value; }
            set { fr_price_book_value = value; }
        }

        public double? FrPriceSalesRatio
        {
            get { return fr_price_sales_ratio; }
            set { fr_price_sales_ratio = value; }
        }

        public double? FrPriceTangibleBookValue
        {
            get { return fr_price_tangible_book_value; }
            set { fr_price_tangible_book_value = value; }
        }

        public double? FrProfitMargin
        {
            get { return fr_profit_margin; }
            set { fr_profit_margin = value; }
        }

        public double? FrRDExpense
        {
            get { return fr_r_d_expense; }
            set { fr_r_d_expense = value; }
        }

        public double? FrReceivablesTurnover
        {
            get { return fr_receivables_turnover; }
            set { fr_receivables_turnover = value; }
        }

        public double? FrRetainedEarnings
        {
            get { return fr_retained_earnings; }
            set { fr_retained_earnings = value; }
        }

        public double? FrRetainedEarningsGrowth
        {
            get { return fr_retained_earnings_growth; }
            set { fr_retained_earnings_growth = value; }
        }

        public double? FrReturnOnAssets
        {
            get { return fr_return_on_assets; }
            set { fr_return_on_assets = value; }
        }

        public double? FrReturnOnEquity
        {
            get { return fr_return_on_equity; }
            set { fr_return_on_equity = value; }
        }

        public double? FrReturnOnInvestedCapital
        {
            get { return fr_return_on_invested_capital; }
            set { fr_return_on_invested_capital = value; }
        }

        public double? FrRevenueGrowth
        {
            get { return fr_revenue_growth; }
            set { fr_revenue_growth = value; }
        }

        public double? FrRevenuePerShareTtm
        {
            get { return fr_revenue_per_share_ttm; }
            set { fr_revenue_per_share_ttm = value; }
        }

        public double? FrRevenues
        {
            get { return fr_revenues; }
            set { fr_revenues = value; }
        }

        public double? FrRevenuesTtm
        {
            get { return fr_revenues_ttm; }
            set { fr_revenues_ttm = value; }
        }

        public double? FrSgAExpense
        {
            get { return fr_sg_a_expense; }
            set { fr_sg_a_expense = value; }
        }

        public double? FrShareholdersEquity
        {
            get { return fr_shareholders_equity; }
            set { fr_shareholders_equity = value; }
        }

        public double? FrSharesOutstanding
        {
            get { return fr_shares_outstanding; }
            set { fr_shares_outstanding = value; }
        }

        public double? FrStockBuybacks
        {
            get { return fr_stock_buybacks; }
            set { fr_stock_buybacks = value; }
        }

        public double? FrTangibleBookValue
        {
            get { return fr_tangible_book_value; }
            set { fr_tangible_book_value = value; }
        }

        public double? FrTangibleBookValuePerShare
        {
            get { return fr_tangible_book_value_per_share; }
            set { fr_tangible_book_value_per_share = value; }
        }

        public double? FrTangibleCommonEquityRatio
        {
            get { return fr_tangible_common_equity_ratio; }
            set { fr_tangible_common_equity_ratio = value; }
        }

        public double? FrTimesInterestEarnedTtm
        {
            get { return fr_times_interest_earned_ttm; }
            set { fr_times_interest_earned_ttm = value; }
        }

        public double? FrTotalAssets
        {
            get { return fr_total_assets; }
            set { fr_total_assets = value; }
        }

        public double? FrTotalReturnPrice
        {
            get { return fr_total_return_price; }
            set { fr_total_return_price = value; }
        }

        public double? FrValuationHistoricalMult
        {
            get { return fr_valuation_historical_mult; }
            set { fr_valuation_historical_mult = value; }
        }

        public double? FrValuationPercentage
        {
            get { return fr_valuation_percentage; }
            set { fr_valuation_percentage = value; }
        }

        public double? FrValueScore
        {
            get { return fr_value_score; }
            set { fr_value_score = value; }
        }

        public double? FrWorkingCapital
        {
            get { return fr_working_capital; }
            set { fr_working_capital = value; }
        }

        public double? FrStandardDeviation
        {
            get { return fr_standard_deviation; }
            set { fr_standard_deviation = value; }
        }

        public double? FrRoicGrowthRate
        {
            get { return fr_roic_growth_rate; }
            set { fr_roic_growth_rate = value; }
        }

        public double? FrQuickRatio
        {
            get { return fr_quick_ratio; }
            set { fr_quick_ratio = value; }
        }

        public double? FrAssetCoverage
        {
            get { return fr_asset_coverage; }
            set { fr_asset_coverage = value; }
        }

        public double? FrDscr
        {
            get { return fr_dscr; }
            set { fr_dscr = value; }
        }

        public double? FrDebtEbitda
        {
            get { return fr_debt_EBITDA; }
            set { fr_debt_EBITDA = value; }
        }

        public double? FrEqPrc
        {
            get { return fr_eq_prc; }
            set { fr_eq_prc = value; }
        }

        public double? FrCashFlowVolatility
        {
            get { return fr_cash_flow_volatility; }
            set { fr_cash_flow_volatility = value; }
        }

        public double? FrTurnoverRatio
        {
            get { return fr_turnover_ratio; }
            set { fr_turnover_ratio = value; }
        }

        public double? FrBookToMarket
        {
            get { return fr_book_to_market; }
            set { fr_book_to_market = value; }
        }

        public double? FrEarningsToPriceRatio
        {
            get { return fr_earnings_to_price_ratio; }
            set { fr_earnings_to_price_ratio = value; }
        }

        public double? FrCashFlowToPriceRatio
        {
            get { return fr_cash_flow_to_price_ratio; }
            set { fr_cash_flow_to_price_ratio = value; }
        }

        public double? FrSalesGrowthRatio
        {
            get { return fr_sales_growth_ratio; }
            set { fr_sales_growth_ratio = value; }
        }

        public double? FrDividendsPaidTtm
        {
            get { return fr_dividendsPaidTTM; }
            set { fr_dividendsPaidTTM = value; }
        }

        public double? FrNetIncomeTtm1
        {
            get { return fr_netIncomeTTM; }
            set { fr_netIncomeTTM = value; }
        }

        public double? FrDividendTtm
        {
            get { return fr_dividendTTM; }
            set { fr_dividendTTM = value; }
        }

        public double? FrSharesTtm
        {
            get { return fr_sharesTTM; }
            set { fr_sharesTTM = value; }
        }

        public double? FrEbitTtm
        {
            get { return fr_ebitTTM; }
            set { fr_ebitTTM = value; }
        }

        public double? FrIncomeTaxesTtm
        {
            get { return fr_incomeTaxesTTM; }
            set { fr_incomeTaxesTTM = value; }
        }

        public double? FrPreTaxIncomeTtm
        {
            get { return fr_preTaxIncomeTTM; }
            set { fr_preTaxIncomeTTM = value; }
        }

        public double? FrContOpsEpsTtm
        {
            get { return fr_contOpsEpsTTM; }
            set { fr_contOpsEpsTTM = value; }
        }

        public double? FrOperatingProfitTtm
        {
            get { return fr_operatingProfitTTM; }
            set { fr_operatingProfitTTM = value; }
        }

        public double? FrBookValuePerShareTtm
        {
            get { return fr_bookValuePerShareTTM; }
            set { fr_bookValuePerShareTTM = value; }
        }

        public double? FrTangibleBookValuePerShareTtm
        {
            get { return fr_tangibleBookValuePerShareTTM; }
            set { fr_tangibleBookValuePerShareTTM = value; }
        }

        public double? FrCostRevenueTtm
        {
            get { return fr_costRevenueTTM; }
            set { fr_costRevenueTTM = value; }
        }

        public double? FrInterestExpensesTtm
        {
            get { return fr_interestExpensesTTM; }
            set { fr_interestExpensesTTM = value; }
        }

        public double? FrDebtTtm
        {
            get { return fr_debtTTM; }
            set { fr_debtTTM = value; }
        }

        public double? FrEbit
        {
            get { return fr_ebit; }
            set { fr_ebit = value; }
        }

        public double? FrEbitda
        {
            get { return fr_ebitda; }
            set { fr_ebitda = value; }
        }

        public double? FrInterestPrc
        {
            get { return fr_interestPrc; }
            set { fr_interestPrc = value; }
        }

        public double? FrAvgPayables2
        {
            get { return fr_avgPayables2; }
            set { fr_avgPayables2 = value; }
        }

        public double? FrAvgReceivables2
        {
            get { return fr_avgReceivables2; }
            set { fr_avgReceivables2 = value; }
        }

        public double? FrAvgInventory2
        {
            get { return fr_avgInventory2; }
            set { fr_avgInventory2 = value; }
        }

        public double? FrAvgAssets5
        {
            get { return fr_avgAssets5; }
            set { fr_avgAssets5 = value; }
        }

        public double? FrAvgInventory5
        {
            get { return fr_avgInventory5; }
            set { fr_avgInventory5 = value; }
        }

        public double? FrAvgReceivables5
        {
            get { return fr_avgReceivables5; }
            set { fr_avgReceivables5 = value; }
        }

        public double? FrAvgPayables5
        {
            get { return fr_avgPayables5; }
            set { fr_avgPayables5 = value; }
        }

        public double? FrAvgEquity5
        {
            get { return fr_avgEquity5; }
            set { fr_avgEquity5 = value; }
        }

        public double? FrAvgInvestedCapital5
        {
            get { return fr_avgInvestedCapital5; }
            set { fr_avgInvestedCapital5 = value; }
        }

        public double? FrCashFlowOperationsTtm1
        {
            get { return fr_cashFlowOperationsTTM1; }
            set { fr_cashFlowOperationsTTM1 = value; }
        }

        public double? FrStdCashFlowOperationsTtm
        {
            get { return fr_stdCashFlowOperationsTTM; }
            set { fr_stdCashFlowOperationsTTM = value; }
        }

        public double? FrCashFlowOperationsTtm2
        {
            get { return fr_cashFlowOperationsTTM2; }
            set { fr_cashFlowOperationsTTM2 = value; }
        }

        public double? FrCashFlowOperationsTtm3
        {
            get { return fr_cashFlowOperationsTTM3; }
            set { fr_cashFlowOperationsTTM3 = value; }
        }

        public double? FrCashFlowOperationsTtm4
        {
            get { return fr_cashFlowOperationsTTM4; }
            set { fr_cashFlowOperationsTTM4 = value; }
        }

        public double? FrCashFlowOperationsTtm5
        {
            get { return fr_cashFlowOperationsTTM5; }
            set { fr_cashFlowOperationsTTM5 = value; }
        }

        public double? FrAvgEps10yTtm
        {
            get { return fr_avgEps10yTTM; }
            set { fr_avgEps10yTTM = value; }
        }

        public double? FrEps4Ago
        {
            get { return fr_eps4ago; }
            set { fr_eps4ago = value; }
        }

        public double? FrRetainedEarnings4Ago
        {
            get { return fr_retainedEarnings4ago; }
            set { fr_retainedEarnings4ago = value; }
        }

        public double? FrRevenue4Ago
        {
            get { return fr_revenue4ago; }
            set { fr_revenue4ago = value; }
        }

        public FinData(XElement item)
        {


            foreach (var el in item.Elements("column"))
            {
                string attri = "";
                try
                {
                    attri = el.Attribute("name").Value;
                }
                catch (NullReferenceException)
                {

                }


                if (!attri.Equals(""))
                {
                    switch (attri)
                    {
                        case "is_kuupaev":
                            DateTime.TryParseExact(el.Value, "yyyy-MM-dd", new CultureInfo("en-US"),
                                                   DateTimeStyles.None, out this.kuupaev);
                            break;
                        case "is_symbol":
                            this.bs_symbol = el.Value;
                            break;
                        case "bs_cash_short_term_investments":
                            this.bs_cash_short_term_investments = TryParseNullable(el.Value);
                            break;
                        case "bs_receivables":
                            this.bs_receivables = TryParseNullable(el.Value);
                            break;
                        case "bs_inventory":
                            this.bs_inventory = TryParseNullable(el.Value);
                            break;
                        case "bs_prepaid_expenses":
                            this.bs_prepaid_expenses = TryParseNullable(el.Value);
                            break;
                        case "bs_other_current_assets":
                            this.bs_other_current_assets = TryParseNullable(el.Value);
                            break;
                        case "bs_total_current_assets":
                            this.bs_total_current_assets = TryParseNullable(el.Value);
                            break;
                        case "bs_gross_property_plant_equipment":
                            this.bs_gross_property_plant_equipment = TryParseNullable(el.Value);
                            break;
                        case "bs_accumulated_depreciation":
                            this.bs_accumulated_depreciation = TryParseNullable(el.Value);
                            break;
                        case "bs_net_property_plant_equipment":
                            this.bs_net_property_plant_equipment = TryParseNullable(el.Value);
                            break;
                        case "bs_long_term_investments":
                            this.bs_long_term_investments = TryParseNullable(el.Value);
                            break;
                        case "bs_goodwill_intangibles":
                            this.bs_goodwill_intangibles = TryParseNullable(el.Value);
                            break;
                        case "bs_other_long_term_assets":
                            this.bs_other_long_term_assets = TryParseNullable(el.Value);
                            break;
                        case "bs_total_long_term_assets":
                            this.bs_total_long_term_assets = TryParseNullable(el.Value);
                            break;
                        case "bs_total_assets":
                            this.bs_total_assets = TryParseNullable(el.Value);
                            break;
                        case "bs_liabilities":
                            this.bs_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_current_portion_of_long_term_debt":
                            this.bs_current_portion_of_long_term_debt = TryParseNullable(el.Value);
                            break;
                        case "bs_accounts_payable":
                            this.bs_accounts_payable = TryParseNullable(el.Value);
                            break;
                        case "bs_accrued_expenses":
                            this.bs_accrued_expenses = TryParseNullable(el.Value);
                            break;
                        case "bs_deferred_revenues":
                            this.bs_deferred_revenues = TryParseNullable(el.Value);
                            break;
                        case "bs_other_current_liabilities":
                            this.bs_other_current_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_total_current_liabilities":
                            this.bs_total_current_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_total_long_term_debt":
                            this.bs_total_long_term_debt = TryParseNullable(el.Value);
                            break;
                        case "bs_shareholders_equity":
                            this.bs_shareholders_equity = TryParseNullable(el.Value);
                            break;
                        case "bs_deferred_income_tax":
                            this.bs_deferred_income_tax = TryParseNullable(el.Value);
                            break;
                        case "bs_minority_interest":
                            this.bs_minority_interest = TryParseNullable(el.Value);
                            break;
                        case "bs_other_long_term_liabilities":
                            this.bs_other_long_term_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_total_long_term_liabilities":
                            this.bs_total_long_term_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_total_liabilities":
                            this.bs_total_liabilities = TryParseNullable(el.Value);
                            break;
                        case "bs_common_shares_outstanding":
                            this.bs_common_shares_outstanding = TryParseNullable(el.Value);
                            break;
                        case "bs_preferred_stock":
                            this.bs_preferred_stock = TryParseNullable(el.Value);
                            break;
                        case "bs_common_stock_net":
                            this.bs_common_stock_net = TryParseNullable(el.Value);
                            break;
                        case "bs_additional_paid_in_capital":
                            this.bs_additional_paid_in_capital = TryParseNullable(el.Value);
                            break;
                        case "bs_retained_earnings":
                            this.bs_retained_earnings = TryParseNullable(el.Value);
                            break;
                        case "bs_treasury_stock":
                            this.bs_treasury_stock = TryParseNullable(el.Value);
                            break;
                        case "bs_other_shareholders_equity":
                            this.bs_other_shareholders_equity = TryParseNullable(el.Value);
                            break;
                        case "bs_shareholders_equity1":
                            this.bs_shareholders_equity1 = TryParseNullable(el.Value);
                            break;
                        case "bs_total_liabilities_shareholders_equity":
                            this.bs_total_liabilities_shareholders_equity = TryParseNullable(el.Value);
                            break;
                        case "is_revenue":
                            this.is_revenue = TryParseNullable(el.Value);
                            break;
                        case "is_cost_of_revenue":
                            this.is_cost_of_revenue = TryParseNullable(el.Value);
                            break;
                        case "is_gross_profit":
                            this.is_gross_profit = TryParseNullable(el.Value);
                            break;
                        case "is_rd_expense":
                            this.is_rd_expense = TryParseNullable(el.Value);
                            break;
                        case "is_selling_general_admin_expense":
                            this.is_selling_general_admin_expense = TryParseNullable(el.Value);
                            break;
                        case "is_depreciation_amortization":
                            this.is_depreciation_amortization = TryParseNullable(el.Value);
                            break;
                        case "is_operating_interest_expense":
                            this.is_operating_interest_expense = TryParseNullable(el.Value);
                            break;
                        case "is_other_operating_income_expense":
                            this.is_other_operating_income_expense = TryParseNullable(el.Value);
                            break;
                        case "is_total_operating_expenses":
                            this.is_total_operating_expenses = TryParseNullable(el.Value);
                            break;
                        case "is_operating_income":
                            this.is_operating_income = TryParseNullable(el.Value);
                            break;
                        case "is_non_operating_income":
                            this.is_non_operating_income = TryParseNullable(el.Value);
                            break;
                        case "is_pretax_income":
                            this.is_pretax_income = TryParseNullable(el.Value);
                            break;
                        case "is_provision_for_income_taxes":
                            this.is_provision_for_income_taxes = TryParseNullable(el.Value);
                            break;
                        case "is_income_after_tax":
                            this.is_income_after_tax = TryParseNullable(el.Value);
                            break;
                        case "is_minority_interest":
                            this.is_minority_interest = TryParseNullable(el.Value);
                            break;
                        case "is_minority_interest1":
                            this.is_minority_interest1 = TryParseNullable(el.Value);
                            break;
                        case "is_equity_in_affiliates":
                            this.is_equity_in_affiliates = TryParseNullable(el.Value);
                            break;
                        case "is_income_before_disc_operations":
                            this.is_income_before_disc_operations = TryParseNullable(el.Value);
                            break;
                        case "is_investment_gains_losses":
                            this.is_investment_gains_losses = TryParseNullable(el.Value);
                            break;
                        case "is_other_income_charges":
                            this.is_other_income_charges = TryParseNullable(el.Value);
                            break;
                        case "is_income_from_disc_operations":
                            this.is_income_from_disc_operations = TryParseNullable(el.Value);
                            break;
                        case "is_net_income":
                            this.is_net_income = TryParseNullable(el.Value);
                            break;
                        case "is_earnings_per_share_data":
                            this.is_earnings_per_share_data = TryParseNullable(el.Value);
                            break;
                        case "is_average_shares_diluted_eps":
                            this.is_average_shares_diluted_eps = TryParseNullable(el.Value);
                            break;
                        case "is_average_shares_basic_eps":
                            this.is_average_shares_basic_eps = TryParseNullable(el.Value);
                            break;
                        case "is_eps_basic":
                            this.is_eps_basic = TryParseNullable(el.Value);
                            break;
                        case "is_eps_diluted":
                            this.is_eps_diluted = TryParseNullable(el.Value);
                            break;
                        case "cfs_net_income":
                            this.cfs_net_income = TryParseNullable(el.Value);
                            break;
                        case "cfs_depreciation_depletion_amortization":
                            this.cfs_depreciation_depletion_amortization = TryParseNullable(el.Value);
                            break;
                        case "cfs_other_non_cash_items":
                            this.cfs_other_non_cash_items = TryParseNullable(el.Value);
                            break;
                        case "cfs_total_non_cash_items":
                            this.cfs_total_non_cash_items = TryParseNullable(el.Value);
                            break;
                        case "cfs_deferred_income_taxes":
                            this.cfs_deferred_income_taxes = TryParseNullable(el.Value);
                            break;
                        case "cfs_total_changes_in_assets_liabilities":
                            this.cfs_total_changes_in_assets_liabilities = TryParseNullable(el.Value);
                            break;
                        case "cfs_other_operating_activities":
                            this.cfs_other_operating_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_net_cash_from_operating_activities":
                            this.cfs_net_cash_from_operating_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_cash_flow_investing":
                            this.cfs_cash_flow_investing = TryParseNullable(el.Value);
                            break;
                        case "cfs_capital_expenditures":
                            this.cfs_capital_expenditures = TryParseNullable(el.Value);
                            break;
                        case "cfs_acquisitions_divestitures":
                            this.cfs_acquisitions_divestitures = TryParseNullable(el.Value);
                            break;
                        case "cfs_investments":
                            this.cfs_investments = TryParseNullable(el.Value);
                            break;
                        case "cfs_other_investing_activities":
                            this.cfs_other_investing_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_cash_flow_financing":
                            this.cfs_cash_flow_financing = TryParseNullable(el.Value);
                            break;
                        case "cfs_net_cash_from_investing_activities":
                            this.cfs_net_cash_from_investing_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_debt_issued":
                            this.cfs_debt_issued = TryParseNullable(el.Value);
                            break;
                        case "cfs_equity_issued":
                            this.cfs_equity_issued = TryParseNullable(el.Value);
                            break;
                        case "cfs_dividends_paid":
                            this.cfs_dividends_paid = TryParseNullable(el.Value);
                            break;
                        case "cfs_other_financing_activities":
                            this.cfs_other_financing_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_net_cash_from_financing_activities":
                            this.cfs_net_cash_from_financing_activities = TryParseNullable(el.Value);
                            break;
                        case "cfs_foreign_exchange_effects":
                            this.cfs_foreign_exchange_effects = TryParseNullable(el.Value);
                            break;
                        case "cfs_net_change_in_cash_equivalents":
                            this.cfs_net_change_in_cash_equivalents = TryParseNullable(el.Value);
                            break;
                        case "cfs_cash_beginning_of_period":
                            this.cfs_cash_beginning_of_period = TryParseNullable(el.Value);
                            break;
                        case "cfs_cash_end_of_period":
                            this.cfs_cash_end_of_period = TryParseNullable(el.Value);
                            break;
                        case "fr_accruals":
                            this.fr_accruals = TryParseNullable(el.Value);
                            break;
                        case "fr_altman_z_score":
                            this.fr_altman_z_score = TryParseNullable(el.Value);
                            break;
                        case "fr_asset_utilization":
                            this.fr_asset_utilization = TryParseNullable(el.Value);
                            break;
                        case "fr_beneish_m_score":
                            this.fr_beneish_m_score = TryParseNullable(el.Value);
                            break;
                        case "fr_beta":
                            this.fr_beta = TryParseNullable(el.Value);
                            break;
                        case "fr_book_value":
                            this.fr_book_value = TryParseNullable(el.Value);
                            break;
                        case "fr_book_value_per_share":
                            this.fr_book_value_per_share = TryParseNullable(el.Value);
                            break;
                        case "fr_capital_expenditures":
                            this.fr_capital_expenditures = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_conversion_cycle":
                            this.fr_cash_conversion_cycle = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_div_payout_ratio_ttm":
                            this.fr_cash_div_payout_ratio_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_financing":
                            this.fr_cash_financing = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_financing_ttm":
                            this.fr_cash_financing_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_investing":
                            this.fr_cash_investing = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_investing_ttm":
                            this.fr_cash_investing_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_operations":
                            this.fr_cash_operations = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_operations_ttm":
                            this.fr_cash_operations_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_and_equivalents":
                            this.fr_cash_and_equivalents = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_and_st_investments":
                            this.fr_cash_and_st_investments = TryParseNullable(el.Value);
                            break;
                        case "fr_current_ratio":
                            this.fr_current_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_days_inventory_outstanding":
                            this.fr_days_inventory_outstanding = TryParseNullable(el.Value);
                            break;
                        case "fr_days_payable_outstanding":
                            this.fr_days_payable_outstanding = TryParseNullable(el.Value);
                            break;
                        case "fr_days_sales_outstanding":
                            this.fr_days_sales_outstanding = TryParseNullable(el.Value);
                            break;
                        case "fr_debt_to_equity_ratio":
                            this.fr_debt_to_equity_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_dividend":
                            this.fr_dividend = TryParseNullable(el.Value);
                            break;
                        case "fr_dividend_yield":
                            this.fr_dividend_yield = TryParseNullable(el.Value);
                            break;
                        case "fr_ebitda_margin_ttm":
                            this.fr_ebitda_margin_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_ebitda_ttm":
                            this.fr_ebitda_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_ev_ebit":
                            this.fr_ev_ebit = TryParseNullable(el.Value);
                            break;
                        case "fr_ev_ebitda":
                            this.fr_ev_ebitda = TryParseNullable(el.Value);
                            break;
                        case "fr_ev_free_cash_flow":
                            this.fr_ev_free_cash_flow = TryParseNullable(el.Value);
                            break;
                        case "fr_ev_revenues":
                            this.fr_ev_revenues = TryParseNullable(el.Value);
                            break;
                        case "fr_earnings_per_share":
                            this.fr_earnings_per_share = TryParseNullable(el.Value);
                            break;
                        case "fr_earnings_per_share_growth":
                            this.fr_earnings_per_share_growth = TryParseNullable(el.Value);
                            break;
                        case "fr_earnings_per_share_ttm":
                            this.fr_earnings_per_share_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_earnings_yield":
                            this.fr_earnings_yield = TryParseNullable(el.Value);
                            break;
                        case "fr_effective_tax_rate_ttm":
                            this.fr_effective_tax_rate_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_enterprise_value":
                            this.fr_enterprise_value = TryParseNullable(el.Value);
                            break;
                        case "fr_expenses":
                            this.fr_expenses = TryParseNullable(el.Value);
                            break;
                        case "fr_expenses_ttm":
                            this.fr_expenses_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_free_cash_flow":
                            this.fr_free_cash_flow = TryParseNullable(el.Value);
                            break;
                        case "fr_free_cash_flow_ttm":
                            this.fr_free_cash_flow_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_free_cash_flow_yield":
                            this.fr_free_cash_flow_yield = TryParseNullable(el.Value);
                            break;
                        case "fr_fundamental_score":
                            this.fr_fundamental_score = TryParseNullable(el.Value);
                            break;
                        case "fr_gross_profit_margin":
                            this.fr_gross_profit_margin = TryParseNullable(el.Value);
                            break;
                        case "fr_gross_profit_ttm":
                            this.fr_gross_profit_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_income_from_cont_ops":
                            this.fr_income_from_cont_ops = TryParseNullable(el.Value);
                            break;
                        case "fr_interest_expense":
                            this.fr_interest_expense = TryParseNullable(el.Value);
                            break;
                        case "fr_interest_income":
                            this.fr_interest_income = TryParseNullable(el.Value);
                            break;
                        case "fr_inventories":
                            this.fr_inventories = TryParseNullable(el.Value);
                            break;
                        case "fr_inventory_turnover":
                            this.fr_inventory_turnover = TryParseNullable(el.Value);
                            break;
                        case "fr_kz_index":
                            this.fr_kz_index = TryParseNullable(el.Value);
                            break;
                        case "fr_liabilities":
                            this.fr_liabilities = TryParseNullable(el.Value);
                            break;
                        case "fr_long_term_debt":
                            this.fr_long_term_debt = TryParseNullable(el.Value);
                            break;
                        case "fr_market_cap":
                            this.fr_market_cap = TryParseNullable(el.Value);
                            break;
                        case "fr_net_income":
                            this.fr_net_income = TryParseNullable(el.Value);
                            break;
                        case "fr_net_income_ttm":
                            this.fr_net_income_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_net_pp_e":
                            this.fr_net_pp_e = TryParseNullable(el.Value);
                            break;
                        case "fr_operating_earnings_yield":
                            this.fr_operating_earnings_yield = TryParseNullable(el.Value);
                            break;
                        case "fr_operating_margin":
                            this.fr_operating_margin = TryParseNullable(el.Value);
                            break;
                        case "fr_operating_margin_ttm":
                            this.fr_operating_margin_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_operating_pe_ratio":
                            this.fr_operating_pe_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_other_comprehensive_income":
                            this.fr_other_comprehensive_income = TryParseNullable(el.Value);
                            break;
                        case "fr_pe_10":
                            this.fr_pe_10 = TryParseNullable(el.Value);
                            break;
                        case "fr_pe_ratio":
                            this.fr_pe_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_pe_value":
                            this.fr_pe_value = TryParseNullable(el.Value);
                            break;
                        case "fr_peg_ratio":
                            this.fr_peg_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_ps_value":
                            this.fr_ps_value = TryParseNullable(el.Value);
                            break;
                        case "fr_payout_ratio_ttm":
                            this.fr_payout_ratio_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_plowback_ratio":
                            this.fr_plowback_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_price":
                            this.fr_price = TryParseNullable(el.Value);
                            break;
                        case "fr_price_book_value":
                            this.fr_price_book_value = TryParseNullable(el.Value);
                            break;
                        case "fr_price_sales_ratio":
                            this.fr_price_sales_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_price_tangible_book_value":
                            this.fr_price_tangible_book_value = TryParseNullable(el.Value);
                            break;
                        case "fr_profit_margin":
                            this.fr_profit_margin = TryParseNullable(el.Value);
                            break;
                        case "fr_r_d_expense":
                            this.fr_r_d_expense = TryParseNullable(el.Value);
                            break;
                        case "fr_receivables_turnover":
                            this.fr_receivables_turnover = TryParseNullable(el.Value);
                            break;
                        case "fr_retained_earnings":
                            this.fr_retained_earnings = TryParseNullable(el.Value);
                            break;
                        case "fr_retained_earnings_growth":
                            this.fr_retained_earnings_growth = TryParseNullable(el.Value);
                            break;
                        case "fr_return_on_assets":
                            this.fr_return_on_assets = TryParseNullable(el.Value);
                            break;
                        case "fr_return_on_equity":
                            this.fr_return_on_equity = TryParseNullable(el.Value);
                            break;
                        case "fr_return_on_invested_capital":
                            this.fr_return_on_invested_capital = TryParseNullable(el.Value);
                            break;
                        case "fr_revenue_growth":
                            this.fr_revenue_growth = TryParseNullable(el.Value);
                            break;
                        case "fr_revenue_per_share_ttm":
                            this.fr_revenue_per_share_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_revenues":
                            this.fr_revenues = TryParseNullable(el.Value);
                            break;
                        case "fr_revenues_ttm":
                            this.fr_revenues_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_sg_a_expense":
                            this.fr_sg_a_expense = TryParseNullable(el.Value);
                            break;
                        case "fr_shareholders_equity":
                            this.fr_shareholders_equity = TryParseNullable(el.Value);
                            break;
                        case "fr_shares_outstanding":
                            this.fr_shares_outstanding = TryParseNullable(el.Value);
                            break;
                        case "fr_stock_buybacks":
                            this.fr_stock_buybacks = TryParseNullable(el.Value);
                            break;
                        case "fr_tangible_book_value":
                            this.fr_tangible_book_value = TryParseNullable(el.Value);
                            break;
                        case "fr_tangible_book_value_per_share":
                            this.fr_tangible_book_value_per_share = TryParseNullable(el.Value);
                            break;
                        case "fr_tangible_common_equity_ratio":
                            this.fr_tangible_common_equity_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_times_interest_earned_ttm":
                            this.fr_times_interest_earned_ttm = TryParseNullable(el.Value);
                            break;
                        case "fr_total_assets":
                            this.fr_total_assets = TryParseNullable(el.Value);
                            break;
                        case "fr_total_return_price":
                            this.fr_total_return_price = TryParseNullable(el.Value);
                            break;
                        case "fr_valuation_historical_mult":
                            this.fr_valuation_historical_mult = TryParseNullable(el.Value);
                            break;
                        case "fr_valuation_percentage":
                            this.fr_valuation_percentage = TryParseNullable(el.Value);
                            break;
                        case "fr_value_score":
                            this.fr_value_score = TryParseNullable(el.Value);
                            break;
                        case "fr_working_capital":
                            this.fr_working_capital = TryParseNullable(el.Value);
                            break;
                        case "fr_standard_deviation":
                            this.fr_standard_deviation = TryParseNullable(el.Value);
                            break;
                        case "fr_roic_growth_rate":
                            this.fr_roic_growth_rate = TryParseNullable(el.Value);
                            break;
                        case "fr_quick_ratio":
                            this.fr_quick_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_asset_coverage":
                            this.fr_asset_coverage = TryParseNullable(el.Value);
                            break;
                        case "fr_dscr":
                            this.fr_dscr = TryParseNullable(el.Value);
                            break;
                        case "fr_debt_EBITDA":
                            this.fr_debt_EBITDA = TryParseNullable(el.Value);
                            break;
                        case "fr_eq_prc":
                            this.fr_eq_prc = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_flow_volatility":
                            this.fr_cash_flow_volatility = TryParseNullable(el.Value);
                            break;
                        case "fr_turnover_ratio":
                            this.fr_turnover_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_book_to_market":
                            this.fr_book_to_market = TryParseNullable(el.Value);
                            break;
                        case "fr_earnings_to_price_ratio":
                            this.fr_earnings_to_price_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_cash_flow_to_price_ratio":
                            this.fr_cash_flow_to_price_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_sales_growth_ratio":
                            this.fr_sales_growth_ratio = TryParseNullable(el.Value);
                            break;
                        case "fr_netIncomeTTM":
                            this.fr_netIncomeTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_dividendsPaidTTM":
                            this.fr_dividendsPaidTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_dividendTTM":
                            this.fr_dividendTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_sharesTTM":
                            this.fr_sharesTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_ebitTTM":
                            this.fr_ebitTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_incomeTaxesTTM":
                            this.fr_incomeTaxesTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_preTaxIncomeTTM":
                            this.fr_preTaxIncomeTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_contOpsEpsTTM":
                            this.fr_contOpsEpsTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_operatingProfitTTM":
                            this.fr_operatingProfitTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_bookValuePerShareTTM":
                            this.fr_bookValuePerShareTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_tangibleBookValuePerShareTTM":
                            this.fr_tangibleBookValuePerShareTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_costRevenueTTM":
                            this.fr_costRevenueTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_interestExpensesTTM":
                            this.fr_interestExpensesTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_debtTTM":
                            this.fr_debtTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_ebit":
                            this.fr_ebit = TryParseNullable(el.Value);
                            break;
                        case "fr_ebitda":
                            this.fr_ebitda = TryParseNullable(el.Value);
                            break;
                        case "fr_interestPrc":
                            this.fr_interestPrc = TryParseNullable(el.Value);
                            break;
                        case "fr_avgPayables2":
                            this.fr_avgPayables2 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgReceivables2":
                            this.fr_avgReceivables2 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgInventory2":
                            this.fr_avgInventory2 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgAssets5":
                            this.fr_avgAssets5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgInventory5":
                            this.fr_avgInventory5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgReceivables5":
                            this.fr_avgReceivables5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgPayables5":
                            this.fr_avgPayables5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgEquity5":
                            this.fr_avgEquity5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgInvestedCapital5":
                            this.fr_avgInvestedCapital5 = TryParseNullable(el.Value);
                            break;
                        case "fr_stdCashFlowOperationsTTM":
                            this.fr_stdCashFlowOperationsTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_cashFlowOperationsTTM1":
                            this.fr_cashFlowOperationsTTM1 = TryParseNullable(el.Value);
                            break;
                        case "fr_cashFlowOperationsTTM2":
                            this.fr_cashFlowOperationsTTM2 = TryParseNullable(el.Value);
                            break;
                        case "fr_cashFlowOperationsTTM3":
                            this.fr_cashFlowOperationsTTM3 = TryParseNullable(el.Value);
                            break;
                        case "fr_cashFlowOperationsTTM4":
                            this.fr_cashFlowOperationsTTM4 = TryParseNullable(el.Value);
                            break;
                        case "fr_cashFlowOperationsTTM5":
                            this.fr_cashFlowOperationsTTM5 = TryParseNullable(el.Value);
                            break;
                        case "fr_avgEps10yTTM":
                            this.fr_avgEps10yTTM = TryParseNullable(el.Value);
                            break;
                        case "fr_eps4ago":
                            this.fr_eps4ago = TryParseNullable(el.Value);
                            break;
                        case "fr_retainedEarnings4ago":
                            this.fr_retainedEarnings4ago = TryParseNullable(el.Value);
                            break;
                        case "fr_revenue4ago":
                            this.fr_revenue4ago = TryParseNullable(el.Value);
                            break;

                    } // switch
                } // if attri.equals("");

            } // foreach

        }

        public Double? TryParseNullable(string val)
        {
            Double outValue;
            return Double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out outValue) ? (Double?)outValue : null;
        }

        public void CopyValues(FinData fd)
        {
            if (fd.BsCashShortTermInvestments != null) { this.bs_cash_short_term_investments = fd.BsCashShortTermInvestments; }
            if (fd.BsReceivables != null) { this.bs_receivables = fd.BsReceivables; }
            if (fd.BsInventory != null) { this.bs_inventory = fd.BsInventory; }
            if (fd.BsPrepaidExpenses != null) { this.bs_prepaid_expenses = fd.BsPrepaidExpenses; }
            if (fd.BsOtherCurrentAssets != null) { this.bs_other_current_assets = fd.BsOtherCurrentAssets; }
            if (fd.BsTotalCurrentAssets != null) { this.bs_total_current_assets = fd.BsTotalCurrentAssets; }
            if (fd.BsGrossPropertyPlantEquipment != null) { this.bs_gross_property_plant_equipment = fd.BsGrossPropertyPlantEquipment; }
            if (fd.BsAccumulatedDepreciation != null) { this.bs_accumulated_depreciation = fd.BsAccumulatedDepreciation; }
            if (fd.BsNetPropertyPlantEquipment != null) { this.bs_net_property_plant_equipment = fd.BsNetPropertyPlantEquipment; }
            if (fd.BsLongTermInvestments != null) { this.bs_long_term_investments = fd.BsLongTermInvestments; }
            if (fd.BsGoodwillIntangibles != null) { this.bs_goodwill_intangibles = fd.BsGoodwillIntangibles; }
            if (fd.BsOtherLongTermAssets != null) { this.bs_other_long_term_assets = fd.BsOtherLongTermAssets; }
            if (fd.BsTotalLongTermAssets != null) { this.bs_total_long_term_assets = fd.BsTotalLongTermAssets; }
            if (fd.BsTotalAssets != null) { this.bs_total_assets = fd.BsTotalAssets; }
            if (fd.BsLiabilities != null) { this.bs_liabilities = fd.BsLiabilities; }
            if (fd.BsCurrentPortionOfLongTermDebt != null) { this.bs_current_portion_of_long_term_debt = fd.BsCurrentPortionOfLongTermDebt; }
            if (fd.BsAccountsPayable != null) { this.bs_accounts_payable = fd.BsAccountsPayable; }
            if (fd.BsAccruedExpenses != null) { this.bs_accrued_expenses = fd.BsAccruedExpenses; }
            if (fd.BsDeferredRevenues != null) { this.bs_deferred_revenues = fd.BsDeferredRevenues; }
            if (fd.BsOtherCurrentLiabilities != null) { this.bs_other_current_liabilities = fd.BsOtherCurrentLiabilities; }
            if (fd.BsTotalCurrentLiabilities != null) { this.bs_total_current_liabilities = fd.BsTotalCurrentLiabilities; }
            if (fd.BsTotalLongTermDebt != null) { this.bs_total_long_term_debt = fd.BsTotalLongTermDebt; }
            if (fd.BsShareholdersEquity != null) { this.bs_shareholders_equity = fd.BsShareholdersEquity; }
            if (fd.BsDeferredIncomeTax != null) { this.bs_deferred_income_tax = fd.BsDeferredIncomeTax; }
            if (fd.BsMinorityInterest != null) { this.bs_minority_interest = fd.BsMinorityInterest; }
            if (fd.BsOtherLongTermLiabilities != null) { this.bs_other_long_term_liabilities = fd.BsOtherLongTermLiabilities; }
            if (fd.BsTotalLongTermLiabilities != null) { this.bs_total_long_term_liabilities = fd.BsTotalLongTermLiabilities; }
            if (fd.BsTotalLiabilities != null) { this.bs_total_liabilities = fd.BsTotalLiabilities; }
            if (fd.BsCommonSharesOutstanding != null) { this.bs_common_shares_outstanding = fd.BsCommonSharesOutstanding; }
            if (fd.BsPreferredStock != null) { this.bs_preferred_stock = fd.BsPreferredStock; }
            if (fd.BsCommonStockNet != null) { this.bs_common_stock_net = fd.BsCommonStockNet; }
            if (fd.BsAdditionalPaidInCapital != null) { this.bs_additional_paid_in_capital = fd.BsAdditionalPaidInCapital; }
            if (fd.BsRetainedEarnings != null) { this.bs_retained_earnings = fd.BsRetainedEarnings; }
            if (fd.BsTreasuryStock != null) { this.bs_treasury_stock = fd.BsTreasuryStock; }
            if (fd.BsOtherShareholdersEquity != null) { this.bs_other_shareholders_equity = fd.BsOtherShareholdersEquity; }
            if (fd.BsShareholdersEquity1 != null) { this.bs_shareholders_equity1 = fd.BsShareholdersEquity1; }
            if (fd.BsTotalLiabilitiesShareholdersEquity != null) { this.bs_total_liabilities_shareholders_equity = fd.BsTotalLiabilitiesShareholdersEquity; }
            if (fd.IsRevenue != null) { this.is_revenue = fd.IsRevenue; }
            if (fd.IsCostOfRevenue != null) { this.is_cost_of_revenue = fd.IsCostOfRevenue; }
            if (fd.IsGrossProfit != null) { this.is_gross_profit = fd.IsGrossProfit; }
            if (fd.IsRdExpense != null) { this.is_rd_expense = fd.IsRdExpense; }
            if (fd.IsSellingGeneralAdminExpense != null) { this.is_selling_general_admin_expense = fd.IsSellingGeneralAdminExpense; }
            if (fd.IsDepreciationAmortization != null) { this.is_depreciation_amortization = fd.IsDepreciationAmortization; }
            if (fd.IsOperatingInterestExpense != null) { this.is_operating_interest_expense = fd.IsOperatingInterestExpense; }
            if (fd.IsOtherOperatingIncomeExpense != null) { this.is_other_operating_income_expense = fd.IsOtherOperatingIncomeExpense; }
            if (fd.IsTotalOperatingExpenses != null) { this.is_total_operating_expenses = fd.IsTotalOperatingExpenses; }
            if (fd.IsOperatingIncome != null) { this.is_operating_income = fd.IsOperatingIncome; }
            if (fd.IsNonOperatingIncome != null) { this.is_non_operating_income = fd.IsNonOperatingIncome; }
            if (fd.IsPretaxIncome != null) { this.is_pretax_income = fd.IsPretaxIncome; }
            if (fd.IsProvisionForIncomeTaxes != null) { this.is_provision_for_income_taxes = fd.IsProvisionForIncomeTaxes; }
            if (fd.IsIncomeAfterTax != null) { this.is_income_after_tax = fd.IsIncomeAfterTax; }
            if (fd.IsMinorityInterest != null) { this.is_minority_interest = fd.IsMinorityInterest; }
            if (fd.IsMinorityInterest1 != null) { this.is_minority_interest1 = fd.IsMinorityInterest1; }
            if (fd.IsEquityInAffiliates != null) { this.is_equity_in_affiliates = fd.IsEquityInAffiliates; }
            if (fd.IsIncomeBeforeDiscOperations != null) { this.is_income_before_disc_operations = fd.IsIncomeBeforeDiscOperations; }
            if (fd.IsInvestmentGainsLosses != null) { this.is_investment_gains_losses = fd.IsInvestmentGainsLosses; }
            if (fd.IsOtherIncomeCharges != null) { this.is_other_income_charges = fd.IsOtherIncomeCharges; }
            if (fd.IsIncomeFromDiscOperations != null) { this.is_income_from_disc_operations = fd.IsIncomeFromDiscOperations; }
            if (fd.IsNetIncome != null) { this.is_net_income = fd.IsNetIncome; }
            if (fd.IsEarningsPerShareData != null) { this.is_earnings_per_share_data = fd.IsEarningsPerShareData; }
            if (fd.IsAverageSharesDilutedEps != null) { this.is_average_shares_diluted_eps = fd.IsAverageSharesDilutedEps; }
            if (fd.IsAverageSharesBasicEps != null) { this.is_average_shares_basic_eps = fd.IsAverageSharesBasicEps; }
            if (fd.IsEpsBasic != null) { this.is_eps_basic = fd.IsEpsBasic; }
            if (fd.IsEpsDiluted != null) { this.is_eps_diluted = fd.IsEpsDiluted; }
            if (fd.CfsNetIncome != null) { this.cfs_net_income = fd.CfsNetIncome; }
            if (fd.CfsDepreciationDepletionAmortization != null) { this.cfs_depreciation_depletion_amortization = fd.CfsDepreciationDepletionAmortization; }
            if (fd.CfsOtherNonCashItems != null) { this.cfs_other_non_cash_items = fd.CfsOtherNonCashItems; }
            if (fd.CfsTotalNonCashItems != null) { this.cfs_total_non_cash_items = fd.CfsTotalNonCashItems; }
            if (fd.CfsDeferredIncomeTaxes != null) { this.cfs_deferred_income_taxes = fd.CfsDeferredIncomeTaxes; }
            if (fd.CfsTotalChangesInAssetsLiabilities != null) { this.cfs_total_changes_in_assets_liabilities = fd.CfsTotalChangesInAssetsLiabilities; }
            if (fd.CfsOtherOperatingActivities != null) { this.cfs_other_operating_activities = fd.CfsOtherOperatingActivities; }
            if (fd.CfsNetCashFromOperatingActivities != null) { this.cfs_net_cash_from_operating_activities = fd.CfsNetCashFromOperatingActivities; }
            if (fd.CfsCashFlowInvesting != null) { this.cfs_cash_flow_investing = fd.CfsCashFlowInvesting; }
            if (fd.CfsCapitalExpenditures != null) { this.cfs_capital_expenditures = fd.CfsCapitalExpenditures; }
            if (fd.CfsAcquisitionsDivestitures != null) { this.cfs_acquisitions_divestitures = fd.CfsAcquisitionsDivestitures; }
            if (fd.CfsInvestments != null) { this.cfs_investments = fd.CfsInvestments; }
            if (fd.CfsOtherInvestingActivities != null) { this.cfs_other_investing_activities = fd.CfsOtherInvestingActivities; }
            if (fd.CfsCashFlowFinancing != null) { this.cfs_cash_flow_financing = fd.CfsCashFlowFinancing; }
            if (fd.CfsNetCashFromInvestingActivities != null) { this.cfs_net_cash_from_investing_activities = fd.CfsNetCashFromInvestingActivities; }
            if (fd.CfsDebtIssued != null) { this.cfs_debt_issued = fd.CfsDebtIssued; }
            if (fd.CfsEquityIssued != null) { this.cfs_equity_issued = fd.CfsEquityIssued; }
            if (fd.CfsDividendsPaid != null) { this.cfs_dividends_paid = fd.CfsDividendsPaid; }
            if (fd.CfsOtherFinancingActivities != null) { this.cfs_other_financing_activities = fd.CfsOtherFinancingActivities; }
            if (fd.CfsNetCashFromFinancingActivities != null) { this.cfs_net_cash_from_financing_activities = fd.CfsNetCashFromFinancingActivities; }
            if (fd.CfsForeignExchangeEffects != null) { this.cfs_foreign_exchange_effects = fd.CfsForeignExchangeEffects; }
            if (fd.CfsNetChangeInCashEquivalents != null) { this.cfs_net_change_in_cash_equivalents = fd.CfsNetChangeInCashEquivalents; }
            if (fd.CfsCashBeginningOfPeriod != null) { this.cfs_cash_beginning_of_period = fd.CfsCashBeginningOfPeriod; }
            if (fd.CfsCashEndOfPeriod != null) { this.cfs_cash_end_of_period = fd.CfsCashEndOfPeriod; }
            if (fd.FrAccruals != null) { this.fr_accruals = fd.FrAccruals; }
            if (fd.FrAltmanZScore != null) { this.fr_altman_z_score = fd.FrAltmanZScore; }
            if (fd.FrAssetUtilization != null) { this.fr_asset_utilization = fd.FrAssetUtilization; }
            if (fd.FrBeneishMScore != null) { this.fr_beneish_m_score = fd.FrBeneishMScore; }
            if (fd.FrBeta != null) { this.fr_beta = fd.FrBeta; }
            if (fd.FrBookValue != null) { this.fr_book_value = fd.FrBookValue; }
            if (fd.FrBookValuePerShare != null) { this.fr_book_value_per_share = fd.FrBookValuePerShare; }
            if (fd.FrCapitalExpenditures != null) { this.fr_capital_expenditures = fd.FrCapitalExpenditures; }
            if (fd.FrCashConversionCycle != null) { this.fr_cash_conversion_cycle = fd.FrCashConversionCycle; }
            if (fd.FrCashDivPayoutRatioTtm != null) { this.fr_cash_div_payout_ratio_ttm = fd.FrCashDivPayoutRatioTtm; }
            if (fd.FrCashFinancing != null) { this.fr_cash_financing = fd.FrCashFinancing; }
            if (fd.FrCashFinancingTtm != null) { this.fr_cash_financing_ttm = fd.FrCashFinancingTtm; }
            if (fd.FrCashInvesting != null) { this.fr_cash_investing = fd.FrCashInvesting; }
            if (fd.FrCashInvestingTtm != null) { this.fr_cash_investing_ttm = fd.FrCashInvestingTtm; }
            if (fd.FrCashOperations != null) { this.fr_cash_operations = fd.FrCashOperations; }
            if (fd.FrCashOperationsTtm != null) { this.fr_cash_operations_ttm = fd.FrCashOperationsTtm; }
            if (fd.FrCashAndEquivalents != null) { this.fr_cash_and_equivalents = fd.FrCashAndEquivalents; }
            if (fd.FrCashAndStInvestments != null) { this.fr_cash_and_st_investments = fd.FrCashAndStInvestments; }
            if (fd.FrCurrentRatio != null) { this.fr_current_ratio = fd.FrCurrentRatio; }
            if (fd.FrDaysInventoryOutstanding != null) { this.fr_days_inventory_outstanding = fd.FrDaysInventoryOutstanding; }
            if (fd.FrDaysPayableOutstanding != null) { this.fr_days_payable_outstanding = fd.FrDaysPayableOutstanding; }
            if (fd.FrDaysSalesOutstanding != null) { this.fr_days_sales_outstanding = fd.FrDaysSalesOutstanding; }
            if (fd.FrDebtToEquityRatio != null) { this.fr_debt_to_equity_ratio = fd.FrDebtToEquityRatio; }
            if (fd.FrDividend != null) { this.fr_dividend = fd.FrDividend; }
            if (fd.FrDividendYield != null) { this.fr_dividend_yield = fd.FrDividendYield; }
            if (fd.FrEbitdaMarginTtm != null) { this.fr_ebitda_margin_ttm = fd.FrEbitdaMarginTtm; }
            if (fd.FrEbitdaTtm != null) { this.fr_ebitda_ttm = fd.FrEbitdaTtm; }
            if (fd.FrEvEbit != null) { this.fr_ev_ebit = fd.FrEvEbit; }
            if (fd.FrEvEbitda != null) { this.fr_ev_ebitda = fd.FrEvEbitda; }
            if (fd.FrEvFreeCashFlow != null) { this.fr_ev_free_cash_flow = fd.FrEvFreeCashFlow; }
            if (fd.FrEvRevenues != null) { this.fr_ev_revenues = fd.FrEvRevenues; }
            if (fd.FrEarningsPerShare != null) { this.fr_earnings_per_share = fd.FrEarningsPerShare; }
            if (fd.FrEarningsPerShareGrowth != null) { this.fr_earnings_per_share_growth = fd.FrEarningsPerShareGrowth; }
            if (fd.FrEarningsPerShareTtm != null) { this.fr_earnings_per_share_ttm = fd.FrEarningsPerShareTtm; }
            if (fd.FrEarningsYield != null) { this.fr_earnings_yield = fd.FrEarningsYield; }
            if (fd.FrEffectiveTaxRateTtm != null) { this.fr_effective_tax_rate_ttm = fd.FrEffectiveTaxRateTtm; }
            if (fd.FrEnterpriseValue != null) { this.fr_enterprise_value = fd.FrEnterpriseValue; }
            if (fd.FrExpenses != null) { this.fr_expenses = fd.FrExpenses; }
            if (fd.FrExpensesTtm != null) { this.fr_expenses_ttm = fd.FrExpensesTtm; }
            if (fd.FrFreeCashFlow != null) { this.fr_free_cash_flow = fd.FrFreeCashFlow; }
            if (fd.FrFreeCashFlowTtm != null) { this.fr_free_cash_flow_ttm = fd.FrFreeCashFlowTtm; }
            if (fd.FrFreeCashFlowYield != null) { this.fr_free_cash_flow_yield = fd.FrFreeCashFlowYield; }
            if (fd.FrFundamentalScore != null) { this.fr_fundamental_score = fd.FrFundamentalScore; }
            if (fd.FrGrossProfitMargin != null) { this.fr_gross_profit_margin = fd.FrGrossProfitMargin; }
            if (fd.FrGrossProfitTtm != null) { this.fr_gross_profit_ttm = fd.FrGrossProfitTtm; }
            if (fd.FrIncomeFromContOps != null) { this.fr_income_from_cont_ops = fd.FrIncomeFromContOps; }
            if (fd.FrInterestExpense != null) { this.fr_interest_expense = fd.FrInterestExpense; }
            if (fd.FrInterestIncome != null) { this.fr_interest_income = fd.FrInterestIncome; }
            if (fd.FrInventories != null) { this.fr_inventories = fd.FrInventories; }
            if (fd.FrInventoryTurnover != null) { this.fr_inventory_turnover = fd.FrInventoryTurnover; }
            if (fd.FrKzIndex != null) { this.fr_kz_index = fd.FrKzIndex; }
            if (fd.FrLiabilities != null) { this.fr_liabilities = fd.FrLiabilities; }
            if (fd.FrLongTermDebt != null) { this.fr_long_term_debt = fd.FrLongTermDebt; }
            if (fd.FrMarketCap != null) { this.fr_market_cap = fd.FrMarketCap; }
            if (fd.FrNetIncome != null) { this.fr_net_income = fd.FrNetIncome; }
            if (fd.FrNetIncomeTtm != null) { this.fr_net_income_ttm = fd.FrNetIncomeTtm; }
            if (fd.FrNetPpE != null) { this.fr_net_pp_e = fd.FrNetPpE; }
            if (fd.FrOperatingEarningsYield != null) { this.fr_operating_earnings_yield = fd.FrOperatingEarningsYield; }
            if (fd.FrOperatingMargin != null) { this.fr_operating_margin = fd.FrOperatingMargin; }
            if (fd.FrOperatingMarginTtm != null) { this.fr_operating_margin_ttm = fd.FrOperatingMarginTtm; }
            if (fd.FrOperatingPeRatio != null) { this.fr_operating_pe_ratio = fd.FrOperatingPeRatio; }
            if (fd.FrOtherComprehensiveIncome != null) { this.fr_other_comprehensive_income = fd.FrOtherComprehensiveIncome; }
            if (fd.FrPe10 != null) { this.fr_pe_10 = fd.FrPe10; }
            if (fd.FrPeRatio != null) { this.fr_pe_ratio = fd.FrPeRatio; }
            if (fd.FrPeValue != null) { this.fr_pe_value = fd.FrPeValue; }
            if (fd.FrPegRatio != null) { this.fr_peg_ratio = fd.FrPegRatio; }
            if (fd.FrPsValue != null) { this.fr_ps_value = fd.FrPsValue; }
            if (fd.FrPayoutRatioTtm != null) { this.fr_payout_ratio_ttm = fd.FrPayoutRatioTtm; }
            if (fd.FrPlowbackRatio != null) { this.fr_plowback_ratio = fd.FrPlowbackRatio; }
            if (fd.FrPrice != null) { this.fr_price = fd.FrPrice; }
            if (fd.FrAdjPrice != null) { this.fr_adjprice = fd.FrAdjPrice; }
            if (fd.FrPriceBookValue != null) { this.fr_price_book_value = fd.FrPriceBookValue; }
            if (fd.FrPriceSalesRatio != null) { this.fr_price_sales_ratio = fd.FrPriceSalesRatio; }
            if (fd.FrPriceTangibleBookValue != null) { this.fr_price_tangible_book_value = fd.FrPriceTangibleBookValue; }
            if (fd.FrProfitMargin != null) { this.fr_profit_margin = fd.FrProfitMargin; }
            if (fd.FrRDExpense != null) { this.fr_r_d_expense = fd.FrRDExpense; }
            if (fd.FrReceivablesTurnover != null) { this.fr_receivables_turnover = fd.FrReceivablesTurnover; }
            if (fd.FrRetainedEarnings != null) { this.fr_retained_earnings = fd.FrRetainedEarnings; }
            if (fd.FrRetainedEarningsGrowth != null) { this.fr_retained_earnings_growth = fd.FrRetainedEarningsGrowth; }
            if (fd.FrReturnOnAssets != null) { this.fr_return_on_assets = fd.FrReturnOnAssets; }
            if (fd.FrReturnOnEquity != null) { this.fr_return_on_equity = fd.FrReturnOnEquity; }
            if (fd.FrReturnOnInvestedCapital != null) { this.fr_return_on_invested_capital = fd.FrReturnOnInvestedCapital; }
            if (fd.FrRevenueGrowth != null) { this.fr_revenue_growth = fd.FrRevenueGrowth; }
            if (fd.FrRevenuePerShareTtm != null) { this.fr_revenue_per_share_ttm = fd.FrRevenuePerShareTtm; }
            if (fd.FrRevenues != null) { this.fr_revenues = fd.FrRevenues; }
            if (fd.FrRevenuesTtm != null) { this.fr_revenues_ttm = fd.FrRevenuesTtm; }
            if (fd.FrSgAExpense != null) { this.fr_sg_a_expense = fd.FrSgAExpense; }
            if (fd.FrShareholdersEquity != null) { this.fr_shareholders_equity = fd.FrShareholdersEquity; }
            if (fd.FrSharesOutstanding != null) { this.fr_shares_outstanding = fd.FrSharesOutstanding; }
            if (fd.FrStockBuybacks != null) { this.fr_stock_buybacks = fd.FrStockBuybacks; }
            if (fd.FrTangibleBookValue != null) { this.fr_tangible_book_value = fd.FrTangibleBookValue; }
            if (fd.FrTangibleBookValuePerShare != null) { this.fr_tangible_book_value_per_share = fd.FrTangibleBookValuePerShare; }
            if (fd.FrTangibleCommonEquityRatio != null) { this.fr_tangible_common_equity_ratio = fd.FrTangibleCommonEquityRatio; }
            if (fd.FrTimesInterestEarnedTtm != null) { this.fr_times_interest_earned_ttm = fd.FrTimesInterestEarnedTtm; }
            if (fd.FrTotalAssets != null) { this.fr_total_assets = fd.FrTotalAssets; }
            if (fd.FrTotalReturnPrice != null) { this.fr_total_return_price = fd.FrTotalReturnPrice; }
            if (fd.FrValuationHistoricalMult != null) { this.fr_valuation_historical_mult = fd.FrValuationHistoricalMult; }
            if (fd.FrWorkingCapital != null) { this.fr_working_capital = fd.FrWorkingCapital; }
            if (fd.FrStandardDeviation != null) { this.fr_standard_deviation = fd.FrStandardDeviation; }
            if (fd.FrRoicGrowthRate != null) { this.fr_roic_growth_rate = fd.FrRoicGrowthRate; }
            if (fd.FrQuickRatio != null) { this.fr_quick_ratio = fd.FrQuickRatio; }
            if (fd.FrAssetCoverage != null) { this.fr_asset_coverage = fd.FrAssetCoverage; }
            if (fd.FrDscr != null) { this.fr_dscr = fd.FrDscr; }
            if (fd.FrDebtEbitda != null) { this.fr_debt_EBITDA = fd.FrDebtEbitda; }
            if (fd.FrEqPrc != null) { this.fr_eq_prc = fd.FrEqPrc; }
            if (fd.FrCashFlowVolatility != null) { this.fr_cash_flow_volatility = fd.FrCashFlowVolatility; }
            if (fd.FrTurnoverRatio != null) { this.fr_turnover_ratio = fd.FrTurnoverRatio; }
            if (fd.FrBookToMarket != null) { this.fr_book_to_market = fd.FrBookToMarket; }
            if (fd.FrEarningsToPriceRatio != null) { this.fr_earnings_to_price_ratio = fd.FrEarningsToPriceRatio; }
            if (fd.FrCashFlowToPriceRatio != null) { this.fr_cash_flow_to_price_ratio = fd.FrCashFlowToPriceRatio; }
            if (fd.FrSalesGrowthRatio != null) { this.fr_sales_growth_ratio = fd.FrSalesGrowthRatio; }
            if (fd.FrNetIncomeTtm != null) { this.fr_netIncomeTTM = fd.FrNetIncomeTtm; }
            if (fd.FrDividendsPaidTtm != null) { this.fr_dividendsPaidTTM = fd.FrDividendsPaidTtm; }
            if (fd.FrDividendTtm != null) { this.fr_dividendTTM = fd.FrDividendTtm; }
            if (fd.FrSharesTtm != null) { this.fr_sharesTTM = fd.FrSharesTtm; }
            if (fd.FrEbitTtm != null) { this.fr_ebitTTM = fd.FrEbitTtm; }
            if (fd.FrIncomeTaxesTtm != null) { this.fr_incomeTaxesTTM = fd.FrIncomeTaxesTtm; }
            if (fd.FrPreTaxIncomeTtm != null) { this.fr_preTaxIncomeTTM = fd.FrPreTaxIncomeTtm; }
            if (fd.FrDebtTtm != null) { this.fr_debtTTM = fd.FrDebtTtm; }
            if (fd.FrEbit != null) { this.fr_ebit = fd.FrEbit; }
            if (fd.FrEbitda != null) { this.fr_ebitda = fd.FrEbitda; }
            if (fd.FrEps4Ago != null) { this.fr_eps4ago = fd.FrEps4Ago; }
            if (fd.FrRetainedEarnings4Ago != null) { this.fr_retainedEarnings4ago = fd.FrRetainedEarnings4Ago; }
            if (fd.FrRevenue4Ago != null) { this.fr_revenue4ago = fd.FrRevenue4Ago; }

        }
    }
}
