using System.Linq;

namespace MarsRoverAPI.Model
{
    public static class ModelBuilder
    {
        public static MarsSurfaceInfo BuildMarsSurfaceInfo(MarsRoverProject.Contracts.Data.IMarsSurfaceInfo surface)
        {
            return new MarsSurfaceInfo
            {
                Rovers = surface?.Rovers?.Select(r => new RoverInfo
                {
                    Heading = r.Heading,
                    Info = r.ToString(),
                    LocationX = r.Location.X,
                    LocationY = r.Location.Y
                }).ToList()
            };
        }

        public static RoverInfo BuildRoverInfo(MarsRoverProject.Domain.Rover rover)
        {
            return new RoverInfo
            {
                Heading = rover.Heading,
                Info = rover.ToString(),
                LocationX = rover.Location.X,
                LocationY = rover.Location.Y,
                Name = rover.Name
            };
        }
    }
}
