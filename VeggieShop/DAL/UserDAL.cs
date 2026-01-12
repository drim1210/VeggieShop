using System;
using System.Data;
using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class UserDAL
    {
        public DataRow GetUser(string username)
        {
            string sql = @"
        SELECT UserId, Username, FullName,
               PasswordHash, PasswordSalt,
               RoleName, IsActive
        FROM Users
        WHERE Username = @u";

            var dt = DbHelper.ExecuteQuery(
                sql,
                new SqlParameter("@u", username)
            );

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }


        public int InsertUser(string username, string fullName, byte[] hash, byte[] salt, int isActive = 1)
        {
            string sql = @"
INSERT INTO Users(Username, FullName, PasswordHash, PasswordSalt, IsActive)
VALUES(@u, @f, @h, @s, @a);
SELECT SCOPE_IDENTITY();";

            object idObj = DbHelper.ExecuteScalar(sql,
                new SqlParameter("@u", username),
                new SqlParameter("@f", fullName),
                new SqlParameter("@h", hash),
                new SqlParameter("@s", salt),
                new SqlParameter("@a", isActive)
            );

            return Convert.ToInt32(idObj);
        }

        public int GetRoleIdByName(string roleName)
        {
            string sql = "SELECT TOP 1 RoleId FROM Roles WHERE RoleName=@r";
            object obj = DbHelper.ExecuteScalar(sql, new SqlParameter("@r", roleName));
            return obj == null || obj == DBNull.Value ? 0 : Convert.ToInt32(obj);
        }

        public int AddUserRole(int userId, int roleId)
        {
            // tránh insert trùng
            string sql = @"
IF NOT EXISTS (SELECT 1 FROM UserRoles WHERE UserId=@u AND RoleId=@r)
    INSERT INTO UserRoles(UserId, RoleId) VALUES(@u, @r);";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@u", userId),
                new SqlParameter("@r", roleId)
            );
        }

        // ====== Bạn đang có sẵn 2 hàm đổi mật khẩu thì giữ lại ======
        public DataRow GetUserById(int userId)
        {
            string sql = "SELECT * FROM Users WHERE UserId=@id";
            var dt = DbHelper.ExecuteQuery(sql, new SqlParameter("@id", userId));
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public int UpdatePassword(int userId, byte[] hash, byte[] salt)
        {
            string sql = @"
UPDATE Users
SET PasswordHash=@h, PasswordSalt=@s
WHERE UserId=@id";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@h", hash),
                new SqlParameter("@s", salt),
                new SqlParameter("@id", userId)
            );
        }
    }
}
