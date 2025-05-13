using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string CCCD { get; set; }
        public bool? Gender { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int UserId { get; set; }

        // Additional properties for user data
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public DateTime? DateRegister { get; set; }

        // Default constructor
        public EmployeeDTO() { }

        // Constructor with parameters
        public EmployeeDTO(int employeeId, string name, string phone, string cccd, bool? gender,
                          string position, decimal salary, int userId)
        {
            EmployeeId = employeeId;
            Name = name;
            Phone = phone;
            CCCD = cccd;
            Gender = gender;
            Position = position;
            Salary = salary;
            UserId = userId;
        }
    }
}