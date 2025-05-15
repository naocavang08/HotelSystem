namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "cccd" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Customers", "cccd", unique: true);
        }
    }
}
