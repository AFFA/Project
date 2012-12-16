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
        /// <summary>
        /// Graafikute konstruktor
        /// </summary>
        /// <param name="list">Ajalooliste andmete list</param>
        /// <param name="i">Graafikule vastav number</param>
        public Graafik(List<Mudelid.FinData> list, int i)
        {
            this.list = list;
            InitializeComponent(i);
        }
        /// <summary>
        /// Graafiku visuaalne pool
        /// </summary>
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
        /// <summary>
        /// Graafiku avamisel checkboxi linnukese panemisel kuvab väärtused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Eps Diluted-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_EpsDiluted(object sender, EventArgs e)
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
        /// <summary>
        /// Revenue väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_Revenue(object sender, EventArgs e)
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
        /// <summary>
        /// Gross Profit Margin-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_GrossProfitMargin(object sender, EventArgs e)
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
                    double fr_GrossProfitMargin = Math.Round(list[i].FrGrossProfitMargin.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, fr_GrossProfitMargin);
                }
            }
        }
        /// <summary>
        /// Operating Margin-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_OperatingMargin(object sender, EventArgs e)
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
                    double fr_OperatingMargin = Math.Round(list[i].FrOperatingMargin.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, fr_OperatingMargin);
                }
            }
        }
        /// <summary>
        /// Profit Margin-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_ProfitMargin(object sender, EventArgs e)
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
                    double is_ProfitMargin = Math.Round(list[i].FrProfitMargin.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, is_ProfitMargin);
                }
            }
        }

        /// <summary>
        /// Total Assets-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_BsTotalAssets(object sender, EventArgs e)
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
        /// <summary>
        /// Total Current Assets ja Total Assets-i väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_BsTotalCurrentAssets_Divided_BsTotalAssets(object sender, EventArgs e)
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
                    double x = list[i].BsTotalCurrentAssets.Value;
                    double y = list[i].BsTotalAssets.Value;
                    double answer = Math.Round(x / y * 100, 1);

                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        /// <summary>
        /// Total Current Liabilities ja Total Assets-i väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_BsTotalCurrentLiabilities_Divided_BsTotalAssets(object sender, EventArgs e)
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
                    double x = list[i].BsTotalCurrentLiabilities.Value;
                    double y = list[i].BsTotalAssets.Value;
                    double answer = Math.Round(x / y * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        /// <summary>
        /// FrEqPrc-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrEqPrc(object sender, EventArgs e)
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
                    double FrEqPrc = Math.Round(list[i].FrEqPrc.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEqPrc);
                }
            }
        }

        /// <summary>
        /// Return On Equity-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_ReturnOnEquity(object sender, EventArgs e)
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
                    double FrReturnOnEquity = Math.Round(list[i].FrReturnOnEquity.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrReturnOnEquity);
                }
            }
        }
        /// <summary>
        /// Total Current Liabilities ja Total Assets-i väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_IsNetIncome_Divided_IsPretaxIncome(object sender, EventArgs e)
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
                    double x = list[i].IsNetIncome.Value;
                    double y = list[i].IsPretaxIncome.Value;
                    double answer = Math.Round(x / y, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        /// <summary>
        /// Total Current Liabilities ja Total Assets-i väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_IsPretaxIncome_Divided_FrEbit(object sender, EventArgs e)
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
                    double x = list[i].IsPretaxIncome.Value;
                    double y = list[i].FrEbit.Value;
                    double answer = Math.Round(x / y, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        /// <summary>
        /// EpsDiluted-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrOperatingMargin(object sender, EventArgs e)
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
                    double FrOperatingMargin = Math.Round(list[i].FrOperatingMargin.Value * 100, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrOperatingMargin);
                }
            }
        }
        /// <summary>
        /// Revenue ja Total Assets-i väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_IsRevenue_Divided_BsTotalAssets(object sender, EventArgs e)
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
                    double x = list[i].IsRevenue.Value;
                    double y = list[i].BsTotalAssets.Value;
                    double answer = Math.Round(x / y, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }
        /// <summary>
        /// Total Assets ja Shareholders Equity1 väärtuste võtmine listist, vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_BsTotalAssets_Divided_BsShareholdersEquity1(object sender, EventArgs e)
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
                    double x = list[i].BsTotalAssets.Value;
                    double y = list[i].BsShareholdersEquity1.Value;
                    double answer = Math.Round(x / y, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, answer);
                }
            }
        }

        /// <summary>
        /// Pe Ratio väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrPeRatio(object sender, EventArgs e)
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
                    double FrPeRatio = Math.Round(list[i].FrPeRatio.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPeRatio);
                }
            }
        }
        /// <summary>
        /// Peg Ratio väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrPegRatio(object sender, EventArgs e)
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
                    double FrPegRatio = Math.Round(list[i].FrPegRatio.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPegRatio);
                }
            }
        }
        /// <summary>
        /// Price Book Value väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrPriceBookValue(object sender, EventArgs e)
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
                    double FrPriceBookValue = Math.Round(list[i].FrPriceBookValue.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPriceBookValue);
                }
            }
        }
        /// <summary>
        /// Price Sales Ratio väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrPriceSalesRatio(object sender, EventArgs e)
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
                    double FrPriceSalesRatio = Math.Round(list[i].FrPriceSalesRatio.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrPriceSalesRatio);
                }
            }
        }
        /// <summary>
        /// EvEbitda väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrEvEbitda(object sender, EventArgs e)
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
                    double FrEvEbitda = Math.Round(list[i].FrEvEbitda.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEvEbitda);
                }
            }
        }
        /// <summary>
        /// EvFree Cash Flow väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrEvFreeCashFlow(object sender, EventArgs e)
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
                    double FrEvFreeCashFlow = Math.Round(list[i].FrEvFreeCashFlow.Value, 1);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrEvFreeCashFlow);
                }
            }
        }

        /// <summary>
        /// Cash Conversion Cycle väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrCashConversionCycle(object sender, EventArgs e)
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
                    double FrCashConversionCycle = Math.Round(list[i].FrCashConversionCycle.Value, 0);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrCashConversionCycle);
                }
            }
        }
        /// <summary>
        /// Days Inventory Outstanding väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrDaysInventoryOutstanding(object sender, EventArgs e)
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
                    double FrDaysInventoryOutstanding = Math.Round(list[i].FrDaysInventoryOutstanding.Value, 0);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrDaysInventoryOutstanding);
                }
            }
        }
        /// <summary>
        /// Days Sales Outstanding-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrDaysSalesOutstanding(object sender, EventArgs e)
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
                    double FrDaysSalesOutstanding = Math.Round(list[i].FrDaysSalesOutstanding.Value, 0);
                    if (a == 01) { q = "Q1"; }
                    else if (a == 04) { q = "Q2"; }
                    else if (a == 07) { q = "Q3"; }
                    else { q = "Q4"; }
                    // MessageBox.Show(q);
                    chart1.Series[0].Points.AddXY(ab + q, FrDaysSalesOutstanding);
                }
            }
        }
        /// <summary>
        /// Days Payable Outstanding-i väärtuste võtmine listist ja vajalike tehete tegemine ning arvutuste lisamine graafikusse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load_FrDaysPayableOutstanding(object sender, EventArgs e)
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
                    double FrDaysPayableOutstanding = Math.Round(list[i].FrDaysPayableOutstanding.Value, 0);
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
