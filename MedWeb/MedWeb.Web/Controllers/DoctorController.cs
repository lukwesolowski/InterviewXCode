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
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;
        private ISpecializationRepository _specializationRepository;

        public DoctorController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _doctorRepository = scope.Resolve<IDoctorRepository>();
                _specializationRepository = scope.Resolve<ISpecializationRepository>();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page = 0)
        {
            List<Doctor> doctorsFromDb = _doctorRepository.GetAllDoctors();
            var viewModel = new List<DoctorViewModel>();

            doctorsFromDb.ForEach(x =>
            {
                DoctorViewModel doctor = new DoctorViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Specialization = x.Specialization,
                };

                viewModel.Add(doctor);
            });

            var currentPageInfo = new PageInfo(viewModel.Count(), page);
            var viewPagingModel = new PagingInfoModel
            {
                ItemsOfDoctors = viewModel
                .Skip((currentPageInfo.CurrentPage - 1) * currentPageInfo.PageSize)
                .Take(currentPageInfo.PageSize),

                PageInfo = currentPageInfo
            };

            var pagingDoctorModel = new PagingDoctorViewModel(viewModel, viewPagingModel);

            return View(pagingDoctorModel);
        }

        [HttpGet]
        public ActionResult Details(int doctorId)
        {
            Doctor currentDoctor = _doctorRepository.DetailsOfDoctor(doctorId);
            DoctorViewModel viewModel = new DoctorViewModel
            {
                Id = currentDoctor.Id,
                FirstName = currentDoctor.FirstName,
                LastName = currentDoctor.LastName,
                Specialization = currentDoctor.Specialization,
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddDoctor(DoctorViewModel viewModel)
        {
            Doctor doctor = new Doctor
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                SpecializationId = viewModel.SelectedSpecializationId
            };

            _doctorRepository.AddDoctor(doctor);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddDoctor()
        {
            DoctorViewModel viewModel = new DoctorViewModel();
            List<SelectListItem> specializationList = new List<SelectListItem>();

            _specializationRepository.GetAllSpecializations()
                .ForEach(x =>
                {
                    SelectListItem listItem = new SelectListItem { Value = x.Id.ToString(), Text = x.Name };
                    specializationList.Add(listItem);
                });

            viewModel.SpecializationList = specializationList;

            return View(viewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteDoctor(int doctorId)
        {
            _doctorRepository.DeleteDoctor(doctorId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddSpecialization()
        {
            Specialization specialization = new Specialization();
            SpecializationViewModel viewModel = new SpecializationViewModel
            {
                Id = specialization.Id,
                Name = specialization.Name
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult AddSpecialization(SpecializationViewModel viewModel)
        {
            Specialization specialization = new Specialization
            {
                Name = viewModel.Name,
                InsertTime = DateTime.Now
            };

            _specializationRepository.AddSpecializaion(specialization);

            return RedirectToAction("Index");
        }
    }
}