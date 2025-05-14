using HotelSystem.View.AdminForm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class BookingRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookingRoom()
        {
            this.Invoices = new HashSet<Invoice>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int booking_id { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int customer_id { get; set; }
        [Required]
        [ForeignKey("Room")]
        public int room_id { get; set; }
        [Required]
        public System.DateTime check_in { get; set; }
        [Required]
        public System.DateTime check_out { get; set; }
        [Required]
        [StringLength(50)]
        public string status { get; set; } // booked, checked in, checked out
        [Required]
        [Column("total_price")]
        public decimal total_price { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
