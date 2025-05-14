using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HotelSystem.DTO
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int BookingServiceId { get; set; } 
        public string CustomerName { get; set; }
        public string RoomNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}

