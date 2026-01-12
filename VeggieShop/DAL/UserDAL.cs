using System.Data;
using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class UserDAL
    {
        public DataRow GetUserById(int userId)
        {
            string sql = @"SELECT UserId, Username, PasswordHash, PasswordSalt, IsActive
                           FROM Users
                           WHERE UserId = @id";
            var dt = DbHelper.ExecuteQuery(sql, new SqlParameter("@id", userId));
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public int UpdatePassword(int userId, byte[] newHash, byte[] salt)
        {
            string sql = @"UPDATE Users
                           SET PasswordHash = @hash, PasswordSalt = @salt
                           WHERE UserId = @id";
            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@hash", newHash),
                new SqlParameter("@salt", salt),
                new SqlParameter("@id", userId));
        }
    }
}
