using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}