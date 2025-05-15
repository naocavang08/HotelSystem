using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelSystem.BLL
{
    public class BLL_BookingRoom
    {
        private DAL_BookingRoom dalBookingRoom = new DAL_BookingRoom();
        private BLL_Room bllRoom = new BLL_Room();

        // Lấy danh sách phòng trống
        public List<Room> GetAvailableRooms(int roomTypeId, DateTime checkIn, DateTime checkOut)
        {
            return dalBookingRoom.GetAvailableRooms(roomTypeId, checkIn, checkOut);
        }
        // Lấy danh sách booking của khách hàng theo ID
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
            var booking = new BookingRoom
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
        
        // Cập nhật booking
        public void UpdateBooking(DTO_BookingRoom dtoBooking)
        {
            var booking = new BookingRoom
            {
                booking_id = dtoBooking.BookingId,
                customer_id = dtoBooking.CustomerId,
                room_id = dtoBooking.RoomId,
                check_in = dtoBooking.CheckIn,
                check_out = dtoBooking.CheckOut,
                status = dtoBooking.Status,
                total_price = dtoBooking.TotalPrice
            };

            dalBookingRoom.UpdateBooking(booking);
        }
        
        // Cập nhật trạng thái booking
        public bool UpdateBookingStatus(int bookingId, string newStatus)
        {
            return dalBookingRoom.UpdateStatus(bookingId, newStatus);
        }
        
        // Chuyển trạng thái từ Booked sang Payment
        public bool SetBookingToPayment(int bookingId)
        {
            return dalBookingRoom.UpdateStatus(bookingId, "Payment");
        }
        
        // Lấy danh sách ID các phòng đã được đặt trong một khoảng thời gian
        public HashSet<int> GetBookedRoomIds(DateTime checkIn, DateTime checkOut)
        {
            HashSet<int> bookedRoomIds = new HashSet<int>();
            
            // Không tự động cập nhật trạng thái booking nữa
            // Thay vào đó, chỉ lấy các booking mà thời gian trùng với thời gian tìm kiếm
            var overlappingBookings = dalBookingRoom.GetOverlappingBookings(checkIn, checkOut);
            
            // Thêm ID của các phòng đã được đặt vào HashSet
            foreach (var booking in overlappingBookings)
            {
                // Chỉ xem xét các booking với trạng thái "Booked" hoặc "Checked In"
                if ((booking.status == "Booked" || booking.status == "Checked In"))
                {
                    // Nếu ngày check-out của booking <= ngày check-in của tìm kiếm
                    // thì booking đó đã hết hạn (theo ngầm định) và phòng có thể đặt được
                    if (booking.check_out > checkIn)
                    {
                        bookedRoomIds.Add(booking.room_id);
                    }
                }
            }
            
            return bookedRoomIds;
        }
        
        // Giữ phương thức này để sử dụng khi cần (ví dụ: khi admin muốn cập nhật hệ thống)
        private void UpdateExpiredBookings(DateTime searchDate)
        {
            try
            {
                // Lấy danh sách các booking mà ngày checkout <= ngày tìm kiếm và trạng thái vẫn là "Booked"
                var expiredBookings = dalBookingRoom.GetExpiredBookings(searchDate);
                
                // Cập nhật trạng thái các booking đã hết hạn và phòng tương ứng
                if (expiredBookings.Any())
                {
                    dalBookingRoom.UpdateExpiredBookingsAndRooms(expiredBookings);
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine($"Error updating expired bookings: {ex.Message}");
            }
        }
    }
}
