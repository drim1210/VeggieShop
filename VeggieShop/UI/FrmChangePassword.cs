using System;
using System.Windows.Forms;
using VeggieShop.BUS;
using VeggieShop.DTO;

namespace VeggieShop.UI
{
    public partial class FrmChangePassword : Form
    {
        private readonly UserBUS _bus = new UserBUS();

        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            int userId = UserSession.Current.UserId;


            var (ok, msg) = _bus.ChangePassword(
                userId,
                txtOldPassword.Text,
                txtNewPassword.Text,
                txtConfirmPassword.Text
            );

            MessageBox.Show(msg);
            if (ok) Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
