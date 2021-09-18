using MarsRoverProject.Domain;
using System.Collections.Generic;

namespace MarsRoverProject.Contracts.Data
{
    public interface IMarsSurfaceCommand
    {
        Location Borders { get; }
        List<RoverCommand> RoverCommands { get; }
    }
}
