using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.Session;

namespace HotelSystem.View.CustomerForm
{
    public partial class CustomerForm: Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UserSession.Clear();
                Application.Exit();
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UserSession.Clear();
                Application.Exit();
            }
        }

        private void lblCusInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo op = new CustomerInfo();
            op.Show();

            this.Hide();
        }

        private void picCusInfo_Click(object sender, EventArgs e)
        {
            CustomerInfo op = new CustomerInfo();
            op.Show();

            this.Hide();
        }

        private void lblBookRoom_Click(object sender, EventArgs e)
        {
            BookingRoom op = new BookingRoom();
            op.Show();

            this.Hide();
        }

        private void picBookRoom_Click(object sender, EventArgs e)
        {
            BookingRoom op = new BookingRoom();
            op.Show();

            this.Hide();
        }

        private void lblBookService_Click(object sender, EventArgs e)
        {
            BookingService op = new BookingService();
            op.Show();

            this.Hide();
        }

        private void picBookService_Click(object sender, EventArgs e)
        {
            BookingService op = new BookingService();
            op.Show();

            this.Hide();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Xin chào, {UserSession.Username}!";
        }
    }
}
