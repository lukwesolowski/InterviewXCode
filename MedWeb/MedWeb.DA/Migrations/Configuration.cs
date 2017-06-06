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
            var firstSepcializaton = new Specialization { Name = "Diabetologia", InsertTime = DateTime.Now };
            var secondSpecialization = new Specialization { Name = "Gastroenterologia ", InsertTime = DateTime.Now };
            var thirdSpecialization = new Specialization { Name = "Diabetologia", InsertTime = DateTime.Now };
            var fourthSpecialization = new Specialization { Name = "Gastroenterologia ", InsertTime = DateTime.Now };

            context.Specialization.Add(firstSepcializaton);
            context.Specialization.Add(secondSpecialization);
            context.Specialization.Add(thirdSpecialization);
            context.Specialization.Add(fourthSpecialization);

            var firstDoctor = new Doctor { FirstName = "Jan", LastName = "Kowalski", SpecializationId = firstSepcializaton.Id };
            var secondDoctor = new Doctor { FirstName = "Zbigniew", LastName = "Nowak", SpecializationId = secondSpecialization.Id };
            var thirdDoctor = new Doctor { FirstName = "Zenon", LastName = "Kwiatkowski", SpecializationId = thirdSpecialization.Id };
            var fourthDoctor = new Doctor { FirstName = "W³adys³aw", LastName = "Norek", SpecializationId = fourthSpecialization.Id };

            context.Doctor.Add(firstDoctor);
            context.Doctor.Add(secondDoctor);
            context.Doctor.Add(thirdDoctor);
            context.Doctor.Add(fourthDoctor);

            var firstPatient = new Patient
            {
                FirstName = "Marian",
                LastName = "Koz³owski",
                Pesel = "88071585321",
                BirthDate = DateTime.ParseExact("1988-07-15", "yyyy-MM-dd",
                                 null),
                City = "Bia³ystok",
                Street = "Pi³sudskiego",
                HouseNumber = "53",
                ZipCode = "15-091"
            };

            var secondPatient = new Patient
            {
                FirstName = "Kazimierz",
                LastName = "Mazur",
                Pesel = "91110865932",
                BirthDate = DateTime.ParseExact("1991-11-08", "yyyy-MM-dd",
                                 null),
                City = "Zakopane",
                Street = "Brukowa",
                HouseNumber = "101",
                ZipCode = "13-153"
            };

            var thirdPatient = new Patient
            {
                FirstName = "Rados³aw",
                LastName = "Olszewski",
                Pesel = "66042584036",
                BirthDate = DateTime.ParseExact("1966-04-25", "yyyy-MM-dd",
                                 null),
                City = "Warszawa",
                Street = "Pozioma",
                HouseNumber = "9",
                ZipCode = "11-378"
            };

            var fourthPatient = new Patient
            {
                FirstName = "Mieczys³aw",
                LastName = "Górski",
                Pesel = "73032285321",
                BirthDate = DateTime.ParseExact("1973-03-22", "yyyy-MM-dd",
                                 null),
                City = "Bia³ystok",
                Street = "Pi³sudskiego",
                HouseNumber = "53",
                ZipCode = "15-091"
            };

            context.Patient.Add(firstPatient);
            context.Patient.Add(secondPatient);
            context.Patient.Add(thirdPatient);
            context.Patient.Add(fourthPatient);

            var firstVisit = new RegisteredVisit
            {
                PatientId = firstPatient.Id,
                DoctorId = fourthDoctor.Id,
                DateTime = DateTime.ParseExact("2017-07-02 15:30", "yyyy-MM-dd H:mm:",
                                 null),
                Complaint = "Ból brzucha"
            };

            var secondVisit = new RegisteredVisit
            {
                PatientId = secondPatient.Id,
                DoctorId = thirdDoctor.Id,
                DateTime = DateTime.ParseExact("2017-06-15 9:00", "yyyy-MM-dd H:mm:",
                                 null),
                Complaint = "Zwichniêta noga"
            };

            var thirdVisit = new RegisteredVisit
            {
                PatientId = thirdPatient.Id,
                DoctorId = secondDoctor.Id,
                DateTime = DateTime.ParseExact("2017-06-29 11:30", "yyyy-MM-dd H:mm:",
                                 null),
                Complaint = "Zapelenie uszu"
            };

            var fourthVisit = new RegisteredVisit
            {
                PatientId = fourthPatient.Id,
                DoctorId = firstDoctor.Id,
                DateTime = DateTime.ParseExact("2017-06-15 12:30", "yyyy-MM-dd H:mm:",
                                 null),
                Complaint = "Nadciœnienie"
            };

            context.RegisteredVisit.Add(firstVisit);
            context.RegisteredVisit.Add(secondVisit);
            context.RegisteredVisit.Add(thirdVisit);
            context.RegisteredVisit.Add(fourthVisit);

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