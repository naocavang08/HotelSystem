using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class DTO_Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; } 
        public DateTime CheckOut { get; set; } 
        public string Status { get; set; } 
        public decimal TotalPrice { get; set; }
    }
}
