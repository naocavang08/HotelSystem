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

namespace HotelSystem.View.CustomerForm
{
    public partial class CustomerForm: Form
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

        public CustomerForm()
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
            
            // Get booked rooms that overlap with the selected dates
            var bookedRoomIds = GetBookedRoomIds(checkIn, checkOut);
            
            // Create a container panel with full width for the count text
            Panel countPanel = new Panel
            {
                Width = flowLayoutPanel.Width - 40,
                Height = 30,
                Margin = new Padding(10, 0, 10, 10)
            };
            
            // Create a label to show the count of available rooms
            int availableRoomCount = allRooms.Count(r => !bookedRoomIds.Contains(r.RoomId) && r.Status == "Available");
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
                bool isAvailable = !bookedRoomIds.Contains(room.RoomId) && room.Status == "Available";
                
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
                    ForeColor = Color.FromArgb(60, 34, 217)
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
                    ForeColor = Color.FromArgb(60, 34, 217),
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
                        BackColor = Color.FromArgb(60, 34, 217),
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
        
        private HashSet<int> GetBookedRoomIds(DateTime checkIn, DateTime checkOut)
        {
            HashSet<int> bookedRoomIds = new HashSet<int>();
            
            using (var db = new DBHotelSystem())
            {
                // Find all bookings that overlap with the selected date range
                var overlappingBookings = db.Bookings
                    .Where(b => (b.check_in < checkOut && b.check_out > checkIn) && 
                               (b.status == "Booked" || b.status == "Checked In"))
                    .ToList();
                
                foreach (var booking in overlappingBookings)
                {
                    bookedRoomIds.Add(booking.room_id);
                }
            }
            
            return bookedRoomIds;
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
                this.Close();
                LoginForm op = new LoginForm();
                op.Show();
            }
        }

        private void lblCusInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo op = new CustomerInfo(this);
            op.Show();

            this.Hide();
        }

        private void picCusInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo op = new CustomerInfo(this);
            op.Show();

            this.Hide();
        }

        private void lblBookService_Click(object sender, EventArgs e)
        {
            BookingService op = new BookingService();
            op.Show();

            this.Hide();
        }

        private void picBookService_Click(object sender, EventArgs e)
        {
            BookingService op = new BookingService();
            op.Show();

            this.Hide();
        }

        private void picDelRoom_Click(object sender, EventArgs e)
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            BookingRoomInfo op = new BookingRoomInfo(customer.CustomerId);
            op.Show();

            this.Hide();
        }

        private void lblDelRoom_Click(object sender, EventArgs e)
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            BookingRoomInfo op = new BookingRoomInfo(customer.CustomerId);
            op.Show();

            this.Hide();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào, {UserSession.Username}!";
            
            // Load available rooms for the default date range
            LoadAvailableRooms(checkInDate, checkOutDate);
        }

        private void LoadRoomPanel()
        {
            // This method is replaced by LoadAvailableRooms
            // Keep it for backward compatibility
            LoadAvailableRooms(checkInDate, checkOutDate);
        }

        private void RoomPanel_Click(object sender, EventArgs e, DTO_Room room)
        {
            // Chuyển đến form đặt phòng với thông tin ngày đã chọn
            BookingRoom bookingRoomForm = new BookingRoom(room.RoomName, checkInDate, checkOutDate);
            bookingRoomForm.Show();
            this.Hide();
        }

        private void lblInvoice_Click(object sender, EventArgs e)
        {
            ShowInvoice();
        }

        private void picInvoice_Click(object sender, EventArgs e)
        {
            ShowInvoice();
        }

        private void ShowInvoice()
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            int customerId = customer.CustomerId;

            var bllBookingRoom = new BLL_BookingRoom();
            var bllBookingService = new BLL_BookingService();

            // Lấy danh sách booking room và lọc theo trạng thái "Booked"
            var allBookingRooms = bllBookingRoom.GetBookingRoomsByCustomerId(customerId);
            var bookingRooms = allBookingRooms.Where(br => br.Status == "Booked").ToList();

            // Lấy danh sách booking service và lọc theo trạng thái "Booked"
            var allBookingServices = bllBookingService.GetBookingServicesByCustomerId(customerId);
            var bookingServices = allBookingServices.Where(bs => bs.Status == "Booked").ToList();

            // Kiểm tra nếu không có booking nào có trạng thái "Booked"
            if (bookingRooms.Count == 0)
            {
                MessageBox.Show("Không có phòng nào đang trong trạng thái chờ thanh toán!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Invoice invoiceForm = new Invoice
            (
                bookingRooms, 
                (bookingServices != null && bookingServices.Count > 0) ? bookingServices : null
            );
            invoiceForm.Show();
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

        public void LoadCustomerProfile(int customerId)
        {
            try
            {
                // Get customer information
                var bllTTKH = new BLL_TTKH();
                var customer = bllTTKH.GetAllCustomer().FirstOrDefault(c => c.CustomerId == customerId);
                
                if (customer != null)
                {
                    // Update welcome message with customer name
                    lblWelcome.Text = $"Xin chào, {customer.Name}!";
                    
                    // Load bookings for this customer
                    var bookings = bllBookingRoom.GetBookingRoomsByCustomerId(customerId);
                    
                    // Clear existing room panels
                    flowLayoutPanel.Controls.Clear();
                    
                    // Create a container panel with full width for the customer info
                    Panel customerInfoPanel = new Panel
                    {
                        Width = flowLayoutPanel.Width - 40,
                        Height = 100,
                        Margin = new Padding(10, 0, 10, 10),
                        BackColor = Color.FromArgb(240, 240, 255)
                    };
                    
                    // Add customer info
                    Label lblTitle = new Label
                    {
                        Text = $"Thông tin khách hàng:",
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 34, 217),
                        Location = new Point(10, 10),
                        AutoSize = true
                    };
                    
                    Label lblName = new Label
                    {
                        Text = $"Họ tên: {customer.Name}",
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.Black,
                        Location = new Point(10, 35),
                        AutoSize = true
                    };
                    
                    Label lblPhone = new Label
                    {
                        Text = $"SĐT: {customer.Phone}",
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.Black,
                        Location = new Point(10, 60),
                        AutoSize = true
                    };
                    
                    Label lblCCCD = new Label
                    {
                        Text = $"CCCD: {customer.CCCD}",
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.Black, 
                        Location = new Point(250, 35),
                        AutoSize = true
                    };
                    
                    string gender = customer.Gender.HasValue ? (customer.Gender.Value ? "Nam" : "Nữ") : "";
                    Label lblGender = new Label
                    {
                        Text = $"Giới tính: {gender}",
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        ForeColor = Color.Black,
                        Location = new Point(250, 60),
                        AutoSize = true
                    };
                    
                    customerInfoPanel.Controls.Add(lblTitle);
                    customerInfoPanel.Controls.Add(lblName);
                    customerInfoPanel.Controls.Add(lblPhone);
                    customerInfoPanel.Controls.Add(lblCCCD);
                    customerInfoPanel.Controls.Add(lblGender);
                    
                    flowLayoutPanel.Controls.Add(customerInfoPanel);
                    
                    // Create a panel for bookings title
                    Panel bookingsTitlePanel = new Panel
                    {
                        Width = flowLayoutPanel.Width - 40,
                        Height = 40,
                        Margin = new Padding(10, 10, 10, 0),
                        BackColor = Color.FromArgb(230, 240, 255)
                    };
                    
                    Label lblBookingsTitle = new Label
                    {
                        Text = $"Danh sách đặt phòng ({bookings.Count}):",
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        ForeColor = Color.FromArgb(60, 34, 217),
                        Location = new Point(10, 10),
                        AutoSize = true
                    };
                    
                    bookingsTitlePanel.Controls.Add(lblBookingsTitle);
                    flowLayoutPanel.Controls.Add(bookingsTitlePanel);
                    
                    // Get all rooms for room information
                    var allRooms = bllRoom.GetAllRooms();
                    
                    // Add bookings
                    if (bookings.Count > 0)
                    {
                        foreach (var booking in bookings)
                        {
                            // Find room details
                            var room = allRooms.FirstOrDefault(r => r.RoomId == booking.RoomId);
                            if (room == null) continue;
                            
                            // Create booking panel
                            Panel bookingPanel = new Panel
                            {
                                Size = new Size(flowLayoutPanel.Width - 60, 120),
                                BorderStyle = BorderStyle.None,
                                Margin = new Padding(15, 5, 15, 5),
                                Tag = booking,
                                BackColor = booking.Status == "Checked Out" ? Color.FromArgb(240, 240, 240) : Color.FromArgb(228, 249, 228),
                                Padding = new Padding(10)
                            };
                            
                            // Room icon
                            Label lblIcon = new Label
                            {
                                Text = "🛏️",
                                Font = new Font("Segoe UI", 16, FontStyle.Regular),
                                Size = new Size(32, 32),
                                Location = new Point(10, 10),
                                ForeColor = Color.FromArgb(60, 34, 217)
                            };
                            
                            // Room information
                            Label lblRoomName = new Label
                            {
                                Text = $"Phòng {room.RoomName}",
                                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                                ForeColor = Color.FromArgb(50, 50, 50),
                                Location = new Point(50, 10),
                                AutoSize = true
                            };
                            
                            // Booking dates
                            Label lblDates = new Label
                            {
                                Text = $"Từ {booking.CheckIn:dd/MM/yyyy} đến {booking.CheckOut:dd/MM/yyyy}",
                                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                                Location = new Point(50, 40),
                                ForeColor = Color.FromArgb(80, 80, 80),
                                AutoSize = true
                            };
                            
                            // Booking status
                            Label lblStatus = new Label
                            {
                                Text = $"Trạng thái: {booking.Status}",
                                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                                Location = new Point(50, 65),
                                ForeColor = booking.Status == "Checked Out" ? Color.Gray : (booking.Status == "Booked" ? Color.Blue : Color.Green),
                                AutoSize = true
                            };
                            
                            // Total price
                            Label lblPrice = new Label
                            {
                                Text = $"Tổng tiền: {booking.TotalPrice:N0}đ",
                                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                                Location = new Point(350, 40),
                                ForeColor = Color.FromArgb(60, 34, 217),
                                AutoSize = true
                            };
                            
                            bookingPanel.Controls.Add(lblIcon);
                            bookingPanel.Controls.Add(lblRoomName);
                            bookingPanel.Controls.Add(lblDates);
                            bookingPanel.Controls.Add(lblStatus);
                            bookingPanel.Controls.Add(lblPrice);
                            
                            flowLayoutPanel.Controls.Add(bookingPanel);
                        }
                    }
                    else
                    {
                        // No bookings message
                        Panel noBookingsPanel = new Panel
                        {
                            Width = flowLayoutPanel.Width - 60,
                            Height = 60,
                            Margin = new Padding(15, 10, 15, 10),
                            BackColor = Color.FromArgb(255, 245, 230)
                        };
                        
                        Label lblNoBookings = new Label
                        {
                            Text = "Không có đặt phòng nào cho khách hàng này.",
                            Font = new Font("Segoe UI", 10, FontStyle.Italic),
                            ForeColor = Color.FromArgb(180, 100, 60),
                            Location = new Point(10, 20),
                            AutoSize = true
                        };
                        
                        noBookingsPanel.Controls.Add(lblNoBookings);
                        flowLayoutPanel.Controls.Add(noBookingsPanel);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
