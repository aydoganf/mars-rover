using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Domain;
using MarsRoverProject.Exceptions;
using MarsRoverProject.Managers;
using MarsRoverProject.Persistance;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MarsRoverTests
{
    public class CommandRecieverTests
    {
        [Fact]
        public void Test_Should_Land_Over_Mars_Succesfully()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var rover = new Rover("curiosity 1", new Location(1, 5), "W");
            commandReciever.LandTheRoverOnMars(rover);

            var roverOnMars = marsSurfaceRepository.GetRover(1, 5);

            Assert.Equal(rover, roverOnMars);
        }



        [Fact]
        public void Test_Should_Rover_Not_Land_To_Already_Used_Location()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var curiosity1 = new Rover("curiosity", new Location(1, 3), "N");

            commandReciever.LandTheRoverOnMars(curiosity1);

            var curiosity2 = new Rover("curiusity 2", new Location(1, 3), "W");

            Assert.Throws<AlreadyRoverExistsAtGivenLocationException>(() => commandReciever.LandTheRoverOnMars(curiosity2));
        }

        [Fact]
        public void Test_Should_Not_Work_When_No_Rover_Found_At_Given_Location()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var curiosity1 = new Rover("curiosity", new Location(1, 3), "N");

            commandReciever.LandTheRoverOnMars(curiosity1);

            Assert.Throws<NoRoverFoundAtGivenLocationException>(() =>
                commandReciever.SendMessageToSurface(MarsSurfaceCommand.With(3, 3, new List<(string, string)>()
                {
                    new ("0 0", "MMLM")
                }))
            );
        }

        [Fact]
        public void Test_Should_Rover_Move_Succesfully()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var curiosity1 = new Rover("curiosity 1", new Location(1, 3), "E");
            commandReciever.LandTheRoverOnMars(curiosity1);

            var curiosity2 = new Rover("curiosity 2", new Location(1, 2), "W");
            commandReciever.LandTheRoverOnMars(curiosity2);

            commandReciever.SendMessageToSurface(MarsSurfaceCommand.With(3, 3, new List<(string, string)>()
            {
                new ("1 3", "MMLMRMMMM"), // 7 4 E
                new ("1 2", "MLMMMRMMRM") // -2 0 N
            }));

            Assert.Equal(7, curiosity1.Location.X);
            Assert.Equal(4, curiosity1.Location.Y);
            Assert.Equal(-2, curiosity2.Location.X);
            Assert.Equal(0, curiosity2.Location.Y);
            Assert.Equal("E", curiosity1.Heading);
            Assert.Equal("N", curiosity2.Heading);
        }

        [Fact]
        public void Test_Should_Not_Move_To_Already_Used_Location()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var curiosity1 = new Rover("curiosity", new Location(1, 3), "N");

            commandReciever.LandTheRoverOnMars(curiosity1);

            var curiosity2 = new Rover("curiosity2", new Location(1, 2), "N");

            commandReciever.LandTheRoverOnMars(curiosity2);

            Assert.Throws<AlreadyRoverExistsAtGivenLocationException>(() =>
                commandReciever.SendMessageToSurface(MarsSurfaceCommand.With(3, 3, "1 2", "M")));
        }

        [Fact]
        public void Test_Should_Land_And_Move_Succesfully()
        {
            var marsSurfaceRepository = new MarsSurfaceRepository();
            var commandReciever = new CommandReciever(marsSurfaceRepository, new RoverManager(marsSurfaceRepository));

            var surface = commandReciever.LandAndMoveOnMarsSurface(MarsSurfaceCommand.With(3, 3, "1 1 N", "MMRMMRM"));

            Assert.Equal(3, surface.Rovers.First().Location.X);
            Assert.Equal(2, surface.Rovers.First().Location.Y);
        }
    }
}
