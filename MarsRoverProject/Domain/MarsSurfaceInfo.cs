using MarsRoverProject.Contracts.Data;
using System.Collections.Generic;

namespace MarsRoverProject.Domain
{
    public class MarsSurfaceInfo : IMarsSurfaceInfo
    {
        private List<Rover> rovers;
        private Location borders;

        public MarsSurfaceInfo(List<Rover> rovers, Location borders)
        {
            this.rovers = rovers;
            this.borders = borders;
        }

        public List<Rover> Rovers { get => rovers; set => rovers = value; }

        public Location Borders => borders;
    }
}
