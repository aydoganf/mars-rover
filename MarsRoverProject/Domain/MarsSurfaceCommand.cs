using MarsRoverProject.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverProject.Domain
{
    public class MarsSurfaceCommand : IMarsSurfaceCommand
    {
        public Location Borders { get; set; }

        public List<RoverCommand> RoverCommands { get; set; }

        public static MarsSurfaceCommand With(
            int x, 
            int y, 
            string selectorCommand, 
            string movementCommand)
        {
            return new MarsSurfaceCommand
            {
                Borders = new Location(x, y),
                RoverCommands = new List<RoverCommand>() 
                {
                    new RoverCommand(selectorCommand, movementCommand)
                }
            };
        }

        public static MarsSurfaceCommand With(
            int x, 
            int y, 
            List<(string SelectorCommand, string MovementCommand)> commands)
        {
            return new MarsSurfaceCommand
            {
                Borders = new Location(x, y),
                RoverCommands = commands
                    .Select(c => new RoverCommand(c.SelectorCommand, c.MovementCommand))
                    .ToList()
            };
        }
    }
}
