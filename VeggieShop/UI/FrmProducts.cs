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
    public partial class FrmProducts : Form
    {
        private readonly ProductBUS _bus = new ProductBUS();
        private int _selectedId = 0;

        public FrmProducts()
        {
            InitializeComponent();
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            dgvProducts.AutoGenerateColumns = true;

            LoadCategories();
            txtUnit.Text = "kg";
            LoadData();
        }

        private void LoadCategories()
        {
            var dt = _bus.GetCategoriesForCombo();

            // cboCategory (form nhập)
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";
            cboCategory.DataSource = dt.Copy();

            // cboCategoryFilter (lọc)
            var dtFilter = dt.Copy();
            var row = dtFilter.NewRow();
            row["CategoryId"] = 0;
            row["CategoryName"] = "Tất cả";
            dtFilter.Rows.InsertAt(row, 0);

            cboCategoryFilter.DisplayMember = "CategoryName";
            cboCategoryFilter.ValueMember = "CategoryId";
            cboCategoryFilter.DataSource = dtFilter;
            cboCategoryFilter.SelectedValue = 0;
        }

        private void LoadData()
        {
            int catId = 0;
            if (cboCategoryFilter.SelectedValue != null)
                int.TryParse(cboCategoryFilter.SelectedValue.ToString(), out catId);

            dgvProducts.DataSource = _bus.GetAll(txtSearch.Text, catId);
            _selectedId = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var r = dgvProducts.Rows[e.RowIndex];

            _selectedId = Convert.ToInt32(r.Cells["ProductId"].Value);
            txtCode.Text = r.Cells["ProductCode"].Value.ToString();
            txtName.Text = r.Cells["ProductName"].Value.ToString();

            cboCategory.SelectedValue = Convert.ToInt32(r.Cells["CategoryId"].Value);

            txtUnit.Text = r.Cells["Unit"].Value.ToString();
            txtPurchasePrice.Text = r.Cells["PurchasePrice"].Value.ToString();
            txtSalePrice.Text = r.Cells["SalePrice"].Value.ToString();
            txtStockQty.Text = r.Cells["StockQty"].Value.ToString();
            chkActive.Checked = Convert.ToBoolean(r.Cells["IsActive"].Value);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int catId = Convert.ToInt32(cboCategory.SelectedValue);
            var (ok, msg) = _bus.Create(
                txtCode.Text, txtName.Text, catId, txtUnit.Text,
                txtPurchasePrice.Text, txtSalePrice.Text, txtStockQty.Text,
                chkActive.Checked);

            MessageBox.Show(msg);
            if (ok) { ClearForm(); LoadData(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int catId = Convert.ToInt32(cboCategory.SelectedValue);
            var (ok, msg) = _bus.Update(
                _selectedId,
                txtCode.Text, txtName.Text, catId, txtUnit.Text,
                txtPurchasePrice.Text, txtSalePrice.Text, txtStockQty.Text,
                chkActive.Checked);

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
            txtUnit.Text = "kg";
            txtPurchasePrice.Text = "0";
            txtSalePrice.Text = "0";
            txtStockQty.Text = "0";
            chkActive.Checked = true;
            txtCode.Focus();
        }
    }
}

