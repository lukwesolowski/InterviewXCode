using MedWeb.DA.Tables;

namespace MedWeb.BO.Converters
{
    public class Converter
    {
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

        public static T DoctorTableToModel<T>(Doctor doctor)
            where T : class
        {
            var convertedObject = new
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization
            };

            return convertedObject as T;
        }

        public static T PatientTableToModel<T>(Patient patient)
            where T : class
        {
            var convertedObject = new
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Pesel = patient.LastName,
                BirthDate = patient.BirthDate,
                City = patient.City,
                Street = patient.Street,
                HouseNumber = patient.HouseNumber,
                ZipCode = patient.ZipCode
            };

            return convertedObject as T;
        }

        public static T SpecializationTableToModel<T>(Specialization specialization)
            where T : class
        {
            var convertedObject = new
            {
                Id = specialization.Id,
                Name = specialization.Name,
                InsertTime = specialization.InsertTime
            };

            return convertedObject as T;
        }
    }
}
