using MedWeb.DA.Tables;

namespace MedWeb.BO.Converters
{
    public class Converter
    {
        public void DoctorTableToModel()
        {
            
        }

        public void PatientTableToModel()
        {

        }

        public static T VisitTableToModel<T>(RegisteredVisit registerVisit) 
            where T : class
        {
            var convertedObject = new
            {
                Id = registerVisit.Id,
                Doctor = registerVisit.Doctor,
                Patient = registerVisit.Patient,
                DateTime = registerVisit.DateTime,
                Complaint = registerVisit.Complaint
            };

            return convertedObject as T;
        }

        public void SpecializationTableToModel()
        {

        }
    }
}
