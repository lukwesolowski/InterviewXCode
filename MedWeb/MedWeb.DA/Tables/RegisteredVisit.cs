using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedWeb.DA.Tables
{
    class RegisteredVisit
    {
        [Key]
        public int Id { get; set; }

        [Key]
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Complaint { get; set; }

        [ForeignKey(nameof(Patient))]
        public virtual Patient Patient { get; set; }

        [ForeignKey(nameof(Doctor))]
        public virtual Doctor Doctor { get; set; }
    }
}
