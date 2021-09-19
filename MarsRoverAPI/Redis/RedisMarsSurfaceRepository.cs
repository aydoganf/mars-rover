using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Contracts.Persistance;
using MarsRoverProject.Domain;
using Microsoft.Extensions.Options;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRoverAPI.Redis
{
    public class RedisMarsSurfaceRepository : IMarsSurfaceRepository
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly AppRedisConfiguration _appRedisConfiguration;

        public RedisMarsSurfaceRepository(
            IRedisCacheClient redisCacheClient, 
            IOptions<AppRedisConfiguration> appRedisConfiguration)
        {
            _redisCacheClient = redisCacheClient;
            _appRedisConfiguration = appRedisConfiguration.Value;
        }

        private async Task<MarsSurfaceInfo> GetMarsSurfaceInfo()
        {
            return await _redisCacheClient
                .GetDbFromConfiguration()
                .GetAsync<MarsSurfaceInfo>(_appRedisConfiguration.MarsSurfaceRedisCacheKey);
        }

        private List<Rover> GetAllRovers()
        {
            var surface = GetMarsSurfaceInfo().GetAwaiter().GetResult();

            return surface?.Rovers;
        }

        private void UpdateRovers(List<Rover> rovers)
        {
            var surface = GetMarsSurfaceInfo().GetAwaiter().GetResult();

            if (surface == null)
                surface = new MarsSurfaceInfo(null, null);

            surface.Rovers = rovers;

            _redisCacheClient.GetDbFromConfiguration().AddAsync(
                _appRedisConfiguration.MarsSurfaceRedisCacheKey,
                surface);
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

            UpdateRovers(rovers);

            return true;
        }

        public bool UpdateLocationAndHeading(int x, int y, Location newLocation, string newHeading)
        {
            var rovers = GetAllRovers();

            var target = rovers.FirstOrDefault(r => r.Location.X == x && r.Location.Y == y);

            target.Location = newLocation;
            target.SetHeading(newHeading);

            UpdateRovers(rovers);

            return true;
        }

        public void ClearSurface()
        {
            _redisCacheClient.GetDbFromConfiguration().AddAsync(
                _appRedisConfiguration.MarsSurfaceRedisCacheKey,
                new MarsSurfaceInfo(null, null));
        }
    }
}
