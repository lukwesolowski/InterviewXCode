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

        // GET: Doctor
        public ActionResult ListOfDoctors()
        {
            return Content("ListOfDoctors");
        }

        public ActionResult ReadOnlyListOfDoctors()
        {
            return Content("ReadOnlyListDoctors");
        }

        public ActionResult DetailsOfDoctor()
        {
            return Content("DetailsOfDoctor");
        }

        public ActionResult DoctorForm()
        {
            return Content("DoctorForm");
        }
    }
}