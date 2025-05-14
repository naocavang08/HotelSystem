using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Session;
using System.Runtime.InteropServices;

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

        public CustomerForm()
        {
            InitializeComponent();
            bllRoom = new BLL_Room();
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
            LoadRoomPanel();
        }

        private void LoadRoomPanel()
        {
            var rooms = bllRoom.GetAllRooms();

            foreach (var room in rooms)
            {
                Panel roomPanel = new Panel()
                {
                    Size = new Size(200, 150),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10),
                    Tag = room
                };
                Label lblRoomName = new Label
                {
                    Text = $"Phòng: {room.RoomName}",
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                Label lblRoomType = new Label
                {
                    Text = $"Loại: {room.RoomType}",
                    Location = new Point(10, 40),
                    AutoSize = true
                };

                Label lblPrice = new Label
                {
                    Text = $"Giá: {room.Price:C}",
                    Location = new Point(10, 70),
                    AutoSize = true
                };

                Label lblStatus = new Label
                {
                    Text = room.Status,
                    ForeColor = room.Status == "Available" ? Color.Green : Color.Red,
                    Location = new Point(10, 100),
                    AutoSize = true
                };
                roomPanel.Controls.Add(lblRoomName);
                roomPanel.Controls.Add(lblRoomType);
                roomPanel.Controls.Add(lblPrice);
                roomPanel.Controls.Add(lblStatus);

                roomPanel.Click += (s, e) => RoomPanel_Click(s, e, room);

                flowLayoutPanel.Controls.Add(roomPanel);
            }
            this.Controls.Add(flowLayoutPanel);
        }

        private void RoomPanel_Click(object sender, EventArgs e, DTO_Room room)
        {
            if (room.Status != "Available")
            {
                return;
            }

            // Chuyển đến form đặt phòng
            BookingRoom bookingRoomForm = new BookingRoom(room.RoomName);
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
    }
}
