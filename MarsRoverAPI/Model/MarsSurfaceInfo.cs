using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Model
{
    public class MarsSurfaceInfo : IApiModel
    {
        public List<RoverInfo> Rovers { get; set; }
    }
}
