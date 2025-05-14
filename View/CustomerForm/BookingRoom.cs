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
        private int? _bookingId;
        private int? _customerId;
        private bool _isEditMode = false;
        
        // Constructor mặc định cho đặt phòng mới
        public BookingRoom(string roomNumber)
        {
            InitializeComponent();
            _roomNumber = roomNumber;
            _isEditMode = false;
        }
        
        // Constructor mới cho chỉnh sửa đặt phòng
        public BookingRoom(int? customerId, int bookingId)
        {
            InitializeComponent();
            _bookingId = bookingId;
            _customerId = customerId;
            _isEditMode = true;
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            if (_isEditMode)
            {
                // Chế độ chỉnh sửa - Tải thông tin đặt phòng hiện có
                LoadBookingData();
                LoadCustomerData(UserSession.UserId);
                this.Text = "Chỉnh sửa đặt phòng";
                btnSubmit.Text = "Cập nhật";
            }
            else
            {
                // Chế độ thêm mới - Khởi tạo giá trị mặc định
                dtpCheck_in.Value = DateTime.Today;
                dtpCheck_out.Value = DateTime.Today.AddDays(1);
                txtRoomNumber.Text = _roomNumber;
                
                // Tải thông tin khách hàng từ UserId hiện tại
                LoadCustomerData(UserSession.UserId);
            }
        }
        
        private void LoadBookingData()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Tìm thông tin đặt phòng
                    var booking = db.Bookings.Find(_bookingId);
                    if (booking != null)
                    {
                        // Tìm thông tin phòng
                        var room = db.Rooms.Find(booking.room_id);
                        if (room != null)
                        {
                            _roomNumber = room.room_number;
                            txtRoomNumber.Text = _roomNumber;
                        }
                        
                        // Đặt ngày nhận và trả phòng
                        dtpCheck_in.Value = booking.check_in;
                        dtpCheck_out.Value = booking.check_out;
                        
                        // Tải thông tin khách hàng
                        LoadCustomerData(booking.customer_id);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        
        private void LoadCustomerData(int userId)
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(userId);
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

            var bllBookingRoom = new BLL_BookingRoom();
            
            if (_isEditMode && _bookingId.HasValue)
            {
                // Cập nhật thông tin đặt phòng
                var dtoUpdateBooking = new DTO_BookingRoom
                {
                    BookingId = _bookingId.Value,
                    CustomerId = customer.CustomerId,
                    RoomId = selectedRoom.RoomId,
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Status = "Booked", // Vẫn giữ trạng thái đã đặt
                    TotalPrice = totalPrice
                };
                
                bllBookingRoom.UpdateBooking(dtoUpdateBooking);
                MessageBox.Show("Cập nhật đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Tạo đặt phòng mới
                var dtobooking = new DTO_BookingRoom
                {
                    CustomerId = customer.CustomerId,
                    RoomId = selectedRoom.RoomId,
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Status = "Booked",
                    TotalPrice = totalPrice
                };

                // Thêm booking
                bllBookingRoom.AddBooking(dtobooking);

                // Cập nhật trạng thái phòng
                bllRoom.UpdateRoomStatus(selectedRoom.RoomId, "Booked");
                
                MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
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

