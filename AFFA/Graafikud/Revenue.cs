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
    public partial class Revenue : Form
    {
        private List<Mudelid.FinData> list;

        public Revenue(List<Mudelid.FinData> list)
        {

            this.list = list;
            InitializeComponent();

        }
        public void Revenue_Load(object sender, EventArgs e)
        {
            this.Text = "Revenue";
            this.BackColor = Color.Gainsboro;
            chart1.BackColor = Color.Gainsboro;

            chart1.ChartAreas.Add("chartArea");
            chart1.ChartAreas[0].AxisX.Interval = 1;
            // chart1.Legends.Add("Legends").BackColor = Color.Gainsboro; //Legend paremal
            chart1.Series.Add("Revenue");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].Color = Color.Green;
            chart1.Series[0].BorderWidth = 2;

            List<Mudelid.FinData> lst = list;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsRevenue.Equals(null))
                {
                    i++;
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
                    // MessageBox.Show(list[i].Kuupaev + " age " + list[i].IsRevenue);
                }
            }
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
    }
}
