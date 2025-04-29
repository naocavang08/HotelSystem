using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystem.Model
{
    public class HotelDB : CreateDatabaseIfNotExists<DBHotelSystem> 
    {
        protected override void Seed (DBHotelSystem db)
        {
            if (!db.Users.Any(u => u.username == "admin"))
            {
                db.Users.Add(new User {
                    username = "admin", 
                    password = "admin", 
                    role = "Admin",
                    status = "Active",
                    date_register = DateTime.Now
                });
            }

            if (!db.RoomTypes.Any())
            {
                db.RoomTypes.Add(new RoomType { room_type = "VIP", price = 1200000 });
                db.RoomTypes.Add(new RoomType { room_type = "Deluxe", price = 800000 });
                db.RoomTypes.Add(new RoomType { room_type = "Standard", price = 500000 });
            }

            base.Seed(db);
        }
    }
}