using MarsRoverAPI.Model;
using MarsRoverAPI.Requests;
using MarsRoverAPI.Responses;
using MarsRoverProject.Contracts;
using MarsRoverProject.Contracts.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRoverAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarsSurfaceController : ControllerBase
    {
        private readonly ICommandReciever _commandReciever;

        public MarsSurfaceController(ICommandReciever commandReciever)
        {
            _commandReciever = commandReciever;
        }

        [HttpGet]
        public GetMarsSurfaceInfoResponse GetMarsSurfaceInfo()
        {
            try
            {
                var surface = _commandReciever.GetSurfaceInfo();

                return new GetMarsSurfaceInfoResponse
                {
                    Data = ModelBuilder.BuildMarsSurfaceInfo(surface)
                };
            }
            catch (Exception ex)
            {
                return new GetMarsSurfaceInfoResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }            
        }

        [HttpPost("land")]
        public LandTheRoverOnMarsResponse LandTheRoverOnMars(LandTheRoverOnMarsRequest request)
        {
            try
            {
                var surface = _commandReciever.LandTheRoverOnMars(
                    request.RoverName, 
                    request.Location.X, 
                    request.Location.Y, 
                    request.Heading);

                var landedRover = surface.Rovers
                    .FirstOrDefault(r => r.Location.X == request.Location.X && r.Location.Y == request.Location.Y);

                return new LandTheRoverOnMarsResponse
                {
                    Data = ModelBuilder.BuildRoverInfo(landedRover)
                };
            }
            catch (Exception ex)
            {
                return new LandTheRoverOnMarsResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpPost("sendMessage")]
        public SendMessageToMarsSurfaceResponse SendMessageToMarsSurface(SendMessageToMarsSurfaceRequest request)
        {
            try
            {
                var surface = _commandReciever.SendMessageToSurface(
                    MarsRoverProject.Domain.MarsSurfaceCommand.With(
                        x: request.BorderX,
                        y: request.BorderY,
                        commands: request.RoverCommands
                            .Select(c => new { c.Selector, c.Command })
                            .Select(c => (c.Selector, c.Command))
                            .ToList()
                    )
                );

                return new SendMessageToMarsSurfaceResponse
                {
                    Data = ModelBuilder.BuildMarsSurfaceInfo(surface)
                };
            }
            catch (Exception ex)
            {
                return new SendMessageToMarsSurfaceResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpPost("landAndMove")]
        public LandAndMoveResponse LandAndMove(LandAndMoveRequest request)
        {
            try
            {
                var surface = _commandReciever.LandAndMoveOnMarsSurface(
                    MarsRoverProject.Domain.MarsSurfaceCommand.With(
                        x: request.BorderX,
                        y: request.BorderY,
                        commands: request.Commands
                            .Select(c => new { c.Selector, c.Command })
                            .Select(c => (c.Selector, c.Command))
                            .ToList()
                    )
               );

                return new LandAndMoveResponse
                {
                    Data = ModelBuilder.BuildMarsSurfaceInfo(surface)
                };
            }
            catch (Exception ex)
            {
                return new LandAndMoveResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
