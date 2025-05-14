using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSystem.Model;
using HotelSystem.DTO;

namespace HotelSystem.DAL
{
    class UserDAL
    {
        private DBHotelSystem db = new DBHotelSystem();
        public UserDTO GetUserByUsername(string username)
        {
            var user = db.Users.FirstOrDefault(u => u.username == username);
            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.id,
                Username = user.username,
                Password = user.password,
                Role = user.role
            };
        }
    }
}
