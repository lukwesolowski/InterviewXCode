﻿using FluentValidation;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;

namespace MedWeb.BO.Validation
{
    public class PatientValidation : AbstractValidator<Patient>
    {
        public readonly List<Patient> _patients;

        public PatientValidation(List<Patient> patients)
        {
            _patients = patients;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Podanie imienia pacjenta jest wymagane")
                .Length(1, 20).WithMessage("Długość imienia przekroczyła dozwoloną liczbę znaków");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Podanie nazwiska pacjenta jest wymagane")
                .Length(1, 40).WithMessage("Długość nazwiska przekroczyła dozwoloną liczbę znaków");
            RuleFor(x => x.Pesel)
                .NotEmpty().WithMessage("Podanie numeru pesel jest wymagane")
                .IsInEnum().WithMessage("Numer pesel może składać się jedynie z cyfr (nie liter)");
            RuleFor(x => x.Pesel.ToString())
                .Length(11, 11).WithMessage("Ilość liczb w numerze pesel musi być równa 11ście");
            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Podanie daty urodziń pacjenta jest wymagane")
                .Must(IsAtleast16YearsOld).WithMessage("Pacjent musi mieć ukończone 16 lat")
                .Must(IsYoungerThanHundred).WithMessage("Pacjent nie może mieć więcej niż 100 lat");
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Podanie miasta zamieszkania pacjenta jest wymagane");
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Podanie ulicy jest wymagane");
            RuleFor(x => x.HouseNumber)
                .NotEmpty().WithMessage("Podanie numeru domu jest wymagane");
            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Podanie kodu pocztowego jest wymagane");
        }

        public bool IsAtleast16YearsOld(DateTime dateTime)
        {
            int sixteenYearsOld = 16;
            return dateTime.Year >= sixteenYearsOld ? true : false;
        }

        public bool IsYoungerThanHundred(DateTime dateTime)
        {
            int hundred = 100;
            return dateTime.Year <= hundred ? true : false;
        }
    }
}