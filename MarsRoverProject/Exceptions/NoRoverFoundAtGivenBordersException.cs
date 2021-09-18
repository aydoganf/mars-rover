using System;

namespace MarsRoverProject.Exceptions
{
    public class NoRoverFoundAtGivenBordersException : Exception
    {
        public NoRoverFoundAtGivenBordersException(string message) : base(message)
        {
        }
    }
}
