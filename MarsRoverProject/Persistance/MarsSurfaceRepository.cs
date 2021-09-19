using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject.Persistance
{
    public class MarsSurfaceRepository : IMarsSurfaceRepository
    {
        private List<Rover> _rovers = new List<Rover>()
        {
            //new Rover("curiosity 1", new Location(1, 5), 'W'),
            //new Rover("curiosity 2", new Location(2, 2), 'S')
        };

        public void ClearSurface()
        {
            _rovers.Clear();
        }

        public Rover GetRover(int x, int y) => _rovers.FirstOrDefault(r => r.Location.X == x && r.Location.Y == y);

        public List<Rover> GetRovers() => _rovers.ToList();

        public List<Rover> GetRovers(int x, int y) => _rovers.Where(r => r.Location.X <= x && r.Location.Y <= y).ToList();

        public bool LandRover(string name, int x, int y, string heading)
        {
            _rovers.Add(new Rover(name, new Location(x, y), heading));

            return true;
        }

        public bool LandRover(Rover rover)
        {
            _rovers.Add(rover);

            return true;
        }

        public bool UpdateLocationAndHeading(int x, int y, Location newLocation, string newHeading)
        {
            return true;
        }


    }
}
