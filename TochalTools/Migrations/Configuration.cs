namespace TochalTools.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TochalTools.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TochalTools.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Blogs', RESEED, 1000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Products', RESEED, 1000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Comments', RESEED, 1000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Inbox', RESEED, 1000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Orders', RESEED, 100000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('OrderItems', RESEED, 100000)");
        }
    }
}
