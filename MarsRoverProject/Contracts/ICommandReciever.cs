using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Domain;

namespace MarsRoverProject.Contracts
{
    public interface ICommandReciever
    {
        IMarsSurfaceInfo SendMessageToSurface(MarsSurfaceCommand marsSurfaceCommand);

        IMarsSurfaceInfo LandTheRoverOnMars(Rover rover);

        IMarsSurfaceInfo LandTheRoverOnMars(string roverName, int x, int y, string heading);

        IMarsSurfaceInfo LandAndMoveOnMarsSurface(MarsSurfaceCommand marsSurfaceCommand);

        IMarsSurfaceInfo GetSurfaceInfo();

        void ClearMarsSurface();
    }
}
