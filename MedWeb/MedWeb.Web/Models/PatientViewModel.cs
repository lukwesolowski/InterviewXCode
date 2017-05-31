using System;
using System.ComponentModel.DataAnnotations;

namespace MedWeb.Web.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Numer pesel")]
        public int Pesel { get; set; }

        [Display(Name = "Data urodziń")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Numer domu")]
        public string HouseNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string ZipCode { get; set; }
    }
}