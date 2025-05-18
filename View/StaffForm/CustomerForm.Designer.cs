namespace HotelSystem.View.StaffForm
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
            this.pnLogout = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pnHorizontal = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnBookService = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnBookingRoomInfo = new System.Windows.Forms.Button();
            this.BookingContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnBooking = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnBookingServiceInfo = new System.Windows.Forms.Button();
            this.ManageContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnManager = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnCustomerList = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnInvoiceForm = new System.Windows.Forms.Button();
            this.pnVertical = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpCheckin = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckout = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnLogout.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.BookingContainer.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.ManageContainer.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(1400, 40);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1363, 3);
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
            this.btnHide.Location = new System.Drawing.Point(1327, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(30, 30);
            this.btnHide.TabIndex = 6;
            this.btnHide.Text = "-";
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // pnLogout
            // 
            this.pnLogout.Controls.Add(this.btnLogout);
            this.pnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnLogout.Location = new System.Drawing.Point(0, 760);
            this.pnLogout.Name = "pnLogout";
            this.pnLogout.Size = new System.Drawing.Size(234, 60);
            this.pnLogout.TabIndex = 5;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.White;
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
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
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
            this.flowLayoutPanel.Location = new System.Drawing.Point(268, 187);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(1100, 647);
            this.flowLayoutPanel.TabIndex = 2;
            // 
            // pnHorizontal
            // 
            this.pnHorizontal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(81)))), ((int)(((byte)(64)))));
            this.pnHorizontal.Location = new System.Drawing.Point(240, 40);
            this.pnHorizontal.Name = "pnHorizontal";
            this.pnHorizontal.Size = new System.Drawing.Size(1160, 4);
            this.pnHorizontal.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(81)))), ((int)(((byte)(64)))));
            this.panel3.Location = new System.Drawing.Point(240, 874);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1160, 26);
            this.panel3.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.panel5.Controls.Add(this.btnBookService);
            this.panel5.Location = new System.Drawing.Point(3, 64);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(234, 55);
            this.panel5.TabIndex = 12;
            // 
            // btnBookService
            // 
            this.btnBookService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnBookService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookService.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBookService.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookService.ForeColor = System.Drawing.Color.White;
            this.btnBookService.Location = new System.Drawing.Point(-14, -13);
            this.btnBookService.Name = "btnBookService";
            this.btnBookService.Size = new System.Drawing.Size(260, 79);
            this.btnBookService.TabIndex = 11;
            this.btnBookService.Text = "Đặt dịch vụ";
            this.btnBookService.UseVisualStyleBackColor = false;
            this.btnBookService.Click += new System.EventHandler(this.btnBookService_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.panel6.Controls.Add(this.btnInvoice);
            this.panel6.Location = new System.Drawing.Point(3, 125);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(234, 55);
            this.panel6.TabIndex = 13;
            // 
            // btnInvoice
            // 
            this.btnInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.ForeColor = System.Drawing.Color.White;
            this.btnInvoice.Location = new System.Drawing.Point(-14, -13);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(260, 79);
            this.btnInvoice.TabIndex = 11;
            this.btnInvoice.Text = "Thanh toán";
            this.btnInvoice.UseVisualStyleBackColor = false;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.panel7.Controls.Add(this.btnBookingRoomInfo);
            this.panel7.Location = new System.Drawing.Point(3, 186);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(234, 55);
            this.panel7.TabIndex = 14;
            // 
            // btnBookingRoomInfo
            // 
            this.btnBookingRoomInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnBookingRoomInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookingRoomInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBookingRoomInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookingRoomInfo.ForeColor = System.Drawing.Color.White;
            this.btnBookingRoomInfo.Location = new System.Drawing.Point(-14, -13);
            this.btnBookingRoomInfo.Name = "btnBookingRoomInfo";
            this.btnBookingRoomInfo.Size = new System.Drawing.Size(260, 79);
            this.btnBookingRoomInfo.TabIndex = 11;
            this.btnBookingRoomInfo.Text = "Thông tin đặt phòng";
            this.btnBookingRoomInfo.UseVisualStyleBackColor = false;
            this.btnBookingRoomInfo.Click += new System.EventHandler(this.btnBookingRoomInfo_Click);
            // 
            // BookingContainer
            // 
            this.BookingContainer.Controls.Add(this.panel8);
            this.BookingContainer.Controls.Add(this.panel5);
            this.BookingContainer.Controls.Add(this.panel6);
            this.BookingContainer.Controls.Add(this.panel7);
            this.BookingContainer.Controls.Add(this.panel4);
            this.BookingContainer.Location = new System.Drawing.Point(0, 396);
            this.BookingContainer.Name = "BookingContainer";
            this.BookingContainer.Size = new System.Drawing.Size(234, 307);
            this.BookingContainer.TabIndex = 15;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnBooking);
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(231, 55);
            this.panel8.TabIndex = 12;
            // 
            // btnBooking
            // 
            this.btnBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBooking.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBooking.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBooking.ForeColor = System.Drawing.Color.White;
            this.btnBooking.Location = new System.Drawing.Point(-14, -13);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(260, 79);
            this.btnBooking.TabIndex = 11;
            this.btnBooking.Text = "Đặt đơn khách hàng";
            this.btnBooking.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.panel4.Controls.Add(this.btnBookingServiceInfo);
            this.panel4.Location = new System.Drawing.Point(3, 247);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(234, 55);
            this.panel4.TabIndex = 15;
            // 
            // btnBookingServiceInfo
            // 
            this.btnBookingServiceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnBookingServiceInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBookingServiceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBookingServiceInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookingServiceInfo.ForeColor = System.Drawing.Color.White;
            this.btnBookingServiceInfo.Location = new System.Drawing.Point(-14, -13);
            this.btnBookingServiceInfo.Name = "btnBookingServiceInfo";
            this.btnBookingServiceInfo.Size = new System.Drawing.Size(260, 79);
            this.btnBookingServiceInfo.TabIndex = 11;
            this.btnBookingServiceInfo.Text = "Thông tin dịch vụ";
            this.btnBookingServiceInfo.UseVisualStyleBackColor = false;
            this.btnBookingServiceInfo.Click += new System.EventHandler(this.btnBookingServiceInfo_Click);
            // 
            // ManageContainer
            // 
            this.ManageContainer.Controls.Add(this.panel9);
            this.ManageContainer.Controls.Add(this.panel10);
            this.ManageContainer.Controls.Add(this.panel11);
            this.ManageContainer.Location = new System.Drawing.Point(0, 209);
            this.ManageContainer.Name = "ManageContainer";
            this.ManageContainer.Size = new System.Drawing.Size(234, 181);
            this.ManageContainer.TabIndex = 10;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnManager);
            this.panel9.Location = new System.Drawing.Point(3, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(231, 55);
            this.panel9.TabIndex = 13;
            // 
            // btnManager
            // 
            this.btnManager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManager.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManager.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManager.ForeColor = System.Drawing.Color.White;
            this.btnManager.Location = new System.Drawing.Point(-14, -13);
            this.btnManager.Name = "btnManager";
            this.btnManager.Size = new System.Drawing.Size(260, 79);
            this.btnManager.TabIndex = 11;
            this.btnManager.Text = "Quản lý khách hàng";
            this.btnManager.UseVisualStyleBackColor = false;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnCustomerList);
            this.panel10.Location = new System.Drawing.Point(3, 64);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(231, 55);
            this.panel10.TabIndex = 14;
            // 
            // btnCustomerList
            // 
            this.btnCustomerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnCustomerList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomerList.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCustomerList.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerList.ForeColor = System.Drawing.Color.White;
            this.btnCustomerList.Location = new System.Drawing.Point(-14, -13);
            this.btnCustomerList.Name = "btnCustomerList";
            this.btnCustomerList.Size = new System.Drawing.Size(260, 79);
            this.btnCustomerList.TabIndex = 11;
            this.btnCustomerList.Text = "Danh sách khách hàng";
            this.btnCustomerList.UseVisualStyleBackColor = false;
            this.btnCustomerList.Click += new System.EventHandler(this.btnCustomerList_Click);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.btnInvoiceForm);
            this.panel11.Location = new System.Drawing.Point(3, 125);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(231, 55);
            this.panel11.TabIndex = 15;
            // 
            // btnInvoiceForm
            // 
            this.btnInvoiceForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(122)))), ((int)(((byte)(150)))));
            this.btnInvoiceForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInvoiceForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnInvoiceForm.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoiceForm.ForeColor = System.Drawing.Color.White;
            this.btnInvoiceForm.Location = new System.Drawing.Point(-14, -13);
            this.btnInvoiceForm.Name = "btnInvoiceForm";
            this.btnInvoiceForm.Size = new System.Drawing.Size(260, 79);
            this.btnInvoiceForm.TabIndex = 11;
            this.btnInvoiceForm.Text = "Lịch sử hóa đơn";
            this.btnInvoiceForm.UseVisualStyleBackColor = false;
            this.btnInvoiceForm.Click += new System.EventHandler(this.btnInvoiceForm_Click);
            // 
            // pnVertical
            // 
            this.pnVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(81)))), ((int)(((byte)(64)))));
            this.pnVertical.Location = new System.Drawing.Point(240, 40);
            this.pnVertical.Name = "pnVertical";
            this.pnVertical.Size = new System.Drawing.Size(4, 860);
            this.pnVertical.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(76)))), ((int)(((byte)(172)))));
            this.panel2.Controls.Add(this.ManageContainer);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.pnLogout);
            this.panel2.Controls.Add(this.BookingContainer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 860);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HotelSystem.Properties.Resources.icons8_user_100;
            this.pictureBox1.Location = new System.Drawing.Point(64, 42);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 120);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(486, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ngày đặt:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(885, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ngày trả:";
            // 
            // dtpCheckin
            // 
            this.dtpCheckin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckin.Location = new System.Drawing.Point(591, 126);
            this.dtpCheckin.Name = "dtpCheckin";
            this.dtpCheckin.Size = new System.Drawing.Size(200, 34);
            this.dtpCheckin.TabIndex = 5;
            this.dtpCheckin.ValueChanged += new System.EventHandler(this.DtpCheckIn_ValueChanged);
            // 
            // dtpCheckout
            // 
            this.dtpCheckout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckout.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckout.Location = new System.Drawing.Point(985, 126);
            this.dtpCheckout.Name = "dtpCheckout";
            this.dtpCheckout.Size = new System.Drawing.Size(200, 34);
            this.dtpCheckout.TabIndex = 6;
            this.dtpCheckout.ValueChanged += new System.EventHandler(this.DtpCheckOut_ValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(34)))), ((int)(((byte)(217)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(268, 125);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(187, 41);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(208)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(1400, 900);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnVertical);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnHorizontal);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpCheckout);
            this.Controls.Add(this.dtpCheckin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerForm";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            this.panel1.ResumeLayout(false);
            this.pnLogout.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.BookingContainer.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ManageContainer.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel pnLogout;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Panel pnHorizontal;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnBookService;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnBookingRoomInfo;
        private System.Windows.Forms.FlowLayoutPanel BookingContainer;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnBooking;
        private System.Windows.Forms.FlowLayoutPanel ManageContainer;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnManager;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btnCustomerList;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnInvoiceForm;
        private System.Windows.Forms.Panel pnVertical;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnBookingServiceInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCheckin;
        private System.Windows.Forms.DateTimePicker dtpCheckout;
        private System.Windows.Forms.Button btnSearch;
    }
}