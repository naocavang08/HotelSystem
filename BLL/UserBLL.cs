using HotelSystem.DAL;
using HotelSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.BLL
{
    class UserBLL
    {
        private UserDAL userDAL = new UserDAL();

        public UserDTO Login(string username, string password)
        {
            var user = userDAL.GetUserByUsername(username);
            if (user == null) return null;

            if (user.Password == password) // Có thể hash password nếu cần
                return user;

            return null;
        }
    }
}
