using MedWeb.DA.Tables;
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
    }
}
