namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingRooms",
                c => new
                    {
                        booking_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        room_id = c.Int(nullable: false),
                        check_in = c.DateTime(nullable: false),
                        check_out = c.DateTime(nullable: false),
                        status = c.String(nullable: false, maxLength: 50, unicode: false),
                        total_price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.booking_id)
                .ForeignKey("dbo.Customers", t => t.customer_id)
                .ForeignKey("dbo.Rooms", t => t.room_id)
                .Index(t => t.customer_id)
                .Index(t => t.room_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 4000),
                        phone = c.String(nullable: false, maxLength: 11, unicode: false),
                        cccd = c.String(nullable: false, maxLength: 20, unicode: false),
                        gender = c.Boolean(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.customer_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.BookingServices",
                c => new
                    {
                        booking_service_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        service_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        service_date = c.DateTime(nullable: false),
                        total_price = c.Decimal(nullable: false, precision: 10, scale: 2),
                        status = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.booking_service_id)
                .ForeignKey("dbo.Customers", t => t.customer_id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.service_id)
                .Index(t => t.customer_id)
                .Index(t => t.service_id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        invoice_id = c.Int(nullable: false, identity: true),
                        total_amount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        payment_status = c.String(nullable: false, maxLength: 50, unicode: false),
                        payment_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.invoice_id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        service_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.service_id);
            
            CreateTable(
                "dbo.RoomHistories",
                c => new
                    {
                        history_id = c.Int(nullable: false, identity: true),
                        room_id = c.Int(nullable: false),
                        customer_id = c.Int(nullable: false),
                        check_in = c.DateTime(nullable: false),
                        check_out = c.DateTime(nullable: false),
                        status_after = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.history_id)
                .ForeignKey("dbo.Rooms", t => t.room_id)
                .ForeignKey("dbo.Customers", t => t.customer_id)
                .Index(t => t.room_id)
                .Index(t => t.customer_id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        room_id = c.Int(nullable: false, identity: true),
                        room_number = c.String(nullable: false, unicode: false),
                        status = c.String(nullable: false, unicode: false),
                        roomtype_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.room_id)
                .ForeignKey("dbo.RoomTypes", t => t.roomtype_id)
                .Index(t => t.roomtype_id);
            
            CreateTable(
                "dbo.RoomTypes",
                c => new
                    {
                        roomtype_id = c.Int(nullable: false, identity: true),
                        room_type = c.String(nullable: false, maxLength: 4000),
                        price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.roomtype_id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        employee_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 100),
                        phone = c.String(nullable: false, maxLength: 15, unicode: false),
                        cccd = c.String(nullable: false, maxLength: 20, unicode: false),
                        gender = c.Boolean(),
                        position = c.String(nullable: false, maxLength: 50, unicode: false),
                        salary = c.Decimal(nullable: false, precision: 10, scale: 2),
                        id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.employee_id)
                .ForeignKey("dbo.Users", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(unicode: false),
                        password = c.String(unicode: false),
                        role = c.String(unicode: false),
                        status = c.String(unicode: false),
                        date_register = c.DateTime(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.WorkSchedules",
                c => new
                    {
                        schedule_id = c.Int(nullable: false, identity: true),
                        employee_id = c.Int(nullable: false),
                        shift_date = c.String(nullable: false),
                        shift_time = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.schedule_id)
                .ForeignKey("dbo.Employees", t => t.employee_id)
                .Index(t => t.employee_id);
            
            CreateTable(
                "dbo.BookingServiceInvoice",
                c => new
                    {
                        booking_service_id = c.Int(nullable: false),
                        invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.booking_service_id, t.invoice_id })
                .ForeignKey("dbo.BookingServices", t => t.booking_service_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .Index(t => t.booking_service_id)
                .Index(t => t.invoice_id);
            
            CreateTable(
                "dbo.BookingInvoice",
                c => new
                    {
                        booking_id = c.Int(nullable: false),
                        invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.booking_id, t.invoice_id })
                .ForeignKey("dbo.BookingRooms", t => t.booking_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.invoice_id, cascadeDelete: true)
                .Index(t => t.booking_id)
                .Index(t => t.invoice_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkSchedules", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "id", "dbo.Users");
            DropForeignKey("dbo.Customers", "User_id", "dbo.Users");
            DropForeignKey("dbo.BookingInvoice", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.BookingInvoice", "booking_id", "dbo.BookingRooms");
            DropForeignKey("dbo.RoomHistories", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Rooms", "roomtype_id", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomHistories", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.BookingRooms", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.BookingServices", "service_id", "dbo.Services");
            DropForeignKey("dbo.BookingServiceInvoice", "invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.BookingServiceInvoice", "booking_service_id", "dbo.BookingServices");
            DropForeignKey("dbo.BookingServices", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.BookingRooms", "customer_id", "dbo.Customers");
            DropIndex("dbo.BookingInvoice", new[] { "invoice_id" });
            DropIndex("dbo.BookingInvoice", new[] { "booking_id" });
            DropIndex("dbo.BookingServiceInvoice", new[] { "invoice_id" });
            DropIndex("dbo.BookingServiceInvoice", new[] { "booking_service_id" });
            DropIndex("dbo.WorkSchedules", new[] { "employee_id" });
            DropIndex("dbo.Employees", new[] { "id" });
            DropIndex("dbo.Rooms", new[] { "roomtype_id" });
            DropIndex("dbo.RoomHistories", new[] { "customer_id" });
            DropIndex("dbo.RoomHistories", new[] { "room_id" });
            DropIndex("dbo.BookingServices", new[] { "service_id" });
            DropIndex("dbo.BookingServices", new[] { "customer_id" });
            DropIndex("dbo.Customers", new[] { "User_id" });
            DropIndex("dbo.BookingRooms", new[] { "room_id" });
            DropIndex("dbo.BookingRooms", new[] { "customer_id" });
            DropTable("dbo.BookingInvoice");
            DropTable("dbo.BookingServiceInvoice");
            DropTable("dbo.WorkSchedules");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.RoomTypes");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomHistories");
            DropTable("dbo.Services");
            DropTable("dbo.Invoices");
            DropTable("dbo.BookingServices");
            DropTable("dbo.Customers");
            DropTable("dbo.BookingRooms");
        }
    }
}
