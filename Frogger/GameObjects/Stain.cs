using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.GameObjects
{
    public class Stain : GameObject
    {
        private const Direction INITIAL_DIRECTION = Direction.Up;
        private const int MOVE_SPEED = 0;

        public Stain(Position initialPosition, IRenderer renderer) : base(initialPosition, renderer, INITIAL_DIRECTION, MOVE_SPEED)
        {
        }
    }
}