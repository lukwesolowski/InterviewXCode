namespace MedWeb.DA.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MedWeb.DA.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MedWeb.DA.ApplicationDbContext context)
        {
            //context.Roles.AddOrUpdate(
              //  new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "Administrator" }
                //);

            context.SaveChanges();
        }
    }
}