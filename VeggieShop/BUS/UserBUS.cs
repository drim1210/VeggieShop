using System;
using VeggieShop.DAL;
using VeggieShop.Utils;

namespace VeggieShop.BUS
{
    public class UserBUS
    {
        private readonly UserDAL _dal = new UserDAL();

        // ===== ĐĂNG KÝ =====
        public (bool ok, string msg) Register(
            string username,
            string fullName,
            string password,
            string confirm,
            string roleName)
        {
            username = (username ?? "").Trim();
            fullName = (fullName ?? "").Trim();
            roleName = string.IsNullOrWhiteSpace(roleName) ? "Staff" : roleName.Trim();

            if (username.Length == 0 || password.Length == 0)
                return (false, "Username và mật khẩu không được để trống.");

            if (password.Length < 6)
                return (false, "Mật khẩu phải ≥ 6 ký tự.");

            if (password != confirm)
                return (false, "Xác nhận mật khẩu không khớp.");

            // chỉ cho tạo Admin nếu đang login là Admin (đúng ý bạn)
            if (roleName == "Admin" && VeggieShop.DTO.UserSession.Current.RoleName != "Admin")
                return (false, "Chỉ Admin mới được tạo tài khoản Admin.");

           

            // tạo salt + hash
            byte[] salt = new byte[16];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            byte[] hash = PasswordHasher.ComputeHash(password, salt);

            // insert user
            int newUserId = _dal.InsertUser(username, fullName, hash, salt, 1);
            if (newUserId <= 0)
                return (false, "Tạo tài khoản thất bại.");

            // gán role
            int roleId = _dal.GetRoleIdByName(roleName);
            if (roleId > 0)
                _dal.AddUserRole(newUserId, roleId);

            return (true, "Tạo tài khoản thành công.");
        }

        // ===== ĐỔI MẬT KHẨU =====
        public (bool ok, string msg) ChangePassword(
            int userId,
            string oldPass,
            string newPass,
            string confirmPass)
        {
            if (userId <= 0)
                return (false, "Người dùng chưa đăng nhập.");

            if (string.IsNullOrWhiteSpace(newPass) || newPass.Length < 6)
                return (false, "Mật khẩu mới phải ≥ 6 ký tự.");

            if (newPass != confirmPass)
                return (false, "Xác nhận mật khẩu không khớp.");

            var row = _dal.GetUserById(userId);
            if (row == null)
                return (false, "Không tìm thấy người dùng.");

            var hash = (byte[])row["PasswordHash"];
            var salt = (byte[])row["PasswordSalt"];

            if (!PasswordHasher.Verify(oldPass, salt, hash))
                return (false, "Mật khẩu cũ không đúng.");

            byte[] newSalt = new byte[16];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
                rng.GetBytes(newSalt);

            byte[] newHash = PasswordHasher.ComputeHash(newPass, newSalt);

            int n = _dal.UpdatePassword(userId, newHash, newSalt);
            return n > 0
                ? (true, "Đổi mật khẩu thành công.")
                : (false, "Đổi mật khẩu thất bại.");
        }
    }
}
