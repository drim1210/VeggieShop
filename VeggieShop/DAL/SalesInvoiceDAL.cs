using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class SalesInvoiceDAL
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["VeggieShopDb"].ConnectionString;

        public DataTable GetCustomers()
        {
            return DbHelper.ExecuteQuery(
                "SELECT CustomerId, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName;");
        }

        public DataTable GetProducts()
        {
            return DbHelper.ExecuteQuery(
                "SELECT ProductId, ProductName, SalePrice, StockQty FROM Products WHERE IsActive = 1 ORDER BY ProductName;");
        }


        public bool InvoiceCodeExists(string code)
        {
            var dt = DbHelper.ExecuteQuery(
                "SELECT COUNT(1) FROM SalesInvoices WHERE InvoiceCode=@c;",
                new SqlParameter("@c", code));
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public DataTable GetInvoices(string keyword)
        {
            string sql = @"
SELECT i.InvoiceId, i.InvoiceCode, i.InvoiceDate,
       c.CustomerName, i.TotalAmount, i.Note, i.CustomerId
FROM SalesInvoices i
JOIN Customers c ON c.CustomerId = i.CustomerId
WHERE (@kw = N'' OR i.InvoiceCode LIKE N'%' + @kw + N'%')
ORDER BY i.InvoiceId DESC;";
            return DbHelper.ExecuteQuery(sql, new SqlParameter("@kw", (keyword ?? "").Trim()));
        }

        public DataRow GetInvoiceHeader(int invoiceId)
        {
            var dt = DbHelper.ExecuteQuery(@"
SELECT InvoiceId, InvoiceCode, CustomerId, InvoiceDate, Note, TotalAmount
FROM SalesInvoices WHERE InvoiceId=@id;",
                new SqlParameter("@id", invoiceId));
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        public DataRow GetInvoiceDetailSingle(int invoiceId)
        {
            var dt = DbHelper.ExecuteQuery(@"
SELECT TOP 1 ProductId, Quantity, UnitPrice
FROM SalesInvoiceDetails WHERE InvoiceId=@id;",
                new SqlParameter("@id", invoiceId));
            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        // Insert: trừ kho
        public void InsertInvoiceSingle(string code, int customerId, DateTime date, string note,
            int productId, decimal qty, decimal price)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    // check tồn kho
                    decimal stock = 0;
                    using (var cmdStock = new SqlCommand(
                        "SELECT StockQty FROM Products WHERE ProductId=@p;", conn, tran))
                    {
                        cmdStock.Parameters.AddWithValue("@p", productId);
                        stock = Convert.ToDecimal(cmdStock.ExecuteScalar());
                    }
                    if (stock < qty) throw new Exception("Không đủ tồn kho để bán.");

                    decimal total = qty * price;

                    int invoiceId;
                    using (var cmd = new SqlCommand(@"
INSERT INTO SalesInvoices(InvoiceCode, CustomerId, InvoiceDate, TotalAmount, Note)
VALUES(@code,@cus,@date,@total,@note);
SELECT SCOPE_IDENTITY();", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@cus", customerId);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);
                        invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (var cmd = new SqlCommand(@"
INSERT INTO SalesInvoiceDetails(InvoiceId, ProductId, Quantity, UnitPrice)
VALUES(@iid,@pid,@qty,@price);", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@iid", invoiceId);
                        cmd.Parameters.AddWithValue("@pid", productId);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.ExecuteNonQuery();
                    }

                    // trừ kho
                    using (var cmd = new SqlCommand(
                        "UPDATE Products SET StockQty = StockQty - @q WHERE ProductId=@p;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@q", qty);
                        cmd.Parameters.AddWithValue("@p", productId);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        // Update: hoàn kho cũ rồi trừ kho mới
        public void UpdateInvoiceSingle(int invoiceId, string code, int customerId, DateTime date, string note,
            int newProductId, decimal newQty, decimal newPrice)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    // old detail
                    int oldProductId = 0;
                    decimal oldQty = 0;
                    using (var cmdOld = new SqlCommand(@"
SELECT TOP 1 ProductId, Quantity FROM SalesInvoiceDetails WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmdOld.Parameters.AddWithValue("@id", invoiceId);
                        using (var rd = cmdOld.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                oldProductId = Convert.ToInt32(rd["ProductId"]);
                                oldQty = Convert.ToDecimal(rd["Quantity"]);
                            }
                        }
                    }

                    // hoàn kho cũ (cộng lại)
                    if (oldProductId > 0 && oldQty > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE Products SET StockQty = StockQty + @q WHERE ProductId=@p;", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@q", oldQty);
                            cmd.Parameters.AddWithValue("@p", oldProductId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // xóa detail cũ
                    using (var cmd = new SqlCommand(
                        "DELETE FROM SalesInvoiceDetails WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        cmd.ExecuteNonQuery();
                    }

                    // check tồn kho cho detail mới
                    decimal stock = 0;
                    using (var cmdStock = new SqlCommand(
                        "SELECT StockQty FROM Products WHERE ProductId=@p;", conn, tran))
                    {
                        cmdStock.Parameters.AddWithValue("@p", newProductId);
                        stock = Convert.ToDecimal(cmdStock.ExecuteScalar());
                    }
                    if (stock < newQty) throw new Exception("Không đủ tồn kho để bán (sau khi sửa).");

                    // insert detail mới
                    using (var cmd = new SqlCommand(@"
INSERT INTO SalesInvoiceDetails(InvoiceId, ProductId, Quantity, UnitPrice)
VALUES(@iid,@pid,@qty,@price);", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@iid", invoiceId);
                        cmd.Parameters.AddWithValue("@pid", newProductId);
                        cmd.Parameters.AddWithValue("@qty", newQty);
                        cmd.Parameters.AddWithValue("@price", newPrice);
                        cmd.ExecuteNonQuery();
                    }

                    // trừ kho mới
                    using (var cmd = new SqlCommand(
                        "UPDATE Products SET StockQty = StockQty - @q WHERE ProductId=@p;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@q", newQty);
                        cmd.Parameters.AddWithValue("@p", newProductId);
                        cmd.ExecuteNonQuery();
                    }

                    // update header
                    decimal total = newQty * newPrice;
                    using (var cmd = new SqlCommand(@"
UPDATE SalesInvoices
SET InvoiceCode=@code, CustomerId=@cus, InvoiceDate=@date, Note=@note, TotalAmount=@total
WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@cus", customerId);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        // Delete: hoàn kho rồi xóa
        public void DeleteInvoice(int invoiceId)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    int oldProductId = 0;
                    decimal oldQty = 0;
                    using (var cmdOld = new SqlCommand(@"
SELECT TOP 1 ProductId, Quantity FROM SalesInvoiceDetails WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmdOld.Parameters.AddWithValue("@id", invoiceId);
                        using (var rd = cmdOld.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                oldProductId = Convert.ToInt32(rd["ProductId"]);
                                oldQty = Convert.ToDecimal(rd["Quantity"]);
                            }
                        }
                    }

                    // hoàn kho
                    if (oldProductId > 0 && oldQty > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE Products SET StockQty = StockQty + @q WHERE ProductId=@p;", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@q", oldQty);
                            cmd.Parameters.AddWithValue("@p", oldProductId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (var cmd = new SqlCommand(
                        "DELETE FROM SalesInvoiceDetails WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "DELETE FROM SalesInvoices WHERE InvoiceId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", invoiceId);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
