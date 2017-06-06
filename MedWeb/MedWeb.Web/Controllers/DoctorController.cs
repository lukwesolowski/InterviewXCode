using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorRepository _doctorRepository;

        public DoctorController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _doctorRepository = scope.Resolve<IDoctorRepository>();
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddDoctor(int doctorId)
        {
            Doctor currentDoctor = _doctorRepository.DetailsOfDoctor(doctorId);
            DoctorViewModel viewModel = new DoctorViewModel
            {
                Id = currentDoctor.Id,
                FirstName = currentDoctor.FirstName,
                LastName = currentDoctor.LastName,
                Specialization = currentDoctor.Specialization
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
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Specialization = viewModel.Specialization
            };

            _doctorRepository.AddDoctor(doctor);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteDoctor(int doctorId)
        {
            _doctorRepository.DeleteDoctor(doctorId);
            return RedirectToAction("Index");
        }
    }
}