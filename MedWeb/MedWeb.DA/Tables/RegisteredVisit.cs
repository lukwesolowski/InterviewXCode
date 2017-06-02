using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedWeb.DA.Tables
{
    public class RegisteredVisit
    {
        //[Validator(typeof(VisitDateTimeValidation)]
        [Key]
        public int Id { get; set; }

        [Key, ForeignKey(nameof(Patient))]
        public int PatientId { get; set; }

        [Key, ForeignKey(nameof(Doctor))]
        public int DoctorId { get; set; }

        public DateTime DateTime { get; set; }
        public string Complaint { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}