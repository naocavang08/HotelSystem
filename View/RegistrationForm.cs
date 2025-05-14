using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem.View
{
    public partial class RegistrationForm: Form
    {
        public bool RegistrationSuccessful { get; private set; }
        public int NewUserId { get; private set; }

        public RegistrationForm()
        {
            InitializeComponent();
            RegistrationSuccessful = false;
            NewUserId = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSignin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirm.Text;

            using (var db = new DBHotelSystem())
            {
                var existingUser = db.Users.FirstOrDefault(u => u.username == username);
                if (existingUser != null)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var newUser = new User
                    {
                        username = username,
                        password = password,
                        role = "customer",
                        status = "active",
                        date_register = DateTime.Now
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    NewUserId = newUser.id;
                    RegistrationSuccessful = true;

                    MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
