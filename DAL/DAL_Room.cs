using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DAL
{
    public class DAL_Room
    {
        private DBHotelSystem db = new DBHotelSystem();
        public List<Room> GetAllRooms()
        {
            return db.Rooms.Include("RoomType").ToList();
        }
    }
}
