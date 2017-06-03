using MedWeb.DA.Tables;
using System.Collections.Generic;

namespace MedWeb.DA.Interfaces
{
    public interface ISpecializationRepository
    {
        List<Specialization> GetAllSpecializations();

        Specialization GetSpeacializationById(int id);

        void AddSpecialization(Specialization SpecializationModel);

        bool ChangeSpecializationName(Specialization SpecializationModel);

        bool DeleteSpecialization(int specId);
    }
}