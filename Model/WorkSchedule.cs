using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class WorkSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int schedule_id { get; set; }
        [ForeignKey("Employee")]
        [Required]
        public int employee_id { get; set; }
        [Required]
        public System.DateTime shift_date { get; set; }
        [Required]
        public string shift_time { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
