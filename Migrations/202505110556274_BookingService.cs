namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingServices", "customer_id", c => c.Int(nullable: false));
            CreateIndex("dbo.BookingServices", "customer_id");
            AddForeignKey("dbo.BookingServices", "customer_id", "dbo.Customers", "customer_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingServices", "customer_id", "dbo.Customers");
            DropIndex("dbo.BookingServices", new[] { "customer_id" });
            DropColumn("dbo.BookingServices", "customer_id");
        }
    }
}
