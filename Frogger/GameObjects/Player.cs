using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Player : GameObject
    {
        public Player(Position spawnPosition, IRenderer renderer, Direction initialDirection, int speed, IKeyMapper keyMapper) : base(spawnPosition, renderer, initialDirection, speed)
        {
            if (keyMapper != null)
            {
                keyMapper.OnMoveUpEvent += base.MoveUp;
                keyMapper.OnMoveDownEvent += base.MoveDown;
                keyMapper.OnMoveLeftEvent += base.MoveLeft;
                keyMapper.OnMoveRightEvent += base.MoveRight;
            }
        }

        public bool HasWon()
        {
            return this.Position.YPos < 0;
        }

        public override void AutoMove()
        { }
    }
}