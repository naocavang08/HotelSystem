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
            using (var context = new DBHotelSystem())
            {
                var existingBooking = context.Bookings.Find(booking.booking_id);
                if (existingBooking != null)
                {
                    // Cập nhật thông tin
                    existingBooking.customer_id = booking.customer_id;
                    existingBooking.room_id = booking.room_id;
                    existingBooking.check_in = booking.check_in;
                    existingBooking.check_out = booking.check_out;
                    existingBooking.status = booking.status;
                    existingBooking.total_price = booking.total_price;
                    
                    context.SaveChanges();
                }
            }
        }
        
        // Cập nhật trạng thái booking (từ Booked sang Payment hoặc các trạng thái khác)
        public bool UpdateStatus(int bookingId, string newStatus)
        {
            using (var context = new DBHotelSystem())
            {
                var existingBooking = context.Bookings.Find(bookingId);
                if (existingBooking != null)
                {
                    // Kiểm tra trạng thái hiện tại
                    if (existingBooking.status == "Booked" && newStatus == "Payment")
                    {
                        // Cập nhật trạng thái
                        existingBooking.status = newStatus;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        // Có thể mở rộng để hỗ trợ các kiểu chuyển đổi trạng thái khác
                        existingBooking.status = newStatus;
                        context.SaveChanges();
                        return true;
                    }
                }
            }
            
            return false; // Không tìm thấy booking hoặc không thể cập nhật trạng thái
        }
    }
}
