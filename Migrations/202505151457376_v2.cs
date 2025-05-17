namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "id" });
            RenameColumn(table: "dbo.Customers", name: "id", newName: "User_id");
            AlterColumn("dbo.Customers", "User_id", c => c.Int());
            CreateIndex("dbo.Customers", "User_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "User_id" });
            AlterColumn("dbo.Customers", "User_id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Customers", name: "User_id", newName: "id");
            CreateIndex("dbo.Customers", "id");
        }
    }
}
