using MarsRoverProject.Domain;
using System.Collections.Generic;

namespace MarsRoverProject.Contracts.Data
{
    public interface IMarsSurfaceInfo
    {
        List<Rover> Rovers { get; set; }
        Location Borders { get; }
    }
}
