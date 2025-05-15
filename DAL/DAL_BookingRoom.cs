using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            using (var db = new DBHotelSystem())
            {
                var existingBooking = db.Bookings.Find(bookingId);
                if (existingBooking != null)
                {
                    // Kiểm tra trạng thái hiện tại
                    if (existingBooking.status == "Booked" && newStatus == "Payment")
                    {
                        // Cập nhật trạng thái
                        existingBooking.status = newStatus;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        // Có thể mở rộng để hỗ trợ các kiểu chuyển đổi trạng thái khác
                        existingBooking.status = newStatus;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            
            return false; // Không tìm thấy booking hoặc không thể cập nhật trạng thái
        }
        
        // Lấy các booking trùng với khoảng thời gian đã chọn
        public List<BookingRoom> GetOverlappingBookings(DateTime checkIn, DateTime checkOut)
        {
            using (var db = new DBHotelSystem())
            {
                // Lưu ý: Một booking chỉ trùng lịch nếu:
                // - Ngày check-in của booking < ngày check-out của tìm kiếm VÀ
                // - Ngày check-out của booking > ngày check-in của tìm kiếm
                return db.Bookings
                    .Where(b => 
                        (b.check_in < checkOut && b.check_out > checkIn)
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
                    // Tìm booking trong db hiện tại
                    var booking = db.Bookings.Find(expiredBooking.booking_id);
                    if (booking != null)
                    {
                        // Cập nhật trạng thái booking
                        booking.status = "Checked Out";
                        
                        // Cập nhật trạng thái phòng
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
