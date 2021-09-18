using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.FileRepository
{
    public class FileMarsSurfaceRepository : IMarsSurfaceRepository
    {
        private readonly string filePath;

        public FileMarsSurfaceRepository()
        {
            filePath = @$"{Environment.CurrentDirectory}\mars-surface.txt";
        }

        private MarsSurfaceInfo GetMarsSurface()
        {
            if (File.Exists(filePath) == false)
            {
                var stream = File.Create(filePath);
                stream.Close();
                stream.Dispose();
            }

            string content = File.ReadAllText(filePath);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<MarsSurfaceInfo>(content);
        }

        private List<Rover> GetAllRovers()
        {
            var surface = GetMarsSurface();

            return surface?.Rovers?.ToList();
        }

        private void WriteToFile(List<Rover> rovers)
        {
            var surface = GetMarsSurface();

            if (surface == null)
                surface = new MarsSurfaceInfo(null, null);

            surface.Rovers = rovers;

            var content = Newtonsoft.Json.JsonConvert.SerializeObject(surface);

            File.WriteAllText(filePath, content);
        }

        public Rover GetRover(int x, int y)
        {
            return GetAllRovers()?.FirstOrDefault(r => r.Location.X == x && r.Location.Y == y);
        }

        public List<Rover> GetRovers()
        {
            return GetAllRovers();
        }

        public List<Rover> GetRovers(int x, int y)
        {
            return GetAllRovers()?.Where(r => r.Location.X <= x && r.Location.Y <= y).ToList();
        }

        public bool LandRover(string name, int x, int y, string heading)
        {
            return LandRover(new Rover(name, new Location(x, y), heading));
        }

        public bool LandRover(Rover rover)
        {
            var rovers = GetAllRovers();

            if (rovers == null)
                rovers = new List<Rover>();

            rovers.Add(rover);

            WriteToFile(rovers);
            return true;
        }

        public bool UpdateLocationAndHeading(int x, int y, Location newLocation, string newHeading)
        {
            var rovers = GetAllRovers();

            var target = rovers.FirstOrDefault(r => r.Location.X == x && r.Location.Y == y);

            target.Location = newLocation;
            target.SetHeading(newHeading);

            WriteToFile(rovers);
            return true;
        }
    }
}
