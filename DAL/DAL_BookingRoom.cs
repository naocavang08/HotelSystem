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

        // Thêm booking mới
        public void AddBooking(Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
    }
}
