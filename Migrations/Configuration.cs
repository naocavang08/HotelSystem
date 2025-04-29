namespace HotelSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelSystem.Model.DBHotelSystem>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "HotelSystem.Model.DBHotelSystem";
        }

        protected override void Seed(HotelSystem.Model.DBHotelSystem context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
