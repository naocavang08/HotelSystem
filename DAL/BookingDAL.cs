using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DAL
{
    public class BookingDAL
    {
        private DBHotelSystem db = new DBHotelSystem();

        public List<Room> GetAvailableRoom(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return db.Rooms
                .Where(r => r.roomtype_id == roomTypeId && r.status == "Available" && 
                !db.Bookings.Any(b => b.room_id == r.room_id && ((b.check_in <= checkIn && b.check_out >= checkIn) || (b.check_in <= checkOut && b.check_out >= checkOut))))
                .ToList();
        }
        public void AddBooking(Booking booking)
        {
            db.Bookings.Add(booking);
            db.SaveChanges();
        }
    }
}
