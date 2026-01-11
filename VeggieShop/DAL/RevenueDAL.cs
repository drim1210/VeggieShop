using System;
using System.Data;
using System.Data.SqlClient;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class RevenueDAL
    {
        public DataTable GetRevenueByDay(DateTime from, DateTime to)
        {
            string sql = @"
SELECT 
    CAST(InvoiceDate AS date) AS [Ngay],
    COUNT(*) AS [SoHoaDon],
    SUM(TotalAmount) AS [DoanhThu]
FROM SalesInvoices
WHERE InvoiceDate >= @from 
  AND InvoiceDate < DATEADD(day, 1, @to)
GROUP BY CAST(InvoiceDate AS date)
ORDER BY [Ngay] DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@from", from.Date),
                new SqlParameter("@to", to.Date));
        }

        public DataTable GetRevenueByMonth(DateTime from, DateTime to)
        {
            string sql = @"
SELECT 
    YEAR(InvoiceDate) AS [Nam],
    MONTH(InvoiceDate) AS [Thang],
    COUNT(*) AS [SoHoaDon],
    SUM(TotalAmount) AS [DoanhThu]
FROM SalesInvoices
WHERE InvoiceDate >= @from 
  AND InvoiceDate < DATEADD(day, 1, @to)
GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate)
ORDER BY [Nam] DESC, [Thang] DESC;";

            return DbHelper.ExecuteQuery(sql,
                new SqlParameter("@from", from.Date),
                new SqlParameter("@to", to.Date));
        }

        public decimal GetTotalRevenue(DateTime from, DateTime to)
        {
            string sql = @"
SELECT ISNULL(SUM(TotalAmount), 0)
FROM SalesInvoices
WHERE InvoiceDate >= @from 
  AND InvoiceDate < DATEADD(day, 1, @to);";

            var dt = DbHelper.ExecuteQuery(sql,
                new SqlParameter("@from", from.Date),
                new SqlParameter("@to", to.Date));

            return Convert.ToDecimal(dt.Rows[0][0]);
        }
    }
}
