using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public static class DatabaseMigrationHelper
    {
        // Lưu trữ dữ liệu đã sao lưu
        private static List<User> _users = new List<User>();
        private static List<Customer> _customers = new List<Customer>();
        private static List<BookingRoom> _bookings = new List<BookingRoom>();
        private static List<BookingService> _bookingServices = new List<BookingService>();
        private static List<Room> _rooms = new List<Room>();
        private static List<RoomType> _roomTypes = new List<RoomType>();
        private static List<Service> _services = new List<Service>();
        private static List<Employee> _employees = new List<Employee>();
        private static List<WorkSchedule> _workSchedules = new List<WorkSchedule>();
        private static List<RoomHistory> _roomHistories = new List<RoomHistory>();
        private static List<Invoice> _invoices = new List<Invoice>();

        // Sao lưu dữ liệu từ database
        public static void BackupData()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Tắt tạm thời database initializer để đảm bảo không gặp vấn đề khi đọc dữ liệu
                    Database.SetInitializer<DBHotelSystem>(null);
                    
                    // Sao lưu dữ liệu từ cơ sở dữ liệu
                    _users = db.Users.ToList();
                    _customers = db.Customers.ToList();
                    _roomTypes = db.RoomTypes.ToList();
                    _rooms = db.Rooms.ToList();
                    _bookings = db.Bookings.ToList();
                    _services = db.Services.ToList();
                    _bookingServices = db.BookingServices.ToList();
                    _employees = db.Employees.ToList();
                    _workSchedules = db.WorkSchedules.ToList();
                    _roomHistories = db.RoomHistories.ToList();
                    _invoices = db.Invoices.ToList();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi sao lưu dữ liệu: {ex.Message}", "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        // Khôi phục dữ liệu đã sao lưu
        public static void RestoreData()
        {
            try
            {
                using (var db = new DBHotelSystem())
                {
                    // Khôi phục dữ liệu theo thứ tự đúng để đảm bảo tính toàn vẹn tham chiếu
                    
                    // 1. Khôi phục RoomTypes
                    foreach (var roomType in _roomTypes)
                    {
                        if (!db.RoomTypes.Any(r => r.room_type_id == roomType.room_type_id))
                        {
                            db.RoomTypes.Add(roomType);
                        }
                    }
                    db.SaveChanges();

                    // 2. Khôi phục Users
                    foreach (var user in _users)
                    {
                        if (!db.Users.Any(u => u.user_id == user.user_id))
                        {
                            db.Users.Add(user);
                        }
                    }
                    db.SaveChanges();

                    // 3. Khôi phục Rooms
                    foreach (var room in _rooms)
                    {
                        if (!db.Rooms.Any(r => r.room_id == room.room_id))
                        {
                            db.Rooms.Add(room);
                        }
                    }
                    db.SaveChanges();

                    // 4. Khôi phục Customers
                    foreach (var customer in _customers)
                    {
                        if (!db.Customers.Any(c => c.customer_id == customer.customer_id))
                        {
                            db.Customers.Add(customer);
                        }
                    }
                    db.SaveChanges();

                    // 5. Khôi phục Employees
                    foreach (var employee in _employees)
                    {
                        if (!db.Employees.Any(e => e.employee_id == employee.employee_id))
                        {
                            db.Employees.Add(employee);
                        }
                    }
                    db.SaveChanges();

                    // 6. Khôi phục Bookings
                    foreach (var booking in _bookings)
                    {
                        if (!db.Bookings.Any(b => b.booking_id == booking.booking_id))
                        {
                            db.Bookings.Add(booking);
                        }
                    }
                    db.SaveChanges();

                    // 7. Khôi phục Services
                    foreach (var service in _services)
                    {
                        if (!db.Services.Any(s => s.service_id == service.service_id))
                        {
                            db.Services.Add(service);
                        }
                    }
                    db.SaveChanges();
                    
                    // 8. Khôi phục BookingServices
                    foreach (var bookingService in _bookingServices)
                    {
                        if (!db.BookingServices.Any(bs => bs.booking_service_id == bookingService.booking_service_id))
                        {
                            db.BookingServices.Add(bookingService);
                        }
                    }
                    db.SaveChanges();

                    // 9. Khôi phục WorkSchedules
                    foreach (var workSchedule in _workSchedules)
                    {
                        if (!db.WorkSchedules.Any(ws => ws.schedule_id == workSchedule.schedule_id))
                        {
                            db.WorkSchedules.Add(workSchedule);
                        }
                    }
                    db.SaveChanges();

                    // 10. Khôi phục RoomHistories
                    foreach (var roomHistory in _roomHistories)
                    {
                        if (!db.RoomHistories.Any(rh => rh.history_id == roomHistory.history_id))
                        {
                            db.RoomHistories.Add(roomHistory);
                        }
                    }
                    db.SaveChanges();

                    // 11. Khôi phục Invoices với quan hệ nhiều-nhiều mới
                    foreach (var oldInvoice in _invoices)
                    {
                        if (!db.Invoices.Any(i => i.invoice_id == oldInvoice.invoice_id))
                        {
                            // Tạo invoice mới với cấu trúc đã cập nhật
                            var newInvoice = new Invoice
                            {
                                invoice_id = oldInvoice.invoice_id,
                                payment_status = oldInvoice.payment_status,
                                payment_date = oldInvoice.payment_date,
                                total_amount = oldInvoice.total_amount
                            };

                            // Tìm booking liên quan
                            var booking = db.Bookings.FirstOrDefault(b => b.booking_id == oldInvoice.booking_id);
                            if (booking != null)
                            {
                                newInvoice.Bookings.Add(booking);
                            }

                            // Tìm booking service liên quan nếu có
                            if (oldInvoice.booking_service_id != null)
                            {
                                var bookingService = db.BookingServices.FirstOrDefault(bs => bs.booking_service_id == oldInvoice.booking_service_id);
                                if (bookingService != null)
                                {
                                    newInvoice.BookingServices.Add(bookingService);
                                }
                            }

                            db.Invoices.Add(newInvoice);
                        }
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi khi khôi phục dữ liệu: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
} 