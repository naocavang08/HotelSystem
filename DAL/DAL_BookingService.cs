using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DAL
{
    public class DAL_BookingService
    {
        private DBHotelSystem db = new DBHotelSystem();

        // Thêm booking service mới
        public void AddBookingService(BookingService bookingService)
        {
            db.BookingServices.Add(bookingService);
            db.SaveChanges();
        }
        // Lấy danh sách booking service
        public List<BookingService> GetBookingServices()
        {
            return db.BookingServices.ToList();
        }
        // Lấy booking service theo ID
        public List<BookingService> GetBookingServicesByServiceId(int serviceId)
        {
            return db.BookingServices.Where(bs => bs.service_id == serviceId).ToList();
        }
        // Xóa dịch vụ đã đặt
        public void DeleteBookingService(int bookingServiceId)
        {
            var bookingService = db.BookingServices.FirstOrDefault(bs => bs.booking_service_id == bookingServiceId);
            if (bookingService != null)
            {
                db.BookingServices.Remove(bookingService);
                db.SaveChanges();
            }
        }
        // Cập nhật dịch vụ đã đặt
        public void UpdateBookingService(BookingService bookingService)
        {
            var existingBookingService = db.BookingServices.FirstOrDefault(bs => bs.booking_service_id == bookingService.booking_service_id);
            if (existingBookingService != null)
            {
                existingBookingService.service_id = bookingService.service_id;
                existingBookingService.customer_id = bookingService.customer_id;
                existingBookingService.quantity = bookingService.quantity;
                existingBookingService.service_date = bookingService.service_date;
                existingBookingService.total_price = bookingService.total_price;
                db.SaveChanges();
            }
        }
    }
}
