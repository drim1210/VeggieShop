using System;
using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class GoodsReceiptBUS
    {
        private readonly GoodsReceiptDAL _dal = new GoodsReceiptDAL();

        public DataTable GetSuppliers() => _dal.GetSuppliers();
        public DataTable GetProducts() => _dal.GetProducts();
        public DataTable GetReceipts(string keyword) => _dal.GetReceipts(keyword);

        public (bool ok, string msg) SaveSingle(string code, int supplierId, DateTime date, string note,
            int productId, string qtyText, string priceText)
        {
            code = (code ?? "").Trim();
            if (code == "") return (false, "Chưa nhập mã phiếu.");
            if (supplierId <= 0) return (false, "Chưa chọn nhà cung cấp.");
            if (productId <= 0) return (false, "Chưa chọn sản phẩm.");

            if (!decimal.TryParse(qtyText, out var qty) || qty <= 0)
                return (false, "Số lượng phải > 0.");

            if (!decimal.TryParse(priceText, out var price) || price < 0)
                return (false, "Giá nhập không hợp lệ.");

            if (_dal.ReceiptCodeExists(code))
                return (false, "Mã phiếu đã tồn tại.");

            _dal.InsertReceiptSingle(code, supplierId, date, note, productId, qty, price);
            return (true, "Lưu phiếu nhập thành công (tồn kho đã tăng).");
        }

        public (bool ok, string msg) LoadReceiptToForm(int receiptId,
            out string code, out int supplierId, out DateTime date, out string note,
            out int productId, out decimal qty, out decimal price)
        {
            code = ""; supplierId = 0; date = DateTime.Now; note = "";
            productId = 0; qty = 0; price = 0;

            var h = _dal.GetReceiptHeader(receiptId);
            var d = _dal.GetReceiptDetailSingle(receiptId);
            if (h == null || d == null) return (false, "Không tìm thấy phiếu.");

            code = h["ReceiptCode"].ToString();
            supplierId = Convert.ToInt32(h["SupplierId"]);
            date = Convert.ToDateTime(h["ReceiptDate"]);
            note = h["Note"] == DBNull.Value ? "" : h["Note"].ToString();

            productId = Convert.ToInt32(d["ProductId"]);
            qty = Convert.ToDecimal(d["Quantity"]);
            price = Convert.ToDecimal(d["UnitPrice"]);

            return (true, "OK");
        }

        public (bool ok, string msg) UpdateSingle(int receiptId, string code, int supplierId, DateTime date, string note,
            int productId, string qtyText, string priceText)
        {
            if (receiptId <= 0) return (false, "Chưa chọn phiếu để sửa.");

            code = (code ?? "").Trim();
            if (code == "") return (false, "Chưa nhập mã phiếu.");

            if (!decimal.TryParse(qtyText, out var qty) || qty <= 0)
                return (false, "Số lượng phải > 0.");

            if (!decimal.TryParse(priceText, out var price) || price < 0)
                return (false, "Giá nhập không hợp lệ.");

            _dal.UpdateReceiptSingle(receiptId, code, supplierId, date, note, productId, qty, price);
            return (true, "Cập nhật phiếu nhập thành công.");
        }

        public (bool ok, string msg) Delete(int receiptId)
        {
            if (receiptId <= 0) return (false, "Chưa chọn phiếu để xóa.");
            _dal.DeleteReceipt(receiptId);
            return (true, "Xóa phiếu nhập thành công (đã hoàn tồn kho).");
        }
    }
}
