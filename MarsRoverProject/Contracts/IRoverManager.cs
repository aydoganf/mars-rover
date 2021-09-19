using MarsRoverProject.Domain;

namespace MarsRoverProject.Contracts
{
    public interface IRoverManager
    {
        Rover Command(Rover rover, MovementCommand command);

        void CheckLocationIsEmpty(int x, int y);
    }
}
