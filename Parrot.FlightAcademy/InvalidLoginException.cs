using System;

namespace Parrot.FlightAcademy
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message)
        {
        }
    }
}