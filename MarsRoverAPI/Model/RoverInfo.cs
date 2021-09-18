using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Model
{
    public class RoverInfo : IApiModel
    {
        public string Name { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public string Heading { get; set; }
        public string Info { get; set; }
    }
}
