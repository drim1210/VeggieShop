using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class CategoryBUS
    {
        private readonly CategoryDAL _dal = new CategoryDAL();

        public DataTable GetAll(string keyword)
        {
            return _dal.GetAll(keyword);
        }

        public (bool ok, string msg) Create(string code, string name, string description, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            description = (description ?? "").Trim();

            if (code.Length == 0) return (false, "Mã danh mục không được để trống.");
            if (name.Length == 0) return (false, "Tên danh mục không được để trống.");
            if (_dal.ExistsCode(code, 0)) return (false, "Mã danh mục đã tồn tại.");

            _dal.Insert(code, name, description, isActive);
            return (true, "Thêm danh mục thành công.");
        }

        public (bool ok, string msg) Update(int id, string code, string name, string description, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            description = (description ?? "").Trim();

            if (id <= 0) return (false, "Chưa chọn danh mục.");
            if (code.Length == 0) return (false, "Mã danh mục không được để trống.");
            if (name.Length == 0) return (false, "Tên danh mục không được để trống.");
            if (_dal.ExistsCode(code, id)) return (false, "Mã danh mục đã tồn tại.");

            _dal.Update(id, code, name, description, isActive);
            return (true, "Cập nhật danh mục thành công.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            if (id <= 0) return (false, "Chưa chọn danh mục.");
            _dal.Delete(id);
            return (true, "Xóa danh mục thành công.");
        }
    }
}
