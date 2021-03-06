﻿using Autofac;
using MedWeb.DA.Interfaces;
using MedWeb.DA.Repositories;

namespace MedWeb.Web.App_Start
{
    public class DepedencyResolverConfig
    {
        public static void RegisterDepedency()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PatientRepository>().As<IPatientRepository>();
            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>();
            builder.RegisterType<RegisteredVisitRepository>().As<IRegisteredVisitRepository>();
            builder.RegisterType<SpecializationRepository>().As<ISpecializationRepository>();

            MvcApplication.DepedencyResolver = builder.Build();
        }
    }
}