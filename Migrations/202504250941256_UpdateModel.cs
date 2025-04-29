namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "booking_id", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.RoomHistories", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "id", "dbo.Users");
            DropForeignKey("dbo.RoomHistories", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "roomtype_id", "dbo.RoomTypes");
            DropForeignKey("dbo.Employees", "id", "dbo.Users");
            DropForeignKey("dbo.WorkSchedules", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Invoices", "booking_service_id", "dbo.BookingServices");
            DropForeignKey("dbo.BookingServices", "service_id", "dbo.Services");
            AlterColumn("dbo.Bookings", "status", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Bookings", "total_price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Customers", "name", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Customers", "phone", c => c.String(nullable: false, maxLength: 11, unicode: false));
            AlterColumn("dbo.Customers", "cccd", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.RoomHistories", "status_after", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Rooms", "room_number", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Rooms", "status", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.RoomTypes", "room_type", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.RoomTypes", "price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Users", "username", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "password", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "role", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "status", c => c.String(unicode: false));
            AlterColumn("dbo.Employees", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Employees", "phone", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("dbo.Employees", "cccd", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.Employees", "position", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Employees", "salary", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.WorkSchedules", "shift_time", c => c.String(nullable: false, unicode: false));
            AlterColumn("dbo.Invoices", "total_amount", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Invoices", "payment_status", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.BookingServices", "total_price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AlterColumn("dbo.Services", "name", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Services", "price", c => c.Decimal(nullable: false, precision: 10, scale: 2));
            AddForeignKey("dbo.Bookings", "customer_id", "dbo.Customers", "customer_id");
            AddForeignKey("dbo.Invoices", "booking_id", "dbo.Bookings", "booking_id");
            AddForeignKey("dbo.Bookings", "room_id", "dbo.Rooms", "room_id");
            AddForeignKey("dbo.RoomHistories", "customer_id", "dbo.Customers", "customer_id");
            AddForeignKey("dbo.Customers", "id", "dbo.Users", "id");
            AddForeignKey("dbo.RoomHistories", "room_id", "dbo.Rooms", "room_id");
            AddForeignKey("dbo.Rooms", "roomtype_id", "dbo.RoomTypes", "roomtype_id");
            AddForeignKey("dbo.Employees", "id", "dbo.Users", "id");
            AddForeignKey("dbo.WorkSchedules", "employee_id", "dbo.Employees", "employee_id");
            AddForeignKey("dbo.Invoices", "booking_service_id", "dbo.BookingServices", "booking_service_id");
            AddForeignKey("dbo.BookingServices", "service_id", "dbo.Services", "service_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingServices", "service_id", "dbo.Services");
            DropForeignKey("dbo.Invoices", "booking_service_id", "dbo.BookingServices");
            DropForeignKey("dbo.WorkSchedules", "employee_id", "dbo.Employees");
            DropForeignKey("dbo.Employees", "id", "dbo.Users");
            DropForeignKey("dbo.Rooms", "roomtype_id", "dbo.RoomTypes");
            DropForeignKey("dbo.RoomHistories", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.Customers", "id", "dbo.Users");
            DropForeignKey("dbo.RoomHistories", "customer_id", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "room_id", "dbo.Rooms");
            DropForeignKey("dbo.Invoices", "booking_id", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "customer_id", "dbo.Customers");
            AlterColumn("dbo.Services", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Services", "name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.BookingServices", "total_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "payment_status", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Invoices", "total_amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.WorkSchedules", "shift_time", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "salary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Employees", "position", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Employees", "cccd", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Employees", "phone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Employees", "name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "status", c => c.String());
            AlterColumn("dbo.Users", "role", c => c.String());
            AlterColumn("dbo.Users", "password", c => c.String());
            AlterColumn("dbo.Users", "username", c => c.String());
            AlterColumn("dbo.RoomTypes", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.RoomTypes", "room_type", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "status", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "room_number", c => c.String(nullable: false));
            AlterColumn("dbo.RoomHistories", "status_after", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "cccd", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Customers", "phone", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.Customers", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Bookings", "total_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Bookings", "status", c => c.String(nullable: false, maxLength: 50));
            AddForeignKey("dbo.BookingServices", "service_id", "dbo.Services", "service_id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "booking_service_id", "dbo.BookingServices", "booking_service_id", cascadeDelete: true);
            AddForeignKey("dbo.WorkSchedules", "employee_id", "dbo.Employees", "employee_id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "id", "dbo.Users", "id", cascadeDelete: true);
            AddForeignKey("dbo.Rooms", "roomtype_id", "dbo.RoomTypes", "roomtype_id", cascadeDelete: true);
            AddForeignKey("dbo.RoomHistories", "room_id", "dbo.Rooms", "room_id", cascadeDelete: true);
            AddForeignKey("dbo.Customers", "id", "dbo.Users", "id", cascadeDelete: true);
            AddForeignKey("dbo.RoomHistories", "customer_id", "dbo.Customers", "customer_id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "room_id", "dbo.Rooms", "room_id", cascadeDelete: true);
            AddForeignKey("dbo.Invoices", "booking_id", "dbo.Bookings", "booking_id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "customer_id", "dbo.Customers", "customer_id", cascadeDelete: true);
        }
    }
}
