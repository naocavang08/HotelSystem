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

        // Lấy booking theo ID
        public BookingRoom GetBookingById(int bookingId)
        {
            return db.Bookings.FirstOrDefault(b => b.booking_id == bookingId);
        }
        
        // Lấy danh sách phòng trống
        public List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return db.Rooms
                .Where(r => r.status == "Available" && r.roomtype_id == roomTypeId &&
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
    }
}
