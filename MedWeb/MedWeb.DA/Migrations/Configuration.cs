namespace MedWeb.DA.Migrations
{
    using MedWeb.DA.Tables;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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

            //var firstSepcializaton = new Specialization { Name = "Spec1", InsertTime = DateTime.Now };
            //var secondSpecialization = new Specialization { Name = "Spec2", InsertTime = DateTime.Now };

            //context.Specialization.Add(firstSepcializaton);
            //context.Specialization.Add(secondSpecialization);

            //context.Doctor.AddOrUpdate(
            //    new Doctor { FirstName = "Jan", LastName = "Kowalski", SpecializationId = firstSepcializaton.Id },
            //    new Doctor { FirstName = "Eugeniusz", LastName = "Poljanowicz", SpecializationId = secondSpecialization.Id }
            //    );

            var store = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(store);
            ApplicationUser user = new ApplicationUser
            {
                Email = "admin@gmail.com",
                PasswordHash = um.PasswordHasher.HashPassword("!QAZ2wsx"),
                EmailConfirmed = true,
                UserName = "admin@gmail.com"
            };

            um.Create(user);
            um.AddToRole(user.Id, "Administrator");

            context.SaveChanges();
        }
    }
}