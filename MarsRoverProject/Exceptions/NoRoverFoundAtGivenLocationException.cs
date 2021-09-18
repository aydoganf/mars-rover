using System;

namespace MarsRoverProject.Exceptions
{
    public class NoRoverFoundAtGivenLocationException : Exception
    {
        public NoRoverFoundAtGivenLocationException(string message): base(message)
        {
        }
    }
}
