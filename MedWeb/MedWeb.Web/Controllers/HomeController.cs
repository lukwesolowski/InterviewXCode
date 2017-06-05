using Autofac;
using MedWeb.DA.Interfaces;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        private IPatientRepository _patientRepository;

        public HomeController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _patientRepository = scope.Resolve<IPatientRepository>();
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            string applicationInfo = "Aplikacja rekrutacyjna XCode";
            ViewBag.Message = applicationInfo;

            return View();
        }
    }
}