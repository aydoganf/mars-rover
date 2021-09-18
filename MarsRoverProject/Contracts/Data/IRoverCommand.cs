namespace MarsRoverProject.Contracts.Data
{
    public interface IRoverCommand
    {
        string Selector { get; }
        string Command { get; }

        ILocationInfo GetSelectorLocation();
        IMovementCommand GetMovementCommand();
    }
}
