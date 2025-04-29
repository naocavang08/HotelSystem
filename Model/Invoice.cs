using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int invoice_id { get; set; }
        [Required]
        [ForeignKey("Booking")]
        public int booking_id { get; set; }
        [Required]
        [ForeignKey("BookingService")]
        public int booking_service_id { get; set; }
        [Required]
        public decimal total_amount { get; set; }
        [Required]
        [StringLength(50)]
        public string payment_status { get; set; }
        public Nullable<System.DateTime> payment_date { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual BookingService BookingService { get; set; }
    }
}
