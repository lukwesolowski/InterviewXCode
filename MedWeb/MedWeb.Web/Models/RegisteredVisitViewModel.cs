using System;

namespace MedWeb.Web.Models
{
    public class RegisteredVisitViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateTime { get; set; }
        public string Complaint { get; set; }
    }
}