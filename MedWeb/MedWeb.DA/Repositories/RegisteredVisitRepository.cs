using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System.Collections.Generic;
using System.Linq;

namespace MedWeb.DA.Repositories
{
    public class RegisteredVisitRepository : IRegisteredVisitRepository
    {
        private ApplicationDbContext _dbContext;

        public RegisteredVisitRepository()
        {
            _dbContext = ApplicationDbContext.Create();
        }

        public List<RegisteredVisit> GetAllRegisteredVisits()
        {
            return _dbContext
                .RegisteredVisit
                .ToList();                
        }

        public RegisteredVisit GetVisitByPatientLastName(string lastName)
        {
            return _dbContext
                .RegisteredVisit
                .Where(x => x.Patient.LastName.Equals(lastName.ToLower()))
                .FirstOrDefault();
        }

        public RegisteredVisit GetVisitByPacientPesel(int pesel)
        {
            return _dbContext
                .RegisteredVisit
                .Where(x => x.Patient.Pesel.Equals(pesel))
                .FirstOrDefault();
        }

        public List<RegisteredVisit> GetVisitsByDoctorLastName(string lastName)
        {
            return _dbContext
                .RegisteredVisit
                .Where(x => x.Doctor.LastName.Equals(lastName.ToLower()))
                .ToList();
        }

        public int GetCountOfRegisteredVisits()
        {
            return _dbContext
                .RegisteredVisit
                .Count();
        }

        public int GetCountOfRegisteredVisitsToDoctor(string lastName)
        {
            return _dbContext
                .RegisteredVisit
                .Where(x => x.Doctor.LastName.Equals(lastName.ToLower()))
                .Count();
        }
    }
}
