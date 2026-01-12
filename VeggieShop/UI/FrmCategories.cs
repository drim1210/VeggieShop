using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeggieShop.BUS;

namespace VeggieShop.UI
{
    public partial class FrmCategories : Form
    {
        private readonly CategoryBUS _bus = new CategoryBUS();
        private int _selectedId = 0;

        public FrmCategories()
        {
            InitializeComponent();
        }

        private void FrmCategories_Load(object sender, EventArgs e)
        {
            dgvCategories.AutoGenerateColumns = true;
            LoadData();
        }

        private void LoadData()
        {
            dgvCategories.DataSource = _bus.GetAll(txtSearch.Text);
            _selectedId = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCategories.Rows[e.RowIndex];

            _selectedId = Convert.ToInt32(row.Cells["CategoryId"].Value);
            txtCode.Text = row.Cells["CategoryCode"].Value.ToString();
            txtName.Text = row.Cells["CategoryName"].Value.ToString();
            txtDescription.Text = row.Cells["Description"].Value == DBNull.Value ? "" : row.Cells["Description"].Value.ToString();
            chkActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var (ok, msg) = _bus.Create(txtCode.Text, txtName.Text, txtDescription.Text, chkActive.Checked);
            MessageBox.Show(msg);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var (ok, msg) = _bus.Update(_selectedId, txtCode.Text, txtName.Text, txtDescription.Text, chkActive.Checked);
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
            txtDescription.Text = "";
            chkActive.Checked = true;
            txtCode.Focus();
        }
    }
}
