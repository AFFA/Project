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
        public Revenue()
        {
            InitializeComponent();
        }
        private void Revenue_Load(object sender, EventArgs e)
        {
            List<DateTime> dates = new List<DateTime>();
            dates.Add(new DateTime(2009, 01, 31));
            dates.Add(new DateTime(2009, 04, 30));
            dates.Add(new DateTime(2009, 07, 31));
            dates.Add(new DateTime(2009, 10, 31));
            dates.Add(new DateTime(2010, 01, 31));
            dates.Add(new DateTime(2010, 04, 30));
            dates.Add(new DateTime(2010, 07, 31));
            dates.Add(new DateTime(2010, 10, 31));
            dates.Add(new DateTime(2011, 01, 31));
            dates.Add(new DateTime(2011, 04, 30));
            dates.Add(new DateTime(2011, 07, 31));
            dates.Add(new DateTime(2011, 10, 31));
            List<int> nr = new List<int>();
            nr.Add(9089); nr.Add(8162); nr.Add(8535); nr.Add(9021);
            nr.Add(9815); nr.Add(10368); nr.Add(10836); nr.Add(10750);
            nr.Add(10407); nr.Add(10866); nr.Add(11195); nr.Add(11256);

            this.BackColor = Color.Gainsboro;
            chart1.BackColor = Color.Gainsboro;


            chart1.ChartAreas.Add("chartArea");
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.Legends.Add("Legends").BackColor = Color.Gainsboro;
            chart1.Series.Add("Revenue");
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].Color = Color.Green;
            chart1.Series[0].BorderWidth = 2;

            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].LabelBackColor = Color.Gainsboro;
            string a;
            string b;
            for (int i = 0; i < nr.Count; i++)
            {
                if (dates[i].Month == 01)
                {
                    a = "Q1";
                    b = dates[i].ToString("yy");
                }
                else if (dates[i].Month == 04)
                {
                    a = "Q2";
                    b = dates[i].ToString("yy");
                }
                else if (dates[i].Month == 07)
                {
                    a = "Q3";
                    b = dates[i].ToString("yy");
                }
                else
                {
                    a = "Q4";
                    b = dates[i].ToString("yy");
                }
                chart1.Series[0].Points.AddXY(b + a, nr[i]);
            }
        }



    }
}
