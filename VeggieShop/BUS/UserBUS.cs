using System;
using VeggieShop.DAL;
using VeggieShop.Utils;

namespace VeggieShop.BUS
{
    public class UserBUS
    {
        private readonly UserDAL _dal = new UserDAL();

        public (bool ok, string msg) ChangePassword(int userId, string oldPass, string newPass, string confirmPass)
        {
            if (string.IsNullOrWhiteSpace(oldPass) ||
                string.IsNullOrWhiteSpace(newPass) ||
                string.IsNullOrWhiteSpace(confirmPass))
                return (false, "Vui lòng nhập đầy đủ thông tin.");

            if (newPass != confirmPass)
                return (false, "Mật khẩu mới và nhập lại không khớp.");

            if (newPass.Length < 6)
                return (false, "Mật khẩu mới phải từ 6 ký tự trở lên.");

            var row = _dal.GetUserById(userId);
            if (row == null) return (false, "Không tìm thấy người dùng.");

            var hash = (byte[])row["PasswordHash"];
            var salt = (byte[])row["PasswordSalt"];

            // ✅ check mật khẩu cũ
            if (!PasswordHasher.Verify(oldPass, salt, hash))
                return (false, "Mật khẩu cũ không đúng.");

            // ✅ tạo hash mới (giữ salt cũ)
            var newHash = PasswordHasher.ComputeHash(newPass, salt);

            int n = _dal.UpdatePassword(userId, newHash, salt);
            return n > 0 ? (true, "Đổi mật khẩu thành công!") : (false, "Đổi mật khẩu thất bại.");
        }
    }
}
