using MedWeb.DA.Interfaces;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class RegisteredVisitController : Controller
    {
        private IRegisteredVisitRepository _registeredVisitRepository;

        private RegisteredVisitController(IRegisteredVisitRepository registeredVisitRepository)
        {
            _registeredVisitRepository = registeredVisitRepository;
        }

        public ActionResult ListOfRegisteredVisits()
        {
            return Content("List Of Registered Visits");
        }

        public ActionResult ReadOnlyListOfRegisteredVisits()
        {
            return Content("Read Only List Registered Visits");
        }

        public ActionResult DetailsOfRegisteredVisit()
        {
            return Content("Details Of Registered Visit");
        }

        public ActionResult RegisteredVisitForm()
        {
            return Content("Registered Visit Form");
        }
    }
}