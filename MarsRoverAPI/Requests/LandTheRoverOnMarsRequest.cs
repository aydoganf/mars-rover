using MarsRoverAPI.Model;

namespace MarsRoverAPI.Requests
{
    public class LandTheRoverOnMarsRequest
    {
        public string RoverName { get; set; }
        public LocationInfo Location { get; set; }
        public string Heading { get; set; }
    }
}
