using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HotelSystem.DAL
{
    public class DAL_BookingRoom
    {
        private DBHotelSystem db = new DBHotelSystem();

        // Lấy danh sách phòng trống
        public List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return db.Rooms
                .Where(r => r.status == "available" && r.roomtype_id == roomTypeId &&
                            !db.Bookings.Any(b => b.room_id == r.room_id &&
                                                  ((b.check_in < checkOut && b.check_out > checkIn))))
                .ToList();
        }
        public List<BookingRoom> GetBookingRoomsByCustomerId(int customerId)
        {
           return db.Bookings
                .Where(b => b.customer_id == customerId)
                .ToList();
        }

        // Thêm booking mới
        public void AddBooking(BookingRoom booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
        
        // Cập nhật booking
        public void UpdateBooking(BookingRoom booking)
        {
            using (var db = new DBHotelSystem())
            {
                var existingBooking = db.Bookings.Find(booking.booking_id);
                if (existingBooking != null)
                {
                    // Cập nhật thông tin
                    existingBooking.customer_id = booking.customer_id;
                    existingBooking.room_id = booking.room_id;
                    existingBooking.check_in = booking.check_in;
                    existingBooking.check_out = booking.check_out;
                    existingBooking.status = booking.status;
                    existingBooking.total_price = booking.total_price;
                    
                    db.SaveChanges();
                }
            }
        }
        
        // Cập nhật trạng thái booking (từ Booked sang Payment hoặc các trạng thái khác)
        public bool UpdateStatus(int bookingId, string newStatus)
        {
            
            try
            {
                var existingBooking = db.Bookings.Find(bookingId);
                if (existingBooking != null)
                {
                    existingBooking.status = newStatus;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating booking status: {ex.Message}");
            }
            
            
            return false; 
        }
        
        // Lấy các booking trùng với khoảng thời gian đã chọn
        public List<BookingRoom> GetOverlappingBookings(DateTime checkIn, DateTime checkOut)
        {
            using (var db = new DBHotelSystem())
            {
                // Một booking chỉ trùng lịch nếu:
                // - Check-in của booking < Check-out của tìm kiếm VÀ 
                // - Check-out của booking > Check-in của tìm kiếm
                // CHÚ Ý: Đây là điều kiện đã đúng về mặt logic, nhưng chúng ta cần lọc thêm để 
                // 2 booking có thể nối lưng nhau (khi check-out = check-in)
                
                DateTime checkInDate = checkIn.Date;
                
                return db.Bookings
                    .Where(b => 
                        (b.check_in < checkOut && b.check_out > checkIn) && 
                        
                        !(DbFunctions.TruncateTime(b.check_out) == DbFunctions.TruncateTime(checkIn)) &&
                        
                        b.status != "Checked Out"
                    )
                    .ToList();
            }
        }
        
        // Lấy các booking đã hết hạn (quá ngày check-out nhưng vẫn ở trạng thái "Booked")
        public List<BookingRoom> GetExpiredBookings(DateTime searchDate)
        {
            using (var db = new DBHotelSystem())
            {
                // Tìm các booking mà ngày checkout <= ngày tìm kiếm và trạng thái vẫn là "Booked"
                return db.Bookings
                    .Where(b => b.check_out <= searchDate && b.status == "Booked")
                    .ToList();
            }
        }
        
        // Cập nhật trạng thái các booking đã hết hạn và trạng thái phòng tương ứng
        public void UpdateExpiredBookingsAndRooms(List<BookingRoom> expiredBookings)
        {
            if (expiredBookings == null || !expiredBookings.Any())
                return;
                
            using (var db = new DBHotelSystem())
            {
                foreach (var expiredBooking in expiredBookings)
                {
                    var booking = db.Bookings.Find(expiredBooking.booking_id);
                    if (booking != null)
                    {
                        booking.status = "Checked Out";
                        
                        var room = db.Rooms.FirstOrDefault(r => r.room_id == booking.room_id);
                        if (room != null)
                        {
                            room.status = "Available";
                        }
                    }
                }
                
                db.SaveChanges();
            }
        }
    }
}
