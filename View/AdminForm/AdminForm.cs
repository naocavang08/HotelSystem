using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSystem.View.AdminForm
{
    public partial class AdminForm: Form
    {
        public AdminForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            this.MinimizeBox = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm op = new LoginForm();
            op.Show();
            this.Hide();
        }

        Statistic statistic;
        Customer customer;
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
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (customer == null)
            {
                customer = new Customer();
                customer.FormClosed += Customer_FormClosed;
                customer.MdiParent = this;
                customer.Dock = DockStyle.Fill;
                customer.Show();
            }
            else
            {
                customer.Activate();
            }
        }

        private void Customer_FormClosed(object sender, FormClosedEventArgs e)
        {
            customer = null;
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
    }
}
