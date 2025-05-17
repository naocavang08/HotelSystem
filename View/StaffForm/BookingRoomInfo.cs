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
using HotelSystem.Model;

namespace HotelSystem.View.StaffForm
{
    public partial class BookingRoomInfo: Form
    {
        private DBHotelSystem db = new DBHotelSystem();
        private int? customerId;
        
        public BookingRoomInfo()
        {
            InitializeComponent();
        }
        
        private void btnDelRoom_Click(object sender, EventArgs e)
        {
            if (dgvDelRoom.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dgvDelRoom.SelectedRows[0].Cells["booking_id"].Value);
                
                try
                {
                    var bookingToDelete = db.Bookings.Find(bookingId);
                    
                    if (bookingToDelete != null)
                    {
                    Console.WriteLine($"Trạng thái booking hiện tại: '{bookingToDelete.status}'");
                        
                        DialogResult result = MessageBox.Show(
                            "Bạn có chắc chắn muốn hủy đặt phòng này không?",
                            "Xác nhận hủy đặt phòng",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                                
                        if (result == DialogResult.Yes)
                        {
                            // Remove the booking
                            db.Bookings.Remove(bookingToDelete);
                            db.SaveChanges();

                            // Update the room status to available
                            var bllRoom = new BLL_Room();
                            bllRoom.UpdateRoomStatus(bookingToDelete.room_id, "Available");

                            MessageBox.Show("Hủy đặt phòng thành công!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                
                            LoadBookingRooms();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để hủy!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void BookingRoomInfo_Load(object sender, EventArgs e)
        {
            // Set the selection mode to full row
            dgvDelRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDelRoom.MultiSelect = false;
            
            // Load booking rooms
            LoadBookingRooms();
            
            // Thêm nút thanh toán nếu chưa tồn tại
            if (!Controls.ContainsKey("btnPayment"))
            {
                Button btnPayment = new Button();
                btnPayment.Name = "btnPayment";
                btnPayment.Text = "Thanh toán";
                btnPayment.Location = new Point(btnEditRoom.Right + 20, btnEditRoom.Top);
                btnPayment.Size = btnEditRoom.Size;
                btnPayment.BackColor = Color.FromArgb(0, 122, 204);
                btnPayment.ForeColor = Color.White;
                btnPayment.FlatStyle = FlatStyle.Flat;
                btnPayment.Click += btnPayment_Click;
                this.Controls.Add(btnPayment);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem form cha có thể là CustomerForm
            if (this.Owner is CustomerForm customerForm)
            {
                this.Close();
            }
            else
            {
                try
                {
                    // Nếu không phải được gọi từ CustomerForm, tạo mới và hiển thị
                    CustomerForm newCustomerForm = new CustomerForm();
                    newCustomerForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi trở về trang chính: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditRoom_Click(object sender, EventArgs e)
        {
            if (dgvDelRoom.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dgvDelRoom.SelectedRows[0].Cells["booking_id"].Value);
                try
                {
                    // Find the booking to edit
                    var bookingToEdit = db.Bookings.Find(bookingId);
                    
                    if (bookingToEdit != null)
                    {
                        // Check if booking can be edited (e.g., not already checked-in)
                        if (bookingToEdit.status == "Booked")
                        {
                            // Open edit form
                            BookingRoom editBooking = new BookingRoom(customerId, bookingId);
                            editBooking.ShowDialog();
                            
                            // Refresh the grid after returning
                            LoadBookingRooms();
                        }
                        else
                        {
                            MessageBox.Show("Không thể chỉnh sửa phòng đã check-in hoặc check-out!", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phòng để chỉnh sửa!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (dgvDelRoom.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phòng để thanh toán!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingId = Convert.ToInt32(dgvDelRoom.SelectedRows[0].Cells["booking_id"].Value);
            try
            {
                // Tìm booking để thanh toán
                var bookingToPayment = db.Bookings.Find(bookingId);
                if (bookingToPayment != null)
                {
                    // Kiểm tra xem booking có thể thanh toán không
                    if (bookingToPayment.status != "Booked")
                    {
                        MessageBox.Show("Chỉ có thể thanh toán phòng có trạng thái là 'Đã đặt'!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Tạo DTO_BookingRoom từ booking
                    var bllBookingRoom = new BLL_BookingRoom();
                    var bookingRooms = bllBookingRoom.GetBookingRoomsByCustomerId(customerId.Value);
                    var selectedBooking = bookingRooms.FirstOrDefault(br => br.BookingId == bookingId);
                    
                    if (selectedBooking == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin đặt phòng!", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    // Lấy danh sách booking có trạng thái "Booked"
                    var bookingsToPayment = new List<HotelSystem.DTO.DTO_BookingRoom> { selectedBooking };
                    
                    // Lấy dịch vụ đã đặt (chỉ lấy những dịch vụ có trạng thái "Booked")
                    var bllBookingService = new BLL_BookingService();
                    var allBookingServices = bllBookingService.GetBookingServicesByCustomerId(customerId.Value);
                    var bookingServices = allBookingServices.Where(bs => bs.Status == "Booked").ToList();
                    
                    // Mở form Invoice để thanh toán
                    Invoice invoiceForm = new Invoice(bookingsToPayment, bookingServices);
                    invoiceForm.ShowDialog();
                    
                    // Refresh lại danh sách sau khi thanh toán
                    LoadBookingRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadBookingRooms()
        {
            try
            {
                // Lấy tất cả booking (bao gồm cả "Checked Out")
                var bookings = customerId.HasValue
                    ? db.Bookings.Where(b => b.customer_id == customerId.Value).ToList()
                    : db.Bookings.ToList();

                var results = (from b in bookings
                              join r in db.Rooms on b.room_id equals r.room_id
                              join rt in db.RoomTypes on r.roomtype_id equals rt.roomtype_id
                              select new
                              {
                                  b.booking_id,
                                  RoomNumber = r.room_number,
                                  RoomType = rt.room_type,
                                  CheckinDate = b.check_in,
                                  CheckoutDate = b.check_out,
                                  b.status,
                                  TotalPrice = b.total_price
                              }).ToList();

                dgvDelRoom.DataSource = results;
                
                // Định dạng DataGridView
                FormatDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormatDataGrid()
        {
            dgvDelRoom.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            
            // Rename column headers for better display
            if (dgvDelRoom.Columns["booking_id"] != null)
                dgvDelRoom.Columns["booking_id"].HeaderText = "Mã đặt phòng";
                
            if (dgvDelRoom.Columns["RoomNumber"] != null)
                dgvDelRoom.Columns["RoomNumber"].HeaderText = "Số phòng";
                
            if (dgvDelRoom.Columns["RoomType"] != null)
                dgvDelRoom.Columns["RoomType"].HeaderText = "Loại phòng";
                
            if (dgvDelRoom.Columns["CheckinDate"] != null)
            {
                dgvDelRoom.Columns["CheckinDate"].HeaderText = "Ngày nhận phòng";
                dgvDelRoom.Columns["CheckinDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
                
            if (dgvDelRoom.Columns["CheckoutDate"] != null)
            {
                dgvDelRoom.Columns["CheckoutDate"].HeaderText = "Ngày trả phòng";
                dgvDelRoom.Columns["CheckoutDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
                
            if (dgvDelRoom.Columns["status"] != null)
            {
                dgvDelRoom.Columns["status"].HeaderText = "Trạng thái";
                
                // Tùy chỉnh hiển thị trạng thái
                foreach (DataGridViewRow row in dgvDelRoom.Rows)
                {
                    if (row.Cells["status"].Value != null)
                    {
                        string status = row.Cells["status"].Value.ToString();
                        switch (status.ToLower())
                        {
                            case "booked":
                                row.Cells["status"].Value = "Đã đặt";
                                break;
                            case "checked in":
                                row.Cells["status"].Value = "Đã nhận phòng";
                                break;
                            case "checked out":
                                row.Cells["status"].Value = "Đã trả phòng";
                                break;
                        }
                    }
                }
            }
                
            if (dgvDelRoom.Columns["TotalPrice"] != null)
            {
                dgvDelRoom.Columns["TotalPrice"].HeaderText = "Tổng tiền";
                dgvDelRoom.Columns["TotalPrice"].DefaultCellStyle.Format = "N0";
            }
            
            // Set the grid to read-only
            dgvDelRoom.ReadOnly = true;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookingRooms();
        }


        private void dgvDelRoom_SelectionChanged(object sender, EventArgs e)
        {
        }
    }
}
