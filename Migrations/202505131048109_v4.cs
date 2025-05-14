namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Bookings", newName: "BookingRooms");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.BookingRooms", newName: "Bookings");
        }
    }
}
