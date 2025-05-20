using System.Drawing;

namespace HotelSystem.View.AdminForm
{
    partial class AdminForm
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
            
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnRoom = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnStatistic = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnStaff = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnService = new System.Windows.Forms.Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnRoom);
            this.panel6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(-4, 518);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(281, 75);
            this.panel6.TabIndex = 6;
            // 
            // btnRoom
            // 
            this.btnRoom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoom.ForeColor = System.Drawing.Color.DarkGray;
            this.btnRoom.Image = global::HotelSystem.Properties.Resources.icons8_room_50;
            this.btnRoom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoom.Location = new System.Drawing.Point(-11, -8);
            this.btnRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnRoom.Size = new System.Drawing.Size(303, 96);
            this.btnRoom.TabIndex = 2;
            this.btnRoom.Text = "               Phòng";
            this.btnRoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoom.UseVisualStyleBackColor = false;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnLogout);
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(-4, 710);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(278, 75);
            this.panel5.TabIndex = 4;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.DarkGray;
            this.btnLogout.Image = global::HotelSystem.Properties.Resources.icons8_exit_64;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(-11, -10);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(303, 96);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "               Đăng xuất";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // picHome
            // 
            this.picHome.Image = global::HotelSystem.Properties.Resources.iconAdmin;
            this.picHome.Location = new System.Drawing.Point(58, 81);
            this.picHome.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(135, 150);
            this.picHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHome.TabIndex = 0;
            this.picHome.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnStatistic);
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(-4, 270);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(281, 75);
            this.panel2.TabIndex = 3;
            // 
            // btnStatistic
            // 
            this.btnStatistic.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStatistic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistic.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatistic.ForeColor = System.Drawing.Color.DarkGray;
            this.btnStatistic.Image = global::HotelSystem.Properties.Resources.icons8_analytics_60;
            this.btnStatistic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStatistic.Location = new System.Drawing.Point(-10, -15);
            this.btnStatistic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnStatistic.Size = new System.Drawing.Size(303, 96);
            this.btnStatistic.TabIndex = 2;
            this.btnStatistic.Text = "              Thống kê";
            this.btnStatistic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStatistic.UseVisualStyleBackColor = false;
            this.btnStatistic.Click += new System.EventHandler(this.btnStatistic_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnStaff);
            this.panel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(-4, 435);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(281, 75);
            this.panel4.TabIndex = 5;
            // 
            // btnStaff
            // 
            this.btnStaff.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaff.ForeColor = System.Drawing.Color.DarkGray;
            this.btnStaff.Image = global::HotelSystem.Properties.Resources.icons8_staff_64;
            this.btnStaff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.Location = new System.Drawing.Point(-14, -11);
            this.btnStaff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnStaff.Size = new System.Drawing.Size(303, 96);
            this.btnStaff.TabIndex = 2;
            this.btnStaff.Text = "               Nhân viên";
            this.btnStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.UseVisualStyleBackColor = false;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnService);
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(-4, 353);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(281, 75);
            this.panel3.TabIndex = 4;
            // 
            // btnService
            // 
            this.btnService.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService.ForeColor = System.Drawing.Color.DarkGray;
            this.btnService.Image = global::HotelSystem.Properties.Resources.icons8_customer_service_48;
            this.btnService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnService.Location = new System.Drawing.Point(-9, -14);
            this.btnService.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnService.Name = "btnService";
            this.btnService.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnService.Size = new System.Drawing.Size(303, 96);
            this.btnService.TabIndex = 2;
            this.btnService.Text = "              Dịch vụ";
            this.btnService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnService.UseVisualStyleBackColor = false;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnClose.Font = new System.Drawing.Font("Gadugi", 16F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.LightGray;
            this.btnClose.Location = new System.Drawing.Point(1189, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(161, 71);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Thoát";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 60);
            this.label1.TabIndex = 7;
            this.label1.Text = "TML HOTEL";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.panel7.Controls.Add(this.btnClose);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1350, 65);
            this.panel7.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.picHome);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 873);
            this.panel1.TabIndex = 9;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1350, 938);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel7);
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminForm";
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnStatistic;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.PictureBox picHome;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel1;
    }
}