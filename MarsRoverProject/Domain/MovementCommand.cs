using MarsRoverProject.Contracts.Data;

namespace MarsRoverProject.Domain
{
    public class MovementCommand : IMovementCommand
    {
        public string Message { get; internal set; }

        public MovementCommand(string message)
        {
            Message = message;
        }
    }
}
