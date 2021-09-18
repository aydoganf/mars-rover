using MarsRoverAPI.Model;
using System.Collections.Generic;

namespace MarsRoverAPI.Requests
{
    public class SendMessageToMarsSurfaceRequest
    {
        public int BorderX { get; set; }
        public int BorderY { get; set; }
        public List<RoverCommandModel> RoverCommands { get; set; }
    }
}
