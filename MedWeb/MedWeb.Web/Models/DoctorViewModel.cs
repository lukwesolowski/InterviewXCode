using FluentValidation.Attributes;
using MedWeb.BO.Validation;
using MedWeb.DA.Tables;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        public int SelectedSpecializationId { get; set; }
        public List<SelectListItem> SpecializationList { get; set; }
    }
}