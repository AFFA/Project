namespace AFFA.Graafikud
{
    partial class Graafik
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(int nr)
        {
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(518, 316);
            this.chart1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "YValues";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Revenue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(542, 340);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chart1);
            this.Name = "Revenue";
            switch (nr)
            {
                case 1: this.Load += new System.EventHandler(this.Revenue_Load_EpsDiluted); break;
                case 2: this.Load += new System.EventHandler(this.Revenue_Load_Revenue); break;
                case 3: this.Load += new System.EventHandler(this.Revenue_Load_GrossProfitMargin); break;
                case 4: this.Load += new System.EventHandler(this.Revenue_Load_OperatingMargin); break;
                case 5: this.Load += new System.EventHandler(this.Revenue_Load_ProfitMargin); break;
                case 6: this.Load += new System.EventHandler(this.Revenue_Load_BsTotalAssets); break;
                case 7: this.Load += new System.EventHandler(this.Revenue_Load_BsTotalCurrentAssets_Divided_BsTotalAssets); break;
                case 8: this.Load += new System.EventHandler(this.Revenue_Load_BsTotalCurrentLiabilities_Divided_BsTotalAssets); break;
                case 9: this.Load += new System.EventHandler(this.Revenue_Load_FrEqPrc); break;
                case 10: this.Load += new System.EventHandler(this.Revenue_Load_ReturnOnEquity); break;
                case 11: this.Load += new System.EventHandler(this.Revenue_Load_IsNetIncome_Divided_IsPretaxIncome); break;
                case 12: this.Load += new System.EventHandler(this.Revenue_Load_IsPretaxIncome_Divided_FrEbit); break;
                case 13: this.Load += new System.EventHandler(this.Revenue_Load_FrOperatingMargin); break;
                case 14: this.Load += new System.EventHandler(this.Revenue_Load_IsRevenue_Divided_BsTotalAssets); break;
                case 15: this.Load += new System.EventHandler(this.Revenue_Load_BsTotalAssets_Divided_BsShareholdersEquity1); break;
                /* case 16: this.Load += new System.EventHandler(this.Revenue_Load1);  break;
                 case 17: this.Load += new System.EventHandler(this.Revenue_Load1);  break;
                 case 18: this.Load += new System.EventHandler(this.Revenue_Load1);  break;
                 case 19: this.Load += new System.EventHandler(this.Revenue_Load1);  break;
                 case 20: this.Load += new System.EventHandler(this.Revenue_Load1);  break;
                 case 21: this.Load += new System.EventHandler(this.Revenue_Load1);  break;*/
                case 22: this.Load += new System.EventHandler(this.Revenue_Load_FrCashConversionCycle); break;
                case 23: this.Load += new System.EventHandler(this.Revenue_Load_FrDaysInventoryOutstanding); break;
                case 24: this.Load += new System.EventHandler(this.Revenue_Load_FrDaysSalesOutstanding); break;
                case 25: this.Load += new System.EventHandler(this.Revenue_Load_FrDaysPayableOutstanding); break;
            }
           // this.Load += new System.EventHandler(this.Revenue_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox checkBox1;


    }
}

