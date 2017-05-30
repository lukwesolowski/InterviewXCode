using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.DA.Tables
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}