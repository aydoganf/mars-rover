using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Domain;
using System.Collections.Generic;

namespace MarsRoverProject.Contracts.Persistance
{
    public interface IMarsSurfaceRepository
    {
        List<Rover> GetRovers();
        List<Rover> GetRovers(int x, int y);
        Rover GetRover(int x, int y);

        bool LandRover(string name, int x, int y, string heading);

        bool LandRover(Rover rover);

        bool UpdateLocationAndHeading(int x, int y, Location newLocation, string newHeading);
    }
}
