namespace HotelSystem.View.Staff
{
    partial class StaffForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.Exitbtn = new System.Windows.Forms.Button();
            this.g = new System.Windows.Forms.Button();
            this.setServicebtn = new System.Windows.Forms.Button();
            this.showBillbtn = new System.Windows.Forms.Button();
            this.showCustomManagebtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(76)))), ((int)(((byte)(172)))));
            this.panel2.Controls.Add(this.Exitbtn);
            this.panel2.Controls.Add(this.g);
            this.panel2.Controls.Add(this.setServicebtn);
            this.panel2.Controls.Add(this.showBillbtn);
            this.panel2.Controls.Add(this.showCustomManagebtn);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(280, 877);
            this.panel2.TabIndex = 2;
            this.panel2.BackgroundImageLayoutChanged += new System.EventHandler(this.panel2_BackgroundImageLayoutChanged);
            // 
            // Exitbtn
            // 
            this.Exitbtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.Exitbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exitbtn.Location = new System.Drawing.Point(43, 687);
            this.Exitbtn.Name = "Exitbtn";
            this.Exitbtn.Size = new System.Drawing.Size(184, 71);
            this.Exitbtn.TabIndex = 3;
            this.Exitbtn.Text = "Đăng xuất";
            this.Exitbtn.UseVisualStyleBackColor = false;
            this.Exitbtn.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // g
            // 
            this.g.BackColor = System.Drawing.Color.MediumPurple;
            this.g.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.g.Location = new System.Drawing.Point(-4, 523);
            this.g.Name = "g";
            this.g.Size = new System.Drawing.Size(290, 109);
            this.g.TabIndex = 6;
            this.g.Text = "Đặt phòng";
            this.g.UseVisualStyleBackColor = false;
            // 
            // setServicebtn
            // 
            this.setServicebtn.BackColor = System.Drawing.Color.MediumPurple;
            this.setServicebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setServicebtn.Location = new System.Drawing.Point(-4, 405);
            this.setServicebtn.Name = "setServicebtn";
            this.setServicebtn.Size = new System.Drawing.Size(290, 109);
            this.setServicebtn.TabIndex = 5;
            this.setServicebtn.Text = "Dịch vụ";
            this.setServicebtn.UseVisualStyleBackColor = false;
            this.setServicebtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // showBillbtn
            // 
            this.showBillbtn.BackColor = System.Drawing.Color.MediumPurple;
            this.showBillbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showBillbtn.Location = new System.Drawing.Point(-4, 288);
            this.showBillbtn.Name = "showBillbtn";
            this.showBillbtn.Size = new System.Drawing.Size(290, 109);
            this.showBillbtn.TabIndex = 4;
            this.showBillbtn.Text = "Hóa đơn";
            this.showBillbtn.UseVisualStyleBackColor = false;
            this.showBillbtn.Click += new System.EventHandler(this.showbillbtn_Click);
            // 
            // showCustomManagebtn
            // 
            this.showCustomManagebtn.BackColor = System.Drawing.Color.MediumPurple;
            this.showCustomManagebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showCustomManagebtn.Location = new System.Drawing.Point(-4, 172);
            this.showCustomManagebtn.Name = "showCustomManagebtn";
            this.showCustomManagebtn.Size = new System.Drawing.Size(290, 109);
            this.showCustomManagebtn.TabIndex = 3;
            this.showCustomManagebtn.Text = "Khách hàng";
            this.showCustomManagebtn.UseVisualStyleBackColor = false;
            this.showCustomManagebtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HotelSystem.Properties.Resources._2332039;
            this.pictureBox1.Location = new System.Drawing.Point(57, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // StaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(208)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(1326, 877);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "StaffForm";
            this.Text = "Staff";
            this.Load += new System.EventHandler(this.Staff_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button showCustomManagebtn;
        private System.Windows.Forms.Button showBillbtn;
        private System.Windows.Forms.Button setServicebtn;
        private System.Windows.Forms.Button g;
        private System.Windows.Forms.Button Exitbtn;
    }
}