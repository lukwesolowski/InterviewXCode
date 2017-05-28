using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedWeb.Web.Startup))]

namespace MedWeb.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}