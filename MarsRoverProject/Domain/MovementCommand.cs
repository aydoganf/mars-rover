namespace MarsRoverProject.Domain
{
    public class MovementCommand
    {
        public string Message { get; internal set; }

        public MovementCommand(string message)
        {
            Message = message;
        }
    }
}
