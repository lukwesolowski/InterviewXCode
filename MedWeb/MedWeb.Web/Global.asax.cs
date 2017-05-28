using Autofac;
using MedWeb.Web.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MedWeb.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IContainer DepedencyResolver;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DepedencyResolverConfig.RegisterDepedency();
        }
    }
}