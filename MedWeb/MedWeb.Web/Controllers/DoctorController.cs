using System.Web.Mvc;
using MedWeb.Web.Models;
using MedWeb.DA.Interfaces;

namespace MedWeb.Web.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;

        DoctorController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        // GET: Doctor
        public ActionResult ListOfDoctors()
        {
            var doctor = new DoctorViewModel()
            {
                FirstName = "Jan",
                LastName = "Kowalski"
            };

            return View(doctor);
        }


    }
}