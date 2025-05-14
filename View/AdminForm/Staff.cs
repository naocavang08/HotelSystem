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

namespace HotelSystem.View.AdminForm
{
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadStaffData(); // Load initial data
        }

        private void SetupDataGridView()
        {
            // Clear existing columns
            dataGridView1.Columns.Clear();

            // Add columns to the DataGridView
            dataGridView1.Columns.Add("name", "Họ tên");
            dataGridView1.Columns.Add("phone", "Số điện thoại");
            dataGridView1.Columns.Add("cccd", "CCCD");
            dataGridView1.Columns.Add("gender", "Giới tính");
            dataGridView1.Columns.Add("position", "Chức vụ");
            dataGridView1.Columns.Add("salary", "Lương");

            // Set column properties for better display
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add handler for cell click event to populate text fields when a row is selected
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtName.Text = row.Cells["name"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtCCCD.Text = row.Cells["cccd"].Value.ToString();

                // Set gender radio buttons based on the gender value
                string gender = row.Cells["gender"].Value.ToString();
                if (gender == "Nam")
                {
                    radioButtonNam.Checked = true;
                    radioButtonNu.Checked = false;
                }
                else
                {
                    radioButtonNam.Checked = false;
                    radioButtonNu.Checked = true;
                }

                txbChucVu.Text = row.Cells["position"].Value.ToString();

                // Clean salary value - remove formatted separators for editing
                string salaryText = row.Cells["salary"].Value.ToString();
                txbLuong.Text = salaryText.Replace(",", "");
            }
        }

        // Method to load staff data into the DataGridView
        private void LoadStaffData()
        {
            // Clear existing rows
            dataGridView1.Rows.Clear();

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    // Get all employees from the database
                    var employees = dbContext.Employees.ToList();

                    // Add each employee to the DataGridView
                    foreach (var employee in employees)
                    {
                        string genderText = employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            employee.salary.ToString("N0")
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff data: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtPhone.Text = "";
            txtCCCD.Text = "";
            txbChucVu.Text = "";
            txbLuong.Text = "";
            // Reset gender selection
            radioButtonNam.Checked = true;
            radioButtonNu.Checked = false;
        }

        private bool ValidateInput()
        {
            // Perform validation on input fields
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập CCCD!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbChucVu.Text))
            {
                MessageBox.Show("Vui lòng nhập chức vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập lương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate that salary is a number
            decimal salary;
            if (!decimal.TryParse(txbLuong.Text.Replace(",", ""), out salary))
            {
                MessageBox.Show("Lương phải là một số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Validate gender
            if (!radioButtonNam.Checked && !radioButtonNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    // Get gender value from radio buttons
                    bool gender = radioButtonNam.Checked;

                    // Parse the salary (already validated in ValidateInput)
                    decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                    // First create a User account for the employee
                    var newUser = new User
                    {
                        username = txtPhone.Text, // Using phone as username
                        password = "defaultpassword", // Default password - should be changed by user
                        role = "staff",
                        status = "active",
                        date_register = DateTime.Now
                    };

                    // Add user to context
                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges(); // Save to get the ID

                    // Create new employee with reference to the User
                    var newEmployee = new Employee
                    {
                        name = txtName.Text,
                        phone = txtPhone.Text,
                        cccd = txtCCCD.Text,
                        gender = gender,
                        position = txbChucVu.Text,
                        salary = salary,
                        id = newUser.id // Link to the User record
                    };

                    // Add to database
                    dbContext.Employees.Add(newEmployee);
                    dbContext.SaveChanges();

                    // Refresh data
                    LoadStaffData();

                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear input fields after adding
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                // Show more detailed error information
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\n\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show($"Lỗi: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                // Get the selected employee's CCCD from the row
                string selectedCccd = dataGridView1.SelectedRows[0].Cells["cccd"].Value.ToString();

                using (var dbContext = new DBHotelSystem())
                {
                    // Find the employee by CCCD
                    var employee = dbContext.Employees.FirstOrDefault(emp => emp.cccd == selectedCccd);

                    if (employee != null)
                    {
                        // Get gender value from radio buttons
                        bool gender = radioButtonNam.Checked;

                        // Parse the salary (already validated in ValidateInput)
                        decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                        // Update employee data
                        employee.name = txtName.Text;
                        employee.phone = txtPhone.Text;
                        employee.cccd = txtCCCD.Text;
                        employee.gender = gender;
                        employee.position = txbChucVu.Text;
                        employee.salary = salary;

                        // Also update the associated user's phone (since we're using phone as username)
                        var user = dbContext.Users.Find(employee.id);
                        if (user != null)
                        {
                            user.username = txtPhone.Text;
                        }

                        // Save changes to database
                        dbContext.SaveChanges();

                        // Refresh data
                        LoadStaffData();

                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear input fields after updating
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show more detailed error information
                string errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\n\nInner Exception: " + ex.InnerException.Message;
                }
                MessageBox.Show($"Lỗi: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Get the selected employee's CCCD from the row
                    string selectedCccd = dataGridView1.SelectedRows[0].Cells["cccd"].Value.ToString();

                    using (var dbContext = new DBHotelSystem())
                    {
                        // Find the employee by CCCD
                        var employee = dbContext.Employees.FirstOrDefault(emp => emp.cccd == selectedCccd);

                        if (employee != null)
                        {
                            // Delete employee from database
                            dbContext.Employees.Remove(employee);
                            dbContext.SaveChanges();

                            // Refresh data
                            LoadStaffData();

                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear input fields after deleting
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Show more detailed error information
                    string errorMessage = ex.Message;
                    if (ex.InnerException != null)
                    {
                        errorMessage += "\n\nInner Exception: " + ex.InnerException.Message;
                    }
                    MessageBox.Show($"Lỗi: {errorMessage}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchName = txtName.Text.ToLower();
            string searchPhone = txtPhone.Text;
            string searchCCCD = txtCCCD.Text;
            string searchPosition = txbChucVu.Text.ToLower();

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    // Start with all employees
                    var query = dbContext.Employees.AsQueryable();

                    // Apply filters if provided
                    if (!string.IsNullOrWhiteSpace(searchName))
                        query = query.Where(emp => emp.name.ToLower().Contains(searchName));

                    if (!string.IsNullOrWhiteSpace(searchPhone))
                        query = query.Where(emp => emp.phone.Contains(searchPhone));

                    if (!string.IsNullOrWhiteSpace(searchCCCD))
                        query = query.Where(emp => emp.cccd.Contains(searchCCCD));

                    if (!string.IsNullOrWhiteSpace(searchPosition))
                        query = query.Where(emp => emp.position.ToLower().Contains(searchPosition));

                    // Execute query
                    var filteredEmployees = query.ToList();

                    // Display results
                    dataGridView1.Rows.Clear();
                    foreach (var employee in filteredEmployees)
                    {
                        string genderText = employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            employee.salary.ToString("N0") + " VND"
                        );
                    }

                    if (filteredEmployees.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Load all staff data again
                        LoadStaffData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadStaffData();
        }
    }
}
