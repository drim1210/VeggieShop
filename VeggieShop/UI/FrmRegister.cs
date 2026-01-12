using System;
using System.Windows.Forms;
using VeggieShop.BUS;
using VeggieShop.DTO;

namespace VeggieShop.UI
{
    public partial class FrmRegister : Form
    {
        private readonly UserBUS _bus = new UserBUS();

        public FrmRegister()
        {
            InitializeComponent();
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            // chỉ Admin mới được vào
            if (UserSession.Current.RoleName != "Admin")
            {
                MessageBox.Show("Chỉ Admin mới được tạo tài khoản.");
                Close();
                return;
            }

            cboRole.Items.Clear();
            cboRole.Items.Add("Staff");
            cboRole.Items.Add("Admin");
            cboRole.SelectedIndex = 0;

            txtPassword.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var role = cboRole.SelectedItem?.ToString() ?? "Staff";

            var (ok, msg) = _bus.Register(
                txtUsername.Text,
                txtFullName.Text,
                txtPassword.Text,
                txtConfirm.Text,
                role
            );

            MessageBox.Show(msg);
            if (ok)
            {
                txtUsername.Text = "";
                txtFullName.Text = "";
                txtPassword.Text = "";
                txtConfirm.Text = "";
                cboRole.SelectedIndex = 0;
            }
        }
    }
}
