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

namespace HotelSystem.View.StaffForm
{
    public partial class BookingRoom: Form
    {
        private string _roomNumber;
        private int? _bookingId;
        private int? _customerId;
        private bool _isEditMode = false;
        private DateTime _checkInDate;
        private DateTime _checkOutDate;
        
        // Constructor mặc định cho đặt phòng mới (được gọi từ code cũ)
        public BookingRoom(string roomNumber)
        {
            InitializeComponent();  
            _roomNumber = roomNumber;
            _isEditMode = false;
            _checkInDate = DateTime.Today;
            _checkOutDate = DateTime.Today.AddDays(1);
        }
        
        // Constructor mới với ngày check-in và check-out
        public BookingRoom(string roomNumber, DateTime checkIn, DateTime checkOut)
        {
            InitializeComponent();
            _roomNumber = roomNumber;
            _isEditMode = false;
            _checkInDate = checkIn;
            _checkOutDate = checkOut;
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
                this.Text = "Chỉnh sửa đặt phòng";
                btnSubmit.Text = "Cập nhật";
            }
            else
            {
                // Chế độ thêm mới - Khởi tạo giá trị mặc định
                dtpCheck_in.Value = _checkInDate;
                dtpCheck_out.Value = _checkOutDate;
                txtRoomNumber.Text = _roomNumber;
                
                // Hiển thị thời gian ở phòng và tổng tiền
                UpdateTotalPrice();
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
                        _checkInDate = booking.check_in;
                        _checkOutDate = booking.check_out;
                        
                        // Hiển thị thời gian ở phòng và tổng tiền
                        UpdateTotalPrice();
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

        private void dtpCheck_in_ValueChanged(object sender, EventArgs e)
        {
            _checkInDate = dtpCheck_in.Value;
            
            // Ensure check-out date is after check-in date
            if (_checkInDate >= dtpCheck_out.Value)
            {
                dtpCheck_out.Value = _checkInDate.AddDays(1);
            }
            
            UpdateTotalPrice();
        }

        private void dtpCheck_out_ValueChanged(object sender, EventArgs e)
        {
            _checkOutDate = dtpCheck_out.Value;
            UpdateTotalPrice();
        }
        
        private void UpdateTotalPrice()
        {
            try
            {
                // Calculate the number of days
                TimeSpan duration = _checkOutDate - _checkInDate;
                int days = (int)duration.TotalDays;
                
                if (days <= 0)
                {
                    // Handle invalid date range
                    lblDuration.Text = "Thời gian ở: N/A";
                    lblTotalPrice.Text = "Tổng tiền: N/A";
                    return;
                }
                
                // Get room price
                var bllRoom = new BLL_Room();
                var rooms = bllRoom.GetAllRooms();
                var selectedRoom = rooms.FirstOrDefault(r => r.RoomName == _roomNumber);
                
                if (selectedRoom != null)
                {
                    decimal totalPrice = selectedRoom.Price * days;
                    
                    // Display duration and total price
                    lblDuration.Text = $"Thời gian ở: {days} ngày";
                    lblTotalPrice.Text = $"Tổng tiền: {totalPrice:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng tiền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    var newCustomerId = bllTTKH.GetNextCustomerId();
                    CustomerDetail opform = new CustomerDetail(newCustomerId);
                    opform.ShowDialog();
                    if (opform.DialogResult == DialogResult.OK)
                    {
                        var newCustomer = bllTTKH.GetCustomerByCustomerId(newCustomerId);
                        if (newCustomer != null)
                        {
                            txtName.Text = newCustomer.Name;
                            txtCCCD.Text = newCustomer.CCCD;
                            txtPhone.Text = newCustomer.Phone;
                        }
                    }
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
                // Check if the room is available for the selected dates
                if (!IsRoomAvailable(selectedRoom.RoomId, checkIn, checkOut))
                {
                    MessageBox.Show("Phòng này đã được đặt trong khoảng thời gian bạn chọn!", 
                        "Phòng không khả dụng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
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
            
            // Quay về CustomerForm với các ngày đã chọn
            CustomerForm customerForm = new CustomerForm();
            customerForm.LoadAvailableRooms(checkIn, checkOut);
            customerForm.Show();
            this.Close();
        }
        
        private bool IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut)
        {
            using (var db = new DBHotelSystem())
            {
                // Lấy các booking trùng thời gian
                var overlappingBookings = db.Bookings.Where(b => 
                    b.room_id == roomId && 
                    b.status != "Checked Out" && 
                    b.check_in < checkOut && 
                    b.check_out > checkIn).ToList();
                
                // Kiểm tra nếu có booking nào trùng thời gian
                foreach (var booking in overlappingBookings)
                {
                    // Loại trừ trường hợp checkout của booking cũ = checkin của booking mới
                    // Sử dụng phương pháp so sánh dựa trên ngày tháng cơ bản
                    DateTime bookingCheckoutDate = new DateTime(booking.check_out.Year, booking.check_out.Month, booking.check_out.Day, 0, 0, 0);
                    DateTime checkInDate = new DateTime(checkIn.Year, checkIn.Month, checkIn.Day, 0, 0, 0);
                    
                    if (bookingCheckoutDate != checkInDate)
                    {
                        // Có trùng lịch đặt phòng thực sự
                        return false;
                    }
                }
                
                // Không có trùng lịch hoặc chỉ có trùng kiểu "nối đuôi"
                return true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerForm op = new CustomerForm();
            op.LoadAvailableRooms(_checkInDate, _checkOutDate);
            op.Show();
        }
    }
}

