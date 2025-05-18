using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HotelSystem.Model;

namespace HotelSystem.View.AdminForm
{
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
            InitializeRoleComboBox();
            SetupDataGridView();
            LoadStaffData(); // Tải dữ liệu nhân viên ban đầu
        }

        private void InitializeRoleComboBox()
        {
            // Khởi tạo ComboBox Role
            cbbRole.Items.Clear();
            cbbRole.Items.Add("Admin");
            cbbRole.Items.Add("Staff");
            cbbRole.SelectedIndex = 1; // Mặc định là Staff
        }

        private void SetupDataGridView()
        {
            // Cấu hình DataGridView
            dataGridView1.Columns.Clear();


            // Thêm các cột vào DataGridView
            dataGridView1.Columns.Add("name", "Họ tên");
            dataGridView1.Columns.Add("phone", "SĐT");
            dataGridView1.Columns.Add("cccd", "CCCD");
            dataGridView1.Columns.Add("gender", "Giới tính");
            dataGridView1.Columns.Add("position", "Chức vụ");
            dataGridView1.Columns.Add("shift_date", "Lịch làm việc");
            dataGridView1.Columns.Add("shift_time", "Ca làm việc");
            dataGridView1.Columns.Add("salary", "Lương");
            dataGridView1.Columns.Add("username", "Username");
            dataGridView1.Columns.Add("password", "Password");
            dataGridView1.Columns.Add("role", "Vai trò");

            // Cấu hình hiển thị
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            // Gắn sự kiện khi click vào ô
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

                // Gán giá trị giới tính
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
                txbLichLamViec.Text = row.Cells["shift_date"].Value.ToString();
                txbCaLamViec.Text = row.Cells["shift_time"].Value.ToString();
                txbLuong.Text = row.Cells["salary"].Value.ToString().Replace(",", "");
                txbTenDangNhap.Text = row.Cells["username"].Value.ToString();
                txbMatKhau.Text = row.Cells["password"].Value.ToString();

                // Cập nhật ComboBox Role
                string role = row.Cells["role"].Value.ToString();
                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    cbbRole.SelectedIndex = 0;
                else
                    cbbRole.SelectedIndex = 1;
            }
        }

        private void LoadStaffData()
        {
            // Tải dữ liệu nhân viên từ cơ sở dữ liệu
            dataGridView1.Rows.Clear();

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    var employees = dbContext.Employees.ToList();

                    foreach (var employee in employees)
                    {
                        string genderText =
                            employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        var workSchedule = dbContext
                            .WorkSchedules.Where(ws => ws.employee_id == employee.employee_id)
                            .OrderByDescending(ws => ws.schedule_id)
                            .FirstOrDefault();

                        string shiftDate = workSchedule != null ? workSchedule.shift_date : "";
                        string shiftTime = workSchedule != null ? workSchedule.shift_time : "";

                        // Lấy thông tin tài khoản
                        var user = dbContext.Users.FirstOrDefault(u => u.id == employee.id);
                        string username = user != null ? user.username : "";
                        string password = user != null ? user.password : "";
                        string role = user != null ? user.role : "Staff";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            shiftDate,
                            shiftTime,
                            employee.salary.ToString("N0"),
                            username,
                            password,
                            role
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải dữ liệu nhân viên: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ClearFields()
        {
            // Xóa trắng các trường nhập liệu
            txtName.Text = "";
            txtPhone.Text = "";
            txtCCCD.Text = "";
            txbChucVu.Text = "";
            txbLuong.Text = "";
            txbLichLamViec.Text = "";
            txbCaLamViec.Text = "";
            txbTenDangNhap.Text = "";
            txbMatKhau.Text = "";
            cbbRole.SelectedIndex = 1;
            radioButtonNam.Checked = true;
            radioButtonNu.Checked = false;
        }

        private bool ValidateInput()
        {
            // Kiểm tra tính hợp lệ của dữ liệu nhập
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập họ tên!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCCCD.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập CCCD!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbChucVu.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập chức vụ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbLuong.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập lương!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (!decimal.TryParse(txbLuong.Text.Replace(",", ""), out _))
            {
                MessageBox.Show(
                    "Lương phải là một số hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (!radioButtonNam.Checked && !radioButtonNu.Checked)
            {
                MessageBox.Show(
                    "Vui lòng chọn giới tính!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (
                !string.IsNullOrWhiteSpace(txbLichLamViec.Text)
                && string.IsNullOrWhiteSpace(txbCaLamViec.Text)
            )
            {
                MessageBox.Show(
                    "Vui lòng nhập ca làm việc!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (
                string.IsNullOrWhiteSpace(txbLichLamViec.Text)
                && !string.IsNullOrWhiteSpace(txbCaLamViec.Text)
            )
            {
                MessageBox.Show(
                    "Vui lòng nhập lịch làm việc!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbTenDangNhap.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (string.IsNullOrWhiteSpace(txbMatKhau.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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
                    bool isExists = dbContext.Employees.Any(emp =>
                        emp.phone == txtPhone.Text || emp.cccd == txtCCCD.Text
                    );
                    bool isUsernameExists = dbContext.Users.Any(u =>
                        u.username == txbTenDangNhap.Text
                    );

                    if (isExists)
                    {
                        MessageBox.Show(
                            "Nhân viên với số điện thoại hoặc CCCD này đã tồn tại!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (isUsernameExists)
                    {
                        MessageBox.Show(
                            "Tên đăng nhập này đã tồn tại!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    bool gender = radioButtonNam.Checked;
                    decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                    var newUser = new User
                    {
                        username = txbTenDangNhap.Text,
                        password = txbMatKhau.Text,
                        role = cbbRole.SelectedItem.ToString(),
                        status = "active",
                        date_register = DateTime.Now,
                    };

                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();

                    var newEmployee = new Employee
                    {
                        name = txtName.Text,
                        phone = txtPhone.Text,
                        cccd = txtCCCD.Text,
                        gender = gender,
                        position = txbChucVu.Text,
                        salary = salary,
                        id = newUser.id,
                    };

                    dbContext.Employees.Add(newEmployee);
                    dbContext.SaveChanges();

                    if (
                        !string.IsNullOrWhiteSpace(txbLichLamViec.Text)
                        && !string.IsNullOrWhiteSpace(txbCaLamViec.Text)
                    )
                    {
                        var workSchedule = new WorkSchedule
                        {
                            employee_id = newEmployee.employee_id,
                            shift_date = txbLichLamViec.Text,
                            shift_time = txbCaLamViec.Text,
                        };

                        dbContext.WorkSchedules.Add(workSchedule);
                        dbContext.SaveChanges();
                    }

                    LoadStaffData();
                    MessageBox.Show(
                        "Thêm nhân viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi thêm nhân viên: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần cập nhật!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            if (!ValidateInput())
                return;

            try
            {
                string selectedCccd = dataGridView1.SelectedRows[0].Cells["cccd"].Value.ToString();

                using (var dbContext = new DBHotelSystem())
                {
                    var employee = dbContext.Employees.FirstOrDefault(emp =>
                        emp.cccd == selectedCccd
                    );

                    if (employee != null)
                    {
                        bool isExists = dbContext.Employees.Any(emp =>
                            (emp.phone == txtPhone.Text || emp.cccd == txtCCCD.Text)
                            && emp.employee_id != employee.employee_id
                        );

                        var user = dbContext.Users.Find(employee.id);
                        bool isUsernameExists = dbContext.Users.Any(u =>
                            u.username == txbTenDangNhap.Text && u.id != employee.id
                        );

                        if (isExists)
                        {
                            MessageBox.Show(
                                "Nhân viên với số điện thoại hoặc CCCD này đã tồn tại!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }

                        if (isUsernameExists)
                        {
                            MessageBox.Show(
                                "Tên đăng nhập này đã tồn tại!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                            return;
                        }

                        // Update employee info
                        bool gender = radioButtonNam.Checked;
                        decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                        employee.name = txtName.Text;
                        employee.phone = txtPhone.Text;
                        employee.cccd = txtCCCD.Text;
                        employee.gender = gender;
                        employee.position = txbChucVu.Text;
                        employee.salary = salary;

                        // Update user info
                        if (user != null)
                        {
                            user.username = txbTenDangNhap.Text;
                            user.password = txbMatKhau.Text;
                            user.role = cbbRole.SelectedItem.ToString();
                        }

                        if (
                            !string.IsNullOrWhiteSpace(txbLichLamViec.Text)
                            && !string.IsNullOrWhiteSpace(txbCaLamViec.Text)
                        )
                        {
                            var workSchedule = dbContext
                                .WorkSchedules.Where(ws => ws.employee_id == employee.employee_id)
                                .OrderByDescending(ws => ws.schedule_id)
                                .FirstOrDefault();

                            if (workSchedule != null)
                            {
                                workSchedule.shift_date = txbLichLamViec.Text;
                                workSchedule.shift_time = txbCaLamViec.Text;
                            }
                            else
                            {
                                var newWorkSchedule = new WorkSchedule
                                {
                                    employee_id = employee.employee_id,
                                    shift_date = txbLichLamViec.Text,
                                    shift_time = txbCaLamViec.Text,
                                };
                                dbContext.WorkSchedules.Add(newWorkSchedule);
                            }
                        }

                        dbContext.SaveChanges();
                        LoadStaffData();
                        MessageBox.Show(
                            "Cập nhật nhân viên thành công!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Không tìm thấy nhân viên!",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi cập nhật nhân viên: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn nhân viên cần xóa!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhân viên này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string selectedCccd = dataGridView1
                        .SelectedRows[0]
                        .Cells["cccd"]
                        .Value.ToString();

                    using (var dbContext = new DBHotelSystem())
                    {
                        var employee = dbContext.Employees.FirstOrDefault(emp =>
                            emp.cccd == selectedCccd
                        );

                        if (employee != null)
                        {
                            var workSchedules = dbContext
                                .WorkSchedules.Where(ws => ws.employee_id == employee.employee_id)
                                .ToList();

                            foreach (var schedule in workSchedules)
                            {
                                dbContext.WorkSchedules.Remove(schedule);
                            }

                            dbContext.Employees.Remove(employee);
                            dbContext.SaveChanges();
                            LoadStaffData();
                            MessageBox.Show(
                                "Xóa nhân viên thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Không tìm thấy nhân viên!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi khi xóa nhân viên: {ex.Message}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
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
            string searchUsername = txbTenDangNhap.Text;
            string searchRole = cbbRole.SelectedItem?.ToString();

            try
            {
                using (var dbContext = new DBHotelSystem())
                {
                    var employees = dbContext.Employees.ToList();
                    var filteredEmployees = new List<Employee>();

                    // Filter by basic info
                    var query = employees.AsQueryable();

                    if (!string.IsNullOrWhiteSpace(searchName))
                        query = query.Where(emp => emp.name.ToLower().Contains(searchName));

                    if (!string.IsNullOrWhiteSpace(searchPhone))
                        query = query.Where(emp => emp.phone.Contains(searchPhone));

                    if (!string.IsNullOrWhiteSpace(searchCCCD))
                        query = query.Where(emp => emp.cccd.Contains(searchCCCD));

                    if (!string.IsNullOrWhiteSpace(searchPosition))
                        query = query.Where(emp => emp.position.ToLower().Contains(searchPosition));

                    // Filter by username if provided
                    if (!string.IsNullOrWhiteSpace(searchUsername))
                    {
                        query = query.Where(emp =>
                            dbContext.Users.Any(u =>
                                u.id == emp.id && u.username.Contains(searchUsername)
                            )
                        );
                    }

                    if (!string.IsNullOrWhiteSpace(searchRole))
                    {
                        query = query.Where(emp =>
                            dbContext.Users.Any(u => u.id == emp.id && u.role == searchRole)
                        );
                    }

                    if (
                        string.IsNullOrWhiteSpace(searchShiftDate)
                        && string.IsNullOrWhiteSpace(searchShiftTime)
                    )
                    {
                        filteredEmployees = query.ToList();
                    }
                    else
                    {
                        foreach (var employee in query)
                        {
                            var workSchedules = dbContext
                                .WorkSchedules.Where(ws => ws.employee_id == employee.employee_id)
                                .ToList();

                            bool matchesSchedule = workSchedules.Any(ws =>
                                (
                                    string.IsNullOrWhiteSpace(searchShiftDate)
                                    || ws.shift_date.Contains(searchShiftDate)
                                )
                                && (
                                    string.IsNullOrWhiteSpace(searchShiftTime)
                                    || ws.shift_time.Contains(searchShiftTime)
                                )
                            );

                            if (matchesSchedule)
                            {
                                filteredEmployees.Add(employee);
                            }
                        }
                    }

                    dataGridView1.Rows.Clear();
                    foreach (var employee in filteredEmployees)
                    {
                        string genderText =
                            employee.gender.HasValue && employee.gender.Value ? "Nam" : "Nữ";

                        var workSchedule = dbContext
                            .WorkSchedules.Where(ws => ws.employee_id == employee.employee_id)
                            .OrderByDescending(ws => ws.schedule_id)
                            .FirstOrDefault();

                        string shiftDate = workSchedule != null ? workSchedule.shift_date : "";
                        string shiftTime = workSchedule != null ? workSchedule.shift_time : "";

                        // Get user info
                        var user = dbContext.Users.FirstOrDefault(u => u.id == employee.id);
                        string username = user != null ? user.username : "";
                        string password = user != null ? user.password : "";

                        dataGridView1.Rows.Add(
                            employee.name,
                            employee.phone,
                            employee.cccd,
                            genderText,
                            employee.position,
                            shiftDate,
                            shiftTime,
                            employee.salary.ToString("N0"),
                            username,
                            password,
                            user != null ? user.role : "Staff"
                        );
                    }

                    if (filteredEmployees.Count == 0)
                    {
                        MessageBox.Show(
                            "Không tìm thấy nhân viên nào phù hợp!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        LoadStaffData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tìm kiếm: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadStaffData();
        }
    }
}
