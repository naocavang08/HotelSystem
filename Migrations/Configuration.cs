namespace HotelSystem.Migrations
{
    using HotelSystem.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelSystem.Model.DBHotelSystem>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HotelSystem.Model.DBHotelSystem";
        }

        protected override void Seed(HotelSystem.Model.DBHotelSystem db)
        {
            if (!db.Users.Any(u => u.username == "admin"))
            {
                db.Users.Add(new User
                {
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
