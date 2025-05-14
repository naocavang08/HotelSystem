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
            //name: lấy từ bảng Employee
            dataGridView1.Columns.Add("name", "Họ tên");
            //phone: lấy từ bảng Employee
            dataGridView1.Columns.Add("phone", "Số điện thoại");
            //cccd: lấy từ bảng Employee
            dataGridView1.Columns.Add("cccd", "CCCD");
            //gender: lấy từ bảng Employee
            dataGridView1.Columns.Add("gender", "Giới tính");
            //position: lấy từ bảng Employee
            dataGridView1.Columns.Add("position", "Chức vụ");
            //shift_date: lấy từ bảng WorkSchedule
            dataGridView1.Columns.Add("shift_date", "Lịch làm việc");
            //shift_time: lấy từ bảng WorkSchedule
            dataGridView1.Columns.Add("shift_time", "Ca làm việc");
            //salary: lấy từ bảng Employee
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

                // Set work schedule information
                txbLichLamViec.Text = row.Cells["shift_date"].Value.ToString();
                txbCaLamViec.Text = row.Cells["shift_time"].Value.ToString();

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
                    // Get all employees from the database with their work schedules
                    var employees = dbContext.Employees.ToList();

                    // Add each employee to the DataGridView
                    foreach (var employee in employees)
                    {
                        string genderText = employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        // Get the most recent work schedule for this employee (if any)
                        var workSchedule = dbContext.WorkSchedules
                            .Where(ws => ws.employee_id == employee.employee_id)
                            .OrderByDescending(ws => ws.schedule_id)
                            .FirstOrDefault();

                        string shiftDate = workSchedule != null ? workSchedule.shift_date : "";
                        string shiftTime = workSchedule != null ? workSchedule.shift_time : "";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            shiftDate,
                            shiftTime,
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
            txbLichLamViec.Text = "";
            txbCaLamViec.Text = "";
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

            // Lịch làm việc and Ca làm việc are optional, but if one is provided, the other should be too
            if (!string.IsNullOrWhiteSpace(txbLichLamViec.Text) && string.IsNullOrWhiteSpace(txbCaLamViec.Text))
            {
                MessageBox.Show("Vui lòng nhập ca làm việc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbLichLamViec.Text) && !string.IsNullOrWhiteSpace(txbCaLamViec.Text))
            {
                MessageBox.Show("Vui lòng nhập lịch làm việc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số điện thoại phải là chuỗi số và đúng 10 ký tự
            if (txtPhone.Text.Length != 10 || !txtPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải gồm 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra CCCD chỉ được chứa số và đúng 12 ký tự
            if (!txtCCCD.Text.All(char.IsDigit) || txtCCCD.Text.Length != 12)
            {
                MessageBox.Show("CCCD chỉ được chứa các ký tự số và dài 12 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    // Kiểm tra xem nhân viên đã tồn tại chưa
                    bool isExists = dbContext.Employees.Any(emp => emp.phone == txtPhone.Text || emp.cccd == txtCCCD.Text);

                    if (isExists)
                    {
                        MessageBox.Show("Nhân viên với số điện thoại hoặc CCCD này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

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

                    // Create work schedule if the data is provided
                    if (!string.IsNullOrWhiteSpace(txbLichLamViec.Text) &&
                        !string.IsNullOrWhiteSpace(txbCaLamViec.Text))
                    {
                        var workSchedule = new WorkSchedule
                        {
                            employee_id = newEmployee.employee_id,
                            shift_date = txbLichLamViec.Text,
                            shift_time = txbCaLamViec.Text
                        };

                        dbContext.WorkSchedules.Add(workSchedule);
                        dbContext.SaveChanges();
                    }

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

            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số điện thoại phải là chuỗi số và đúng 10 ký tự
            if (txtPhone.Text.Length != 10 || !txtPhone.Text.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải gồm 10 chữ số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra CCCD chỉ được chứa số và đúng 12 ký tự
            if (!txtCCCD.Text.All(char.IsDigit) || txtCCCD.Text.Length != 12)
            {
                MessageBox.Show("CCCD chỉ được chứa các ký tự số và dài 12 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                        // Kiểm tra xem thông tin nhân viên đã tồn tại chưa (trừ chính nhân viên này)
                        bool isExists = dbContext.Employees.Any(emp =>
                            (emp.phone == txtPhone.Text || emp.cccd == txtCCCD.Text) &&
                            emp.employee_id != employee.employee_id);

                        if (isExists)
                        {
                            MessageBox.Show("Nhân viên với số điện thoại hoặc CCCD này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

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

                        // Update or create work schedule if the data is provided
                        if (!string.IsNullOrWhiteSpace(txbLichLamViec.Text) &&
                            !string.IsNullOrWhiteSpace(txbCaLamViec.Text))
                        {
                            var workSchedule = dbContext.WorkSchedules
                                .Where(ws => ws.employee_id == employee.employee_id)
                                .OrderByDescending(ws => ws.schedule_id)
                                .FirstOrDefault();

                            if (workSchedule != null)
                            {
                                // Update existing work schedule
                                workSchedule.shift_date = txbLichLamViec.Text;
                                workSchedule.shift_time = txbCaLamViec.Text;
                            }
                            else
                            {
                                // Create new work schedule
                                var newWorkSchedule = new WorkSchedule
                                {
                                    employee_id = employee.employee_id,
                                    shift_date = txbLichLamViec.Text,
                                    shift_time = txbCaLamViec.Text
                                };
                                dbContext.WorkSchedules.Add(newWorkSchedule);
                            }
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
                            // Delete work schedules first (due to foreign key constraint)
                            var workSchedules = dbContext.WorkSchedules
                                .Where(ws => ws.employee_id == employee.employee_id)
                                .ToList();

                            foreach (var schedule in workSchedules)
                            {
                                dbContext.WorkSchedules.Remove(schedule);
                            }

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
            string searchShiftDate = txbLichLamViec.Text;
            string searchShiftTime = txbCaLamViec.Text;

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    // Get all employees with their work schedules
                    var employees = dbContext.Employees.ToList();
                    var filteredEmployees = new List<Employee>();

                    // First filter by employee properties
                    var query = employees.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(searchName))
                        query = query.Where(emp => emp.name.ToLower().Contains(searchName));

                    if (!string.IsNullOrWhiteSpace(searchPhone))
                        query = query.Where(emp => emp.phone.Contains(searchPhone));

                    if (!string.IsNullOrWhiteSpace(searchCCCD))
                        query = query.Where(emp => emp.cccd.Contains(searchCCCD));

                    if (!string.IsNullOrWhiteSpace(searchPosition))
                        query = query.Where(emp => emp.position.ToLower().Contains(searchPosition));

                    // If searching for schedule, we need a different approach
                    if (string.IsNullOrWhiteSpace(searchShiftDate) && string.IsNullOrWhiteSpace(searchShiftTime))
                    {
                        // No schedule search, just use the employee filters
                        filteredEmployees = query.ToList();
                    }
                    else
                    {
                        // Filter by schedule as well
                        foreach (var employee in query)
                        {
                            var workSchedules = dbContext.WorkSchedules
                                .Where(ws => ws.employee_id == employee.employee_id).ToList();

                            bool matchesSchedule = workSchedules.Any(ws =>
                                (string.IsNullOrWhiteSpace(searchShiftDate) || ws.shift_date.Contains(searchShiftDate)) &&
                                (string.IsNullOrWhiteSpace(searchShiftTime) || ws.shift_time.Contains(searchShiftTime))
                            );

                            if (matchesSchedule)
                            {
                                filteredEmployees.Add(employee);
                            }
                        }
                    }

                    // Display results
                    dataGridView1.Rows.Clear();
                    foreach (var employee in filteredEmployees)
                    {
                        string genderText = employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        // Get the most recent work schedule for this employee (if any)
                        var workSchedule = dbContext.WorkSchedules
                            .Where(ws => ws.employee_id == employee.employee_id)
                            .OrderByDescending(ws => ws.schedule_id)
                            .FirstOrDefault();

                        string shiftDate = workSchedule != null ? workSchedule.shift_date : "";
                        string shiftTime = workSchedule != null ? workSchedule.shift_time : "";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            shiftDate,
                            shiftTime,
                            employee.salary.ToString("N0")
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
