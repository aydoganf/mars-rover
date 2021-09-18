namespace MarsRoverProject.Contracts.Data
{
    public interface IRover
    {
        string Name { get; }
        ILocationInfo Location { get; }
        string Heading { get; }
        int HeadingDegree { get; internal set; }
        
        string ToString();
    }
}
