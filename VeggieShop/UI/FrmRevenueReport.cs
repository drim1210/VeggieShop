using System;
using System.Data;
using System.Windows.Forms;
using VeggieShop.BUS;

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

            // mặc định: tháng hiện tại
            var now = DateTime.Now;
            dtFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtTo.Value = dtFrom.Value.AddMonths(1).AddDays(-1);

            dgvRevenue.AutoGenerateColumns = true;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtFrom.Value.Date > dtTo.Value.Date)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày.");
                return;
            }

            DataTable dt;
            if (radDay.Checked)
                dt = _bus.ByDay(dtFrom.Value, dtTo.Value);
            else
                dt = _bus.ByMonth(dtFrom.Value, dtTo.Value);

            dgvRevenue.DataSource = dt;

            var total = _bus.Total(dtFrom.Value, dtTo.Value);
            lblTotal.Text = $"Tổng doanh thu: {total:n0}";
        }
    }
}
