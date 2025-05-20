using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSystem.Model;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Session;

namespace HotelSystem.View.StaffForm
{
    public partial class BookingService: Form
    {
        public BookingService()
        {
            InitializeComponent();
        }

        private void BookingService_Load(object sender, EventArgs e)
        {
            LoadServices();
            LoadCustomerInfo();
        }

        private void LoadCustomerInfo()
        {
            
        }

        private void LoadServices()
        {
            using (var db = new DBHotelSystem())
            {
                var services = db.Services.ToList();
                var bllBookingService = new BLL_BookingService();

                // Xóa control dịch vụ
                var ctrlRemove = pnSubService.Controls.OfType<Control>()
                    .Where(c => c is CheckBox || c is NumericUpDown || c is DateTimePicker)
                    .ToList();
                foreach (var ctrl in ctrlRemove)
                {
                    pnSubService.Controls.Remove(ctrl);
                }

                int y = 10;
                foreach (var service in services)
                {
                    // Tạo checkbox cho từng dịch vụ
                    CheckBox cb = new CheckBox();
                    cb.Text = service.name;
                    cb.Tag = service.service_id;
                    cb.Location = new Point(30, y);
                    cb.AutoSize = true;

                    // Tạo numbericupdown cho số lượng
                    NumericUpDown nud = new NumericUpDown();
                    nud.Name = "nud_" + service.service_id; 
                    nud.Minimum = 1;
                    nud.Maximum = 100;
                    nud.Value = 1;
                    nud.Location = new Point(160, y - 2);
                    nud.Width = 60;

                    // Tạo DateTimePicker cho ngày sử dụng dịch vụ
                    DateTimePicker dtp = new DateTimePicker();
                    dtp.Name = "dtp_" + service.service_id;
                    dtp.Format = DateTimePickerFormat.Short;
                    dtp.Location = new Point(230, y - 2);
                    dtp.Width = 120;

                    pnSubService.Controls.Add(cb);
                    pnSubService.Controls.Add(nud);
                    pnSubService.Controls.Add(dtp);

                    y += 35;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var BLL_BookingService = new BLL_BookingService();
            var selectedServices = new List<DTO_BookingService>();
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByName(txtName.Text);

            if (customer == null)
            {
                DialogResult result = MessageBox.Show("Họ tên không tồn tại, vui lòng thêm thông tin cá nhân!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    var newCustomerId = bllTTKH.GetNextCustomerId();
                    CustomerDetail opform = new CustomerDetail(newCustomerId);
                    opform.ShowDialog();
                    if (opform.DialogResult == DialogResult.OK)
                    {
                        var newCustomer = bllTTKH.GetCustomerByCustomerId(newCustomerId);
                        if (newCustomer != null)
                        {
                            txtName.Text = newCustomer.Name;
                            txtCCCD.Text = newCustomer.CCCD;
                            txtPhone.Text = newCustomer.Phone;
                        }
                    }
                }
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtCCCD.Text) || string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (customer.CCCD != txtCCCD.Text.Trim() || customer.Phone != txtPhone.Text.Trim())
            {
                MessageBox.Show("CCCD hoặc số điện thoại không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Control ctrl in pnSubService.Controls)
            {
                if (ctrl is CheckBox cb && cb.Checked)
                {
                    if (!cb.Enabled) continue;

                    int serviceId = (int)cb.Tag;
                    // Tìm NumericUpDown và DateTimePicker tương ứng
                    NumericUpDown nud = pnSubService.Controls.Find("nud_" + serviceId, true).FirstOrDefault() as NumericUpDown;
                    DateTimePicker dtp = pnSubService.Controls.Find("dtp_" + serviceId, true).FirstOrDefault() as DateTimePicker;
                    
                    if (nud == null)
                    {
                        MessageBox.Show("Vui lòng chọn số lượng và ngày sử dụng dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dtp.Value < DateTime.Now)
                    {
                        MessageBox.Show("Ngày sử dụng dịch vụ không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (var db = new DBHotelSystem())
                    {
                        var existingBooking = db.BookingServices
                            .FirstOrDefault(bs => bs.customer_id == customer.CustomerId && bs.service_id == serviceId);

                        if (existingBooking != null)
                        {
                            // Dịch vụ đã tồn tại, cập nhật thông tin
                            existingBooking.quantity = (int)nud.Value;
                            existingBooking.service_date = dtp.Value;
                            existingBooking.total_price = db.Services.First(s => s.service_id == serviceId).price * (int)nud.Value;
                            
                            // Cập nhật status thành "Booked" nếu dịch vụ đã hoàn thành (Completed) trước đó
                            if (existingBooking.status == "Completed")
                            {
                                existingBooking.status = "Booked";
                            }
                            
                            db.SaveChanges();
                        }
                        else
                        {
                            // Dịch vụ chưa tồn tại, thêm mới
                            var service = db.Services.FirstOrDefault(s => s.service_id == serviceId);
                            if (service != null)
                            {
                                decimal totalPrice = service.price * (int)nud.Value;
                                var dtoBookingService = new DTO_BookingService
                                {
                                    CustomerId = customer.CustomerId,
                                    Service_id = serviceId,
                                    Quantity = (int)nud.Value,
                                    Service_date = dtp.Value,
                                    TotalPrice = totalPrice,
                                    Status = "Booked"
                                };
                                selectedServices.Add(dtoBookingService);
                            }
                        }
                    }
                }
            }
            // Thêm dịch vụ đã chọn vào cơ sở dữ liệu
            foreach (var dto in selectedServices)
            {
                BLL_BookingService.AddBookingService(dto);
            }

            MessageBox.Show("Đặt dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StaffForm op = new StaffForm();
            op.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            StaffForm op = new StaffForm();
            op.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
