using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Repositories;

namespace MedWeb.Web.App_Start
{
    public class DepedencyResolverConfig
    {
        public static void RegisterDepedency()
        {
            var bulider = new ContainerBuilder();

            // Rejestracja kontenera
            bulider.RegisterType<PatientRepository>().As<IPatientRepository>();

            MvcApplication.DepedencyResolver = bulider.Build();
        }
    }
}