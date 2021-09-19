using System;

namespace MarsRoverProject.Domain
{
    public class Rover
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int HeadingDegree { get; set; }

        public string Heading => GetHeadingInfo();

        public Rover()
        {
            Id = Guid.NewGuid();
            Location = new Location();
        }

        public Rover(string name, Location location, string heading)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = new Location
            {
                X = location.X,
                Y = location.Y
            };

            SetHeading(heading);
        }

        public void SetHeading(string heading)
        {
            switch (heading)
            {
                case "E":
                    HeadingDegree = 0;
                    break;
                case "N":
                    HeadingDegree = 90;
                    break;
                case "W":
                    HeadingDegree = 180;
                    break;
                case "S":
                    HeadingDegree = 270;
                    break;
                default:
                    break;
            }
        }

        private string GetHeadingInfo()
        {
            return HeadingDegree switch
            {
                0 => "E",
                90 => "N",
                180 => "W",
                270 => "S",
                _ => "U",
            };
        }

        public override string ToString()
        {
            return $"{Location.X} {Location.Y} {GetHeadingInfo()}";
        }
    }
}
