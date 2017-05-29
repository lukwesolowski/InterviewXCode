using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface IPatientRepository
    {
        List<Patient> GetAllPatients();

        Patient GetPatientById(int id);

        Patient GetPatientByPesel(int pesel);

        Patient GetPatientByLastName(string lastName);
    }
}