using System;

namespace DataLayer.Exceptions
{
    public class NotUniqueDefaulCityException : Exception
    {
        public NotUniqueDefaulCityException() : base()
        {

        }
        public NotUniqueDefaulCityException(string message) : base(message)
        {

        }
    }
}
