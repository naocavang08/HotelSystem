using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class DTO_BookingService
    {
        public int Booking_service_id { get; set; }
        public int CustomerId { get; set; }
        public int Service_id { get; set; }
        public int Quantity { get; set; }
        public DateTime Service_date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
