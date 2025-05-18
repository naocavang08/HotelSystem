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
using HotelSystem.Model;

namespace HotelSystem.View.StaffForm
{
    public partial class BookingServiceInfo : Form
    {
        private BLL_BookingService bllBookingService = new BLL_BookingService();
        private BLL_TTKH bllCustomer = new BLL_TTKH();
        private int _customerId;
        private List<DTO_BookingService> _bookingServices;

        public BookingServiceInfo()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // Configure DataGridView
            dgvBookedServices.AutoGenerateColumns = false;
            
            // Add columns
            if (dgvBookedServices.Columns.Count == 0)
            {
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Mã đặt dịch vụ",
                    DataPropertyName = "Booking_service_id",
                    Name = "booking_service_id"
                });
                
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tên dịch vụ",
                    Name = "service_name"
                });
                
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Số lượng",
                    DataPropertyName = "Quantity",
                    Name = "quantity"
                });
                
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Ngày sử dụng",
                    DataPropertyName = "Service_date",
                    Name = "service_date"
                });
                
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Tổng tiền",
                    DataPropertyName = "TotalPrice",
                    Name = "total_price"
                });
                
                dgvBookedServices.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Trạng thái",
                    DataPropertyName = "Status",
                    Name = "status"
                });
            }
        }

        private void LoadBookedServices(int customerId)
        {
            try
            {
                _bookingServices = bllBookingService.GetBookingServicesByCustomerId(customerId);
                
                dgvBookedServices.Rows.Clear();
                
                if (_bookingServices == null || _bookingServices.Count == 0)
                {
                    lblNoServices.Visible = true;
                    dgvBookedServices.Visible = false;
                    return;
                }
                
                lblNoServices.Visible = false;
                
                using (var db = new DBHotelSystem())
                {
                    foreach (var bookingService in _bookingServices)
                    {
                        // Lấy tên dịch vụ từ bảng đặt dịch vụ
                        var service = db.Services.FirstOrDefault(s => s.service_id == bookingService.Service_id);
                        string serviceName = service?.name ?? "Unknown";
                        
                        // Add row
                        dgvBookedServices.Rows.Add(
                            bookingService.Booking_service_id,
                            serviceName,
                            bookingService.Quantity,
                            bookingService.Service_date.ToShortDateString(),
                            string.Format("{0:N0} VND", bookingService.TotalPrice),
                            bookingService.Status
                        );
                    }
                }
                
                dgvBookedServices.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dịch vụ đã đặt: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CustomerForm op = new CustomerForm();
            op.Show();
            this.Close();
        }

        private void BookingServiceInfo_Load(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()) || string.IsNullOrEmpty(txtCCCD.Text.Trim()) || string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = bllCustomer.GetCustomerByNameAndCCCD(txtName.Text.Trim(), txtCCCD.Text.Trim());
            if (customer != null)
            {
                LoadBookedServices(customer.CustomerId);
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBookedServices.SelectedRows.Count == 0 && dgvBookedServices.SelectedCells.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần hủy!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int rowIndex;
                if (dgvBookedServices.SelectedRows.Count > 0)
                {
                    rowIndex = dgvBookedServices.SelectedRows[0].Index;
                }
                else
                {
                    rowIndex = dgvBookedServices.SelectedCells[0].RowIndex;
                }

                int bookingServiceId = Convert.ToInt32(dgvBookedServices.Rows[rowIndex].Cells["booking_service_id"].Value);

                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn hủy dịch vụ này?", 
                    "Xác nhận hủy", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bllBookingService.DeleteBookingService(bookingServiceId);

                    MessageBox.Show("Đã hủy dịch vụ thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (txtName.Text.Trim() != "")
                    {
                        var customer = bllCustomer.GetCustomerByNameAndCCCD(txtName.Text.Trim(), txtCCCD.Text.Trim());
                        if (customer != null)
                        {
                            LoadBookedServices(customer.CustomerId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hủy dịch vụ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
