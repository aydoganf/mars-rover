using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using MarsRoverProject.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverProject.Managers
{
    public class CommandReciever : ICommandReciever
    {
        private readonly IMarsSurfaceRepository _marsSurfaceRepository;
        private readonly IRoverManager _roverManager;

        public CommandReciever(IMarsSurfaceRepository marsSurfaceRepository, IRoverManager roverManager)
        {
            _marsSurfaceRepository = marsSurfaceRepository;
            _roverManager = roverManager;
        }

        public IMarsSurfaceInfo SendMessageToSurface(MarsSurfaceCommand marsSurfaceCommand)
        {
            var roversInArea = _marsSurfaceRepository.GetRovers(marsSurfaceCommand.Borders.X, marsSurfaceCommand.Borders.Y);

            if (roversInArea?.Any() == false)
            {
                // throws an exception
                throw new NoRoverFoundAtGivenBordersException($"There is no rover in the given borders {marsSurfaceCommand.Borders}");
            }

            List<(Rover Rover, RoverCommand Command)> selectedRoversWithCommands = new();

            #region check locations

            marsSurfaceCommand.RoverCommands.ForEach(roverCommand =>
            {
                var location = roverCommand.GetSelectorLocation();
                var selectedRover = roversInArea
                    .FirstOrDefault(r =>
                        r.Location.X == location.X &&
                        r.Location.Y == location.Y);

                if (selectedRover == null)
                {
                    throw new NoRoverFoundAtGivenLocationException(
                        $"There is no rover at given location: {roverCommand.Selector}");
                }

                selectedRoversWithCommands.Add(new (selectedRover, roverCommand));
            });

            #endregion

            selectedRoversWithCommands.ForEach(roverWithCommand => 
            {
                var targetLocation = roverWithCommand.Command.GetSelectorLocation();
                
                var rover = _roverManager.Command(
                    roverWithCommand.Rover, 
                    roverWithCommand.Command.GetMovementCommand());
            });


            return new MarsSurfaceInfo(selectedRoversWithCommands.Select(c => c.Rover).ToList(), marsSurfaceCommand.Borders);
        }

        public IMarsSurfaceInfo LandTheRoverOnMars(Rover rover)
        {
            _roverManager.CheckLocationIsEmpty(rover.Location.X, rover.Location.Y);

            _marsSurfaceRepository.LandRover(rover);

            return new MarsSurfaceInfo(_marsSurfaceRepository.GetRovers(), null);
        }

        public IMarsSurfaceInfo LandTheRoverOnMars(string roverName, int x, int y, string heading)
        {
            _roverManager.CheckLocationIsEmpty(x, y);

            _marsSurfaceRepository.LandRover(new Rover(roverName, new Location(x, y), heading));

            return new MarsSurfaceInfo(_marsSurfaceRepository.GetRovers(), null);
        }

        public IMarsSurfaceInfo LandAndMoveOnMarsSurface(MarsSurfaceCommand marsSurfaceCommand)
        {
            if (marsSurfaceCommand.RoverCommands.Any(c => c.GetSelectorLocation().X > marsSurfaceCommand.Borders.X || c.GetSelectorLocation().Y > marsSurfaceCommand.Borders.Y))
            {
                throw new RoverCouldNotLandToOutOfSelectedBorderException("Any rover could not land to selected border");
            }

            marsSurfaceCommand.RoverCommands.ForEach(command => 
            {
                var landingLocation = command.GetSelectorLocation();

                var rover = new Rover($"curiosity {Guid.NewGuid()}", landingLocation, command.GetHeading());

                _marsSurfaceRepository.LandRover(rover);

                Console.WriteLine($"Rover {rover.Name} landed to surfaca with location {rover.Location}");

                _roverManager.Command(rover, command.GetMovementCommand());

                Console.WriteLine($"Rover location is {rover.Location}");
            });

            return new MarsSurfaceInfo(_marsSurfaceRepository.GetRovers(), marsSurfaceCommand.Borders);
        }

        public IMarsSurfaceInfo GetSurfaceInfo()
        {
            return new MarsSurfaceInfo(_marsSurfaceRepository.GetRovers(), null);
        }

        public void ClearMarsSurface()
        {
            _marsSurfaceRepository.ClearSurface();
        }
    }
}
