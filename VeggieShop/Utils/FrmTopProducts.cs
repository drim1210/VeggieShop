using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using VeggieShop.BUS;

namespace VeggieShop.UI
{
    public partial class FrmTopProducts : Form
    {
        private readonly TopProductsBUS _bus = new TopProductsBUS();

        public FrmTopProducts()
        {
            InitializeComponent();
        }

        private void FrmTopProducts_Load(object sender, EventArgs e)
        {
            // mặc định: 30 ngày gần nhất
            dtTo.Value = DateTime.Now.Date;
            dtFrom.Value = dtTo.Value.AddDays(-30);

            nudTop.Minimum = 1;
            nudTop.Maximum = 100;
            nudTop.Value = 10;

            radQty.Checked = true;
            dgvTop.AutoGenerateColumns = true;

            SetupChart();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày.");
                return;
            }

            int topN = (int)nudTop.Value;
            bool byRevenue = radRevenue.Checked;

            var dt = _bus.GetTop(dtFrom.Value, dtTo.Value, topN, byRevenue);
            dgvTop.DataSource = dt;

            RenderChart(dt, byRevenue);
        }

        private void SetupChart()
        {
            chartTop.Series.Clear();
            chartTop.ChartAreas.Clear();
            chartTop.Legends.Clear();

            var area = new ChartArea("Main");
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -45;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            chartTop.ChartAreas.Add(area);

            var s = new Series("Top")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = true
            };
            chartTop.Series.Add(s);
        }

        private void RenderChart(DataTable dt, bool byRevenue)
        {
            var s = chartTop.Series["Top"];
            s.Points.Clear();

            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow r in dt.Rows)
            {
                string name = r["ProductName"].ToString();

                decimal val = byRevenue
                    ? Convert.ToDecimal(r["TotalRevenue"])
                    : Convert.ToDecimal(r["TotalQty"]);

                var p = s.Points.AddXY(name, val);

                // format label
                if (byRevenue)
                {
                    s.LabelFormat = "N0";
                    chartTop.ChartAreas["Main"].AxisY.LabelStyle.Format = "N0";
                }
                else
                {
                    s.LabelFormat = "N0";
                    chartTop.ChartAreas["Main"].AxisY.LabelStyle.Format = "N0";
                }
            }
        }
    }
}
