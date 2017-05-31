using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    public class RegisteredVisitViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [Display(Name = "Data wizyty")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Dolegliwość")]
        public string Complaint { get; set; }
    }
}