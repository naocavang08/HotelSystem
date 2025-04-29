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
            if (string.IsNullOrEmpty(txtCCCD.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtName.Text)) 
            {
                MessageBox.Show("Please fill in all personal information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbbRoomType.SelectedValue == null)
            {
                MessageBox.Show("Please select a room type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int roomTypeId = (int)cbbRoomType.SelectedValue;
            DateTime checkIn = dtpCheck_in.Value;
            DateTime checkOut = dtpCheck_out.Value;

            // Kiểm tra ngày hợp lệ
            if (checkIn >= checkOut)
            {
                MessageBox.Show("Check-in date must be before check-out date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            BookingBLL bookingBLL = new BookingBLL();
            var availableRooms = bookingBLL.GetAvailableRoom(roomTypeId, checkIn, checkOut);

            if (availableRooms.Count == 0)
            {
                MessageBox.Show("No available rooms for the selected dates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Lấy phòng đầu tiên trong danh sách phòng trống
            var selectedRoom = availableRooms.First();

            // Tổng tiền phòng
            decimal totalPrice = selectedRoom.RoomType.price * (decimal)(checkOut - checkIn).TotalDays;

            // Tạo booking
            BookingDTO bookingDTO = new BookingDTO
            {
                CustomerId = UserSession.UserId,
                RoomId = selectedRoom.room_id,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Booked",
                TotalPrice = totalPrice
            };
            bookingBLL.AddBooking(bookingDTO);

            MessageBox.Show("Booking successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
