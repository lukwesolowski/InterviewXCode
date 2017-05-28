using Autofac;

namespace MedWeb.Web.App_Start
{
    public class DepedencyResolverConfig
    {
        public static void RegisterDepedency()
        {
            var bulider = new ContainerBuilder();

            // Rejestracja kontenera

            MvcApplication.DepedencyResolver = bulider.Build();
        }
    }
}