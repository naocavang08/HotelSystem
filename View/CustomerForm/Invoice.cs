using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Session;

namespace HotelSystem.View.CustomerForm
{
    public partial class Invoice: Form
    {
        private List<DTO_BookingService> _bookingServices;
        private List<DTO_BookingRoom> _bookingRooms;
        public Invoice(List<DTO_BookingRoom> bookingRooms, List<DTO_BookingService> bookingServices = null)
        {
            InitializeComponent();
            _bookingRooms = bookingRooms ?? new List<DTO_BookingRoom>();
            _bookingServices = bookingServices;
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.Show();

            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            LoadCustomerInfo();
            LoadBookingRooms();
            LoadBookingServices();
            CalculateTotal();
        }

        private void LoadCustomerInfo()
        {
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            if (customer != null)
            {
                lbCusName.Text = customer.Name;
                lbCusCCCD.Text = customer.CCCD;
                lbCusPhone.Text = customer.Phone;
            }
        }

        private void CalculateTotal()
        {
            decimal totalRoomPrice = _bookingRooms?.Sum(r => r.TotalPrice) ?? 0;
            decimal totalServicePrice = _bookingServices?.Sum(s => s.TotalPrice) ?? 0;
            decimal grandTotal = totalRoomPrice + totalServicePrice;

            lbTotalAmount.Text = $"{grandTotal:C}";
        }

        private void LoadBookingServices()
        {
            if (_bookingServices == null || _bookingServices.Count == 0)
            {
                dgvListService.Visible = false;
                // Nếu có label tiêu đề dịch vụ, cũng ẩn luôn, ví dụ:
                label6.Visible = false;
                return;
            }

            if (dgvListService.Columns.Count == 0)
            {
                dgvListService.Columns.Add("ServiceId", "Service ID");
                dgvListService.Columns.Add("Quantity", "Quantity");
                dgvListService.Columns.Add("ServiceDate", "Service Date");
                dgvListService.Columns.Add("TotalPrice", "Total Price");
            }

            dgvListService.Rows.Clear();

            foreach (var service in _bookingServices)
            {
                dgvListService.Rows.Add(
                    service.Service_id,
                    service.Quantity,
                    service.Service_date.ToShortDateString(),
                    service.TotalPrice.ToString("C")
                );
            }
        }

        private void LoadBookingRooms()
        {
            if (dgvListRoom.Columns.Count == 0)
            {
                dgvListRoom.Columns.Add("RoomId", "Room ID");
                dgvListRoom.Columns.Add("CheckIn", "Check-In Date");
                dgvListRoom.Columns.Add("CheckOut", "Check-Out Date");
                dgvListRoom.Columns.Add("TotalPrice", "Total Price");
            }
            dgvListRoom.Rows.Clear();

            foreach (var room in _bookingRooms)
            {
                // Hiển thị thông tin đặt phòng (ví dụ: ListView hoặc DataGridView)
                dgvListRoom.Rows.Add(
                    room.RoomId,
                    room.CheckIn.ToShortDateString(),
                    room.CheckOut.ToShortDateString(),
                    room.TotalPrice.ToString("C")
                );
            }
        }
    }
}
