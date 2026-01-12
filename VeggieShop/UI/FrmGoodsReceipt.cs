using System;
using System.Windows.Forms;
using VeggieShop.BUS;
using VeggieShop.Utils;

namespace VeggieShop.UI
{
    public partial class FrmGoodsReceipt : Form
    {
        private readonly GoodsReceiptBUS _bus = new GoodsReceiptBUS();
        private int _selectedReceiptId = 0;

        public FrmGoodsReceipt()
        {
            InitializeComponent();
        }

        private void FrmGoodsReceipt_Load(object sender, EventArgs e)
        {
            // load combobox
            cboSupplier.DisplayMember = "SupplierName";
            cboSupplier.ValueMember = "SupplierId";
            cboSupplier.DataSource = _bus.GetSuppliers();

            cboProduct.DisplayMember = "ProductName";
            cboProduct.ValueMember = "ProductId";
            cboProduct.DataSource = _bus.GetProducts();

            // default
            txtQty.Text = "1";
            txtUnitPrice.Text = "0";

            // load list
            LoadReceipts();
        }

        private void LoadReceipts()
        {
            dgvReceipts.AutoGenerateColumns = true;
            dgvReceipts.DataSource = _bus.GetReceipts(txtReceiptSearch.Text);
        }

        private void btnReceiptSearch_Click(object sender, EventArgs e)
        {
            LoadReceipts();
        }

        private void dgvReceipts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvReceipts.Rows[e.RowIndex];
            _selectedReceiptId = Convert.ToInt32(row.Cells["ReceiptId"].Value);

            var ok = _bus.LoadReceiptToForm(_selectedReceiptId,
                out var code, out var supId, out var date, out var note,
                out var productId, out var qty, out var price);

            if (!ok.ok)
            {
                MessageBox.Show(ok.msg);
                return;
            }

            txtReceiptCode.Text = code;
            cboSupplier.SelectedValue = supId;
            dtpDate.Value = date;
            txtNote.Text = note;

            cboProduct.SelectedValue = productId;
            txtQty.Text = qty.ToString();
            txtUnitPrice.Text = price.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboSupplier.SelectedValue == null || cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhà cung cấp và sản phẩm.");
                return;
            }

            if (_selectedReceiptId > 0)
            {
                MessageBox.Show("Bạn đang chọn 1 phiếu. Muốn sửa hãy bấm 'Sửa'. Muốn tạo mới hãy bấm 'Làm mới'.");
                return;
            }

            var (ok, msg) = _bus.SaveSingle(
                txtReceiptCode.Text,
                Convert.ToInt32(cboSupplier.SelectedValue),
                dtpDate.Value,
                txtNote.Text,
                Convert.ToInt32(cboProduct.SelectedValue),
                txtQty.Text,
                txtUnitPrice.Text);

            MessageBox.Show(msg);
            if (ok)
            {
                ClearForm();
                LoadReceipts();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboSupplier.SelectedValue == null || cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhà cung cấp và sản phẩm.");
                return;
            }

            var (ok, msg) = _bus.UpdateSingle(
                _selectedReceiptId,
                txtReceiptCode.Text,
                Convert.ToInt32(cboSupplier.SelectedValue),
                dtpDate.Value,
                txtNote.Text,
                Convert.ToInt32(cboProduct.SelectedValue),
                txtQty.Text,
                txtUnitPrice.Text);

            MessageBox.Show(msg);
            if (ok)
            {
                ClearForm();
                LoadReceipts();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedReceiptId <= 0)
            {
                MessageBox.Show("Chọn 1 phiếu để xóa.");
                return;
            }

            var confirm = MessageBox.Show("Xóa phiếu này? (sẽ hoàn tồn kho)", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var (ok, msg) = _bus.Delete(_selectedReceiptId);
            MessageBox.Show(msg);

            if (ok)
            {
                ClearForm();
                LoadReceipts();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedReceiptId = 0;
            txtReceiptCode.Text = "";
            txtNote.Text = "";
            txtQty.Text = "1";
            txtUnitPrice.Text = "0";
            txtReceiptSearch.Text = "";
            txtReceiptCode.Focus();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportHelper.ExportDataGridViewToExcel(dgvReceipts, "PhieuNhap");
        }

    }
}
