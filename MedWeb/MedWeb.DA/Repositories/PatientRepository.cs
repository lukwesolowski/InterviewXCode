using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.DA.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private ApplicationDbContext _dbContext;

        public PatientRepository()
        {
            _dbContext = ApplicationDbContext.Create();
        }

        public List<Patient> GetAllPatients()
        {
            return _dbContext
                .Patient
                .ToList();
        }

        public Patient GetPatientById(int id)
        {
            Patient patient = _dbContext
                .Patient
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();

            return patient;
        }

        public Patient GetPatientByPesel(int pesel)
        {
            Patient patient = _dbContext
                .Patient
                .Where(x => x.Pesel.Equals(pesel))
                .FirstOrDefault();

            return patient;
        }

        public Patient GetPatientByLastName(string lastName)
        {
            throw new NotImplementedException();
        }
    }
}