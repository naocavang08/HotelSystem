using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Model;
using HotelSystem.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.View.AdminForm;

namespace HotelSystem.View.CustomerForm
{
    public partial class Booking: Form
    {
        public Booking()
        {
            InitializeComponent();
        }

        private void Booking_Load(object sender, EventArgs e)
        {
            LoadCBBRoomType();
            dtpCheck_in.Value = DateTime.Today;
            dtpCheck_out.Value = DateTime.Today.AddDays(1);
        }

        private void LoadCBBRoomType()
        {
            RoomBLL RoomBLL = new RoomBLL();
            cbbRoomType.DataSource = RoomBLL.GetRoomTypeList();
            MessageBox.Show("" + RoomBLL.GetRoomTypeList().Count);
        }

        private void cbbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string roomType = cbbRoomType.SelectedItem.ToString();
        }

        private void dtpCheck_in_ValueChanged(object sender, EventArgs e)
        {
            string checkInDate = dtpCheck_in.Value.ToString("yyyy-MM-dd");
        }

        private void dtpCheck_out_ValueChanged(object sender, EventArgs e)
        {
            string checkOutDate = dtpCheck_out.Value.ToString("yyyy-MM-dd");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var customer = new Customer
            {
                name = txtName.Text,
                phone = txtPhone.Text,
                cccd = txtCCCD.Text,
            };
            //int customerId = CustomerBLL.;

            var booking = new Booking
            {
                customer_id = customerId,
                RoomTypeId = (int)cbbRoomType.SelectedValue,
                CheckIn = dtpCheck_in.Value,
                CheckOut = dtpCheck_out.Value
            };
        }
    }
}
