using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using VeggieShop.BUS;

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

            this.Hide();
            using (var f = new FrmMain(result.session))
            {
                f.ShowDialog();
            }
            this.Show();
        }
    }
}

