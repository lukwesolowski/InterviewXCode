﻿using Autofac;
using MedWeb.BO.Converters;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using MedWeb.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MedWeb.Web.Controllers
{
    public class VisitController : Controller
    {
        private IRegisteredVisitRepository _registeredVisitRepository;

        public VisitController()
        {
            using (ILifetimeScope scope = MvcApplication.DepedencyResolver.BeginLifetimeScope())
            {
                _registeredVisitRepository = scope.Resolve<IRegisteredVisitRepository>();
            }
        }

        // GET: Visit
        public ActionResult Index()
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

        public ActionResult Complaints()
        {
            return View();
        }

        [Authorize(Roles = "Administator")]
        public ActionResult AddVisit()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditVisit()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteVisit()
        {
            return View();
        }
    }
}