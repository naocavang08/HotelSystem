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
                var bllBookingService = new BLL_BookingService();
                var bllTTKH = new BLL_TTKH();
                var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);
                var bookedServices = bllBookingService.GetBookingServicesByCustomerId(customer.CustomerId);

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

                    // Đánh dấu nếu dịch vụ đã được đặt
                    var bookedService = bookedServices.FirstOrDefault(bs => bs.Service_id == service.service_id);
                    if (bookedService != null)
                    {
                        // Kiểm tra nếu dịch vụ đã hoàn thành (Completed) thì vô hiệu hóa
                        if (bookedService.Status == "Completed")
                        {
                            // Checkbox không chọn (unchecked) cho dịch vụ đã hoàn thành
                            cb.Checked = false;
                            nud.Value = 1;
                            dtp.Value = DateTime.Now;
                        }
                        else
                        {
                            cb.Checked = true;
                            nud.Value = bookedService.Quantity;
                            dtp.Value = bookedService.Service_date;
                        }
                    }

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
                    CustomerInfo opform = new CustomerInfo(this);
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

        private void btnDelService_Click(object sender, EventArgs e)
        {
            var selectedServices = pnSubService.Controls.OfType<CheckBox>()
                .Where(cb => cb.Checked && cb.Enabled) // Chỉ lấy các checkbox đã chọn và còn được phép chọn
                .ToList();

            if (selectedServices.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa! Lưu ý: Không thể xóa dịch vụ đã hoàn thành.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            var bllBookingService = new BLL_BookingService();
            var bllTTKH = new BLL_TTKH();
            var customer = bllTTKH.GetCustomerByUserId(UserSession.UserId);

            if (customer == null)
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var cb in selectedServices)
            {
                int serviceId = (int)cb.Tag;

                // Lấy danh sách dịch vụ đã đặt theo ServiceId và CustomerId
                var bookingServices = bllBookingService.GetBookingServicesByCustomerId(customer.CustomerId)
                    .Where(bs => bs.Service_id == serviceId)
                    .ToList();

                if (bookingServices.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy dịch vụ với ID: {serviceId} để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                // Xóa dịch vụ đã đặt
                foreach (var bs in bookingServices)
                {
                    try
                    {
                        bllBookingService.DeleteBookingService(bs.Booking_service_id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa dịch vụ ID: {bs.Booking_service_id}\nChi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            LoadServices();
            MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
