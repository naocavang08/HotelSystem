using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;

namespace HotelSystem.BLL
{
    public class BLL_BookingRoom
    {
        private DAL_BookingRoom dalBookingRoom = new DAL_BookingRoom();

        // Lấy danh sách phòng trống
        public List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return dalBookingRoom.GetAvailableRooms(roomTypeId, checkIn, checkOut);
        }

        public List<DTO_BookingRoom> GetBookingRoomsByCustomerId(int customerId)
        {
            var bookingRooms = dalBookingRoom.GetBookingRoomsByCustomerId(customerId);
            var dtoBookingRooms = new List<DTO_BookingRoom>();

            foreach (var br in bookingRooms)
            {
                dtoBookingRooms.Add(new DTO_BookingRoom
                {
                    BookingId = br.booking_id,
                    CustomerId = br.customer_id,
                    RoomId = br.room_id,
                    CheckIn = br.check_in,
                    CheckOut = br.check_out,
                    Status = br.status,
                    TotalPrice = br.total_price
                });
            }

            return dtoBookingRooms;
        }

        // Thêm booking mới
        public void AddBooking(DTO_BookingRoom dtoBooking)
        {
            var booking = new Booking
            {
                customer_id = dtoBooking.CustomerId,
                room_id = dtoBooking.RoomId,
                check_in = dtoBooking.CheckIn,
                check_out = dtoBooking.CheckOut,
                status = dtoBooking.Status,
                total_price = dtoBooking.TotalPrice
            };

            dalBookingRoom.AddBooking(booking);
        }
    }
}
