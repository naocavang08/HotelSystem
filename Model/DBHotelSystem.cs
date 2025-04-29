namespace HotelSystem.Model
{
    using HotelSystem.View.AdminForm;
    using HotelSystem.View.CustomerForm;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Linq;

    public class DBHotelSystem : DbContext
    {
        public DBHotelSystem()
            : base("name=DBHotelSystem")
        {
            Database.SetInitializer<DBHotelSystem>(new HotelDB());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Booking>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BookingService>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<BookingService>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.BookingService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.cccd)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.RoomHistories)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.cccd)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.WorkSchedules)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.total_amount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.payment_status)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.room_number)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomHistories)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RoomHistory>()
                .Property(e => e.status_after)
                .IsUnicode(false);

            modelBuilder.Entity<RoomType>()
                .Property(e => e.room_type)
                .IsUnicode(false);

            modelBuilder.Entity<RoomType>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<RoomType>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.RoomType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.BookingServices)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkSchedule>()
                .Property(e => e.shift_time)
                .IsUnicode(false);
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingService> BookingServices { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomHistory> RoomHistories { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WorkSchedule> WorkSchedules { get; set; }
    }
}