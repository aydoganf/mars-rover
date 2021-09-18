using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Model
{
    public class LocationInfo : IApiModel
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
