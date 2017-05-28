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
            string countOfPatient = _patientRepository.GetAllPatients().Count.ToString();
            ViewBag.Message = "Liczba pacjentow " + countOfPatient;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}