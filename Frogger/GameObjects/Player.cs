using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     A GameObject whose movement is controlled by a KeyMapper. This GameObject also has a win condition.
    ///     This object does not automatically change position every game cycle.
    /// </summary>
    public class Player : GameObject
    {
        /// <param name="spawnPosition">Initial position on-screen of this object.</param>
        /// <param name="renderer">Used to draw a visual representation of this object to a drawing surface.</param>
        /// <param name="initialDirection">The direction this object faces when first created.</param>
        /// <param name="speed">The distance this object can travel when moved.</param>
        /// <param name="keyMapper">Used to map keypressed events to movement commands.</param>
        public Player(Position spawnPosition, IRenderer renderer, Direction initialDirection, int speed, IKeyMapper keyMapper, IWinCondition[] winConditions) : base(spawnPosition, renderer, initialDirection, speed, winConditions)
        {
            if (keyMapper != null)
            {
                keyMapper.OnMoveUpEvent += base.MoveUp;
                keyMapper.OnMoveDownEvent += base.MoveDown;
                keyMapper.OnMoveLeftEvent += base.MoveLeft;
                keyMapper.OnMoveRightEvent += base.MoveRight;
            }
        }

        public override void AutoMove()
        { }
    }
}