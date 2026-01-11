using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class AuthDAL
    {
        public (int, string, string, byte[], byte[], string, bool)? GetUser(string username)
        {
            string sql = @"
            SELECT u.UserId, u.Username, u.FullName, u.PasswordHash, u.PasswordSalt, u.IsActive,
                   ISNULL(r.RoleName,'Staff') RoleName
            FROM Users u
            LEFT JOIN UserRoles ur ON u.UserId = ur.UserId
            LEFT JOIN Roles r ON ur.RoleId = r.RoleId
            WHERE u.Username = @u";

            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@u", username));

            if (dt.Rows.Count == 0) return null;

            var r = dt.Rows[0];
            return (
                (int)r["UserId"],
                (string)r["Username"],
                (string)r["FullName"],
                (byte[])r["PasswordHash"],
                (byte[])r["PasswordSalt"],
                (string)r["RoleName"],
                (bool)r["IsActive"]
            );
        }
    }
}
