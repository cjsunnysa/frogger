using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     A visual object that replaces the Player object on-screen when the Player object collides with another GameObject.
    ///     This object cannot be moved from its intial position.
    /// </summary>
    public class Stain : GameObject
    {
        private const Direction INITIAL_DIRECTION = Direction.Up;
        private const int MOVE_SPEED = 0;

        public Stain(Position spawnPosition, IRenderer renderer) : base(spawnPosition, renderer, INITIAL_DIRECTION, MOVE_SPEED, null)
        {
        }

        public override void AutoMove()
        { }
    }
}