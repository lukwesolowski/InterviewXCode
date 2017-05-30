using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
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
            return _dbContext
                .Patient
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
        }

        public Patient GetPatientByPesel(int pesel)
        {
            return _dbContext
                .Patient
                .Where(x => x.Pesel.Equals(pesel))
                .FirstOrDefault();
        }

        public Patient GetPatientByLastName(string lastName)
        {
            return _dbContext
                .Patient
                .Where(x => x.LastName.Equals(lastName))
                .FirstOrDefault();
        }

        public bool UpdatePatient(int patientId, Patient patient)
        {
            try
            {
                var PatientModel = _dbContext
                    .Patient
                    .Select(x => x)
                    .Where(x => x.Id == patientId)
                    .FirstOrDefault();

                PatientModel.FirstName = patient.FirstName;
                PatientModel.LastName = patient.LastName;
                PatientModel.Pesel = patient.Pesel;
                PatientModel.BirthDate = patient.BirthDate;
                PatientModel.City = patient.FirstName;
                PatientModel.Street = patient.FirstName;
                PatientModel.HouseNumber = patient.FirstName;
                PatientModel.ZipCode = patient.ZipCode;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePatient(int patientId)
        {
            try
            {
                _dbContext.RegisteredVisit.Remove(_dbContext.RegisteredVisit.Find(patientId));
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}