namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkSchedules", "shift_date", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkSchedules", "shift_date", c => c.DateTime(nullable: false));
        }
    }
}
