using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AFFA.Graafikud
{
    public partial class Graafik : Form
    {
        private List<Mudelid.FinData> list;

        public Graafik(List<Mudelid.FinData> list, int i)
        {
            this.list = list;
            InitializeComponent(i);
        }
        public void Iluasi()
        {
            this.BackColor = Color.Gainsboro;
            chart1.BackColor = Color.Gainsboro;

            chart1.ChartAreas.Add("chartArea");
            chart1.ChartAreas[0].AxisX.Interval = 1;
            // chart1.Legends.Add("Legends").BackColor = Color.Gainsboro; //Legend paremal
            chart1.Series.Add("Revenue");
            chart1.Series[0].Color = Color.Green;
            chart1.Series[0].BorderWidth = 2;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                chart1.Series[0].IsValueShownAsLabel = true;
                //chart1.Series[0].LabelBackColor = Color.Gainsboro;
            }
            else
                chart1.Series[0].IsValueShownAsLabel = false;
        }

        public void Revenue_Load_EpsDiluted(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "EpsDiluted";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;
            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].IsEpsDiluted.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? is_EpsDiluted = list[i].IsEpsDiluted;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    chart1.Series[0].Points.AddXY(ab + q, is_EpsDiluted);

                }
            }
        }
        public void Revenue_Load_Revenue(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "Revenue";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;
            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].IsRevenue.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? is_revenue = list[i].IsRevenue;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, is_revenue);
                }
            }
        }
        public void Revenue_Load_GrossProfitMargin(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "GrossProfitMargin";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrGrossProfitMargin.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? fr_GrossProfitMargin = list[i].FrGrossProfitMargin;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, fr_GrossProfitMargin);
                }
            }
        }
        public void Revenue_Load_OperatingMargin(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "OperatingMargin";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrOperatingMargin.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? fr_OperatingMargin = list[i].FrOperatingMargin;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, fr_OperatingMargin);
                }
            }
        }
        public void Revenue_Load_ProfitMargin(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "ProfitMargin";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrProfitMargin.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? is_ProfitMargin = list[i].FrProfitMargin;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, is_ProfitMargin);
                }
            }
        }

        public void Revenue_Load_BsTotalAssets(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "TotalAssets";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].BsTotalAssets.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? BsTotalAssets = list[i].BsTotalAssets;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, BsTotalAssets);
                }
            }
        }
        public void Revenue_Load_BsTotalCurrentAssets_Divided_BsTotalAssets(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "TotalCurrentAssets Divided BsTotalAssets";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].BsTotalCurrentAssets.Equals(null) || list[i].BsTotalAssets.Equals(null) || list[i].BsTotalAssets.Equals(0))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].BsTotalCurrentAssets;
                    double? y = list[i].BsTotalAssets;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        public void Revenue_Load_BsTotalCurrentLiabilities_Divided_BsTotalAssets(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "BsTotalCurrentLiabilities Divided BsTotalAssets";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].BsTotalCurrentLiabilities.Equals(null) || list[i].BsTotalAssets.Equals(null) || list[i].BsTotalCurrentLiabilities.Equals(0))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].BsTotalCurrentLiabilities;
                    double? y = list[i].BsTotalAssets;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        public void Revenue_Load_FrEqPrc(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "EqPrc";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrEqPrc.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrEqPrc = list[i].FrEqPrc;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEqPrc * 100);
                }
            }
        }

        public void Revenue_Load_ReturnOnEquity(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "ReturnOnEquity";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrReturnOnEquity.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrReturnOnEquity = list[i].FrReturnOnEquity;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrReturnOnEquity);
                }
            }
        }
        public void Revenue_Load_IsNetIncome_Divided_IsPretaxIncome(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "NetIncome Divided PretaxIncome";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].IsNetIncome.Equals(null) || list[i].IsPretaxIncome.Equals(null) || list[i].IsNetIncome.Equals(0))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].IsNetIncome;
                    double? y = list[i].IsPretaxIncome;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        public void Revenue_Load_IsPretaxIncome_Divided_FrEbit(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "PretaxIncome Divided Ebit";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].IsPretaxIncome.Equals(null) || list[i].FrEbit.Equals(null) || list[i].IsPretaxIncome.Equals(0))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].IsPretaxIncome;
                    double? y = list[i].FrEbit;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        public void Revenue_Load_FrOperatingMargin(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "OperatingMargin";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrOperatingMargin.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrOperatingMargin = list[i].FrOperatingMargin;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrOperatingMargin);
                }
            }
        }
        public void Revenue_Load_IsRevenue_Divided_BsTotalAssets(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "Revenue Divided TotalAssets";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].IsRevenue.Equals(null) || list[i].BsTotalAssets.Equals(null) || list[i].IsRevenue.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].IsRevenue;
                    double? y = list[i].BsTotalAssets;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        public void Revenue_Load_BsTotalAssets_Divided_BsShareholdersEquity1(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "TotalAssets Divided ShareholdersEquity1";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].BsTotalAssets.Equals(null) || list[i].BsShareholdersEquity1.Equals(null) || list[i].BsTotalAssets.Equals(0))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? x = list[i].BsTotalAssets;
                    double? y = list[i].BsShareholdersEquity1;
                    double? answer = x / y;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }

        public void Revenue_Load_FrPeRatio(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "PeRatio";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrPeRatio.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrPeRatio = list[i].FrPeRatio;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPeRatio);
                }
            }
        }
        public void Revenue_Load_FrPegRatio(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "PegRatio";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrPegRatio.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrPegRatio = list[i].FrPegRatio;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPegRatio);
                }
            }
        }
        public void Revenue_Load_FrPriceBookValue(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "PriceBookValue";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrPriceBookValue.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrPriceBookValue = list[i].FrPriceBookValue;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPriceBookValue);
                }
            }
        }
        public void Revenue_Load_FrPriceSalesRatio(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "PriceSalesRatio";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrPriceSalesRatio.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrPriceSalesRatio = list[i].FrPriceSalesRatio;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPriceSalesRatio);
                }
            }
        }
        public void Revenue_Load_FrEvEbitda(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "EvEbitda";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrEvEbitda.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrEvEbitda = list[i].FrEvEbitda;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEvEbitda);
                }
            }
        }
        public void Revenue_Load_FrEvFreeCashFlow(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "EvFreeCashFlow";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrEvFreeCashFlow.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrEvFreeCashFlow = list[i].FrEvFreeCashFlow;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEvFreeCashFlow);
                }
            }
        }

        public void Revenue_Load_FrCashConversionCycle(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "CashConversionCycle";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrCashConversionCycle.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrCashConversionCycle = list[i].FrCashConversionCycle;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrCashConversionCycle);
                }
            }
        }
        public void Revenue_Load_FrDaysInventoryOutstanding(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "DaysInventoryOutstanding";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrDaysInventoryOutstanding.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrDaysInventoryOutstanding = list[i].FrDaysInventoryOutstanding;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrDaysInventoryOutstanding);
                }
            }
        }
        public void Revenue_Load_FrDaysSalesOutstanding(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "DaysSalesOutstanding";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrDaysSalesOutstanding.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrDaysSalesOutstanding = list[i].FrDaysSalesOutstanding;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrDaysSalesOutstanding);
                }
            }
        }
        public void Revenue_Load_FrDaysPayableOutstanding(object sender, EventArgs e)
        {
            Iluasi();
            this.Text = "DaysPayableOutstanding";
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            int lugeja;
            if (list.Count - 12 > 0)
                lugeja = list.Count - 12;
            else
                lugeja = 0;

            for (int i = lugeja; i < list.Count; i++)
            {
                if (list[i].FrDaysPayableOutstanding.Equals(null))
                {
                    continue;
                }
                else
                {
                    DateTime aeg = list[i].Kuupaev;
                    int a = aeg.Month;
                    String q;
                    String ab = aeg.Year.ToString().Substring(2);
                    double? FrDaysPayableOutstanding = list[i].FrDaysPayableOutstanding;
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrDaysPayableOutstanding);
                }
            }
        }






    }
}
