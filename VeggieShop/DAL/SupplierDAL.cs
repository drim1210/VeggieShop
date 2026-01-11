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
    public class SupplierDAL
    {
        public DataTable GetAll(string keyword)
        {
            string sql = @"
SELECT SupplierId, SupplierCode, SupplierName, Phone, Address, IsActive
FROM dbo.Suppliers
WHERE (@kw = N'' OR SupplierCode LIKE N'%' + @kw + N'%' OR SupplierName LIKE N'%' + @kw + N'%')
ORDER BY SupplierId DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@kw", (keyword ?? "").Trim()));
        }

        public bool ExistsCode(string supplierCode, int ignoreSupplierId)
        {
            string sql = @"
SELECT COUNT(1)
FROM dbo.Suppliers
WHERE SupplierCode = @code AND SupplierId <> @id;";

            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@code", supplierCode),
                new SqlParameter("@id", ignoreSupplierId));

            int count = (int)dt.Rows[0][0];
            return count > 0;
        }

        public int Insert(string code, string name, string phone, string address, bool isActive)
        {
            string sql = @"
INSERT INTO dbo.Suppliers (SupplierCode, SupplierName, Phone, Address, IsActive)
VALUES (@code, @name, @phone, @addr, @active);";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@addr", (object)address ?? DBNull.Value),
                new SqlParameter("@active", isActive));
        }

        public int Update(int id, string code, string name, string phone, string address, bool isActive)
        {
            string sql = @"
UPDATE dbo.Suppliers
SET SupplierCode = @code,
    SupplierName = @name,
    Phone = @phone,
    Address = @addr,
    IsActive = @active
WHERE SupplierId = @id;";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@id", id),
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@addr", (object)address ?? DBNull.Value),
                new SqlParameter("@active", isActive));
        }

        public int Delete(int id)
        {
            string sql = @"DELETE FROM dbo.Suppliers WHERE SupplierId = @id;";
            return DbHelper.ExecuteNonQuery(sql, new SqlParameter("@id", id));
        }
    }
}
