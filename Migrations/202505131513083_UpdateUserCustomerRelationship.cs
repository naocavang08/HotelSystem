namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserCustomerRelationship : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "id", newName: "user_id");
            RenameIndex(table: "dbo.Customers", name: "IX_id", newName: "IX_user_id");
            AddColumn("dbo.Customers", "Customer_customer_id", c => c.Int());
            AlterColumn("dbo.Users", "username", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Users", "role", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.Users", "status", c => c.String(maxLength: 20, unicode: false));
            CreateIndex("dbo.Customers", "Customer_customer_id");
            AddForeignKey("dbo.Customers", "Customer_customer_id", "dbo.Customers", "customer_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Customer_customer_id", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "Customer_customer_id" });
            AlterColumn("dbo.Users", "status", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "role", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "password", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "username", c => c.String(unicode: false));
            DropColumn("dbo.Customers", "Customer_customer_id");
            RenameIndex(table: "dbo.Customers", name: "IX_user_id", newName: "IX_id");
            RenameColumn(table: "dbo.Customers", name: "user_id", newName: "id");
        }
    }
}
