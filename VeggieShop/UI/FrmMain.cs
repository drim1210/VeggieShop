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
    }
}

