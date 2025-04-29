using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.DAL;
using HotelSystem.Model;
using HotelSystem.DTO;
using System.ComponentModel.DataAnnotations;

namespace HotelSystem.BLL
{
    public class BookingBLL
    {
        private BookingDAL bookingDAL = new BookingDAL();
        public List<Room> GetAvailableRoom(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return bookingDAL.GetAvailableRoom(roomTypeId, checkIn, checkOut);
        }
        public void AddBooking(BookingDTO bookingDTO)
        {
            var booking = new Booking
            {
                customer_id = bookingDTO.CustomerId,
                room_id = bookingDTO.RoomId,
                check_in = bookingDTO.CheckIn,
                check_out = bookingDTO.CheckOut,
                status = bookingDTO.Status,
                total_price = bookingDTO.TotalPrice
            };
            bookingDAL.AddBooking(booking);
        }
    }
}
