using System;
using System.Windows.Forms;
using VeggieShop.BUS;
using VeggieShop.DTO;  

namespace VeggieShop.UI
{
    public partial class FrmLogin : Form
    {
        AuthBUS auth = new AuthBUS();

        public FrmLogin()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var result = auth.Login(txtUsername.Text, txtPassword.Text);

            if (!result.ok || result.session == null)
            {
                MessageBox.Show(result.message, "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            UserSession.Current.UserId = result.session.UserId;
            UserSession.Current.Username = result.session.Username;
            UserSession.Current.FullName = result.session.FullName;
            UserSession.Current.RoleName = result.session.RoleName;

            this.Hide();
            using (var f = new FrmMain(result.session)) 
            {
                f.ShowDialog();
            }
            this.Show();
        }
    }
}
