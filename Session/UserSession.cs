using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.DTO;

namespace HotelSystem.Session
{
    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }
        public static void Clear () { 
            UserId = 0;
            Username = null;
            Role = null;
    }
}
