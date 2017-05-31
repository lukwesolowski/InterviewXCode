using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    public class SpecializationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa specjalizacji")]
        public string Name { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime InsertTime { get; set; }
    }
}