using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    public class DTO_Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
