using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedWeb.DA.Tables
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Key, ForeignKey(nameof(Specialization))]
        public int SpecializationId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Specialization Specialization { get; set; }
    }
}