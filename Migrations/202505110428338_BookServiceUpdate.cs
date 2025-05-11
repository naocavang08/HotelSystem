namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookServiceUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingServices", "service_date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingServices", "service_date");
        }
    }
}
