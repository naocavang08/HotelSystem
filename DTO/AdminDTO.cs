using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    internal class AdminDTO
    {
        // Properties
        public int AdminId { get; set; } // Maps to user_id
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Maps to role
        public string Status { get; set; } // Maps to status
        public DateTime DateRegister { get; set; } // Maps to date_register

        // Default Constructor
        public AdminDTO() { }

        // Constructor with parameters
        public AdminDTO(int adminId, string username, string password, string role, string status, DateTime dateRegister)
        {
            AdminId = adminId;
            Username = username;
            Password = password;
            Role = role;
            Status = status;
            DateRegister = dateRegister;
        }
    }
}
