using FluentValidation;
using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.BO.Validation
{
    public class DoctorValidation : AbstractValidator<Doctor>
    {
        public readonly List<Doctor> _doctors;

        public DoctorValidation(List<Doctor> doctors)
        {
            _doctors = doctors;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Podanie imienia lekarza jest wymagane")
                .Length(1, 20).WithMessage("Długość imienia przekroczyła dozwoloną liczbę znaków");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Podanie nazwiska lekarza jest wymagane")
                .Length(1, 40).WithMessage("Długość nazwiska przekroczyła dozwoloną liczbę znaków");
            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Obowiązkowe jest dodanie specjalizacji lekarza");
        }
    }
}