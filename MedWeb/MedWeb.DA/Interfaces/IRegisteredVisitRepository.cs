﻿using MedWeb.DA.Tables;
using System;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface IRegisteredVisitRepository
    {
        IEnumerable<RegisteredVisit> RegisteredVisits { get; }

        List<RegisteredVisit> GetAllRegisteredVisits();

        RegisteredVisit GetVisitByPatientLastName(string lastName);

        RegisteredVisit GetVisitByPacientPesel(int pesel);

        List<RegisteredVisit> GetVisitsByDoctorLastName(string lastName);

        int GetCountOfRegisteredVisits();

        int GetCountOfRegisteredVisitsToDoctor(string lastName);

        void AddVisit(int visitId, int patientId, int doctorId);

        bool CheckIfVisitIsOutdated(int visitId);

        bool SetVisitDateTime(int visitId, DateTime date, TimeSpan time);

        bool SetVisitComplaint(int visitId, string complaint);

        bool ChangeVisitDoctorToPatient(int newDoctorId, int patientId);

        bool DeleteVisit(int visitId);
    }
}