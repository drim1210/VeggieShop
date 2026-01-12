using System.Drawing;
using System.Windows.Forms;

namespace VeggieShop.Utils
{
    public static class UiStyle
    {
        // Bạn chỉnh màu ở đây
        public static readonly Color Bg = Color.White;
        public static readonly Color Primary = Color.FromArgb(34, 139, 34);   // xanh lá
        public static readonly Color PrimaryDark = Color.FromArgb(25, 110, 25);
        public static readonly Color Text = Color.FromArgb(30, 30, 30);
        public static readonly Font AppFont = new Font("Segoe UI", 10F);

        public static void Apply(Form f)
        {
            f.Font = AppFont;
            f.BackColor = Bg;
            f.ForeColor = Text;
            f.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void StyleButton(Button b, bool primary = true)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.Height = 34;

            if (primary)
            {
                b.BackColor = Primary;
                b.ForeColor = Color.White;
            }
            else
            {
                b.BackColor = Color.Gainsboro;
                b.ForeColor = Text;
            }
        }

        public static void StyleGrid(DataGridView g)
        {
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.ReadOnly = true;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            g.EnableHeadersVisualStyles = false;
            g.ColumnHeadersDefaultCellStyle.BackColor = PrimaryDark;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            g.RowHeadersVisible = false;
            g.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        }

        public static void StyleGroupBox(GroupBox gb)
        {
            gb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
    }
}
