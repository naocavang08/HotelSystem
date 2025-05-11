using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class BookingService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookingService()
        {
            this.Invoices = new HashSet<Invoice>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int booking_service_id { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int customer_id { get; set; }
        [Required]
        [ForeignKey("Service")]
        public int service_id { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public DateTime service_date { get; set; }
        [Required]
        public decimal total_price { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
