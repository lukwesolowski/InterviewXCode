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
        private const int visitLength = 30;
        public string errorMessage;

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
                ItemsOfVisits = viewModel
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

        private AddOrEditRegisteredVisitViewModel GetAddOrEditRegisteredViewModel()
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

            var viewModel = new AddOrEditRegisteredVisitViewModel
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
            return View(GetAddOrEditRegisteredViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddVisit(AddOrEditRegisteredVisitViewModel viewModel)
        {
            RegisteredVisit registeredVisit = new RegisteredVisit
            {
                DoctorId = viewModel.SelectedDoctorId,
                PatientId = viewModel.SelectedPatientId,
                DateTime = viewModel.DateTime,
                Complaint = viewModel.Complaint
            };

            if (_registeredVisitRepository.NumberVisitToDoctorByDay(registeredVisit.DoctorId, registeredVisit.DateTime) >= MaxVisitPerDay)
            {
                errorMessage = "Ilość wizyt dla bieżącego doktora została przekroczona w dniu " + registeredVisit.DateTime.ToShortDateString();
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            if (_registeredVisitRepository.CheckIfTimeOfVisitIsAllowed(registeredVisit.DateTime, visitLength))
            {
                errorMessage = "Długość wizyty trwa: " + visitLength + " minut zatem nie możesz zapisać się na wybraną godzinę";
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            if (_registeredVisitRepository.CheckIfVisitIsOnWeekend(registeredVisit.DateTime))
            {
                errorMessage = "Nie można umówić wizyt na weekend";
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            if (_registeredVisitRepository.CheckIfDoctorIsFreeInCurrentDate(registeredVisit.DoctorId, registeredVisit.DateTime))
            {
                errorMessage = "Wybrany lekarz ma już umówioną wizytę na tą porę";
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            if (_registeredVisitRepository.CheckIfVisitIsOutdated(registeredVisit.DateTime))
            {
                errorMessage = "Data i czas wizyty nie może być starsza od bieżącej";
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            if (registeredVisit.Complaint == string.Empty)
            {
                errorMessage = "Proszę wypełnić pole dolegliwość";
                ViewBag.ErrorMessage = errorMessage;

                return View(GetAddOrEditRegisteredViewModel());
            }

            _registeredVisitRepository.AddVisit(registeredVisit);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult EditVisit(AddOrEditRegisteredVisitViewModel viewModel)
        {
            RegisteredVisit updatedVisit = new RegisteredVisit
            {
                Id = viewModel.Id,
                DoctorId = viewModel.SelectedDoctorId,
                PatientId = viewModel.SelectedPatientId,
                DateTime = viewModel.DateTime,
                Complaint = viewModel.Complaint
            };

            _registeredVisitRepository.UpdateVisit(updatedVisit);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditVisit(int visitId)
        {
            RegisteredVisit currentVisit = _registeredVisitRepository.DetailsOfVisit(visitId);
            AddOrEditRegisteredVisitViewModel viewModelWithDDL = GetAddOrEditRegisteredViewModel();
            AddOrEditRegisteredVisitViewModel viewModel = new AddOrEditRegisteredVisitViewModel
            {
                Id = visitId,
                Complaint = currentVisit.Complaint,
                DateTime = currentVisit.DateTime,
                Doctor = currentVisit.Doctor,
                Patient = currentVisit.Patient,
                DoctorList = viewModelWithDDL.DoctorList,
                PatientList = viewModelWithDDL.PatientList,
                SelectedDoctorId = currentVisit.DoctorId,
                SelectedPatientId = currentVisit.PatientId
            };

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