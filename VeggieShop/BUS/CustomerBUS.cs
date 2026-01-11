using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class CustomerBUS
    {
        private readonly CustomerDAL _dal = new CustomerDAL();

        public DataTable GetAll(string keyword)
        {
            return _dal.GetAll(keyword);
        }

        public (bool ok, string msg) Create(string code, string name, string phone, string address, string pointsText, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            phone = (phone ?? "").Trim();
            address = (address ?? "").Trim();

            if (code.Length == 0) return (false, "Mã khách hàng không được để trống.");
            if (name.Length == 0) return (false, "Tên khách hàng không được để trống.");
            if (_dal.ExistsCode(code, 0)) return (false, "Mã khách hàng đã tồn tại.");

            int points = 0;
            if (!int.TryParse((pointsText ?? "0").Trim(), out points) || points < 0)
                return (false, "Điểm tích lũy phải là số nguyên >= 0.");

            _dal.Insert(code, name, phone, address, points, isActive);
            return (true, "Thêm khách hàng thành công.");
        }

        public (bool ok, string msg) Update(int id, string code, string name, string phone, string address, string pointsText, bool isActive)
        {
            code = (code ?? "").Trim();
            name = (name ?? "").Trim();
            phone = (phone ?? "").Trim();
            address = (address ?? "").Trim();

            if (id <= 0) return (false, "Chưa chọn khách hàng.");
            if (code.Length == 0) return (false, "Mã khách hàng không được để trống.");
            if (name.Length == 0) return (false, "Tên khách hàng không được để trống.");
            if (_dal.ExistsCode(code, id)) return (false, "Mã khách hàng đã tồn tại.");

            int points = 0;
            if (!int.TryParse((pointsText ?? "0").Trim(), out points) || points < 0)
                return (false, "Điểm tích lũy phải là số nguyên >= 0.");

            _dal.Update(id, code, name, phone, address, points, isActive);
            return (true, "Cập nhật khách hàng thành công.");
        }

        public (bool ok, string msg) Delete(int id)
        {
            if (id <= 0) return (false, "Chưa chọn khách hàng.");
            _dal.Delete(id);
            return (true, "Xóa khách hàng thành công.");
        }
    }
}
