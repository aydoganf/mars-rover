using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using MarsRoverProject.Exceptions;
using MarsRoverProject.Extensions;
using System;

namespace MarsRoverProject.Managers
{
    public class RoverManager : IRoverManager
    {
        private readonly IMarsSurfaceRepository _marsSurfaceRepository;

        public RoverManager(IMarsSurfaceRepository marsSurfaceRepository)
        {
            _marsSurfaceRepository = marsSurfaceRepository;
        }

        public Rover Command(Rover rover, MovementCommand command)
        {
            for (int i = 0; i < command.Message.Length; i++)
            {
                var order = command.Message[i];
                var previousX = rover.Location.X;
                var previousY = rover.Location.Y;

                if (order.IsIn('L', 'R'))
                {
                    if (order.Equals('L'))
                        TurnLeft(rover);

                    if (order.Equals('R'))
                        TurnRight(rover);
                }

                if (order.Equals('M'))
                    Move(rover);

                _marsSurfaceRepository.UpdateLocationAndHeading(
                    x: previousX,
                    y: previousY,
                    newLocation: rover.Location,
                    newHeading: rover.Heading);
            }

            return rover;
        }

        private static void ArrangeHeadingDegree(Rover rover)
        {
            if (rover.HeadingDegree == 360)
            {
                rover.HeadingDegree = 0;
                return;
            }

            if (rover.HeadingDegree < 0)
                rover.HeadingDegree += 360;
        }

        private static void TurnLeft(Rover rover)
        {
            Console.WriteLine($"Rover {rover.Name} turning left.");
            rover.HeadingDegree += 90;
            ArrangeHeadingDegree(rover);
            Console.WriteLine($"Heading is {rover.Heading}");
        }

        private static void TurnRight(Rover rover)
        {
            Console.WriteLine($"Rover {rover.Name} turning left.");
            rover.HeadingDegree -= 90;
            ArrangeHeadingDegree(rover);
            Console.WriteLine($"Heading is {rover.Heading}");
        }

        private void Move(Rover rover)
        {
            Console.WriteLine($"Heading degree is {rover.HeadingDegree}");

            if (rover.HeadingDegree == 0)
            {
                Console.WriteLine($"Checking location X:{rover.Location.X + 1} Y:{rover.Location.Y}");
                CheckLocationIsEmpty(rover.Location.X + 1, rover.Location.Y);
                rover.Location.X += 1;
            }

            if (rover.HeadingDegree == 90)
            {
                Console.WriteLine($"Checking location X:{rover.Location.X} Y:{rover.Location.Y + 1}");
                CheckLocationIsEmpty(rover.Location.X, rover.Location.Y + 1);
                rover.Location.Y += 1;
            }

            if (rover.HeadingDegree == 180)
            {
                Console.WriteLine($"Checking location X:{rover.Location.X - 1} Y:{rover.Location.Y}");
                CheckLocationIsEmpty(rover.Location.X - 1, rover.Location.Y);
                rover.Location.X -= 1;
            }

            if (rover.HeadingDegree == 270)
            {
                Console.WriteLine($"Checking location X:{rover.Location.X} Y:{rover.Location.Y - 1}");
                CheckLocationIsEmpty(rover.Location.X, rover.Location.Y - 1);
                rover.Location.Y -= 1;
            }
                
        }

        public void CheckLocationIsEmpty(int x, int y)
        {
            var roverOnMars = _marsSurfaceRepository.GetRover(x, y);

            if (roverOnMars != null)
            {
                throw new AlreadyRoverExistsAtGivenLocationException(
                    $"There is already exists a rover at the given location X: {x} - Y: {y}");
            }
        }
    }

}
