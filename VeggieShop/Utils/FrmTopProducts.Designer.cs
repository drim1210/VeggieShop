namespace VeggieShop.UI
{
    partial class FrmTopProducts
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
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.radQty = new System.Windows.Forms.RadioButton();
            this.radRevenue = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.dgvTop = new System.Windows.Forms.DataGridView();
            this.chartTop = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            this.SuspendLayout();
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(67, 83);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(200, 22);
            this.dtTo.TabIndex = 3;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(67, 43);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(200, 22);
            this.dtFrom.TabIndex = 2;
            // 
            // radQty
            // 
            this.radQty.AutoSize = true;
            this.radQty.Location = new System.Drawing.Point(67, 168);
            this.radQty.Name = "radQty";
            this.radQty.Size = new System.Drawing.Size(114, 20);
            this.radQty.TabIndex = 5;
            this.radQty.TabStop = true;
            this.radQty.Text = "Theo số lượng";
            this.radQty.UseVisualStyleBackColor = true;
            // 
            // radRevenue
            // 
            this.radRevenue.AutoSize = true;
            this.radRevenue.Location = new System.Drawing.Point(198, 168);
            this.radRevenue.Name = "radRevenue";
            this.radRevenue.Size = new System.Drawing.Size(121, 20);
            this.radRevenue.TabIndex = 6;
            this.radRevenue.TabStop = true;
            this.radRevenue.Text = "Theo doanh thu";
            this.radRevenue.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(67, 212);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 7;
            this.btnView.Text = "Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dgvTop
            // 
            this.dgvTop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTop.Location = new System.Drawing.Point(39, 274);
            this.dgvTop.Name = "dgvTop";
            this.dgvTop.RowHeadersWidth = 51;
            this.dgvTop.RowTemplate.Height = 24;
            this.dgvTop.Size = new System.Drawing.Size(723, 150);
            this.dgvTop.TabIndex = 8;
            // 
            // chartTop
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTop.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartTop.Legends.Add(legend2);
            this.chartTop.Location = new System.Drawing.Point(436, 23);
            this.chartTop.Name = "chartTop";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTop.Series.Add(series2);
            this.chartTop.Size = new System.Drawing.Size(300, 231);
            this.chartTop.TabIndex = 9;
            this.chartTop.Text = "chart1";
            // 
            // nudTop
            // 
            this.nudTop.Location = new System.Drawing.Point(67, 120);
            this.nudTop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(120, 22);
            this.nudTop.TabIndex = 10;
            this.nudTop.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // FrmTopProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudTop);
            this.Controls.Add(this.chartTop);
            this.Controls.Add(this.dgvTop);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.radRevenue);
            this.Controls.Add(this.radQty);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Name = "FrmTopProducts";
            this.Text = "FrmTopProducts";
            this.Load += new System.EventHandler(this.FrmTopProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.RadioButton radQty;
        private System.Windows.Forms.RadioButton radRevenue;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DataGridView dgvTop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTop;
        private System.Windows.Forms.NumericUpDown nudTop;
    }
}