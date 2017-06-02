using FluentValidation;
using MedWeb.BO.Exceptions;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.BO.Validation
{
    public class VisitValidation : AbstractValidator<RegisteredVisit>
    {
        public readonly List<RegisteredVisit> _registeredVisits;

        public VisitValidation(List<RegisteredVisit> registeredVisits)
        {
            _registeredVisits = registeredVisits;

            RuleFor(x => x.DateTime)
                .NotEmpty().WithMessage("Wypełnienie daty jest wymagane.")
                .Must(IsDateUniqueForSpecificDoctor).WithMessage("Wizyta o bieżącej dacie już istnieje");
            RuleFor(x => x.Patient)
                .NotEmpty().WithMessage("Obowiązkowe jest dodanie pacjenta do wizyty");
            RuleFor(x => x.Doctor)
                .NotEmpty().WithMessage("Obowiązkowe jest dodanie lekarza do wizyty");       
        }

        public bool IsDateUniqueForSpecificDoctor(RegisteredVisit editedOrNewVisit, DateTime newValue)
        {
            return _registeredVisits.All(x =>
                x.DateTime != newValue || x.Doctor.Equals(editedOrNewVisit.Doctor));       
        }
    }
}
