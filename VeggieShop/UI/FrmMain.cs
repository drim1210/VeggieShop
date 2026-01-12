using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using VeggieShop.DTO;

namespace VeggieShop.UI
{
    public partial class FrmMain : Form
    {
        UserSession _user;

        public FrmMain(UserSession user)
        {
            InitializeComponent();
            _user = user;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào {_user.FullName} ({_user.RoleName})";
            ApplyRolePermission();

            if (UserSession.Current.UserId == 0)
            {
                MessageBox.Show("Bạn chưa đăng nhập.");
                Close();
                return;
            }

        }
        private void ApplyRolePermission()
        {
            bool isAdmin = UserSession.Current.RoleName == "Admin";

            if (isAdmin) return;

            
            btnCategories.Visible = false;
            btnSuppliers.Visible = false;
            mnuRegister.Visible = false;
            btnTopProducts.Visible = false;
        }


        private void mnuSuppliers_Click(object sender, EventArgs e)
        {
            using (var f = new FrmSuppliers())
            {
                f.ShowDialog();
            }
        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            using (var f = new FrmCustomers())
            {
                f.ShowDialog();
            }
        }

        private void mnuCategories_Click(object sender, EventArgs e)
        {
            using (var f = new FrmCategories())
            {
                f.ShowDialog();
            }
        }

        private void mnuProducts_Click(object sender, EventArgs e)
        {
            using (var f = new FrmProducts())
            {
                f.ShowDialog();
            }
        }

        private void mnuGoodsReceipt_Click(object sender, EventArgs e)
        {
            using (var f = new FrmGoodsReceipt())
            {
                f.ShowDialog();
            }
        }

        private void mnuSalesInvoice_Click(object sender, EventArgs e)
        {
            using (var f = new FrmSalesInvoice())
            {
                f.ShowDialog();
            }
        }
        private void mnuStatistics_Click(object sender, EventArgs e)
        {
            using (var f = new FrmRevenueReport())
            {
                f.ShowDialog();
            }
        }

        private void mnuTopProducts_Click(object sender, EventArgs e)
        {
            using (var f = new FrmTopProducts())
            {
                f.ShowDialog();
            }
        }

        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            using (var f = new FrmChangePassword())
                f.ShowDialog();
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            
            UserSession.Current.UserId = 0;
            UserSession.Current.Username = "";
            UserSession.Current.FullName = "";
            UserSession.Current.RoleName = "Staff";

            
            this.Close();
        }
        

        private Form _currentChild;

        private void OpenChild(Form child)
        {
            if (_currentChild != null)
            {
                _currentChild.Close();
                _currentChild.Dispose();
            }

            _currentChild = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(child);
            child.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmProducts());
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmCategories());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmSuppliers());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmCustomers());
        }

        private void btnGoodsReceipt_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmGoodsReceipt());
        }

        private void btnSalesInvoice_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmSalesInvoice());
        }

        private void btnRevenue_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmRevenueReport());
        }

        private void btnTopProducts_Click(object sender, EventArgs e)
        {
            OpenChild(new FrmTopProducts());
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            using (var f = new FrmChangePassword())
                f.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            UserSession.Current.UserId = 0;
            UserSession.Current.Username = "";
            UserSession.Current.FullName = "";
            UserSession.Current.RoleName = "Staff";

            Close(); // quay về login (do login đang Hide/Show như bạn làm)
        }






        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmMain_Load_1(object sender, EventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
        private void mnuRegister_Click(object sender, EventArgs e)
        {
            using (var f = new FrmRegister())
                f.ShowDialog();
        }
    }
}

