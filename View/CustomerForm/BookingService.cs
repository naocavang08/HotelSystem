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

namespace HotelSystem.View.CustomerForm
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
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
            if (customer != null)
            {
                txtName.Text = customer.Name;
                txtCCCD.Text = customer.CCCD;
                txtPhone.Text = customer.Phone;
            }
        }

        private void LoadServices()
        {
            using (var db = new DBHotelSystem())
            {
                var services = db.Services.ToList();

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
                    CustomerInfo opform = new CustomerInfo();
                    opform.ShowDialog();
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
                    int serviceId = (int)cb.Tag;
                    // Tìm NumericUpDown và DateTimePicker tương ứng
                    NumericUpDown nud = pnSubService.Controls.Find("nud_" + serviceId, true).FirstOrDefault() as NumericUpDown;
                    DateTimePicker dtp = pnSubService.Controls.Find("dtp_" + serviceId, true).FirstOrDefault() as DateTimePicker;
                    
                    if (nud != null && dtp != null)
                    {
                        using (var db = new DBHotelSystem())
                        {
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
                                    TotalPrice = service.price * (int)nud.Value
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
            CustomerForm op = new CustomerForm();
            op.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            CustomerForm op = new CustomerForm();
            op.Show();
        }
    }
}
