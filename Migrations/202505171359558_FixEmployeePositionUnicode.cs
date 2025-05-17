namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEmployeePositionUnicode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "position", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "position", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
