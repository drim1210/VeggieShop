using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VeggieShop.Utils;

namespace VeggieShop.DAL
{
    public class GoodsReceiptDAL
    {
        private string ConnStr => ConfigurationManager.ConnectionStrings["VeggieShopDb"].ConnectionString;

        public DataTable GetSuppliers()
        {
            return DbHelper.ExecuteQuery(
                "SELECT SupplierId, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName;");
        }

        public DataTable GetProducts()
        {
            return DbHelper.ExecuteQuery(
                "SELECT ProductId, ProductName FROM Products WHERE IsActive = 1 ORDER BY ProductName;");
        }

        public bool ReceiptCodeExists(string code)
        {
            var dt = DbHelper.ExecuteQuery(
                "SELECT COUNT(1) FROM GoodsReceipts WHERE ReceiptCode=@c;",
                new SqlParameter("@c", code));
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        // Danh sách phiếu
        public DataTable GetReceipts(string keyword)
        {
            string sql = @"
SELECT r.ReceiptId, r.ReceiptCode, r.ReceiptDate,
       s.SupplierName, r.TotalAmount, r.Note, r.SupplierId
FROM GoodsReceipts r
JOIN Suppliers s ON s.SupplierId = r.SupplierId
WHERE (@kw = N'' OR r.ReceiptCode LIKE N'%' + @kw + N'%')
ORDER BY r.ReceiptId DESC;";
            return DbHelper.ExecuteQuery(sql, new SqlParameter("@kw", (keyword ?? "").Trim()));
        }

        // Lấy 1 phiếu (header)
        public DataRow GetReceiptHeader(int receiptId)
        {
            var dt = DbHelper.ExecuteQuery(@"
SELECT ReceiptId, ReceiptCode, SupplierId, ReceiptDate, Note, TotalAmount
FROM GoodsReceipts WHERE ReceiptId=@id;",
                new SqlParameter("@id", receiptId));

            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        // Lấy chi tiết 1 dòng (1 sản phẩm)
        public DataRow GetReceiptDetailSingle(int receiptId)
        {
            var dt = DbHelper.ExecuteQuery(@"
SELECT TOP 1 ProductId, Quantity, UnitPrice
FROM GoodsReceiptDetails
WHERE ReceiptId=@id;",
                new SqlParameter("@id", receiptId));

            return dt.Rows.Count == 0 ? null : dt.Rows[0];
        }

        // Thêm mới phiếu (1 sản phẩm)
        public void InsertReceiptSingle(string code, int supplierId, DateTime date, string note,
            int productId, decimal qty, decimal price)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    decimal total = qty * price;

                    int receiptId;
                    using (var cmd = new SqlCommand(@"
INSERT INTO GoodsReceipts(ReceiptCode, SupplierId, ReceiptDate, TotalAmount, Note)
VALUES(@code,@sup,@date,@total,@note);
SELECT SCOPE_IDENTITY();", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@sup", supplierId);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);
                        receiptId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    using (var cmd = new SqlCommand(@"
INSERT INTO GoodsReceiptDetails(ReceiptId, ProductId, Quantity, UnitPrice)
VALUES(@rid,@pid,@qty,@price);", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@rid", receiptId);
                        cmd.Parameters.AddWithValue("@pid", productId);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(@"
UPDATE Products SET StockQty = StockQty + @qty WHERE ProductId=@pid;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@pid", productId);
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

        // Sửa phiếu (1 sản phẩm) - rollback tồn kho cũ rồi cộng tồn kho mới
        public void UpdateReceiptSingle(int receiptId, string code, int supplierId, DateTime date, string note,
            int newProductId, decimal newQty, decimal newPrice)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    // lấy detail cũ
                    int oldProductId = 0;
                    decimal oldQty = 0;

                    using (var cmdOld = new SqlCommand(@"
SELECT TOP 1 ProductId, Quantity FROM GoodsReceiptDetails WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmdOld.Parameters.AddWithValue("@id", receiptId);
                        using (var rd = cmdOld.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                oldProductId = Convert.ToInt32(rd["ProductId"]);
                                oldQty = Convert.ToDecimal(rd["Quantity"]);
                            }
                        }
                    }

                    // rollback tồn kho cũ
                    if (oldProductId > 0 && oldQty > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE Products SET StockQty = StockQty - @q WHERE ProductId=@p;", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@q", oldQty);
                            cmd.Parameters.AddWithValue("@p", oldProductId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // xóa detail cũ
                    using (var cmd = new SqlCommand(
                        "DELETE FROM GoodsReceiptDetails WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", receiptId);
                        cmd.ExecuteNonQuery();
                    }

                    // insert detail mới
                    using (var cmd = new SqlCommand(@"
INSERT INTO GoodsReceiptDetails(ReceiptId, ProductId, Quantity, UnitPrice)
VALUES(@rid,@pid,@qty,@price);", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@rid", receiptId);
                        cmd.Parameters.AddWithValue("@pid", newProductId);
                        cmd.Parameters.AddWithValue("@qty", newQty);
                        cmd.Parameters.AddWithValue("@price", newPrice);
                        cmd.ExecuteNonQuery();
                    }

                    // cộng tồn kho mới
                    using (var cmd = new SqlCommand(
                        "UPDATE Products SET StockQty = StockQty + @q WHERE ProductId=@p;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@q", newQty);
                        cmd.Parameters.AddWithValue("@p", newProductId);
                        cmd.ExecuteNonQuery();
                    }

                    // update header
                    decimal total = newQty * newPrice;
                    using (var cmd = new SqlCommand(@"
UPDATE GoodsReceipts
SET ReceiptCode=@code, SupplierId=@sup, ReceiptDate=@date, Note=@note, TotalAmount=@total
WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        cmd.Parameters.AddWithValue("@sup", supplierId);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@note", (object)note ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@id", receiptId);
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

        // Xóa phiếu - rollback tồn kho rồi xóa
        public void DeleteReceipt(int receiptId)
        {
            using (var conn = new SqlConnection(ConnStr))
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    // lấy detail cũ
                    int oldProductId = 0;
                    decimal oldQty = 0;

                    using (var cmdOld = new SqlCommand(@"
SELECT TOP 1 ProductId, Quantity FROM GoodsReceiptDetails WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmdOld.Parameters.AddWithValue("@id", receiptId);
                        using (var rd = cmdOld.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                oldProductId = Convert.ToInt32(rd["ProductId"]);
                                oldQty = Convert.ToDecimal(rd["Quantity"]);
                            }
                        }
                    }

                    // rollback tồn kho
                    if (oldProductId > 0 && oldQty > 0)
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE Products SET StockQty = StockQty - @q WHERE ProductId=@p;", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@q", oldQty);
                            cmd.Parameters.AddWithValue("@p", oldProductId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // xóa phiếu (nếu FK cascade thì detail tự xóa; nếu chưa cascade thì xóa detail trước)
                    using (var cmd = new SqlCommand(
                        "DELETE FROM GoodsReceiptDetails WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", receiptId);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "DELETE FROM GoodsReceipts WHERE ReceiptId=@id;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@id", receiptId);
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
