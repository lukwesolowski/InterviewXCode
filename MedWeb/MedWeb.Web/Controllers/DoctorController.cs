using System.Web.Mvc;
using MedWeb.Web.Models;

namespace MedWeb.Web.Controllers
{
    public class DoctorController : Controller
    {
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