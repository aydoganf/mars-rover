using System;

namespace MarsRoverProject.Exceptions
{
    public class RoverCouldNotLandToOutOfSelectedBorderException : Exception
    {
        public RoverCouldNotLandToOutOfSelectedBorderException(string message) : base(message)
        {
        }
    }
}
