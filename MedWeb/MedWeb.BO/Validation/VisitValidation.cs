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
        private readonly List<RegisteredVisit> _registeredVisits;

        public VisitValidation(List<RegisteredVisit> registeredVisits)
        {
            _registeredVisits = registeredVisits;
            RuleFor(x => x.DateTime)
                .NotEmpty().WithMessage("Wypełnienie daty jest wymagane.")
                .Must(IsDateUniqueForSpecificDoctor).WithMessage("Wizyta o bieżącej dacie już istnieje");
            
        }

        public bool IsDateUniqueForSpecificDoctor(RegisteredVisit editedOrNewVisit, DateTime newValue)
        {
            return _registeredVisits.All(x =>
                x.DateTime != newValue || x.Doctor.Equals(editedOrNewVisit.Doctor));
            
        }
    }
}
