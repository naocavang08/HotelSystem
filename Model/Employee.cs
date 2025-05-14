using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.WorkSchedules = new HashSet<WorkSchedule>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employee_id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string name { get; set; }
        [Required]
        [StringLength(15)]
        public string phone { get; set; }
        [Required]
        [StringLength(20)]
        public string cccd { get; set; }
        [Column(TypeName = "bit")]
        public Nullable<bool> gender { get; set; }
        [Required]
        [StringLength(50)]
        public string position { get; set; }
        [Required]
        public decimal salary { get; set; }
        [ForeignKey("User")]
        public int id { get; set; }

        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
