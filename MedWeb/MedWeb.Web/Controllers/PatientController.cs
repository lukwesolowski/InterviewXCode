using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class PatientController : Controller
    {
        private IPatientRepository _patientRepository;

        public PatientController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _patientRepository = scope.Resolve<IPatientRepository>();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page = 0)
        {
            List<Patient> patientsFromDb = _patientRepository.GetAllPatients();
            var viewModel = new List<PatientViewModel>();

            patientsFromDb.ForEach(x =>
            {
                PatientViewModel patient = new PatientViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Pesel = x.Pesel,
                    BirthDate = x.BirthDate,
                    City = x.City,
                    Street = x.Street,
                    ZipCode = x.ZipCode
                };

                viewModel.Add(patient);
            });

            var currentPageInfo = new PageInfo(viewModel.Count(), page);
            var viewPagingModel = new PagingInfoModel
            {
                ItemsOfPatients = viewModel
                .Skip((currentPageInfo.CurrentPage - 1) * currentPageInfo.PageSize)
                .Take(currentPageInfo.PageSize),

                PageInfo = currentPageInfo
            };

            var pagingPatientModel = new PagingPatientViewModel(viewModel, viewPagingModel);

            return View(pagingPatientModel);
        }

        [HttpGet]
        public ActionResult Details(int patientId)
        {
            Patient currentPatient = _patientRepository.DetailsOfPatient(patientId);
            PatientViewModel viewModel = new PatientViewModel
            {
                Id = currentPatient.Id,
                FirstName = currentPatient.FirstName,
                LastName = currentPatient.LastName,
                Pesel = currentPatient.Pesel,
                BirthDate = currentPatient.BirthDate,
                City = currentPatient.City,
                Street = currentPatient.Street,
                ZipCode = currentPatient.ZipCode
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient(PatientViewModel viewModel)
        {
            Patient patient = new Patient
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Pesel = viewModel.Pesel,
                BirthDate = viewModel.BirthDate,
                City = viewModel.City,
                Street = viewModel.Street,
                ZipCode = viewModel.ZipCode
            };

            _patientRepository.AddPatient(patient);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeletePatient(int patientId)
        {
            _patientRepository.DeletePatient(patientId);
            return RedirectToAction("Index");
        }
    }
}