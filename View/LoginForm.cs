using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Session;

namespace HotelSystem.View
{
    public partial class LoginForm: Form
    {
        private UserBLL UserBLL = new UserBLL();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            UserDTO user = UserBLL.Login(username, password);
                
            if (user != null)    
            {
                UserSession.UserId = user.UserId;
                UserSession.Username = user.Username;
                UserSession.Role = user.Role;

                if (user.Role == "admin")
                {
                    AdminForm.AdminForm op = new AdminForm.AdminForm();
                    op.Show();
                }
                else if (user.Role == "staff")
                {
                    StaffForm.StaffForm op = new StaffForm.StaffForm();
                    op.Show();
                }
                
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblSignup_Click(object sender, EventArgs e)
        {
            RegistrationForm op = new RegistrationForm();
            op.Show();

            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
