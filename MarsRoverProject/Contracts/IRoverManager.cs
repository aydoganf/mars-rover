using MarsRoverProject.Contracts.Data;
using MarsRoverProject.Domain;

namespace MarsRoverProject.Contracts
{
    public interface IRoverManager
    {
        Rover Command(Rover rover, IMovementCommand command);

        void CheckLocationIsEmpty(int x, int y);
    }
}
