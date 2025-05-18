using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class DTO_Staff
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CCCD { get; set; }
        public bool Gender { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string ShiftDate { get; set; }
        public string ShiftTime { get; set; }
    }
}
