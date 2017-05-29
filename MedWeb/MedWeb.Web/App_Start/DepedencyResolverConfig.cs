using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Repositories;

namespace MedWeb.Web.App_Start
{
    public class DepedencyResolverConfig
    {
        public static void RegisterDepedency()
        {
            var builder = new ContainerBuilder();

            // Rejestracja kontenera
            builder.RegisterType<PatientRepository>().As<IPatientRepository>();
            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>();
            builder.RegisterType<RegisteredVisitRepository>().As<IRegisteredVisitRepository>();

            MvcApplication.DepedencyResolver = builder.Build();
        }
    }
}