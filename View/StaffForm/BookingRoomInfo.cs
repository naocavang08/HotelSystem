using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.BLL;
using HotelSystem.DTO;
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
        
        public BookingRoomInfo(int? customerId)
        {
            InitializeComponent();
            this.customerId = customerId;
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
                            // Xóa đặt phòng
                            db.Bookings.Remove(bookingToDelete);
                            db.SaveChanges();

                            // Cập nhật trạng thái phòng thành có sẵn
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
            // Đặt chế độ chọn hàng đầy đủ
            dgvDelRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDelRoom.MultiSelect = false;
            
            // Tải danh sách đặt phòng
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
                    var bookingToEdit = db.Bookings.Find(bookingId);
                    
                    if (bookingToEdit != null)
                    {
                        if (bookingToEdit.status == "Booked")
                        {
                            if (!customerId.HasValue)
                            {
                                customerId = bookingToEdit.customer_id;
                            }
                            
                            BookingRoom editBooking = new BookingRoom(customerId, bookingId);
                            editBooking.ShowDialog();
                            
                            if (editBooking.DialogResult == DialogResult.OK)
                            {
                                LoadBookingRooms();
                            }
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

                    // Lấy customerId từ booking nếu chưa có
                    if (!customerId.HasValue)
                    {
                        customerId = bookingToPayment.customer_id;
                    }

                    // Tạo DTO_BookingRoom từ booking
                    List<DTO.DTO_BookingRoom> bookingRooms = new List<DTO.DTO_BookingRoom>();
                    var selectedBooking = db.Bookings
                        .Include("Room.RoomType")
                        .Include("Customer")
                        .FirstOrDefault(b => b.booking_id == bookingId);

                    if (selectedBooking != null)
                    {
                        bookingRooms.Add(new DTO.DTO_BookingRoom
                        {
                            BookingId = selectedBooking.booking_id,
                            RoomId = selectedBooking.room_id,
                            CustomerId = selectedBooking.customer_id,
                            CheckIn = selectedBooking.check_in,
                            CheckOut = selectedBooking.check_out,
                            Status = selectedBooking.status,
                            TotalPrice = selectedBooking.total_price
                        });

                        // Lấy danh sách booking có trạng thái "Booked"
                        var otherBookings = db.Bookings
                            .Where(b => b.customer_id == customerId && b.status == "Booked" && b.booking_id != bookingId)
                            .ToList();

                        // Lấy dịch vụ đã đặt (chỉ lấy những dịch vụ có trạng thái "Booked")
                        var bookingServices = db.BookingServices
                            .Where(bs => bs.customer_id == customerId && bs.status == "Booked")
                            .ToList();

                        // Mở form Invoice để thanh toán
                        Invoice invoiceForm = new Invoice(bookingRooms, null, this);
                        invoiceForm.ShowDialog();

                        // Refresh lại danh sách sau khi thanh toán
                        LoadBookingRooms();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBookingRooms()
        {
            // Tạo đối tượng context mới để đảm bảo lấy dữ liệu mới nhất
            using (var db = new DBHotelSystem())
            {
                var bookings = new List<Model.BookingRoom>();
                
                if (customerId.HasValue)
                {
                    bookings = db.Bookings
                        .Include("Room.RoomType")
                        .Include("Customer")
                        .Where(b => b.customer_id == customerId.Value)
                        .OrderByDescending(b => b.booking_id)
                        .ToList();
                }
                else
                {
                    bookings = db.Bookings
                        .Include("Room.RoomType")
                        .Include("Customer")
                        .OrderByDescending(b => b.booking_id)
                        .ToList();
                }
                
                dgvDelRoom.DataSource = null;
                
                if (bookings.Count > 0)
                {
                    var bookingData = bookings.Select(b => new
                    {
                        booking_id = b.booking_id,
                        customer_name = b.Customer?.name ?? "N/A",
                        room_number = b.Room?.room_number ?? "N/A",
                        room_type = b.Room?.RoomType?.room_type ?? "N/A",
                        check_in = b.check_in,
                        check_out = b.check_out,
                        total_price = b.total_price,
                        status = b.status
                    }).ToList();
                    
                    dgvDelRoom.DataSource = bookingData;
                    FormatDataGrid();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đặt phòng nào!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void FormatDataGrid()
        {
            // Rename column headers for better display
            if (dgvDelRoom.Columns.Count > 0)
            {
                dgvDelRoom.Columns["booking_id"].HeaderText = "Mã đặt phòng";
                dgvDelRoom.Columns["customer_name"].HeaderText = "Tên khách hàng";
                dgvDelRoom.Columns["room_number"].HeaderText = "Số phòng";
                dgvDelRoom.Columns["room_type"].HeaderText = "Loại phòng";
                dgvDelRoom.Columns["check_in"].HeaderText = "Ngày nhận phòng";
                dgvDelRoom.Columns["check_out"].HeaderText = "Ngày trả phòng";
                dgvDelRoom.Columns["total_price"].HeaderText = "Tổng tiền";
                dgvDelRoom.Columns["status"].HeaderText = "Trạng thái";
                
                // Tùy chỉnh hiển thị trạng thái
                dgvDelRoom.CellFormatting += (s, e) => 
                {
                    if (e.ColumnIndex == dgvDelRoom.Columns["status"].Index && e.Value != null)
                    {
                        string status = e.Value.ToString();
                        switch (status)
                        {
                            case "Booked":
                                e.Value = "Đã đặt";
                                break;
                            case "Checked In":
                                e.Value = "Đã nhận phòng";
                                break;
                            case "Checked Out":
                                e.Value = "Đã trả phòng";
                                break;
                            case "Cancelled":
                                e.Value = "Đã hủy";
                                break;
                        }
                        e.FormattingApplied = true;
                    }
                };
                
                // Set the grid to read-only
                dgvDelRoom.ReadOnly = true;
                dgvDelRoom.AllowUserToAddRows = false;
                dgvDelRoom.AllowUserToDeleteRows = false;
                dgvDelRoom.AllowUserToOrderColumns = true;
                
                // Auto-size columns
                dgvDelRoom.AutoResizeColumns();
                dgvDelRoom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookingRooms();
        }

        private void dgvDelRoom_SelectionChanged(object sender, EventArgs e)
        {
            // Cập nhật trạng thái các nút dựa trên hàng được chọn
            if (dgvDelRoom.SelectedRows.Count > 0)
            {
                string status = dgvDelRoom.SelectedRows[0].Cells["status"].Value.ToString();
                btnEditRoom.Enabled = status == "Booked";
                btnDelRoom.Enabled = status == "Booked";
            }
        }
    }
}
