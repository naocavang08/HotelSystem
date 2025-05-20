namespace HotelSystem.View.StaffForm
{
    partial class Invoice
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
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvListRoom = new System.Windows.Forms.DataGridView();
            this.dgvListService = new System.Windows.Forms.DataGridView();
            this.lbInvoiceID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTotalAmount = new System.Windows.Forms.Label();
            this.btnLoadBooking = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblBookRoomCount = new System.Windows.Forms.Label();
            this.lblBookServiceCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListRoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListService)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckout.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCheckout.Location = new System.Drawing.Point(111, 882);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(162, 52);
            this.btnCheckout.TabIndex = 0;
            this.btnCheckout.Text = "Thanh toán";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.Location = new System.Drawing.Point(323, 882);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 52);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Thoát";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã hóa đơn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Họ tên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Số điện thoại:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 32);
            this.label4.TabIndex = 5;
            this.label4.Text = "CCCD:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 306);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "Phòng đã đặt:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(30, 535);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 32);
            this.label6.TabIndex = 7;
            this.label6.Text = "Dịch vụ đã đặt:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // dgvListRoom
            // 
            this.dgvListRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListRoom.Location = new System.Drawing.Point(36, 345);
            this.dgvListRoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvListRoom.Name = "dgvListRoom";
            this.dgvListRoom.RowHeadersWidth = 51;
            this.dgvListRoom.RowTemplate.Height = 24;
            this.dgvListRoom.Size = new System.Drawing.Size(556, 175);
            this.dgvListRoom.TabIndex = 8;
            // 
            // dgvListService
            // 
            this.dgvListService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListService.Location = new System.Drawing.Point(36, 574);
            this.dgvListService.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvListService.Name = "dgvListService";
            this.dgvListService.RowHeadersWidth = 51;
            this.dgvListService.RowTemplate.Height = 24;
            this.dgvListService.Size = new System.Drawing.Size(556, 175);
            this.dgvListService.TabIndex = 9;
            // 
            // lbInvoiceID
            // 
            this.lbInvoiceID.AutoSize = true;
            this.lbInvoiceID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInvoiceID.Location = new System.Drawing.Point(200, 70);
            this.lbInvoiceID.Name = "lbInvoiceID";
            this.lbInvoiceID.Size = new System.Drawing.Size(144, 32);
            this.lbInvoiceID.TabIndex = 10;
            this.lbInvoiceID.Text = "Mã hóa đơn";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(14, 756);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(597, 2);
            this.panel1.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 775);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "Thành tiền:";
            // 
            // lbTotalAmount
            // 
            this.lbTotalAmount.AutoSize = true;
            this.lbTotalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalAmount.Location = new System.Drawing.Point(200, 775);
            this.lbTotalAmount.Name = "lbTotalAmount";
            this.lbTotalAmount.Size = new System.Drawing.Size(129, 32);
            this.lbTotalAmount.TabIndex = 16;
            this.lbTotalAmount.Text = "Thành tiền";
            // 
            // btnLoadBooking
            // 
            this.btnLoadBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnLoadBooking.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadBooking.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadBooking.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnLoadBooking.Location = new System.Drawing.Point(447, 288);
            this.btnLoadBooking.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLoadBooking.Name = "btnLoadBooking";
            this.btnLoadBooking.Size = new System.Drawing.Size(145, 50);
            this.btnLoadBooking.TabIndex = 17;
            this.btnLoadBooking.Text = "Chi tiết";
            this.btnLoadBooking.UseVisualStyleBackColor = false;
            this.btnLoadBooking.Click += new System.EventHandler(this.btnLoadBooking_Click);
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(206, 130);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(233, 32);
            this.txtName.TabIndex = 18;
            // 
            // txtCCCD
            // 
            this.txtCCCD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(206, 189);
            this.txtCCCD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(233, 32);
            this.txtCCCD.TabIndex = 19;
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(206, 250);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(233, 32);
            this.txtPhone.TabIndex = 20;
            // 
            // lblBookRoomCount
            // 
            this.lblBookRoomCount.AutoSize = true;
            this.lblBookRoomCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookRoomCount.Location = new System.Drawing.Point(200, 306);
            this.lblBookRoomCount.Name = "lblBookRoomCount";
            this.lblBookRoomCount.Size = new System.Drawing.Size(0, 32);
            this.lblBookRoomCount.TabIndex = 21;
            // 
            // lblBookServiceCount
            // 
            this.lblBookServiceCount.AutoSize = true;
            this.lblBookServiceCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookServiceCount.Location = new System.Drawing.Point(200, 535);
            this.lblBookServiceCount.Name = "lblBookServiceCount";
            this.lblBookServiceCount.Size = new System.Drawing.Size(0, 32);
            this.lblBookServiceCount.TabIndex = 22;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(624, 996);
            this.Controls.Add(this.lblBookServiceCount);
            this.Controls.Add(this.lblBookRoomCount);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnLoadBooking);
            this.Controls.Add(this.lbTotalAmount);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbInvoiceID);
            this.Controls.Add(this.dgvListService);
            this.Controls.Add(this.dgvListRoom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCheckout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Invoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phòng đã đặt";
            this.Load += new System.EventHandler(this.Invoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListRoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListService)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvListRoom;
        private System.Windows.Forms.DataGridView dgvListService;
        private System.Windows.Forms.Label lbInvoiceID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbTotalAmount;
        private System.Windows.Forms.Button btnLoadBooking;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblBookRoomCount;
        private System.Windows.Forms.Label lblBookServiceCount;
    }
}