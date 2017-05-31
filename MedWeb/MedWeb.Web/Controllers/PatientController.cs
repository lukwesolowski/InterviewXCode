using MedWeb.DA.Interfaces;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class PatientController : Controller
    {
        private IPatientRepository _patientRepository;

        private PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public ActionResult ListOfPatients()
        {
            return Content("List Of Patients");
        }

        public ActionResult ReadOnlyListOfPatients()
        {
            return Content("Read Only List Patients");
        }

        public ActionResult DetailsOfPatient()
        {
            return Content("Details Of Patient");
        }

        public ActionResult PatientForm()
        {
            return Content("Patient Form");
        }
    }
}