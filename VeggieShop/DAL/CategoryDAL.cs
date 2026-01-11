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
    public class CategoryDAL
    {
        public DataTable GetAll(string keyword)
        {
            string sql = @"
SELECT CategoryId, CategoryCode, CategoryName, Description, IsActive
FROM dbo.Categories
WHERE (@kw = N'' 
       OR CategoryCode LIKE N'%' + @kw + N'%'
       OR CategoryName LIKE N'%' + @kw + N'%')
ORDER BY CategoryId DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@kw", (keyword ?? "").Trim()));
        }

        public bool ExistsCode(string categoryCode, int ignoreCategoryId)
        {
            string sql = @"
SELECT COUNT(1)
FROM dbo.Categories
WHERE CategoryCode = @code AND CategoryId <> @id;";

            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@code", categoryCode),
                new SqlParameter("@id", ignoreCategoryId));

            int count = Convert.ToInt32(dt.Rows[0][0]);
            return count > 0;
        }

        public int Insert(string code, string name, string description, bool isActive)
        {
            string sql = @"
INSERT INTO dbo.Categories (CategoryCode, CategoryName, Description, IsActive)
VALUES (@code, @name, @desc, @active);";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@desc", (object)description ?? DBNull.Value),
                new SqlParameter("@active", isActive));
        }

        public int Update(int id, string code, string name, string description, bool isActive)
        {
            string sql = @"
UPDATE dbo.Categories
SET CategoryCode = @code,
    CategoryName = @name,
    Description = @desc,
    IsActive = @active
WHERE CategoryId = @id;";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@id", id),
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@desc", (object)description ?? DBNull.Value),
                new SqlParameter("@active", isActive));
        }

        public int Delete(int id)
        {
            string sql = @"DELETE FROM dbo.Categories WHERE CategoryId = @id;";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id));
        }
    }
}
