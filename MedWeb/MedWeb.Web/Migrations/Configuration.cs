namespace MedWeb.Web.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MedWeb.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MedWeb.Web.Models.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Administrator" }
                );
        }
    }
}
