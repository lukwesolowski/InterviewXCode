using FluentValidation.Attributes;
using MedWeb.BO.Validation;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MedWeb.Web.Models
{
    [Validator(typeof(VisitValidation))]
    public class RegisteredVisitViewModel
    {
        public int Id { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

        [Display(Name = "Data wizyty")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Dolegliwość")]
        public string Complaint { get; set; }
    }

    public class AddRegisteredVisitViewModel : RegisteredVisitViewModel
    {
        public List<SelectListItem> DoctorList { get; set; }
        public List<SelectListItem> PatientList { get; set; }

        public int SelectedDoctorId { get; set; }
        public int SelectedPatientId { get; set; }
    }
}