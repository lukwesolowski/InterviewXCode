using System;

namespace MedWeb.BO.Exceptions
{
    public class VisitDateIsExsistsException : Exception
    {
        public VisitDateIsExsistsException()
            : base("Wizyta o podanej dacie jest już zarejestrowana!")
        {

        }
    }
}
