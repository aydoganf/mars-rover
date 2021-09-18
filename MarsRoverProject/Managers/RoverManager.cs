using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using MarsRoverProject.Exceptions;
using MarsRoverProject.Extensions;

namespace MarsRoverProject.Managers
{
    public class RoverManager : IRoverManager
    {
        private readonly IMarsSurfaceRepository _marsSurfaceRepository;

        public RoverManager(IMarsSurfaceRepository marsSurfaceRepository)
        {
            _marsSurfaceRepository = marsSurfaceRepository;
        }

        public Rover Command(Rover rover, IMovementCommand command)
        {
            for (int i = 0; i < command.Message.Length; i++)
            {
                var order = command.Message[i];

                if (order.IsIn('L', 'R'))
                {
                    if (order.Equals('L'))
                        TurnLeft(rover);

                    if (order.Equals('R'))
                        TurnRight(rover);

                    continue;
                }

                if (order.Equals('M'))
                    Move(rover);
            }

            return rover;
        }

        private static void ArrangeHeadingDegree(IRover rover)
        {
            if (rover.HeadingDegree == 360)
            {
                rover.HeadingDegree = 0;
                return;
            }

            if (rover.HeadingDegree < 0)
                rover.HeadingDegree += 360;
        }

        private static void TurnLeft(IRover rover)
        {
            rover.HeadingDegree += 90;
            ArrangeHeadingDegree(rover);
        }

        private static void TurnRight(IRover rover)
        {
            rover.HeadingDegree -= 90;
            ArrangeHeadingDegree(rover);
        }

        private void Move(IRover rover)
        {
            if (rover.HeadingDegree == 0)
            {
                CheckLocationIsEmpty(rover.Location.X + 1, rover.Location.Y);
                rover.Location.X += 1;
            }

            if (rover.HeadingDegree == 90)
            {
                CheckLocationIsEmpty(rover.Location.X, rover.Location.Y + 1);
                rover.Location.Y += 1;
            }

            if (rover.HeadingDegree == 180)
            {
                CheckLocationIsEmpty(rover.Location.X - 1, rover.Location.Y);
                rover.Location.X -= 1;
            }

            if (rover.HeadingDegree == 270)
            {
                CheckLocationIsEmpty(rover.Location.X, rover.Location.Y - 1);
                rover.Location.Y -= 1;
            }
                
        }

        public void CheckLocationIsEmpty(int x, int y)
        {
            var roverOnMars = _marsSurfaceRepository.GetRover(x, y);

            if (roverOnMars != null)
            {
                throw new AlreadyRoverExistsAtGivenLocationException("There is already exists a rover at the given location!");
            }
        }
    }

}
