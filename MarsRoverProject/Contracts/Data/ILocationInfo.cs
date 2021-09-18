namespace MarsRoverProject.Contracts.Data
{
    public interface ILocationInfo
    {
        int X { get; internal set; }
        int Y { get; internal set; }

        string ToString();
    }
}
