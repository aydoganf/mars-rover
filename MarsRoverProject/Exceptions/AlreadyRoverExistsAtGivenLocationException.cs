using System;

namespace MarsRoverProject.Exceptions
{
    public class AlreadyRoverExistsAtGivenLocationException : Exception
    {
        public AlreadyRoverExistsAtGivenLocationException(string message) : base(message)
        {
        }
    }
}
