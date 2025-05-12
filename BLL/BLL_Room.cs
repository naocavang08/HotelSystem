using HotelSystem.DAL;
using HotelSystem.DTO;
using HotelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.BLL
{
    public class BLL_Room
    {
        private DAL_Room dalRoom = new DAL_Room();
        public List<DTO_Room> GetAllRooms()
        {
            var rooms = dalRoom.GetAllRooms();
            if (rooms == null || !rooms.Any())
            {
                return new List<DTO_Room>();
            }
            return rooms.Select(r => new DTO_Room
            {
                RoomId = r.room_id,
                RoomName = r.room_number,
                RoomType = r.RoomType?.room_type, 
                Price = r.RoomType?.price ?? 0, 
                Status = r.status
            }).ToList();
        }
    }
}
