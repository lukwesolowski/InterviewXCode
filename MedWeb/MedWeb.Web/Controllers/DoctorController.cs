using Autofac;
using MedWeb.DA.Interfaces;
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
    }
}