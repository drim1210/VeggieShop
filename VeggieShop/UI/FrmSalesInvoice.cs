using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VeggieShop.Utils;
using VeggieShop.BUS;

namespace VeggieShop.UI
{
    public partial class FrmSalesInvoice : Form
    {
        private readonly SalesInvoiceBUS _bus = new SalesInvoiceBUS();
        private int _selectedInvoiceId = 0;

        public FrmSalesInvoice()
        {
            InitializeComponent();
        }

        private void FrmSalesInvoice_Load(object sender, EventArgs e)
        {
            // Load khách hàng
            cboCustomer.DisplayMember = "CustomerName";
            cboCustomer.ValueMember = "CustomerId";
            cboCustomer.DataSource = _bus.GetCustomers();

            // Load sản phẩm (CÓ SellPrice)
            cboProduct.DisplayMember = "ProductName";
            cboProduct.ValueMember = "ProductId";
            cboProduct.DataSource = _bus.GetProducts();

            // 🔴 GẮN EVENT SAU KHI SET DATASOURCE
            cboProduct.SelectedIndexChanged += cboProduct_SelectedIndexChanged;

            // Giá trị mặc định
            txtQty.Text = "1";
            txtUnitPrice.Text = "0";

            // 🔴 GỌI CHẠY LẦN ĐẦU
            cboProduct_SelectedIndexChanged(null, null);

            LoadInvoices();
        }
        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboProduct.SelectedItem is DataRowView row)
                {
                    // 🔴 LẤY GIÁ BÁN TỪ DATASOURCE
                    var priceObj = row["SalePrice"]; // đổi tên nếu DB khác
                    decimal price = 0;

                    if (priceObj != DBNull.Value)
                        price = Convert.ToDecimal(priceObj);

                    txtUnitPrice.Text = price.ToString();
                }
            }
            catch
            {
                // tránh lỗi khi form đang load
            }
        }


        private void LoadInvoices()
        {
            dgvInvoices.AutoGenerateColumns = true;
            dgvInvoices.DataSource = _bus.GetInvoices(txtSearch.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvInvoices.Rows[e.RowIndex];
            if (row.Cells["InvoiceId"].Value == null) return;

            _selectedInvoiceId = Convert.ToInt32(row.Cells["InvoiceId"].Value);

            var res = _bus.LoadInvoiceToForm(_selectedInvoiceId,
                out var code, out var customerId, out var date, out var note,
                out var productId, out var qty, out var price);

            if (!res.ok)
            {
                MessageBox.Show(res.msg);
                return;
            }

            txtInvoiceCode.Text = code;
            cboCustomer.SelectedValue = customerId;
            dtpDate.Value = date;
            txtNote.Text = note;

            cboProduct.SelectedValue = productId;
            txtQty.Text = qty.ToString();
            txtUnitPrice.Text = price.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedValue == null || cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Sản phẩm.");
                return;
            }

            if (_selectedInvoiceId > 0)
            {
                MessageBox.Show("Bạn đang chọn 1 hóa đơn. Muốn sửa hãy bấm 'Sửa'. Muốn tạo mới hãy bấm 'Làm mới'.");
                return;
            }

            var (ok, msg) = _bus.SaveSingle(
                txtInvoiceCode.Text,
                Convert.ToInt32(cboCustomer.SelectedValue),
                dtpDate.Value,
                txtNote.Text,
                Convert.ToInt32(cboProduct.SelectedValue),
                txtQty.Text,
                txtUnitPrice.Text);

            MessageBox.Show(msg);

            if (ok)
            {
                ClearForm();
                LoadInvoices();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId <= 0)
            {
                MessageBox.Show("Chọn 1 hóa đơn để sửa.");
                return;
            }

            if (cboCustomer.SelectedValue == null || cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Sản phẩm.");
                return;
            }

            var (ok, msg) = _bus.UpdateSingle(
                _selectedInvoiceId,
                txtInvoiceCode.Text,
                Convert.ToInt32(cboCustomer.SelectedValue),
                dtpDate.Value,
                txtNote.Text,
                Convert.ToInt32(cboProduct.SelectedValue),
                txtQty.Text,
                txtUnitPrice.Text);

            MessageBox.Show(msg);

            if (ok)
            {
                ClearForm();
                LoadInvoices();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId <= 0)
            {
                MessageBox.Show("Chọn 1 hóa đơn để xóa.");
                return;
            }

            var confirm = MessageBox.Show("Xóa hóa đơn này? (sẽ hoàn tồn kho)", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            var (ok, msg) = _bus.Delete(_selectedInvoiceId);
            MessageBox.Show(msg);

            if (ok)
            {
                ClearForm();
                LoadInvoices();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedInvoiceId = 0;
            txtInvoiceCode.Text = "";
            txtNote.Text = "";
            txtQty.Text = "1";
            txtUnitPrice.Text = "0";
            txtSearch.Text = "";
            txtInvoiceCode.Focus();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            
            ExportHelper.ExportDataGridViewToExcel(dgvInvoices, "HoaDonBan");
        }


        
        private void FrmSalesInvoice_Click(object sender, EventArgs e) { }
    }
}
