using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class BookingServiceView
    {
        public int Booking_service_id { get; set; }
        public string ServiceName { get; set; }
        public int Quantity { get; set; }
        public DateTime Service_date { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
