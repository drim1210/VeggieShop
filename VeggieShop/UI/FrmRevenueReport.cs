using System;
using System.Data;
using System.Windows.Forms;
using VeggieShop.BUS;
using System.Windows.Forms.DataVisualization.Charting;

namespace VeggieShop.UI
{
    public partial class FrmRevenueReport : Form
    {
        private readonly RevenueBUS _bus = new RevenueBUS();

        public FrmRevenueReport()
        {
            InitializeComponent();
        }

        private void FrmRevenueReport_Load(object sender, EventArgs e)
        {
            radDay.Checked = true;

            var now = DateTime.Now;
            dtFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtTo.Value = dtFrom.Value.AddMonths(1).AddDays(-1);

            dgvRevenue.AutoGenerateColumns = true;

            SetupChart();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày.");
                return;
            }

            bool byDay = radDay.Checked;

            DataTable dt = byDay
                ? _bus.ByDay(dtFrom.Value, dtTo.Value)
                : _bus.ByMonth(dtFrom.Value, dtTo.Value);

            dgvRevenue.DataSource = dt;

            var total = _bus.Total(dtFrom.Value, dtTo.Value);
            lblTotal.Text = $"Tổng doanh thu: {total:n0}";

            RenderChart(dt, byDay);
        }

        private void SetupChart()
        {
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas.Clear();
            chartRevenue.Legends.Clear();

            var area = new ChartArea("Main");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = true;
            area.AxisX.LabelStyle.Angle = -45;
            chartRevenue.ChartAreas.Add(area);

            var s = new Series("DoanhThu")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                IsValueShownAsLabel = true,
                LabelFormat = "N0"
            };
            chartRevenue.Series.Add(s);
        }

        private void RenderChart(DataTable dt, bool byDay)
        {
            var s = chartRevenue.Series["DoanhThu"];
            s.Points.Clear();

            if (dt == null || dt.Rows.Count == 0) return;

            foreach (DataRow r in dt.Rows)
            {
                string labelX;

                if (byDay)
                {
                    var d = Convert.ToDateTime(r["Ngay"]);
                    labelX = d.ToString("dd/MM");
                }
                else
                {
                    int y = Convert.ToInt32(r["Nam"]);
                    int m = Convert.ToInt32(r["Thang"]);
                    labelX = $"{m:00}/{y}";
                }

                decimal revenue = Convert.ToDecimal(r["DoanhThu"]);
                s.Points.AddXY(labelX, revenue);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            VeggieShop.Utils.ExportHelper.ExportDataGridViewToExcel(dgvRevenue, "ThongKe");
        }



    }
}
