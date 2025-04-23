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
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSignin_Click(object sender, EventArgs e)
        {
            LoginForm op = new LoginForm();
            op.Show();

            this.Hide();
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
                    MessageBox.Show("Username already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Passwords do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    db.Users.Add(new User
                    {
                        username = username,
                        password = password,
                        role = "customer",
                        status = "active",
                        date_register = DateTime.Now
                    });
                    db.SaveChanges();
                    MessageBox.Show("Registration successful", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoginForm op = new LoginForm();
                    op.Show();
                    this.Hide();
                }
            }
        }
    }
}
