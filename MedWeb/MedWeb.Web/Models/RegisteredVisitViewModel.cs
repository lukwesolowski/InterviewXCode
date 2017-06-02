using FluentValidation.Attributes;
using MedWeb.BO.Validation;
using MedWeb.DA.Tables;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    [Validator(typeof(VisitDateTimeValidation))]
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
}