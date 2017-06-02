using FluentValidation;
using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.BO.Validation
{
    public class SpecializationValidation : AbstractValidator<Specialization>
    {
        public readonly List<Specialization> _specialization;

        public SpecializationValidation(List<Specialization> specialization)
        {
            _specialization = specialization;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa specjalizacji jest wymagana")
                .MaximumLength(30).WithMessage("Została przekroczona dozwolona liczba znaków specjalizacji");
        }
    }
}
