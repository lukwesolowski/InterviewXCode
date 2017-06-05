using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System;
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

        private const int MaxVisitPerDay = 16;

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
        public ActionResult Index(int? page = 0)
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

        [HttpGet]
        public ActionResult Details(int visitId)
        {
            RegisteredVisit currentVisit = _registeredVisitRepository.DetailsOfVisit(visitId);
            RegisteredVisitViewModel viewModel = new RegisteredVisitViewModel
            {
                Id = currentVisit.Id,
                Complaint = currentVisit.Complaint,
                DateTime = currentVisit.DateTime,
                Doctor = currentVisit.Doctor,
                Patient = currentVisit.Patient
            };
           
            return View(viewModel);
        }

        private AddRegisteredVisitViewModel GetAddRegisteredViewModel()
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
                DoctorList = doctorsList,
                DateTime = DateTime.Now
            };

            return viewModel;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddVisit()
        {
            return View(GetAddRegisteredViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddVisit(AddRegisteredVisitViewModel viewModel)
        {
            RegisteredVisit registeredVisit = new RegisteredVisit
            {
                DoctorId = viewModel.SelectedDoctorId,
                PatientId = viewModel.SelectedPatientId,
                DateTime = viewModel.DateTime,
                Complaint = viewModel.Complaint
            };

            if(_registeredVisitRepository.NumberVisitToDoctorByDay(registeredVisit.DoctorId, registeredVisit.DateTime) >= MaxVisitPerDay)
            {
                ViewBag.ErrorMessage = "Jakis tam alert";
                return View(GetAddRegisteredViewModel());
            }
           
            _registeredVisitRepository.AddVisit(registeredVisit);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditVisit(int visitId)
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

            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteVisit(int visitId)
        {
            _registeredVisitRepository.DeleteVisit(visitId);
            return RedirectToAction("Index");
        }
    }
}