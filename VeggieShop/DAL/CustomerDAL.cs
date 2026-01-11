using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Data;
using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class CustomerDAL
    {
        public DataTable GetAll(string keyword)
        {
            string sql = @"
SELECT CustomerId, CustomerCode, CustomerName, Phone, Address, Points, IsActive
FROM dbo.Customers
WHERE (@kw = N'' 
       OR CustomerCode LIKE N'%' + @kw + N'%' 
       OR CustomerName LIKE N'%' + @kw + N'%'
       OR Phone LIKE N'%' + @kw + N'%')
ORDER BY CustomerId DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@kw", (keyword ?? "").Trim()));
        }

        public bool ExistsCode(string customerCode, int ignoreCustomerId)
        {
            string sql = @"
SELECT COUNT(1)
FROM dbo.Customers
WHERE CustomerCode = @code AND CustomerId <> @id;";

            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@code", customerCode),
                new SqlParameter("@id", ignoreCustomerId));

            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count > 0;
        }

        public int Insert(string code, string name, string phone, string address, int points, bool isActive)
        {
            string sql = @"
INSERT INTO dbo.Customers (CustomerCode, CustomerName, Phone, Address, Points, IsActive)
VALUES (@code, @name, @phone, @addr, @points, @active);";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@addr", (object)address ?? DBNull.Value),
                new SqlParameter("@points", points),
                new SqlParameter("@active", isActive));
        }

        public int Update(int id, string code, string name, string phone, string address, int points, bool isActive)
        {
            string sql = @"
UPDATE dbo.Customers
SET CustomerCode = @code,
    CustomerName = @name,
    Phone = @phone,
    Address = @addr,
    Points = @points,
    IsActive = @active
WHERE CustomerId = @id;";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@id", id),
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@addr", (object)address ?? DBNull.Value),
                new SqlParameter("@points", points),
                new SqlParameter("@active", isActive));
        }

        public int Delete(int id)
        {
            string sql = @"DELETE FROM dbo.Customers WHERE CustomerId = @id;";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id));
        }
    }
}
