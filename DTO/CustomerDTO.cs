using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.DTO
{
    class CustomerDTO
    {
        public int customer_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string cccd { get; set; }
        public bool gender { get; set; }
        public string roomNumbers { get; set; }
    }
}
