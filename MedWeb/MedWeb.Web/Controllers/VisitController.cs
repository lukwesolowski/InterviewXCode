using Autofac;
using MedWeb.BO.Converters;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class VisitController : Controller
    {
        private IRegisteredVisitRepository _registeredVisitRepository;
        private ISpecializationRepository _specializationRepository;
        private IPatientRepository _patientRepository;
        private IDoctorRepository _doctorRepository;
        public int PageSize = 8;

        public VisitController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _registeredVisitRepository = scope.Resolve<IRegisteredVisitRepository>();
                _specializationRepository = scope.Resolve<ISpecializationRepository>();
                _patientRepository = scope.Resolve<IPatientRepository>();
                _doctorRepository = scope.Resolve<IDoctorRepository>();
            }
        }

        // GET: Visit
        [HttpGet]
        public ActionResult Index(int? page = 1)
        {
            List<RegisteredVisit> visitsFromDb = _registeredVisitRepository.GetAllRegisteredVisits();
            var viewModel = new List<RegisteredVisitViewModel>();

            visitsFromDb.ForEach(x =>
            {
                RegisteredVisitViewModel visit = new RegisteredVisitViewModel
                {
                    Id = x.Id,
                    Complaint = x.Complaint,
                    DateTime = x.DateTime,
                    Doctor = x.Doctor,
                    Patient = x.Patient
                };

                viewModel.Add(visit);
            });

            var currentPageInfo = new PageInfo(viewModel.Count(), page);
            var viewPagingModel = new PagingInfoModel
            {
                Items = viewModel
                .Skip((currentPageInfo.CurrentPage - 1) * currentPageInfo.PageSize)
                .Take(currentPageInfo.PageSize),

                PageInfo = currentPageInfo
            };

            var pagingVisitModel = new PagingVisitViewModel(viewModel, viewPagingModel);

            return View(pagingVisitModel);
        }

        public ActionResult Details(int patientId)
        {
            return View();
            //return View(_registeredVisitRepository.GetVisitByPatientLastName(patientLastName));
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        public ActionResult AddVisit()
        {
            List<SelectListItem> doctorsList = new List<SelectListItem>();
            List<SelectListItem> patientList = new List<SelectListItem>();
            List<Doctor> doctors = _doctorRepository.GetAllDoctors();
            List<Patient> patients = _patientRepository.GetAllPatients();

            doctors.ForEach(x =>
            {
                var listItem = new SelectListItem
                {
                    Text = string.Concat(x.FirstName, " ", x.LastName),
                    Value = x.Id.ToString()
                };

                doctorsList.Add(listItem);
            });

            patients.ForEach(x =>
            {
                var listItem = new SelectListItem
                {
                    Text = string.Concat(x.FirstName, " ", x.LastName),
                    Value = x.Id.ToString()
                };

                patientList.Add(listItem);
            });

            var viewModel = new AddRegisteredVisitViewModel
            {
                PatientList = patientList,
                DoctorList = doctorsList
            };
            
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddVisit(RegisteredVisitViewModel viewModel)
        {
            //TODO: Obsluga validacji + zapis

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditVisit(int visitId)
        {
            List<RegisteredVisit> visitsFromDb = _registeredVisitRepository.GetAllRegisteredVisits();
            var viewModel = new List<RegisteredVisitViewModel>();

            visitsFromDb.ForEach(x =>
            {
                RegisteredVisitViewModel visit = Converter.VisitTableToModel<RegisteredVisitViewModel>(x);
                viewModel.Add(visit);
            });

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteVisit(int visitId)
        {
            return View(_registeredVisitRepository.DeleteVisit(visitId));
        }
    }
}