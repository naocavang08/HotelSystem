using HotelSystem.Session;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem.View.AdminForm
{
    public partial class AdminForm: Form
    {
        // Dùng để gọi hàm API từ Windows
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Các hằng số
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        public AdminForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
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
                this.Close();
                LoginForm op = new LoginForm();
                op.Show();
            }
        }

        Statistic statistic;
        Service service;
        Room room;
        Staff staff;
        private void btnStatistic_Click(object sender, EventArgs e)
        {
            if (statistic == null)
            {
                statistic = new Statistic();
                statistic.FormClosed += Statistic_FormClosed;
                statistic.MdiParent = this;
                statistic.Dock = DockStyle.Fill;
                statistic.Show();
            }
            else
            {
                statistic.Activate();
            }
        }

        private void Statistic_FormClosed(object sender, FormClosedEventArgs e)
        {
            statistic = null;
        }

        private void Service_FormClosed(object sender, FormClosedEventArgs e)
        {
            service = null;
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            if (staff == null)
            {
                staff = new Staff();
                staff.FormClosed += Staff_FormClosed;
                staff.MdiParent = this;
                staff.Dock = DockStyle.Fill;
                staff.Show();
            }
            else
            {
                staff.Activate();
            }
        }

        private void Staff_FormClosed(object sender, FormClosedEventArgs e)
        {
            staff = null;
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            if (room == null)
            {
                room = new Room();
                room.FormClosed += Room_FormClosed;
                room.MdiParent = this;
                room.Dock = DockStyle.Fill;
                room.Show();
            }
            else
            {
                room.Activate();
            }
        }

        private void Room_FormClosed(object sender, FormClosedEventArgs e)
        {
            room = null;
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            if (service == null)
            {
                service = new Service();
                service.FormClosed += Service_FormClosed;
                service.MdiParent = this;
                service.Dock = DockStyle.Fill;
                service.Show();
            }
            else
            {
                service.Activate();
            }
        }

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            
            // Gửi thông điệp để giả lập việc kéo thanh tiêu đề
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
    }
}
