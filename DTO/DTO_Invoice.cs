using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class DTO_Invoice
    {
        public int InvoiceId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
        public List<int> BookingRoomIds { get; set; }
        public List<int> BookingServiceIds { get; set; }
    }
}
