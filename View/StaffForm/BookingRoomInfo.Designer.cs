namespace HotelSystem.View.StaffForm
{
    partial class BookingRoomInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelRoom = new System.Windows.Forms.Button();
            this.dgvDelRoom = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditRoom = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelRoom)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(312, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 54);
            this.label1.TabIndex = 5;
            this.label1.Text = "Thông tin đặt phòng";
            // 
            // btnDelRoom
            // 
            this.btnDelRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnDelRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelRoom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelRoom.ForeColor = System.Drawing.Color.White;
            this.btnDelRoom.Location = new System.Drawing.Point(259, 600);
            this.btnDelRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelRoom.Name = "btnDelRoom";
            this.btnDelRoom.Size = new System.Drawing.Size(204, 62);
            this.btnDelRoom.TabIndex = 28;
            this.btnDelRoom.Text = "Hủy";
            this.btnDelRoom.UseVisualStyleBackColor = false;
            this.btnDelRoom.Click += new System.EventHandler(this.btnDelRoom_Click);
            // 
            // dgvDelRoom
            // 
            this.dgvDelRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDelRoom.Location = new System.Drawing.Point(12, 216);
            this.dgvDelRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvDelRoom.Name = "dgvDelRoom";
            this.dgvDelRoom.RowHeadersWidth = 51;
            this.dgvDelRoom.RowTemplate.Height = 24;
            this.dgvDelRoom.Size = new System.Drawing.Size(1011, 354);
            this.dgvDelRoom.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(522, 45);
            this.label5.TabIndex = 30;
            this.label5.Text = "Danh sách các phòng đã đặt:";
            // 
            // btnEditRoom
            // 
            this.btnEditRoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnEditRoom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditRoom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditRoom.ForeColor = System.Drawing.Color.White;
            this.btnEditRoom.Location = new System.Drawing.Point(501, 600);
            this.btnEditRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnEditRoom.Name = "btnEditRoom";
            this.btnEditRoom.Size = new System.Drawing.Size(204, 62);
            this.btnEditRoom.TabIndex = 32;
            this.btnEditRoom.Text = "Chỉnh sửa";
            this.btnEditRoom.UseVisualStyleBackColor = false;
            this.btnEditRoom.Click += new System.EventHandler(this.btnEditRoom_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("Gadugi", 16F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.LightGray;
            this.btnRefresh.Location = new System.Drawing.Point(737, 599);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(204, 62);
            this.btnRefresh.TabIndex = 35;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.btnCancel);
            this.panel7.Controls.Add(this.btnClose);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1035, 65);
            this.panel7.TabIndex = 36;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnCancel.Font = new System.Drawing.Font("Gadugi", 16F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(874, -6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 71);
            this.btnCancel.TabIndex = 37;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(-10, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 49);
            this.label2.TabIndex = 7;
            this.label2.Text = "TML HOTEL";
            // 
            // BookingRoomInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::HotelSystem.Properties.Resources.mau_thiet_ke_noi_that_phong_2_giuong_don_ben_trong_khach_san_3_4_5_sao_3;
            this.ClientSize = new System.Drawing.Size(1035, 719);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnEditRoom);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvDelRoom);
            this.Controls.Add(this.btnDelRoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BookingRoomInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BookingRoomInfo";
            this.Load += new System.EventHandler(this.BookingRoomInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDelRoom)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelRoom;
        private System.Windows.Forms.DataGridView dgvDelRoom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditRoom;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel7;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}