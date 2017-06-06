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

        Doctor DetailsOfDoctor(int doctorId);

        void AddDoctor(Doctor doctor);

        bool UpdateDoctor(int doctorId, Doctor doctor);

        bool DeleteDoctor(int doctorId);
    }
}