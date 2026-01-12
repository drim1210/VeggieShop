using System;
using System.Data;
using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class TopProductsDAL
    {
        public DataTable GetTopProducts(DateTime from, DateTime to, int topN, string orderBy)
        {
            // orderBy: "qty" hoặc "revenue"
            string orderClause = (orderBy == "revenue")
                ? "ORDER BY TotalRevenue DESC"
                : "ORDER BY TotalQty DESC";

            string sql = $@"
SELECT TOP (@topN)
    p.ProductId,
    p.ProductName,
    SUM(d.Quantity) AS TotalQty,
    SUM(d.Quantity * d.UnitPrice) AS TotalRevenue
FROM SalesInvoiceDetails d
JOIN SalesInvoices i ON i.InvoiceId = d.InvoiceId
JOIN Products p ON p.ProductId = d.ProductId
WHERE i.InvoiceDate >= @from
  AND i.InvoiceDate < DATEADD(day, 1, @to)
GROUP BY p.ProductId, p.ProductName
{orderClause};";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@topN", topN),
                new SqlParameter("@from", from.Date),
                new SqlParameter("@to", to.Date));
        }
    }
}
