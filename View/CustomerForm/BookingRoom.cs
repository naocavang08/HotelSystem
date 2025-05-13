using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.View.AdminForm;
using HotelSystem.Session;

namespace HotelSystem.View.CustomerForm
{
    public partial class BookingRoom: Form
    {
        private string _roomNumber;
        public BookingRoom(string roomNumber)
        {
            InitializeComponent();
            _roomNumber = roomNumber;
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            dtpCheck_in.Value = DateTime.Today;
            dtpCheck_out.Value = DateTime.Today.AddDays(1);

            txtRoomNumber.Text = _roomNumber;

            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtCCCD.Text = customer.CCCD;
                txtPhone.Text = customer.Phone;
            }
        }

        private void dtpCheck_in_ValueChanged(object sender, EventArgs e)
        {
            string checkInDate = dtpCheck_in.Value.ToString("yyyy-MM-dd");
        }

        private void dtpCheck_out_ValueChanged(object sender, EventArgs e)
        {
            string checkOutDate = dtpCheck_out.Value.ToString("yyyy-MM-dd");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByName(txtName.Text);
            if (customer == null)
            {
                DialogResult result = MessageBox.Show("Họ tên không tồn tại, vui lòng thêm thông tin cá nhân!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    CustomerInfo opform = new CustomerInfo(this);
                    opform.ShowDialog();    
                }
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCCCD.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (customer.CCCD != txtCCCD.Text.Trim() || customer.Phone != txtPhone.Text.Trim())
            {
                MessageBox.Show("CCCD hoặc số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime checkIn = dtpCheck_in.Value;
            DateTime checkOut = dtpCheck_out.Value;

            if (checkIn >= checkOut)
            {
                MessageBox.Show("Ngày trả phòng không hợp lê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bllRoom = new BLL_Room();
            var rooms = bllRoom.GetAllRooms();
            var selectedRoom = rooms.FirstOrDefault(r => r.RoomName == _roomNumber);

            // Tính tổng giá tiền
            decimal totalPrice = selectedRoom.Price * (decimal)(checkOut - checkIn).TotalDays;

            // Tạo DTO_BookingRoom
            var dtobooking = new DTO_BookingRoom
            {
                CustomerId = customer.CustomerId,
                RoomId = selectedRoom.RoomId, // Gán RoomId từ phòng đã tìm thấy
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Booked",
                TotalPrice = totalPrice
            };

            // Thêm booking
            var bllBookingRoom = new BLL_BookingRoom();
            bllBookingRoom.AddBooking(dtobooking);

            // Cập nhật trạng thái phòng
            bllRoom.UpdateRoomStatus(selectedRoom.RoomId, "Booked");

            MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CustomerForm op = new CustomerForm();
            op.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerForm op = new CustomerForm();
            op.Show();
        }
    }
}
