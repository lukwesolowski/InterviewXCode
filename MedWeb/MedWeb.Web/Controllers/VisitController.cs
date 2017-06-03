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
        public int PageSize = 8;

        public VisitController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _registeredVisitRepository = scope.Resolve<IRegisteredVisitRepository>();
                _specializationRepository = scope.Resolve<ISpecializationRepository>();
            }
        }

        // GET: Visit
        [HttpGet]
        public ActionResult Index(int? page)
        {
            List<RegisteredVisit> visitsFromDb = _registeredVisitRepository.GetAllRegisteredVisits();
            var viewModel = new List<RegisteredVisitViewModel>();

            visitsFromDb.ForEach(x =>
            {
                RegisteredVisitViewModel visit = Converter.VisitTableToModel<RegisteredVisitViewModel>(x);
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

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administator")]
        public ActionResult AddVisit(string Prefix)
        {
            List<RegisteredVisit> visitsFromDb = _registeredVisitRepository.GetAllRegisteredVisits();
            var viewModel = new List<RegisteredVisitViewModel>();

            visitsFromDb.ForEach(x =>
            {
                RegisteredVisitViewModel visit = Converter.VisitTableToModel<RegisteredVisitViewModel>(x);
                viewModel.Add(visit);
            });

            var doctorLastName = (from d in viewModel
                                  where d.Doctor.LastName.StartsWith(Prefix)
                                  select new { d.Doctor.LastName });
            return Json(doctorLastName, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditVisit()
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
        public ActionResult DeleteVisit(int id)
        {
            return View(_registeredVisitRepository.DeleteVisit(id));
        }
    }
}