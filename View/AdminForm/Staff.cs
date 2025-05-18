using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Model;

namespace HotelSystem.View.AdminForm
{
    public partial class Staff : Form
    {
        private BLL_Staff bllStaff = new BLL_Staff();

        public Staff()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadStaffData(); // Tải dữ liệu nhân viên ban đầu
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

            }
        }

        private void LoadStaffData()
        {
            // Tải dữ liệu nhân viên từ BLL
            dataGridView1.Rows.Clear();

            try
            {
                var staffList = bllStaff.GetAllStaff();

                foreach (var staff in staffList)
                {
                    string genderText = staff.Gender ? "Nam" : "Nữ";

                    dataGridView1.Rows.Add(
                        staff.Name,
                        staff.Phone,
                        staff.CCCD,
                        genderText,
                        staff.Position,
                        staff.ShiftDate ?? "",
                        staff.ShiftTime ?? "",
                        staff.Salary.ToString("N0"),
                        staff.Username ?? "",
                        staff.Password ?? "",
                        staff.Role ?? "Staff"
                    );
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
                bool gender = radioButtonNam.Checked;
                decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                // Tạo DTO_Staff để truyền dữ liệu
                var staffDto = new DTO_Staff
                {
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    CCCD = txtCCCD.Text,
                    Gender = gender,
                    Position = txbChucVu.Text,
                    Salary = salary,
                    Username = txbTenDangNhap.Text,
                    Password = txbMatKhau.Text,
                    Role = "Staff",
                    ShiftDate = txbLichLamViec.Text,
                    ShiftTime = txbCaLamViec.Text
                };

                // Gọi phương thức thêm nhân viên từ BLL
                bool success = bllStaff.AddStaff(staffDto);

                if (success)
                {
                    LoadStaffData();
                    MessageBox.Show(
                        "Thêm nhân viên thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    ClearFields();
                }
                else
                {
                    MessageBox.Show(
                        "Nhân viên với số điện thoại, CCCD hoặc tên đăng nhập này đã tồn tại!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
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
                var staffInfo = bllStaff.GetStaffByCCCD(selectedCccd);

                if (staffInfo != null)
                {
                    bool gender = radioButtonNam.Checked;
                    decimal salary = decimal.Parse(txbLuong.Text.Replace(",", ""));

                    // Tạo DTO_Staff để truyền dữ liệu
                    var staffDto = new DTO_Staff
                    {
                        EmployeeId = staffInfo.EmployeeId,
                        Name = txtName.Text,
                        Phone = txtPhone.Text,
                        CCCD = txtCCCD.Text,
                        Gender = gender,
                        Position = txbChucVu.Text,
                        Salary = salary,
                        Username = txbTenDangNhap.Text,
                        Password = txbMatKhau.Text,
                        Role = "Staff",
                        ShiftDate = txbLichLamViec.Text,
                        ShiftTime = txbCaLamViec.Text
                    };

                    // Gọi phương thức cập nhật nhân viên từ BLL
                    bool success = bllStaff.UpdateStaff(staffDto);

                    if (success)
                    {
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
                            "Nhân viên với số điện thoại, CCCD hoặc tên đăng nhập này đã tồn tại!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }
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
                    string selectedCccd = dataGridView1.SelectedRows[0].Cells["cccd"].Value.ToString();
                    var staffInfo = bllStaff.GetStaffByCCCD(selectedCccd);

                    if (staffInfo != null)
                    {
                        // Gọi phương thức xóa nhân viên từ BLL
                        bool success = bllStaff.DeleteStaff(staffInfo.EmployeeId);

                        if (success)
                        {
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
                                "Không thể xóa nhân viên!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
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

            try
            {
                // Gọi phương thức tìm kiếm nhân viên từ BLL
                var staffList = bllStaff.SearchStaff(
                    searchName, searchPhone, searchCCCD, searchPosition, 
                    searchShiftDate, searchShiftTime, searchUsername
                );

                dataGridView1.Rows.Clear();
                foreach (var staff in staffList)
                {
                    string genderText = staff.Gender ? "Nam" : "Nữ";

                    dataGridView1.Rows.Add(
                        staff.Name,
                        staff.Phone,
                        staff.CCCD,
                        genderText,
                        staff.Position,
                        staff.ShiftDate ?? "",
                        staff.ShiftTime ?? "",
                        staff.Salary.ToString("N0"),
                        staff.Username ?? "",
                        staff.Password ?? "",
                        staff.Role ?? "Staff"
                    );
                }

                if (staffList.Count == 0)
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
            ClearFields();
        }
    }
}
