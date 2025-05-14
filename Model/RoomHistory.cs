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
    public class RoomHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int history_id { get; set; }
        [ForeignKey("Room")]
        [Required]
        public int room_id { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int customer_id { get; set; }
        [Required]
        public System.DateTime check_in { get; set; }
        [Required]
        public System.DateTime check_out { get; set; }
        [Required]
        [StringLength(50)]
        public string status_after { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
    }
}
