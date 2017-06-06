using FluentValidation.Attributes;
using MedWeb.BO.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    [Validator(typeof(SpecializationValidation))]
    public class SpecializationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa specjalizacji")]
        public string Name { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime InsertTime { get; set; }
    }
}