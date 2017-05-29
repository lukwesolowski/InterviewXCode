using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAllDoctors();

        Doctor GetDoctorById(int id);

        Doctor GetDoctorByLastName(string lastName);

        List<Doctor> GetDoctorsBySpecialization(string specName);
    }
}
