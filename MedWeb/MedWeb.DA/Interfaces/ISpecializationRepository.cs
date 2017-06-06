using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface ISpecializationRepository
    {
        List<Specialization> GetAllSpecializations();

        Specialization GetSpeacializationByName(string specName);

        void AddSpecializaion(Specialization specialization);

        bool ChangeSpecializationName(Specialization SpecializationModel);

        bool DeleteSpecialization(int specId);
    }
}