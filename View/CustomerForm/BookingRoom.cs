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
        public BookingRoom()
        {
            InitializeComponent();
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            LoadCBBRoomType();
            dtpCheck_in.Value = DateTime.Today;
            dtpCheck_out.Value = DateTime.Today.AddDays(1);

            LoadCustomerInfo();
        }

        private void LoadCustomerInfo()
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtCCCD.Text = customer.CCCD;
                txtPhone.Text = customer.Phone;
            }
        }

        private void LoadCBBRoomType()
        {
            using (var db = new DBHotelSystem())
            {
                var roomTypes = db.RoomTypes.ToList();
                cbbRoomType.DataSource = roomTypes;
                cbbRoomType.DisplayMember = "room_type";
                cbbRoomType.ValueMember = "roomtype_id";
            }
        }

        private void cbbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomType = cbbRoomType.SelectedItem.ToString();
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
            if (cbbRoomType.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByName(txtName.Text);
            if (customer == null)
            {
                DialogResult result = MessageBox.Show("Họ tên không tồn tại, vui lòng thêm thông tin cá nhân!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    CustomerInfo opform = new CustomerInfo();
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

            int roomTypeId = (int)cbbRoomType.SelectedValue;
            DateTime checkIn = dtpCheck_in.Value;
            DateTime checkOut = dtpCheck_out.Value;

            if (checkIn >= checkOut)
            {
                MessageBox.Show("Ngày trả phòng không hợp lê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bllBookingRoom = new BLL_BookingRoom();
            var availableRooms = bllBookingRoom.GetAvailableRooms(roomTypeId, checkIn, checkOut);

            if (availableRooms.Count == 0)
            {
                MessageBox.Show("Không có phòng trống trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRoom = availableRooms.First();
            decimal totalPrice = selectedRoom.RoomType.price * (decimal)(checkOut - checkIn).TotalDays;

            var dtobooking = new DTO_BookingRoom
            {
                CustomerId = customer.CustomerId,
                RoomId = selectedRoom.room_id,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Booked",
                TotalPrice = totalPrice
            };

            bllBookingRoom.AddBooking(dtobooking);

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
