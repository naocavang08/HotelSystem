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
using HotelSystem.View.StaffForm;

namespace HotelSystem.View.StaffForm
{
    public partial class CustomerList : Form
    {
        private BLL_TTKH bllCustomer = new BLL_TTKH();
        private List<DTO_Customer> customers = new List<DTO_Customer>();

        public CustomerList()
        {
            InitializeComponent();
        }

        private void LoadCustomerData()
        {
            customers = bllCustomer.GetAllCustomer();
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customers;
            dgvCustomers.Refresh();
        }

        private void ConfigureDataGridView()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();

            // Add columns
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "CustomerId";
            idColumn.HeaderText = "Mã khách hàng";
            idColumn.Width = 80;
            dgvCustomers.Columns.Add(idColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Name";
            nameColumn.HeaderText = "Họ tên";
            nameColumn.Width = 150;
            dgvCustomers.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn phoneColumn = new DataGridViewTextBoxColumn();
            phoneColumn.DataPropertyName = "Phone";
            phoneColumn.HeaderText = "Số điện thoại";
            phoneColumn.Width = 120;
            dgvCustomers.Columns.Add(phoneColumn);

            DataGridViewTextBoxColumn cccdColumn = new DataGridViewTextBoxColumn();
            cccdColumn.DataPropertyName = "CCCD";
            cccdColumn.HeaderText = "CCCD";
            cccdColumn.Width = 120;
            dgvCustomers.Columns.Add(cccdColumn);

            DataGridViewTextBoxColumn genderColumn = new DataGridViewTextBoxColumn();
            genderColumn.HeaderText = "Giới tính";
            genderColumn.Width = 80;
            genderColumn.DataPropertyName = "Gender";
            // Handle the formatting of the gender column
            genderColumn.DefaultCellStyle.Format = ""; // Clear the format
            genderColumn.DefaultCellStyle.NullValue = "";
            genderColumn.DefaultCellStyle.FormatProvider = null;
            dgvCustomers.Columns.Add(genderColumn);

            // Add a button column for CustomerDetail
            DataGridViewButtonColumn viewDetail = new DataGridViewButtonColumn();
            viewDetail.HeaderText = "Xem chi tiết";
            viewDetail.Text = "Xem";
            viewDetail.UseColumnTextForButtonValue = true;
            viewDetail.Width = 100;
            dgvCustomers.Columns.Add(viewDetail);

            // Add event handlers
            dgvCustomers.CellFormatting += DgvCustomers_CellFormatting;
            dgvCustomers.CellContentClick += DgvCustomers_CellContentClick;
        }

        private void DgvCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Format the gender column
            if (e.ColumnIndex == 4 && e.Value != null) // Gender column
            {
                if (e.Value is bool?)
                {
                    bool? gender = (bool?)e.Value;
                    if (gender.HasValue)
                    {
                        e.Value = gender.Value ? "Nam" : "Nữ";
                    }
                    else
                    {
                        e.Value = "";
                    }
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvCustomers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void DgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is the "View Bookings" button
            if (e.ColumnIndex == 5 && e.RowIndex >= 0 && e.RowIndex < customers.Count)
            {
                int customerId = customers[e.RowIndex].CustomerId;
                
                // Open the CustomerDetail form to show customer details
                CustomerDetail customerDetail = new CustomerDetail(customerId);
                DialogResult result = customerDetail.ShowDialog();
                
                // Sau khi form đóng, nếu DialogResult là OK thì load lại dữ liệu
                if (result == DialogResult.OK)
                {
                    LoadCustomerData();
                }
            }
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadCustomerData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Open the CustomerDetail form to add a new customer
            BLL_TTKH bllTTKH = new BLL_TTKH();
            int nextCustomerId = bllTTKH.GetNextCustomerId();
            CustomerDetail op = new CustomerDetail(nextCustomerId);
            DialogResult result = op.ShowDialog();
            
            // Reload the data after adding if the operation was successful
            if (result == DialogResult.OK)
            {
                LoadCustomerData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null && dgvCustomers.CurrentRow.Index >= 0 
                && dgvCustomers.CurrentRow.Index < customers.Count)
            {
                int customerId = customers[dgvCustomers.CurrentRow.Index].CustomerId;
                
                // Confirm deletion
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", 
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var db = new DBHotelSystem())
                        {
                            // Check if customer has any bookings or invoices
                            bool hasBookings = db.Bookings.Any(b => b.customer_id == customerId);
                            bool hasBookingServices = db.BookingServices.Any(bs => bs.customer_id == customerId);
                            
                            if (hasBookings || hasBookingServices)
                            {
                                MessageBox.Show("Không thể xóa khách hàng này vì có dữ liệu đặt phòng hoặc dịch vụ liên quan.", 
                                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            
                            // Delete customer
                            var customer = db.Customers.FirstOrDefault(c => c.customer_id == customerId);
                            if (customer != null)
                            {
                                db.Customers.Remove(customer);
                                db.SaveChanges();
                                
                                MessageBox.Show("Xóa khách hàng thành công!", "Thành công", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Reload data
                                LoadCustomerData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}", "Lỗi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Chưa chọn", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var allCustomers = bllCustomer.GetAllCustomer();
                customers = allCustomers.Where(c => 
                    c.Name.ToLower().Contains(searchTerm) || 
                    c.Phone.Contains(searchTerm) || 
                    c.CCCD.Contains(searchTerm) ||
                    c.CustomerId.ToString().Contains(searchTerm)
                ).ToList();
                
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = customers;
                dgvCustomers.Refresh();
            }
            else
            {
                LoadCustomerData();
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCustomerData();
        }

        private void CustomerList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StaffForm op = new StaffForm();
            op.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Hide();
            StaffForm staff = new StaffForm();
            staff.Show();
        }
    }
}
