using System;
using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class SalesInvoiceBUS
    {
        private readonly SalesInvoiceDAL _dal = new SalesInvoiceDAL();

        public DataTable GetCustomers() => _dal.GetCustomers();
        public DataTable GetProducts() => _dal.GetProducts();
        public DataTable GetInvoices(string keyword) => _dal.GetInvoices(keyword);

        public (bool ok, string msg) SaveSingle(string code, int customerId, DateTime date, string note,
            int productId, string qtyText, string priceText)
        {
            code = (code ?? "").Trim();
            if (code == "") return (false, "Chưa nhập mã hóa đơn.");
            if (customerId <= 0) return (false, "Chưa chọn khách hàng.");
            if (productId <= 0) return (false, "Chưa chọn sản phẩm.");

            if (!decimal.TryParse(qtyText, out var qty) || qty <= 0)
                return (false, "Số lượng phải > 0.");

            if (!decimal.TryParse(priceText, out var price) || price < 0)
                return (false, "Đơn giá bán không hợp lệ.");

            if (_dal.InvoiceCodeExists(code))
                return (false, "Mã hóa đơn đã tồn tại.");

            try
            {
                _dal.InsertInvoiceSingle(code, customerId, date, note, productId, qty, price);
                return (true, "Bán hàng thành công (tồn kho đã giảm).");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool ok, string msg) LoadInvoiceToForm(int invoiceId,
            out string code, out int customerId, out DateTime date, out string note,
            out int productId, out decimal qty, out decimal price)
        {
            code = ""; customerId = 0; date = DateTime.Now; note = "";
            productId = 0; qty = 0; price = 0;

            var h = _dal.GetInvoiceHeader(invoiceId);
            var d = _dal.GetInvoiceDetailSingle(invoiceId);
            if (h == null || d == null) return (false, "Không tìm thấy hóa đơn.");

            code = h["InvoiceCode"].ToString();
            customerId = Convert.ToInt32(h["CustomerId"]);
            date = Convert.ToDateTime(h["InvoiceDate"]);
            note = h["Note"] == DBNull.Value ? "" : h["Note"].ToString();

            productId = Convert.ToInt32(d["ProductId"]);
            qty = Convert.ToDecimal(d["Quantity"]);
            price = Convert.ToDecimal(d["UnitPrice"]);

            return (true, "OK");
        }

        public (bool ok, string msg) UpdateSingle(int invoiceId, string code, int customerId, DateTime date, string note,
            int productId, string qtyText, string priceText)
        {
            if (invoiceId <= 0) return (false, "Chưa chọn hóa đơn để sửa.");

            if (!decimal.TryParse(qtyText, out var qty) || qty <= 0)
                return (false, "Số lượng phải > 0.");

            if (!decimal.TryParse(priceText, out var price) || price < 0)
                return (false, "Đơn giá bán không hợp lệ.");

            try
            {
                _dal.UpdateInvoiceSingle(invoiceId, code, customerId, date, note, productId, qty, price);
                return (true, "Cập nhật hóa đơn thành công.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public (bool ok, string msg) Delete(int invoiceId)
        {
            if (invoiceId <= 0) return (false, "Chưa chọn hóa đơn để xóa.");
            try
            {
                _dal.DeleteInvoice(invoiceId);
                return (true, "Xóa hóa đơn thành công (đã hoàn tồn kho).");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
