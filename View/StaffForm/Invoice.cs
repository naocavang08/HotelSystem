using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HotelSystem.BLL;
using HotelSystem.DTO;
using HotelSystem.Model;
using HotelSystem.Session;

namespace HotelSystem.View.StaffForm
{
    public partial class Invoice: Form
    {
        private List<DTO_BookingService> _bookingServices;
        private List<DTO_BookingRoom> _bookingRooms;
        private int? _invoiceId;
        private Form _callerForm; // Form đã gọi Invoice

        public Invoice(List<DTO_BookingRoom> bookingRooms, List<DTO_BookingService> bookingServices = null, Form callerForm = null)
        {
            InitializeComponent();
            _bookingRooms = bookingRooms ?? new List<DTO_BookingRoom>();
            _bookingServices = bookingServices;
            _callerForm = callerForm; // Lưu form gọi
        }

        public Invoice(int invoiceId, Form callerForm = null)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
            _bookingRooms = new List<DTO_BookingRoom>();
            _bookingServices = new List<DTO_BookingService>();
            _callerForm = callerForm; // Lưu form gọi
            
            // Hiển thị invoice ID
            lbInvoiceID.Text = invoiceId.ToString();
            
            // Nếu đây là hóa đơn mới (được tạo từ CustomerForm), không cần tải dữ liệu
            // Chỉ tải dữ liệu nếu hóa đơn đã tồn tại trong cơ sở dữ liệu
            using (var db = new DBHotelSystem())
            {
                var existingInvoice = db.Invoices.Find(invoiceId);
                if (existingInvoice != null)
                {
                    // Chỉ tải dữ liệu nếu hóa đơn đã tồn tại
                    LoadInvoiceData(invoiceId);
                }
            }
        }

        private void LoadInvoiceData(int invoiceId)
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Tải dữ liệu hóa đơn
                    var invoice = db.Invoices
                        .Include("Bookings.Customer")
                        .Include("Bookings.Room.RoomType")
                        .Include("BookingServices.Service")
                        .FirstOrDefault(i => i.invoice_id == invoiceId);

                    if (invoice == null)
                    {
                        // Không hiển thị lỗi cho hóa đơn mới
                        return;
                    }

                    // Lấy thông tin khách hàng từ đặt phòng đầu tiên
                    var customer = invoice.Bookings.FirstOrDefault()?.Customer;
                    
                    // Tải thông tin khách hàng nếu có
                    if (customer != null)
                    {
                        txtName.Text = customer.name;
                        txtCCCD.Text = customer.cccd;
                        txtPhone.Text = customer.phone;
                    }

                    // Tải thông tin đặt phòng
                    foreach (var booking in invoice.Bookings)
                    {
                        _bookingRooms.Add(new DTO_BookingRoom
                        {
                            BookingId = booking.booking_id,
                            RoomId = booking.room_id,
                            CustomerId = booking.customer_id,
                            CheckIn = booking.check_in,
                            CheckOut = booking.check_out,
                            Status = booking.status,
                            TotalPrice = booking.total_price
                        });
                    }

                    // Tải thông tin dịch vụ đặt phòng
                    foreach (var bookingService in invoice.BookingServices)
                    {
                        _bookingServices.Add(new DTO_BookingService
                        {
                            Booking_service_id = bookingService.booking_service_id,
                            Service_id = bookingService.service_id,
                            CustomerId = bookingService.customer_id,
                            Quantity = bookingService.quantity,
                            Service_date = bookingService.service_date,
                            TotalPrice = bookingService.total_price,
                            Status = bookingService.status
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            // Sau đó xử lý thanh toán và cập nhật các thông tin khác
            using (var db = new DBHotelSystem())
            {
                try
                {
                    // Kiểm tra xem đã thanh toán chưa
                    bool alreadyPaid = true;
                    
                    // Kiểm tra các booking
                    foreach (var bookingRoom in _bookingRooms)
                    {
                        // Kiểm tra xem booking này đã được liên kết với hóa đơn nào đã thanh toán chưa
                        var booking = db.Bookings.Find(bookingRoom.BookingId);
                        if (booking != null && !booking.Invoices.Any(i => i.payment_status == "paid"))
                        {
                            alreadyPaid = false;
                            break;
                        }
                    }
                    
                    // Nếu đã thanh toán rồi, hiển thị thông báo và thoát
                    if (alreadyPaid)
                    {
                        MessageBox.Show("Bạn đã thanh toán xong rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                    // Cập nhật trạng thái booking sang Checked Out
                    var bllBookingRoom = new BLL_BookingRoom();
                    foreach (var bookingRoom in _bookingRooms)
                    {
                        // Sử dụng phương thức UpdateBookingStatus để cập nhật trạng thái
                        bllBookingRoom.UpdateBookingStatus(bookingRoom.BookingId, "Checked Out");
                    }
                    
                    // Cập nhật trạng thái booking service sang Completed nếu có
                    if (_bookingServices != null && _bookingServices.Any())
                    {
                        var bllBookingService = new BLL_BookingService();
                        foreach (var bookingService in _bookingServices)
                        {
                            bllBookingService.UpdateBookingServiceStatus(bookingService.Booking_service_id, "Completed");
                        }
                    }
                    // Cập nhật trạng thái phòng
                    var bllRoom = new BLL_Room();
                    foreach (var bookingRoom in _bookingRooms)
                    {
                        bllRoom.UpdateRoomStatus(bookingRoom.RoomId, "Available");
                    }

                    // Tạo một hóa đơn duy nhất
                    var invoice = new HotelSystem.Model.Invoice
                    {
                        payment_status = "paid",
                        payment_date = DateTime.Now,
                        total_amount = _bookingRooms.Sum(r => r.TotalPrice) + 
                            (_bookingServices?.Sum(s => s.TotalPrice) ?? 0)
                    };

                    // Liên kết hóa đơn với tất cả các booking
                    foreach (var bookingRoom in _bookingRooms)
                    {
                        var booking = db.Bookings.Find(bookingRoom.BookingId);
                        if (booking != null)
                        {
                            invoice.Bookings.Add(booking);
                        }
                    }

                    // Liên kết hóa đơn với tất cả các dịch vụ nếu có
                    if (_bookingServices != null && _bookingServices.Any())
                    {
                        foreach (var bookingService in _bookingServices)
                        {
                            var service = db.BookingServices.Find(bookingService.Booking_service_id);
                            if (service != null)
                            {
                                invoice.BookingServices.Add(service);
                            }
                        }
                    }

                    db.Invoices.Add(invoice);
                    db.SaveChanges();
                    
                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lưu hóa đơn vào file
                    SaveInvoiceToFile(invoice.invoice_id);

                    DialogResult = DialogResult.OK;

                    // Quay lại màn hình khách hàng hoặc form gọi
                    if (_callerForm != null && !_callerForm.IsDisposed)
                    {
                        _callerForm.Show();
                    }
                    else
                    {
                        CustomerForm customerForm = new CustomerForm();
                        customerForm.Show();
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Nếu có form gọi (caller) thì hiển thị, nếu không thì mở CustomerForm mặc định
            if (_callerForm != null && !_callerForm.IsDisposed)
            {
                _callerForm.Show();
            }
            else if (_invoiceId == null) // Mở từ CustomerForm
            {
                CustomerForm customerForm = new CustomerForm();
                customerForm.Show();
            }
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            try
            {
                LoadBookingRooms(0);
                LoadBookingServices(0);
                CalculateTotal();
            }
            catch (Exception ex)
            {
                // Không hiển thị thông báo lỗi cho người dùng, nhưng thiết lập giá trị mặc định
                lbTotalAmount.Text = "0";
            }
        }

        private void CalculateTotal()
        {
            try
            {
                // Đảm bảo cả hai danh sách đều được khởi tạo
                if (_bookingRooms == null) _bookingRooms = new List<DTO_BookingRoom>();
                if (_bookingServices == null) _bookingServices = new List<DTO_BookingService>();
                
                decimal totalRoomPrice = _bookingRooms.Sum(r => r.TotalPrice);
                decimal totalServicePrice = _bookingServices.Sum(s => s.TotalPrice); 
                decimal grandTotal = totalRoomPrice + totalServicePrice;
                
                lbTotalAmount.Text = $"{grandTotal:C}";
            }
            catch (Exception ex)
            {
                // Đặt tổng mặc định nếu tính toán thất bại
                lbTotalAmount.Text = "0";
                // Không hiển thị thông báo lỗi cho người dùng
            }
        }

        private void LoadBookingRooms(int customerId)
        {
            var bllBookingRoom = new BLL_BookingRoom();
            var allBookingRooms = bllBookingRoom.GetBookingRoomsByCustomerId(customerId);
            
            if (_callerForm is InvoiceForm)
            {
                // Nếu được gọi từ InvoiceForm, chỉ hiển thị phòng đã check-out
                _bookingRooms = allBookingRooms.ToList();
            }
            else if (_callerForm is CustomerForm)
            {
                // Nếu được gọi từ CustomerForm, chỉ hiển thị phòng đã đặt (Booked)
                _bookingRooms = allBookingRooms.Where(b => b.Status == "Booked").ToList();
            }
            else
            {
                _bookingRooms = allBookingRooms;
            }
            
            if (dgvListRoom.Columns.Count == 0)
            {
                dgvListRoom.Columns.Add("RoomId", "Room ID");
                dgvListRoom.Columns.Add("CheckIn", "Check-In Date");
                dgvListRoom.Columns.Add("CheckOut", "Check-Out Date");
                dgvListRoom.Columns.Add("Status", "Status");
                dgvListRoom.Columns.Add("TotalPrice", "Total Price");
            }
            dgvListRoom.Rows.Clear();
            
            foreach (var room in _bookingRooms)
            {
                // Hiển thị thông tin đặt phòng
                dgvListRoom.Rows.Add(
                    room.RoomId,
                    room.CheckIn.ToShortDateString(),
                    room.CheckOut.ToShortDateString(),
                    room.Status,
                    room.TotalPrice.ToString("C")
                );
            }
            
            // Nếu không có đặt phòng nào
            if (_bookingRooms.Count == 0)
            {
                dgvListRoom.Visible = false;
                lblBookRoomCount.Text = "0";
                return;
            }
            else
            {
                dgvListRoom.Visible = true;
                lblBookRoomCount.Text = _bookingRooms.Count.ToString();
            }
        }

        private void LoadBookingServices(int customerId)
        {
            // Lọc danh sách dịch vụ theo form gọi
            var bllBookingService = new BLL_BookingService();
            var allBookingServices = bllBookingService.GetBookingServicesByCustomerId(customerId);
            
            if (_callerForm is InvoiceForm)
            {
                _bookingServices = allBookingServices.ToList();
            }
            else if (_callerForm is CustomerForm)
            {
                _bookingServices = allBookingServices.Where(s => s.Status == "Booked").ToList();
            }
            else
            {
                _bookingServices = allBookingServices;
            }

            if (_bookingServices == null || _bookingServices.Count == 0)
            {
                dgvListService.Visible = false;

                lblBookServiceCount.Text = "0";
                return;
            }
            else 
            { 
                dgvListService.Visible = true;
                lblBookServiceCount.Text = _bookingServices.Count.ToString();
            }

            if (dgvListService.Columns.Count == 0)
            {
                dgvListService.Columns.Add("ServiceId", "Service ID");
                dgvListService.Columns.Add("Quantity", "Quantity");
                dgvListService.Columns.Add("ServiceDate", "Service Date");
                dgvListService.Columns.Add("Status", "Status");
                dgvListService.Columns.Add("TotalPrice", "Total Price");
            }

            dgvListService.Rows.Clear();

            foreach (var service in _bookingServices)
            {
                dgvListService.Rows.Add(
                    service.Service_id,
                    service.Quantity,
                    service.Service_date.ToShortDateString(),
                    service.Status,
                    service.TotalPrice.ToString("C")
                );
            }
            dgvListService.Visible = true;
            label6.Visible = true;
        }

        private void SaveInvoiceToFile(int invoiceId)
        {
            try
            {
                // Tạo SaveFileDialog để cho phép người dùng chọn nơi lưu file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveFileDialog.Title = "Lưu hóa đơn";
                    saveFileDialog.FileName = $"Invoice_{invoiceId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Chuẩn bị nội dung hóa đơn
                        StringBuilder sb = new StringBuilder();
                        
                        // Thêm tiêu đề
                        sb.AppendLine("==================================================");
                        sb.AppendLine("                 HÓA ĐƠN THANH TOÁN               ");
                        sb.AppendLine("==================================================");
                        sb.AppendLine();
                        
                        // Thông tin khách hàng
                        sb.AppendLine($"Mã hóa đơn: {invoiceId}");
                        sb.AppendLine($"Ngày thanh toán: {DateTime.Now}");
                        sb.AppendLine();
                        sb.AppendLine($"Khách hàng: {txtName.Text}");
                        sb.AppendLine($"CCCD: {txtCCCD.Text}");
                        sb.AppendLine($"Điện thoại: {txtPhone.Text}");
                        sb.AppendLine();
                        
                        // Thông tin phòng
                        sb.AppendLine("THÔNG TIN PHÒNG:");
                        sb.AppendLine("--------------------------------------------------");
                        sb.AppendLine(String.Format("{0,-10} {1,-15} {2,-15} {3,-15}", 
                            "Phòng", "Ngày nhận", "Ngày trả", "Tổng tiền"));
                        
                        foreach (var room in _bookingRooms)
                        {
                            sb.AppendLine(String.Format("{0,-10} {1,-15} {2,-15} {3,-15}", 
                                room.RoomId, 
                                room.CheckIn.ToShortDateString(), 
                                room.CheckOut.ToShortDateString(), 
                                room.TotalPrice.ToString("N0") + " VND"));
                        }
                        sb.AppendLine();
                        
                        // Thông tin dịch vụ nếu có
                        if (_bookingServices != null && _bookingServices.Count > 0)
                        {
                            sb.AppendLine("THÔNG TIN DỊCH VỤ:");
                            sb.AppendLine("--------------------------------------------------");
                            sb.AppendLine(String.Format("{0,-10} {1,-10} {2,-15} {3,-15}", 
                                "Dịch vụ", "Số lượng", "Ngày sử dụng", "Tổng tiền"));
                            
                            foreach (var service in _bookingServices)
                            {
                                sb.AppendLine(String.Format("{0,-10} {1,-10} {2,-15} {3,-15}", 
                                    service.Service_id, 
                                    service.Quantity, 
                                    service.Service_date.ToShortDateString(), 
                                    service.TotalPrice.ToString("N0") + " VND"));
                            }
                            sb.AppendLine();
                        }
                        
                        // Tổng tiền
                        decimal totalRoomPrice = _bookingRooms.Sum(r => r.TotalPrice);
                        decimal totalServicePrice = _bookingServices?.Sum(s => s.TotalPrice) ?? 0;
                        decimal grandTotal = totalRoomPrice + totalServicePrice;
                        
                        sb.AppendLine("==================================================");
                        sb.AppendLine($"Tổng tiền phòng:        {totalRoomPrice.ToString("N0")} VND");
                        sb.AppendLine($"Tổng tiền dịch vụ:      {totalServicePrice.ToString("N0")} VND");
                        sb.AppendLine($"TỔNG THANH TOÁN:        {grandTotal.ToString("N0")} VND");
                        sb.AppendLine("==================================================");
                        sb.AppendLine();
                        sb.AppendLine("             Cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi!");
                        sb.AppendLine("                    Hẹn gặp lại quý khách lần sau!");
                        
                        // Lưu vào file
                        File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                        
                        MessageBox.Show($"Đã lưu hóa đơn vào file:\n{saveFileDialog.FileName}", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hóa đơn: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadBooking_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra tên khách hàng
                if (string.IsNullOrEmpty(txtName.Text.Trim()) || string.IsNullOrEmpty(txtCCCD.Text.Trim()) || string.IsNullOrEmpty(txtPhone.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Tìm khách hàng theo tên
                BLL_TTKH bLL_TTKH = new BLL_TTKH();
                var customer = bLL_TTKH.GetCustomerByNameAndCCCD(txtName.Text.Trim(), txtCCCD.Text.Trim());
                
                if (customer == null)
                {
                    MessageBox.Show("Không tìm thấy khách hàng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Tải danh sách đặt phòng và dịch vụ của khách hàng
                int customerId = customer.CustomerId;
                LoadBookingRooms(customerId);
                LoadBookingServices(customerId);
                
                // Tính toán lại tổng tiền
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
