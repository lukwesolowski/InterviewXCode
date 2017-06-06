using MedWeb.DA.Interfaces;
using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void AddVisit(RegisteredVisit registerVisit)
        {
            _dbContext.RegisteredVisit.Add(registerVisit);
            _dbContext.SaveChanges();
        }

        public RegisteredVisit DetailsOfVisit(int visitId)
        {
            return _dbContext
                 .RegisteredVisit
                 .Select(x => x)
                 .Where(x => x.Id == visitId)
                 .FirstOrDefault();
        }

        public int NumberVisitToDoctorByDay(int doctorId, DateTime datetime)
        {
            int doctorVisitCount = 0;

            _dbContext
                .RegisteredVisit
                .Where(x => x.DoctorId == doctorId)
                .ToList()
                .ForEach(x =>
                {
                    if (x.DateTime.Date.Equals(datetime.Date))
                        doctorVisitCount++;
                });

            return doctorVisitCount;
        }

        public bool CheckIfTimeOfVisitIsAllowed(DateTime datetime, int visitLength)
        {
            return datetime.Minute % visitLength != 0;
        }

        public bool CheckIfVisitIsOnWeekend(DateTime datetime)
        {
            string currentDay = datetime.DayOfWeek.ToString();

            return currentDay.Contains("Saturday") || currentDay.Contains("Sunday");
        }

        public bool CheckIfDoctorIsFreeInCurrentDate(int doctorId, DateTime datetime)
        {
            int maxNumOfVisit = 1;
            int numOfVisits = 0;

            _dbContext
                .RegisteredVisit
                .Where(x => x.DoctorId == doctorId)
                .ToList()
                .ForEach(x =>
                {
                    if (x.DateTime.ToString("MM/dd/yyyy hh:mm tt").Equals(datetime.ToString("MM/dd/yyyy hh:mm tt")))
                        numOfVisits++;
                });

            return numOfVisits == maxNumOfVisit;
        }

        public bool CheckIfVisitIsOutdated(DateTime datetime)
        {
            return datetime <= DateTime.Now ? true : false;
        }

        public void UpdateVisit(RegisteredVisit visit)
        {
            RegisteredVisit registerVisitFromDb = _dbContext.RegisteredVisit.Find(visit.Id);

            registerVisitFromDb.DoctorId = visit.DoctorId;
            registerVisitFromDb.PatientId = visit.PatientId;
            registerVisitFromDb.Complaint = visit.Complaint;
            registerVisitFromDb.DateTime = visit.DateTime;

            _dbContext.SaveChanges();
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