using MarsRoverAPI.Model;
using System.Collections.Generic;

namespace MarsRoverAPI.Requests
{
    public class LandAndMoveRequest
    {
        public int BorderX { get; set; }
        public int BorderY { get; set; }

        public List<RoverCommandModel> Commands { get; set; }
    }
}
