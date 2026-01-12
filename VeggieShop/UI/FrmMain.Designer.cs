namespace VeggieShop.UI
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSuppliers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGoodsReceipt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSales = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalesInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatistics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTopProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.tàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnTopProducts = new System.Windows.Forms.Button();
            this.btnRevenue = new System.Windows.Forms.Button();
            this.btnSalesInvoice = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnGoodsReceipt = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.mnuRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhMụcToolStripMenuItem,
            this.tàiKhoảnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(988, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSuppliers,
            this.mnuCustomers,
            this.mnuCategories,
            this.mnuProducts,
            this.mnuGoodsReceipt,
            this.mnuSales,
            this.mnuStatistics});
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(90, 24);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            // 
            // mnuSuppliers
            // 
            this.mnuSuppliers.Name = "mnuSuppliers";
            this.mnuSuppliers.Size = new System.Drawing.Size(227, 26);
            this.mnuSuppliers.Text = "Nhà cung cấp";
            this.mnuSuppliers.Click += new System.EventHandler(this.mnuSuppliers_Click);
            // 
            // mnuCustomers
            // 
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(227, 26);
            this.mnuCustomers.Text = "Khách hàng";
            this.mnuCustomers.Click += new System.EventHandler(this.mnuCustomers_Click);
            // 
            // mnuCategories
            // 
            this.mnuCategories.Name = "mnuCategories";
            this.mnuCategories.Size = new System.Drawing.Size(227, 26);
            this.mnuCategories.Text = "Danh mục sản phẩm";
            this.mnuCategories.Click += new System.EventHandler(this.mnuCategories_Click);
            // 
            // mnuProducts
            // 
            this.mnuProducts.Name = "mnuProducts";
            this.mnuProducts.Size = new System.Drawing.Size(227, 26);
            this.mnuProducts.Text = "Sản phẩm";
            this.mnuProducts.Click += new System.EventHandler(this.mnuProducts_Click);
            // 
            // mnuGoodsReceipt
            // 
            this.mnuGoodsReceipt.Name = "mnuGoodsReceipt";
            this.mnuGoodsReceipt.Size = new System.Drawing.Size(227, 26);
            this.mnuGoodsReceipt.Text = "Nhập hàng";
            this.mnuGoodsReceipt.Click += new System.EventHandler(this.mnuGoodsReceipt_Click);
            // 
            // mnuSales
            // 
            this.mnuSales.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSalesInvoice});
            this.mnuSales.Name = "mnuSales";
            this.mnuSales.Size = new System.Drawing.Size(227, 26);
            this.mnuSales.Text = "Bán hàng";
            // 
            // mnuSalesInvoice
            // 
            this.mnuSalesInvoice.Name = "mnuSalesInvoice";
            this.mnuSalesInvoice.Size = new System.Drawing.Size(179, 26);
            this.mnuSalesInvoice.Text = "Hóa đơn bán";
            this.mnuSalesInvoice.Click += new System.EventHandler(this.mnuSalesInvoice_Click);
            // 
            // mnuStatistics
            // 
            this.mnuStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTopProducts});
            this.mnuStatistics.Name = "mnuStatistics";
            this.mnuStatistics.Size = new System.Drawing.Size(227, 26);
            this.mnuStatistics.Text = "Thống kê";
            this.mnuStatistics.Click += new System.EventHandler(this.mnuStatistics_Click);
            // 
            // mnuTopProducts
            // 
            this.mnuTopProducts.Name = "mnuTopProducts";
            this.mnuTopProducts.Size = new System.Drawing.Size(248, 26);
            this.mnuTopProducts.Text = "Top sản phẩm bán chạy";
            this.mnuTopProducts.Click += new System.EventHandler(this.mnuTopProducts_Click);
            // 
            // tàiKhoảnToolStripMenuItem
            // 
            this.tàiKhoảnToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChangePassword,
            this.mnuLogout,
            this.mnuRegister});
            this.tàiKhoảnToolStripMenuItem.Name = "tàiKhoảnToolStripMenuItem";
            this.tàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.tàiKhoảnToolStripMenuItem.Text = "Tài khoản";
            // 
            // mnuChangePassword
            // 
            this.mnuChangePassword.Name = "mnuChangePassword";
            this.mnuChangePassword.Size = new System.Drawing.Size(224, 26);
            this.mnuChangePassword.Text = "Đổi mật khẩu";
            this.mnuChangePassword.Click += new System.EventHandler(this.mnuChangePassword_Click);
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(224, 26);
            this.mnuLogout.Text = "Đăng xuất";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnTopProducts);
            this.panel1.Controls.Add(this.btnRevenue);
            this.panel1.Controls.Add(this.btnSalesInvoice);
            this.panel1.Controls.Add(this.lblWelcome);
            this.panel1.Controls.Add(this.btnGoodsReceipt);
            this.panel1.Controls.Add(this.btnCustomers);
            this.panel1.Controls.Add(this.btnSuppliers);
            this.panel1.Controls.Add(this.btnCategories);
            this.panel1.Controls.Add(this.btnProducts);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 571);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnLogout
            // 
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogout.Location = new System.Drawing.Point(12, 517);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(135, 39);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnTopProducts
            // 
            this.btnTopProducts.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnTopProducts.Location = new System.Drawing.Point(12, 366);
            this.btnTopProducts.Name = "btnTopProducts";
            this.btnTopProducts.Size = new System.Drawing.Size(135, 39);
            this.btnTopProducts.TabIndex = 8;
            this.btnTopProducts.Text = "Top sản phẩm";
            this.btnTopProducts.UseVisualStyleBackColor = true;
            this.btnTopProducts.Click += new System.EventHandler(this.btnTopProducts_Click);
            // 
            // btnRevenue
            // 
            this.btnRevenue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRevenue.Location = new System.Drawing.Point(12, 321);
            this.btnRevenue.Name = "btnRevenue";
            this.btnRevenue.Size = new System.Drawing.Size(135, 39);
            this.btnRevenue.TabIndex = 7;
            this.btnRevenue.Text = "Thống kê";
            this.btnRevenue.UseVisualStyleBackColor = true;
            this.btnRevenue.Click += new System.EventHandler(this.btnRevenue_Click);
            // 
            // btnSalesInvoice
            // 
            this.btnSalesInvoice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSalesInvoice.Location = new System.Drawing.Point(12, 276);
            this.btnSalesInvoice.Name = "btnSalesInvoice";
            this.btnSalesInvoice.Size = new System.Drawing.Size(135, 39);
            this.btnSalesInvoice.TabIndex = 6;
            this.btnSalesInvoice.Text = "Bán hàng";
            this.btnSalesInvoice.UseVisualStyleBackColor = true;
            this.btnSalesInvoice.Click += new System.EventHandler(this.btnSalesInvoice_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(23, 13);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(82, 20);
            this.lblWelcome.TabIndex = 5;
            this.lblWelcome.Text = "Xin chào";
            // 
            // btnGoodsReceipt
            // 
            this.btnGoodsReceipt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGoodsReceipt.Location = new System.Drawing.Point(12, 231);
            this.btnGoodsReceipt.Name = "btnGoodsReceipt";
            this.btnGoodsReceipt.Size = new System.Drawing.Size(135, 39);
            this.btnGoodsReceipt.TabIndex = 4;
            this.btnGoodsReceipt.Text = "Phiếu nhập";
            this.btnGoodsReceipt.UseVisualStyleBackColor = true;
            this.btnGoodsReceipt.Click += new System.EventHandler(this.btnGoodsReceipt_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCustomers.Location = new System.Drawing.Point(12, 186);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(135, 39);
            this.btnCustomers.TabIndex = 3;
            this.btnCustomers.Text = "Khách hàng";
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSuppliers.Location = new System.Drawing.Point(12, 141);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(135, 39);
            this.btnSuppliers.TabIndex = 2;
            this.btnSuppliers.Text = "Nhà cung cấp";
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCategories
            // 
            this.btnCategories.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCategories.Location = new System.Drawing.Point(12, 96);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(135, 39);
            this.btnCategories.TabIndex = 1;
            this.btnCategories.Text = "Danh mục SP";
            this.btnCategories.UseVisualStyleBackColor = true;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnProducts.Location = new System.Drawing.Point(12, 51);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(135, 39);
            this.btnProducts.TabIndex = 0;
            this.btnProducts.Text = "Sản phẩm";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(168, 28);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(820, 571);
            this.pnlContent.TabIndex = 4;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // mnuRegister
            // 
            this.mnuRegister.Name = "mnuRegister";
            this.mnuRegister.Size = new System.Drawing.Size(224, 26);
            this.mnuRegister.Text = "Đăng ký";
            this.mnuRegister.Click += new System.EventHandler(this.mnuRegister_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 599);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSuppliers;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.Windows.Forms.ToolStripMenuItem mnuCategories;
        private System.Windows.Forms.ToolStripMenuItem mnuProducts;
        private System.Windows.Forms.ToolStripMenuItem mnuGoodsReceipt;
        private System.Windows.Forms.ToolStripMenuItem mnuSales;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesInvoice;
        private System.Windows.Forms.ToolStripMenuItem mnuStatistics;
        private System.Windows.Forms.ToolStripMenuItem mnuTopProducts;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGoodsReceipt;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnTopProducts;
        private System.Windows.Forms.Button btnRevenue;
        private System.Windows.Forms.Button btnSalesInvoice;
        private System.Windows.Forms.ToolStripMenuItem mnuRegister;
    }
}