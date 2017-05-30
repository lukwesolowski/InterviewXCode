using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System;
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
                .Where(x => x.Patient.LastName.Equals(lastName))
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
                .Where(x => x.Doctor.LastName.Equals(lastName))
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
                .Where(x => x.Doctor.LastName.Equals(lastName))
                .Count();
        }

        public void AddVisit(int visitId, int patientId, int doctorId)
        {
            RegisteredVisit registeredVisit = new RegisteredVisit
            {
                Id = visitId,
                PatientId = patientId,
                DoctorId = doctorId
            };

            _dbContext.RegisteredVisit.Add(registeredVisit);
            _dbContext.SaveChanges();
        }

        public bool CheckIfVisitIsOutdated(int visitId)
        {
            var VisitModel = _dbContext
                .RegisteredVisit
                .Select(x => x)
                .Where(x => x.Id == visitId)
                .FirstOrDefault();

            return VisitModel.DateTime < DateTime.Now ? true : false;
        }

        public bool SetVisitDateTime(int visitId, DateTime date, TimeSpan time)
        {
            try
            {
                var VisitModel = _dbContext
                .RegisteredVisit
                .Select(x => x)
                .Where(x => x.Id == visitId)
                .FirstOrDefault();

                VisitModel.DateTime = date + time;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetVisitComplaint(int visitId, string complaint)
        {
            try
            {
                var VisitModel = _dbContext
                .RegisteredVisit
                .Select(x => x)
                .Where(x => x.Id == visitId)
                .FirstOrDefault();

                VisitModel.Complaint = complaint;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeVisitDoctorToPatient(int newDoctorId, int patientId)
        {
            try
            {
                var VisitModel = _dbContext
                    .RegisteredVisit
                    .Select(x => x)
                    .Where(x => x.PatientId == patientId)
                    .FirstOrDefault();

                VisitModel.DoctorId = newDoctorId;
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteVisit(int visitId)
        {
            try
            {
                _dbContext.RegisteredVisit.Remove(_dbContext.RegisteredVisit.Find(visitId));
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