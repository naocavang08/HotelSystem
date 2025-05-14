using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.Model;

namespace HotelSystem.View.CustomerForm
{
    public partial class DelBookingRoom: Form
    {
        private DBHotelSystem db = new DBHotelSystem();
        private int? customerId;
        
        public DelBookingRoom()
        {
            InitializeComponent();
        }
        
        public DelBookingRoom(int customerID) : this()
        {
            this.customerId = customerID;
        }
        
        private void btnDelRoom_Click(object sender, EventArgs e)
        {
            if (dgvDelRoom.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dgvDelRoom.SelectedRows[0].Cells["booking_id"].Value);
                
                try
                {
                    // Find the booking to delete
                    var bookingToDelete = db.Bookings.Find(bookingId);
                    
                    if (bookingToDelete != null)
                    {
                        // Check if booking can be deleted (e.g., not already checked-in)
                        if (bookingToDelete.status == "booked")
                        {
                            // Confirm deletion
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
                                
                                MessageBox.Show("Hủy đặt phòng thành công!", "Thông báo", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Refresh the grid
                                LoadBookingRooms();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không thể hủy phòng đã check-in hoặc check-out!", 
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
                MessageBox.Show("Vui lòng chọn một phòng để hủy!", "Cảnh báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void DelBookingRoom_Load(object sender, EventArgs e)
        {
            // Set the selection mode to full row
            dgvDelRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDelRoom.MultiSelect = false;
            
            // Load booking rooms
            LoadBookingRooms();
        }
        
        private void LoadBookingRooms()
        {
            try
            {
                // Sử dụng cách tiếp cận đơn giản hơn với việc lọc trực tiếp
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
        
        // Thêm phương thức tìm kiếm theo mã đặt phòng hoặc số phòng
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDelRoom.DataSource is List<dynamic> sourceList)
                {
                    string searchText = txtSearch.Text.ToLower();
                    if (string.IsNullOrWhiteSpace(searchText))
                    {
                        // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ dữ liệu
                        LoadBookingRooms();
                        return;
                    }
                    
                    var filteredList = sourceList.Where(item => 
                        item.booking_id.ToString().Contains(searchText) || 
                        item.RoomNumber.ToString().ToLower().Contains(searchText) ||
                        item.RoomType.ToString().ToLower().Contains(searchText)
                    ).ToList();
                    
                    dgvDelRoom.DataSource = filteredList;
                    FormatDataGrid();
                }
                else
                {
                    // Nếu không thể tìm kiếm trên DataSource hiện tại, tải lại dữ liệu
                    LoadBookingRooms();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Phương thức khi nhấn phím Enter trên ô tìm kiếm
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn âm thanh beep
                txtSearch_TextChanged(sender, EventArgs.Empty);
            }
        }
        
        // Phương thức làm mới danh sách
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadBookingRooms();
        }
    }
} 