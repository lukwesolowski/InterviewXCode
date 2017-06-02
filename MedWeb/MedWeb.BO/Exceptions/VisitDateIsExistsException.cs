using System;

namespace MedWeb.BO.Exceptions
{
    public class VisitDateIsExistsException : Exception
    {
        public VisitDateIsExistsException()
            : base("Wizyta o podanej dacie jest już zarejestrowana!")
        {

        }
    }
}
