using MedWeb.DA.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedWeb.DA
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<RegisteredVisit> RegisteredVisit { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasKey(x => x.Id);

            modelBuilder.Entity<Specialization>().HasKey(x => x.Id);

            modelBuilder.Entity<Doctor>().HasKey(x => x.Id);
            modelBuilder.Entity<Doctor>().HasRequired(x => x.Specialization).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Entity<RegisteredVisit>().HasKey(x => x.Id);
            modelBuilder.Entity<RegisteredVisit>().HasRequired(x => x.Patient).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<RegisteredVisit>().HasRequired(x => x.Doctor).WithMany().WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}