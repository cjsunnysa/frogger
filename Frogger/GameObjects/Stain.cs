using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Stain : GameObject
    {
        private const Direction INITIAL_DIRECTION = Direction.Up;
        private const int MOVE_SPEED = 0;

        public Stain(Position spawnPosition, IRenderer renderer) : base(spawnPosition, renderer, INITIAL_DIRECTION, MOVE_SPEED)
        {
        }

        public override void AutoMove()
        { }
    }
}