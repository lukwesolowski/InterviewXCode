using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface IRegisteredVisitRepository
    {
        List<RegisteredVisit> GetAllRegisteredVisits();

        RegisteredVisit GetVisitByPatientLastName(string lastName);

        RegisteredVisit GetVisitByPacientPesel(int pesel);

        List<RegisteredVisit> GetVisitsByDoctorLastName(string lastName);

        int GetCountOfRegisteredVisits();

        int GetCountOfRegisteredVisitsToDoctor(string lastName);

        void AddVisit(RegisteredVisit registerVisit);

        RegisteredVisit DetailsOfVisit(int visitId);

        int NumberVisitToDoctorByDay(int doctorId, DateTime datetime);

        bool CheckIfTimeOfVisitIsAllowed(DateTime datetime, int visitLength);

        bool CheckIfVisitIsOnWeekend(DateTime datetime);

        bool CheckIfDoctorIsFreeInCurrentDate(int doctorId, DateTime datetime);

        bool CheckIfVisitIsOutdated(DateTime datetime);

        bool SetVisitDateTime(int visitId, DateTime date, TimeSpan time);

        bool SetVisitComplaint(int visitId, string complaint);

        bool ChangeVisitDoctorToPatient(int newDoctorId, int patientId);

        bool DeleteVisit(int visitId);
    }
}