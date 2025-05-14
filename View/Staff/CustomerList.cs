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

namespace HotelSystem.View.Staff
{
    
    public partial class CustomerList: Form
    {
        private List<DTO_Customer> _customerList;
        private readonly BLL_TTKH _bllTTKH = new BLL_TTKH();
        public CustomerList()
        {
            InitializeComponent();
            ConfigureDataGridView();
            
            dgvCustomers.DataError += dgvCustomers_DataError;
            LoadCustomerData();

        }

        private void dgvCustomers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ngăn không cho hiện dialog lỗi mặc định
            e.ThrowException = false;
        }
        private void LoadCustomerData()
        {
            try
            {
                _customerList = _bllTTKH.GetAllCustomers();
                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = _customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigureDataGridView()
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.EditMode = DataGridViewEditMode.EditOnEnter;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Columns.Clear();

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerId",
                HeaderText = "Mã KH",
                Name = "CustomerId",
                ReadOnly = true
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Họ tên",
                Name = "Name"
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "SĐT",
                Name = "Phone"
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CCCD",
                HeaderText = "CCCD",
                Name = "CCCD"
            });
            dgvCustomers.Columns.Add(new DataGridViewComboBoxColumn
            {
                DataPropertyName = "GenderDisplay", // Binding với property string
                HeaderText = "Giới tính",
                Name = "Gender",
                DataSource = new List<string> { "", "Nam", "Nữ" },
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                FlatStyle = FlatStyle.Flat
            });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserId",
                HeaderText = "User ID",
                Name = "UserId",
               
            });
            var saveButton = new DataGridViewButtonColumn
            {
                Text = "Lưu",
                UseColumnTextForButtonValue = true,
                HeaderText = "Thao tác",
                Name = "SaveButton"
            };
            dgvCustomers.Columns.Add(saveButton);
        }
        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCustomers.Columns["SaveButton"].Index && e.RowIndex >= 0)
            {
                Console.WriteLine($"Debug - Save button clicked for row {e.RowIndex}"); // Debug log
                SaveCustomer(e.RowIndex);
            }
        }

        private void SaveCustomer(int rowIndex)
        {
            try
            {
                Console.WriteLine($"Debug - SaveCustomer called for row {rowIndex}"); // Debug log
                var row = dgvCustomers.Rows[rowIndex];
                var customer = _customerList[rowIndex];

                if (customer.UserId <= 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản người dùng! Vui lòng tạo tài khoản trước.",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(row.Cells["Name"].Value?.ToString()))
                {
                    MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string phone = row.Cells["Phone"].Value?.ToString().Trim();
                if (string.IsNullOrWhiteSpace(phone) || !phone.All(char.IsDigit) || phone.Length != 10)
                {
                    MessageBox.Show("Số điện thoại phải gồm 10 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string cccd = row.Cells["CCCD"].Value?.ToString().Trim();
                if (string.IsNullOrWhiteSpace(cccd) || !cccd.All(char.IsDigit) || cccd.Length != 12)
                {
                    MessageBox.Show("CCCD phải gồm 12 chữ số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                customer.Name = row.Cells["Name"].Value?.ToString().Trim();
                customer.Phone = phone;
                customer.CCCD = cccd;

                var genderCell = row.Cells["Gender"];
                Console.WriteLine($"Debug - Gender cell value: {genderCell.Value}"); // Debug log
                if (genderCell.Value != null)
                {
                    if (genderCell.Value is KeyValuePair<string, bool?> genderPair)
                    {
                        customer.Gender = genderPair.Value;
                    }
                    else
                    {
                        string genderStr = genderCell.Value.ToString();
                        Console.WriteLine($"Debug - Gender string found: {genderStr}"); // Debug log
                        customer.Gender = genderStr == "Nam" ? true : genderStr == "Nữ" ? false : (bool?)null;
                    }
                }
                else
                {
                    customer.Gender = null;
                }
                Console.WriteLine($"Debug - Final gender value to save: {customer.Gender}"); // Debug log

                bool result;
                if (customer.CustomerId == 0)
                {
                    result = _bllTTKH.AddCustomer(customer);
                    if (result)
                    {
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    _bllTTKH.UpdateCustomer(customer);
                    result = true;
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (result)
                {
                    LoadCustomerData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Debug - Error in SaveCustomer: {ex}"); // Debug log
            }
        }
        private void CustomerList_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Hiện thông báo yêu cầu tạo tài khoản
                DialogResult result = MessageBox.Show(
                    "Hãy tạo tài khoản cho khách hàng mới",
                    "Thông báo",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    // Mở form đăng ký
                    RegistrationForm registrationForm = new RegistrationForm();
                    registrationForm.FormClosed += (s, args) =>
                    {
                        // Khi form đăng ký đóng và đăng ký thành công
                        if (registrationForm.RegistrationSuccessful)
                        {
                            // Lấy user_id của tài khoản vừa tạo
                            int newUserId = registrationForm.NewUserId;
                            Console.WriteLine($"Debug - New UserId: {newUserId}"); // Debug line

                            // Tạo customer mới với user_id
                            var newCustomer = new DTO_Customer
                            {
                                CustomerId = 0,
                                Name = "",
                                Phone = "",
                                CCCD = "",
                                Gender = null,
                                UserId = newUserId  // Quan trọng: Đảm bảo UserId được gán
                            };

                            _customerList.Add(newCustomer);
                            dgvCustomers.DataSource = null;
                            dgvCustomers.DataSource = _customerList;

                            // Select dòng mới thêm và cho phép edit
                            int newRowIndex = dgvCustomers.Rows.Count - 1;
                            dgvCustomers.CurrentCell = dgvCustomers.Rows[newRowIndex].Cells["Name"];
                            dgvCustomers.BeginEdit(true);
                        }
                    };
                    registrationForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khách hàng mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.");
                return;
            }
            var selectedId = (int)dgvCustomers.SelectedRows[0].Cells["CustomerId"].Value;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _bllTTKH.DeleteCustomer(selectedId);
                LoadCustomerData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            var keyword = txtSearch.Text.Trim();
            _customerList = string.IsNullOrEmpty(keyword)
                ? _bllTTKH.GetAllCustomers()
                : _bllTTKH.SearchCustomers(keyword);
            dgvCustomers.DataSource = _customerList;
            if (_customerList == null || _customerList.Count == 0)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào phù hợp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_ReloadClick(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerData();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lại dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
