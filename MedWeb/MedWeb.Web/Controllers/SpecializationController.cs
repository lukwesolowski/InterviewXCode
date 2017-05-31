using MedWeb.DA.Interfaces;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class SpecializationController : Controller
    {
        private ISpecializationRepository _specializationRepository;

        private SpecializationController(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public ActionResult ListOfSpecializations()
        {
            return Content("List Of Specializations");
        }

        public ActionResult ReadOnlyListOfSpecializations()
        {
            return Content("Read Only List Of Specializations");
        }

        public ActionResult DetailsOfSpecialization()
        {
            return Content("Details Of Specialization");
        }

        public ActionResult SpecializationForm()
        {
            return Content("Specialization Form");
        }
    }
}