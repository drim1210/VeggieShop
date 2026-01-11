using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Windows.Forms;
using VeggieShop.BUS;
using System.Xml.Linq;

namespace VeggieShop.UI
{
    public partial class FrmCustomers : Form
    {
        private readonly CustomerBUS _bus = new CustomerBUS();
        private int _selectedId = 0;

        public FrmCustomers()
        {
            InitializeComponent();
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = true;
            LoadData();
        }

        private void LoadData()
        {
            dgvCustomers.DataSource = _bus.GetAll(txtSearch.Text);
            _selectedId = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCustomers.Rows[e.RowIndex];

            _selectedId = Convert.ToInt32(row.Cells["CustomerId"].Value);
            txtCode.Text = row.Cells["CustomerCode"].Value.ToString();
            txtName.Text = row.Cells["CustomerName"].Value.ToString();
            txtPhone.Text = row.Cells["Phone"].Value == DBNull.Value ? "" : row.Cells["Phone"].Value.ToString();
            txtAddress.Text = row.Cells["Address"].Value == DBNull.Value ? "" : row.Cells["Address"].Value.ToString();
            txtPoints.Text = row.Cells["Points"].Value.ToString();
            chkActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var (ok, msg) = _bus.Create(txtCode.Text, txtName.Text, txtPhone.Text, txtAddress.Text, txtPoints.Text, chkActive.Checked);
            MessageBox.Show(msg);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var (ok, msg) = _bus.Update(_selectedId, txtCode.Text, txtName.Text, txtPhone.Text, txtAddress.Text, txtPoints.Text, chkActive.Checked);
            MessageBox.Show(msg);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId <= 0)
            {
                MessageBox.Show("Chọn 1 dòng để xóa.");
                return;
            }

            var confirm = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (ok, msg) = _bus.Delete(_selectedId);
            MessageBox.Show(msg);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedId = 0;
            txtCode.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtPoints.Text = "0";
            chkActive.Checked = true;
            txtCode.Focus();
        }
    }
}
