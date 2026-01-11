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
    public class ProductDAL
    {
        public DataTable GetCategoriesForCombo()
        {
            string sql = @"SELECT CategoryId, CategoryName FROM dbo.Categories WHERE IsActive = 1 ORDER BY CategoryName;";
            return DbHelper.ExecuteQuery(sql);
        }

        public DataTable GetAll(string keyword, int categoryId)
        {
            string sql = @"
SELECT p.ProductId, p.ProductCode, p.ProductName,
       p.CategoryId, c.CategoryName,
       p.Unit, p.PurchasePrice, p.SalePrice, p.StockQty, p.IsActive
FROM dbo.Products p
JOIN dbo.Categories c ON c.CategoryId = p.CategoryId
WHERE
    (@kw = N'' OR p.ProductCode LIKE N'%' + @kw + N'%' OR p.ProductName LIKE N'%' + @kw + N'%')
    AND (@catId = 0 OR p.CategoryId = @catId)
ORDER BY p.ProductId DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@kw", (keyword ?? "").Trim()),
                new SqlParameter("@catId", categoryId));
        }

        public bool ExistsCode(string productCode, int ignoreProductId)
        {
            string sql = @"SELECT COUNT(1) FROM dbo.Products WHERE ProductCode = @code AND ProductId <> @id;";
            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@code", productCode),
                new SqlParameter("@id", ignoreProductId));
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public int Insert(string code, string name, int categoryId, string unit,
                          decimal purchasePrice, decimal salePrice, decimal stockQty, bool isActive)
        {
            string sql = @"
INSERT INTO dbo.Products (ProductCode, ProductName, CategoryId, Unit, PurchasePrice, SalePrice, StockQty, IsActive)
VALUES (@code, @name, @catId, @unit, @buy, @sell, @stock, @active);";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@catId", categoryId),
                new SqlParameter("@unit", unit),
                new SqlParameter("@buy", purchasePrice),
                new SqlParameter("@sell", salePrice),
                new SqlParameter("@stock", stockQty),
                new SqlParameter("@active", isActive));
        }

        public int Update(int id, string code, string name, int categoryId, string unit,
                          decimal purchasePrice, decimal salePrice, decimal stockQty, bool isActive)
        {
            string sql = @"
UPDATE dbo.Products
SET ProductCode = @code,
    ProductName = @name,
    CategoryId = @catId,
    Unit = @unit,
    PurchasePrice = @buy,
    SalePrice = @sell,
    StockQty = @stock,
    IsActive = @active
WHERE ProductId = @id;";

            return DbHelper.ExecuteNonQuery(sql,
                new SqlParameter("@id", id),
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@catId", categoryId),
                new SqlParameter("@unit", unit),
                new SqlParameter("@buy", purchasePrice),
                new SqlParameter("@sell", salePrice),
                new SqlParameter("@stock", stockQty),
                new SqlParameter("@active", isActive));
        }

        public int Delete(int id)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM dbo.Products WHERE ProductId = @id;",
                new SqlParameter("@id", id));
        }
    }
}
