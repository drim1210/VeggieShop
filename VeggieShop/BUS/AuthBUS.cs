using System;
using VeggieShop.DAL;
using VeggieShop.DTO;
using VeggieShop.Utils;

namespace VeggieShop.BUS
{
    public class AuthBUS
    {
        private readonly AuthDAL _dal = new AuthDAL();

        public (bool ok, string message, UserSession session) Login(string user, string pass)
        {
            user = (user ?? "").Trim();
            pass = pass ?? "";

            if (user.Length == 0 || pass.Length == 0)
                return (false, "Vui lòng nhập username và password.", null);

            var data = _dal.GetUser(user);
            if (data == null) return (false, "Sai tài khoản hoặc mật khẩu.", null);

            var (id, u, name, hash, salt, role, active) = data.Value;
            if (!active) return (false, "Tài khoản bị khóa.", null);

            if (!PasswordHasher.Verify(pass, salt, hash))
                return (false, "Sai tài khoản hoặc mật khẩu.", null);

            var session = new UserSession
            {
                UserId = id,
                Username = u,
                FullName = name,
                RoleName = role
            };

            

            return (true, "OK", session);
        }
    }
}
