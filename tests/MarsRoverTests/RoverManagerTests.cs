using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Domain;
using MarsRoverProject.Managers;
using MarsRoverProject.Persistance;
using System;
using Xunit;

namespace MarsRoverTests
{
    public class RoverManagerTests
    {
        private readonly IRoverManager _roverManager;

        public RoverManagerTests()
        {
            _roverManager = new RoverManager(new MarsSurfaceRepository());
        }

        [Fact]
        public void Test_Sholud_Rover_Rotate_Succesfully1()
        {
            var rover = new Rover("curiosity", new Location(1, 3), "N");

            var command = new MovementCommand("LMLMLMMMLMRMM");

            _roverManager.Command(rover, command);

            Assert.Equal(0, rover.HeadingDegree);
        }

        [Fact]
        public void Test_Should_Rover_Relocate_Succesfully1()
        {
            var command = new MovementCommand("MMRMLMMMRMMRM");

            var rover = new Rover("curiosity", new Location(0, 0), "N");

            _roverManager.Command(rover, command);

            Assert.Equal(3, rover.Location.X);
        }

        [Fact]
        public void Test_Should_Rover_Relocate_Succesfully2()
        {
            var command = new MovementCommand("RMMMLRMMMMLMRMM");

            var rover = new Rover("curiosity", new Location(0, 0), "S");

            _roverManager.Command(rover, command);

            Assert.Equal(-9, rover.Location.X);
            Assert.Equal(-1, rover.Location.Y);
        }

        [Fact]
        public void Test_Sholud_Rover_Rotate_Succesfully2()
        {
            var command = new MovementCommand("MMRMRMRMLLRMMRLRLMMRMRMRRLMM");

            var rover = new Rover("curiosity", new Location(1, 1), "W");

            _roverManager.Command(rover, command);

            Assert.Equal(90, rover.HeadingDegree);
        }

        [Fact]
        public void Test_Sholud_Rover_Rotate_Succesfully3()
        {
            var command = new MovementCommand("R");

            var rover = new Rover("curiosity", new Location(0, 0), "E");

            _roverManager.Command(rover, command);

            Assert.Equal(270, rover.HeadingDegree);
        }
    }
}
