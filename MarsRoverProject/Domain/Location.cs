using MarsRoverProject.Contracts.Data;

namespace MarsRoverProject.Domain
{
    public class Location : ILocationInfo
    {
        public int X { get; set; }
        public int Y { get; set; }

        int ILocationInfo.X { get => X; set => X = value; }
        int ILocationInfo.Y { get => Y; set => Y = value; }

        public Location()
        {
        }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} - Y: {Y}";
        }
    }
}
