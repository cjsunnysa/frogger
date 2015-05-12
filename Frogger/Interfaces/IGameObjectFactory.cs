using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IGameObjectFactory
    {
        void Initialise();
        Player CreatePlayer(Position startPosition, Direction initialDirection);
        Car CreateCarDrivingLeft(Position startPosition);
        Car CreateCarDrivingRight(Position startPosition);
        Stain CreateStainFromPlayer(Player player);
        GameObjectQueue CreateNextQueue();
    }
}