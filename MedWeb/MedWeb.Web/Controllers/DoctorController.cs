using MedWeb.Web.Models;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class DoctorController : Controller
    {
        public ActionResult Random()
        {
            var doctor = new DoctorViewModel() { FirstName = "Jan!" };
            return View();
        }
    }
}