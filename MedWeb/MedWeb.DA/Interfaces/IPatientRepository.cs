using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface IPatientRepository
    {
        List<Patient> GetAllPatients();

        Patient GetPatientById(int id);

        Patient GetPatientByPesel(int pesel);

        Patient GetPatientByLastName(string lastName);

        bool UpdatePatient(int patientId, Patient patient);

        bool DeletePatient(int patientId);
    }
}