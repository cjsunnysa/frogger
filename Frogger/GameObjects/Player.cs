using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Player : GameObject
    {
        public Player(Position initialPosition, IRenderer renderer, Direction initialDirection) : base(initialPosition, renderer, initialDirection)
        {
        }
    }
}