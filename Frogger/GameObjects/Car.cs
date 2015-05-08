using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Car : GameObject
    {
        public Car(Position spawnPosition, IRenderer renderer, Direction initialDirection, int speed) : base(spawnPosition, renderer, initialDirection, speed)
        {
        }
    }
}