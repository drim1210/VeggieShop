using System;
using System.Data;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace VeggieShop.Utils
{
    public static class ExportHelper
    {
        public static void ExportDataGridViewToExcel(DataGridView dgv, string title = "Export")
        {
            if (dgv.DataSource == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.");
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"{title}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                using (var wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Sheet1");

                    // Header
                    int colIndex = 1;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        if (!col.Visible) continue;
                        ws.Cell(1, colIndex).Value = col.HeaderText;
                        ws.Cell(1, colIndex).Style.Font.Bold = true;
                        colIndex++;
                    }

                    // Data
                    int rowIndex = 2;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.IsNewRow) continue;

                        colIndex = 1;
                        foreach (DataGridViewColumn col in dgv.Columns)
                        {
                            if (!col.Visible) continue;
                            ws.Cell(rowIndex, colIndex).Value = row.Cells[col.Index].Value?.ToString();
                            colIndex++;
                        }
                        rowIndex++;
                    }

                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                }

                MessageBox.Show("Xuất Excel thành công!");
            }
        }
    }
}
