using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Session;
using System.Runtime.InteropServices;
using HotelSystem.Model;

namespace HotelSystem.View.StaffForm
{
    public partial class StaffForm: Form
    {
        // Dùng để gọi hàm API từ Windows
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Các hằng số
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        private BLL_Room bllRoom;
        private BLL_BookingRoom bllBookingRoom;
        private DateTime checkInDate;
        private DateTime checkOutDate;

        public StaffForm()
        {
            InitializeComponent();
            bllRoom = new BLL_Room();
            bllBookingRoom = new BLL_BookingRoom();
            
            // Set default dates
            checkInDate = DateTime.Today;
            checkOutDate = DateTime.Today.AddDays(1);
        }
        
        private void DtpCheckIn_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            checkInDate = dtp.Value;
            
            // Ensure checkout is after checkin
            DateTimePicker dtpCheckOut = this.Controls.Find("dtpCheckOut", true).FirstOrDefault() as DateTimePicker;
            if (dtpCheckOut != null && checkInDate >= dtpCheckOut.Value)
            {
                dtpCheckOut.Value = checkInDate.AddDays(1);
            }
        }
        
        private void DtpCheckOut_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            checkOutDate = dtp.Value;
        }
        
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Validate dates
            if (checkInDate >= checkOutDate)
            {
                MessageBox.Show("Ngày trả phòng phải sau ngày nhận phòng!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Filter and load available rooms
            LoadAvailableRooms(checkInDate, checkOutDate);
        }
        
        public void LoadAvailableRooms(DateTime checkIn, DateTime checkOut)
        {
            // Save the dates
            checkInDate = checkIn;
            checkOutDate = checkOut;
            
            // Update date pickers if they exist
            DateTimePicker dtpCheckIn = this.Controls.Find("dtpCheckIn", true).FirstOrDefault() as DateTimePicker;
            DateTimePicker dtpCheckOut = this.Controls.Find("dtpCheckOut", true).FirstOrDefault() as DateTimePicker;
            
            if (dtpCheckIn != null) dtpCheckIn.Value = checkIn;
            if (dtpCheckOut != null) dtpCheckOut.Value = checkOut;
            
            // Clear existing room panels
            flowLayoutPanel.Controls.Clear();
            
            // Calculate duration
            int days = (int)(checkOut - checkIn).TotalDays;
            
            // Get all rooms
            var allRooms = bllRoom.GetAllRooms();
            
            // Create a container panel with full width for the count text
            Panel countPanel = new Panel
            {
                Width = flowLayoutPanel.Width - 40,
                Height = 30,
                Margin = new Padding(10, 0, 10, 10)
            };
            
            // Create a label to show the count of available rooms
            int availableRoomCount = allRooms.Count(r => r.Status == "Available");
            Label lblCount = new Label
            {
                Text = $"Tìm thấy {availableRoomCount} phòng trống",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.Black,
                Location = new Point(5, 5),
                AutoSize = true
            };
            countPanel.Controls.Add(lblCount);
            flowLayoutPanel.Controls.Add(countPanel);
            
            // Add rooms
            foreach (var room in allRooms)
            {
                // Check if the room is already booked during the selected dates
                bool isAvailable = room.Status == "Available";
                
                // Create room panel
                Panel roomPanel = new Panel()
                {
                    Size = new Size(240, 180),
                    BorderStyle = BorderStyle.None,
                    Margin = new Padding(10),
                    Tag = room,
                    BackColor = isAvailable ? Color.FromArgb(228, 249, 228) : Color.FromArgb(255, 235, 235),
                    Padding = new Padding(10)
                };
                
                // Room image icon (use a simple label instead of picture box if resource is missing)
                Label lblIcon = new Label
                {
                    Text = "🛏️",
                    Font = new Font("Segoe UI", 16, FontStyle.Regular),
                    Size = new Size(32, 32),
                    Location = new Point(10, 10),
                    ForeColor = Color.FromArgb(31,31,31)
                };
                
                Label lblRoomName = new Label
                {
                    Text = $"Phòng {room.RoomName}",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(50, 50, 50),
                    Location = new Point(50, 10),
                    AutoSize = true
                };

                Label lblRoomType = new Label
                {
                    Text = $"Loại: {room.RoomType}",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                    Location = new Point(10, 50),
                    ForeColor = Color.FromArgb(80, 80, 80),
                    AutoSize = true
                };

                Label lblPrice = new Label
                {
                    Text = $"Giá: {room.Price:N0}đ/ngày",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                    Location = new Point(10, 75),
                    ForeColor = Color.FromArgb(80, 80, 80),
                    AutoSize = true
                };
                
                // Calculate total price for the stay
                decimal totalPrice = room.Price * days;
                Label lblTotalPrice = new Label
                {
                    Text = $"Tổng: {totalPrice:N0}đ ({days} ngày)",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    Location = new Point(10, 100),
                    ForeColor = Color.FromArgb(31, 31, 31),
                    AutoSize = true
                };

                Label lblStatus = new Label
                {
                    Text = isAvailable ? "Có sẵn" : "Đã đặt",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                    ForeColor = isAvailable ? Color.Green : Color.Red,
                    Location = new Point(10, 125),
                    AutoSize = true
                };
                
                roomPanel.Controls.Add(lblIcon);
                roomPanel.Controls.Add(lblRoomName);
                roomPanel.Controls.Add(lblRoomType);
                roomPanel.Controls.Add(lblPrice);
                roomPanel.Controls.Add(lblTotalPrice);
                roomPanel.Controls.Add(lblStatus);

                if (isAvailable)
                {
                    roomPanel.Cursor = Cursors.Hand;
                    roomPanel.Click += (s, e) => RoomPanel_Click(s, e, room);
                    
                    // Add a visual indicator that the room is bookable
                    Button btnBook = new Button
                    {
                        Text = "Đặt phòng",
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Size = new Size(100, 30),
                        Location = new Point(roomPanel.Width - 115, roomPanel.Height - 45),
                        BackColor = Color.FromArgb(31,31,31),
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand
                    };
                    btnBook.FlatAppearance.BorderSize = 0;
                    btnBook.Click += (s, e) => RoomPanel_Click(roomPanel, e, room);
                    roomPanel.Controls.Add(btnBook);
                }

                flowLayoutPanel.Controls.Add(roomPanel);
            }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UserSession.Clear();
                Application.Exit();
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UserSession.Clear();
                this.Hide();
                LoginForm op = new LoginForm();
                op.Show();
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            //lblWelcome.Text = $"Xin chào, {UserSession.Username}!";
            
            // Load available rooms for the default date range
            LoadAvailableRooms(checkInDate, checkOutDate);
        }

        private void RoomPanel_Click(object sender, EventArgs e, DTO_Room room)
        {
            // Chuyển đến form đặt phòng với thông tin ngày đã chọn
            BookingRoom bookingRoomForm = new BookingRoom(room.RoomName, checkInDate, checkOutDate);
            bookingRoomForm.Show();
            this.Hide();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            // Gửi thông điệp để giả lập việc kéo thanh tiêu đề
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBookService_Click(object sender, EventArgs e)
        {
            BookingService op = new BookingService();
            op.Show();

            this.Hide();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ID hóa đơn mới
                var bllInvoice = new BLL_Invoice();
                int nextInvoiceId = bllInvoice.GetNextInvoiceId();
                
                // Tạo form hóa đơn mới với ID vừa lấy và truyền CustomerForm hiện tại làm caller
                Invoice invoiceForm = new Invoice(nextInvoiceId, this);
                
                // Hiển thị form hóa đơn
                invoiceForm.ShowDialog();
                if (invoiceForm.DialogResult == DialogResult.OK)
                {
                    LoadAvailableRooms(checkInDate, checkOutDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo hóa đơn mới: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBookingRoomInfo_Click(object sender, EventArgs e)
        {
            BookingRoomInfo op = new BookingRoomInfo();
            op.Show();
            this.Hide();
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            CustomerList op = new CustomerList();
            op.Show();
            this.Hide();
        }

        private void btnInvoiceForm_Click(object sender, EventArgs e)
        {
            InvoiceForm op = new InvoiceForm();
            op.Show();
            this.Hide();
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnBookingServiceInfo_Click(object sender, EventArgs e)
        {
            BookingServiceInfo op = new BookingServiceInfo();
            op.Show();
            this.Hide();
        }

        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
