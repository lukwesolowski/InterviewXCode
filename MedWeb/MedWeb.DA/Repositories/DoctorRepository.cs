using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.DA.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private ApplicationDbContext _dbContext;

        public DoctorRepository()
        {
            _dbContext = ApplicationDbContext.Create();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _dbContext
                .Doctor
                .ToList();
        }

        public Doctor GetDoctorById(int id)
        {
            Doctor doctor = _dbContext
                .Doctor
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();

            return doctor;
        }

        public Doctor GetDoctorByLastName(string lastName)
        {
            Doctor doctor = _dbContext
                .Doctor
                .Where(x => x.LastName.Equals(lastName.ToLower()))
                .FirstOrDefault();

            return doctor;
        }

        public List<Doctor> GetDoctorsBySpecialization(string specName)
        {
            return _dbContext
                .Doctor
                .Where(x => x.Specialization.Name.Equals(specName.ToLower()))
                .ToList();
        }
    }
}
