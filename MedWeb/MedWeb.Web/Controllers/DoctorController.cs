using MedWeb.DA.Interfaces;
using MedWeb.Web.Models;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;

        private DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public ActionResult ListOfDoctors()
        {
            return Content("List Of Doctors");
        }

        public ActionResult ReadOnlyListOfDoctors()
        {
            return Content("Read Only List Doctors");
        }

        public ActionResult DetailsOfDoctor()
        {
            return Content("Details Of Doctor");
        }

        public ActionResult DoctorForm()
        {
            return Content("Doctor Form");
        }
    }
}