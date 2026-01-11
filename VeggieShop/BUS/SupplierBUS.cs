using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class SupplierBUS
    {
        private readonly SupplierDAL _dal = new SupplierDAL();

        public DataTable GetAll(string keyword)
        {
            return _dal.GetAll(keyword);
        }

        public (bool ok, string msg) Create(string code, string name, string phone, string address, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            phone = (phone ?? "").Trim();
            address = (address ?? "").Trim();

            if (code.Length == 0) return (false, "Mã nhà cung cấp không được để trống.");
            if (name.Length == 0) return (false, "Tên nhà cung cấp không được để trống.");
            if (_dal.ExistsCode(code, 0)) return (false, "Mã nhà cung cấp đã tồn tại.");

            _dal.Insert(code, name, phone, address, isActive);
            return (true, "Thêm nhà cung cấp thành công.");
        }

        public (bool ok, string msg) Update(int id, string code, string name, string phone, string address, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            phone = (phone ?? "").Trim();
            address = (address ?? "").Trim();

            if (id <= 0) return (false, "Chưa chọn nhà cung cấp.");
            if (code.Length == 0) return (false, "Mã nhà cung cấp không được để trống.");
            if (name.Length == 0) return (false, "Tên nhà cung cấp không được để trống.");
            if (_dal.ExistsCode(code, id)) return (false, "Mã nhà cung cấp đã tồn tại.");

            _dal.Update(id, code, name, phone, address, isActive);
            return (true, "Cập nhật nhà cung cấp thành công.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            if (id <= 0) return (false, "Chưa chọn nhà cung cấp.");
            _dal.Delete(id);
            return (true, "Xóa nhà cung cấp thành công.");
        }
    }
}
