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
                    
                    // Tiếp tục xử lý thanh toán nếu chưa thanh toán
                    // Cập nhật trạng thái phòng
                    var bllRoom = new BLL_Room();
                    foreach (var room in _bookingRooms)
                    {
                        bllRoom.UpdateRoomStatus(room.RoomId, "Available");
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

                    // Quay lại màn hình khách hàng
                    CustomerForm customerForm = new CustomerForm();
                    customerForm.Show();
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
                        sb.AppendLine($"Khách hàng: {lbCusName.Text}");
                        sb.AppendLine($"CCCD: {lbCusCCCD.Text}");
                        sb.AppendLine($"Điện thoại: {lbCusPhone.Text}");
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
    }
}
