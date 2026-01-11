using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class ProductBUS
    {
        private readonly ProductDAL _dal = new ProductDAL();

        public DataTable GetCategoriesForCombo()
        {
            return _dal.GetCategoriesForCombo();
        }

        public DataTable GetAll(string keyword, int categoryId)
        {
            return _dal.GetAll(keyword, categoryId);
        }

        public (bool ok, string msg) Create(string code, string name, int categoryId, string unit,
            string purchaseText, string saleText, string stockText, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            unit = (unit ?? "").Trim();

            if (code.Length == 0) return (false, "Mã sản phẩm không được để trống.");
            if (name.Length == 0) return (false, "Tên sản phẩm không được để trống.");
            if (categoryId <= 0) return (false, "Vui lòng chọn danh mục.");
            if (unit.Length == 0) unit = "kg";
            if (_dal.ExistsCode(code, 0)) return (false, "Mã sản phẩm đã tồn tại.");

            if (!decimal.TryParse(purchaseText, out var purchase) || purchase < 0) return (false, "Giá nhập không hợp lệ.");
            if (!decimal.TryParse(saleText, out var sale) || sale < 0) return (false, "Giá bán không hợp lệ.");
            if (!decimal.TryParse(stockText, out var stock) || stock < 0) return (false, "Tồn kho không hợp lệ.");

            _dal.Insert(code, name, categoryId, unit, purchase, sale, stock, isActive);
            return (true, "Thêm sản phẩm thành công.");
        }

        public (bool ok, string msg) Update(int id, string code, string name, int categoryId, string unit,
            string purchaseText, string saleText, string stockText, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            unit = (unit ?? "").Trim();

            if (id <= 0) return (false, "Chưa chọn sản phẩm.");
            if (code.Length == 0) return (false, "Mã sản phẩm không được để trống.");
            if (name.Length == 0) return (false, "Tên sản phẩm không được để trống.");
            if (categoryId <= 0) return (false, "Vui lòng chọn danh mục.");
            if (unit.Length == 0) unit = "kg";
            if (_dal.ExistsCode(code, id)) return (false, "Mã sản phẩm đã tồn tại.");

            if (!decimal.TryParse(purchaseText, out var purchase) || purchase < 0) return (false, "Giá nhập không hợp lệ.");
            if (!decimal.TryParse(saleText, out var sale) || sale < 0) return (false, "Giá bán không hợp lệ.");
            if (!decimal.TryParse(stockText, out var stock) || stock < 0) return (false, "Tồn kho không hợp lệ.");

            _dal.Update(id, code, name, categoryId, unit, purchase, sale, stock, isActive);
            return (true, "Cập nhật sản phẩm thành công.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            if (id <= 0) return (false, "Chưa chọn sản phẩm.");
            _dal.Delete(id);
            return (true, "Xóa sản phẩm thành công.");
        }
    }
}
