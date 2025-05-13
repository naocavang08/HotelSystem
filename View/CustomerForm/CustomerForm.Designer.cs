namespace HotelSystem.View.CustomerForm
{
    partial class CustomerForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.pnLogout = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.picInvoice = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblBookService = new System.Windows.Forms.Label();
            this.picCusInfo = new System.Windows.Forms.PictureBox();
            this.lblCusInfo = new System.Windows.Forms.Label();
            this.picBookService = new System.Windows.Forms.PictureBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnLogout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCusInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBookService)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(194)))), ((int)(((byte)(236)))));
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnHide);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1500, 40);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1463, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.ForeColor = System.Drawing.Color.White;
            this.btnHide.Location = new System.Drawing.Point(1427, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(30, 30);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "-";
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(76)))), ((int)(((byte)(172)))));
            this.panel2.Controls.Add(this.lblInvoice);
            this.panel2.Controls.Add(this.pnLogout);
            this.panel2.Controls.Add(this.picInvoice);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblBookService);
            this.panel2.Controls.Add(this.picCusInfo);
            this.panel2.Controls.Add(this.lblCusInfo);
            this.panel2.Controls.Add(this.picBookService);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 860);
            this.panel2.TabIndex = 1;
            // 
            // lblInvoice
            // 
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInvoice.Location = new System.Drawing.Point(68, 661);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(120, 28);
            this.lblInvoice.TabIndex = 9;
            this.lblInvoice.Text = "Thanh toán";
            this.lblInvoice.Click += new System.EventHandler(this.lblInvoice_Click);
            // 
            // pnLogout
            // 
            this.pnLogout.Controls.Add(this.btnLogout);
            this.pnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnLogout.Location = new System.Drawing.Point(3, 770);
            this.pnLogout.Name = "pnLogout";
            this.pnLogout.Size = new System.Drawing.Size(247, 60);
            this.pnLogout.TabIndex = 5;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = global::HotelSystem.Properties.Resources.icons8_log_out_64_white_rotate;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(-10, -8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(269, 77);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "               Đăng xuất";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // picInvoice
            // 
            this.picInvoice.Image = global::HotelSystem.Properties.Resources.icons8_invoice_96;
            this.picInvoice.Location = new System.Drawing.Point(76, 558);
            this.picInvoice.Name = "picInvoice";
            this.picInvoice.Size = new System.Drawing.Size(100, 100);
            this.picInvoice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInvoice.TabIndex = 8;
            this.picInvoice.TabStop = false;
            this.picInvoice.Click += new System.EventHandler(this.picInvoice_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HotelSystem.Properties.Resources.icons8_user_100;
            this.pictureBox1.Location = new System.Drawing.Point(65, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblBookService
            // 
            this.lblBookService.AutoSize = true;
            this.lblBookService.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookService.Location = new System.Drawing.Point(67, 490);
            this.lblBookService.Name = "lblBookService";
            this.lblBookService.Size = new System.Drawing.Size(121, 28);
            this.lblBookService.TabIndex = 7;
            this.lblBookService.Text = "Đặt dịch vụ";
            this.lblBookService.Click += new System.EventHandler(this.lblBookService_Click);
            // 
            // picCusInfo
            // 
            this.picCusInfo.Image = global::HotelSystem.Properties.Resources.customers_icon_3;
            this.picCusInfo.Location = new System.Drawing.Point(76, 219);
            this.picCusInfo.Name = "picCusInfo";
            this.picCusInfo.Size = new System.Drawing.Size(100, 100);
            this.picCusInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCusInfo.TabIndex = 2;
            this.picCusInfo.TabStop = false;
            this.picCusInfo.Click += new System.EventHandler(this.picCusInfo_Click);
            // 
            // lblCusInfo
            // 
            this.lblCusInfo.AutoSize = true;
            this.lblCusInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusInfo.Location = new System.Drawing.Point(15, 322);
            this.lblCusInfo.Name = "lblCusInfo";
            this.lblCusInfo.Size = new System.Drawing.Size(219, 28);
            this.lblCusInfo.TabIndex = 5;
            this.lblCusInfo.Text = "Thông tin khách hàng";
            this.lblCusInfo.Click += new System.EventHandler(this.lblCusInfo_Click);
            // 
            // picBookService
            // 
            this.picBookService.Image = global::HotelSystem.Properties.Resources.icons8_service_96;
            this.picBookService.Location = new System.Drawing.Point(76, 387);
            this.picBookService.Name = "picBookService";
            this.picBookService.Size = new System.Drawing.Size(100, 100);
            this.picBookService.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBookService.TabIndex = 4;
            this.picBookService.TabStop = false;
            this.picBookService.Click += new System.EventHandler(this.picBookService_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(259, 52);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(196, 50);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Welcome!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.Location = new System.Drawing.Point(268, 105);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1220, 783);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(208)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(1500, 900);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerForm";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnLogout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCusInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBookService)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnLogout;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox picCusInfo;
        private System.Windows.Forms.PictureBox picBookService;
        private System.Windows.Forms.Label lblCusInfo;
        private System.Windows.Forms.Label lblBookService;
        private System.Windows.Forms.PictureBox picInvoice;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}