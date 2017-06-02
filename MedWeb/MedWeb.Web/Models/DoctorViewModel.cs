﻿using FluentValidation.Attributes;
using MedWeb.BO.Validation;
using MedWeb.DA.Tables;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    [Validator(typeof(DoctorValidation))]
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        public Specialization Specialization { get; set; }
    }
}