using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Player : GameObject
    {
        public Player(Position initialPosition, IRenderer renderer, Direction initialDirection, int speed, IKeyMapper keyMapper) : base(initialPosition, renderer, initialDirection, speed)
        {
            if (keyMapper != null)
            {
                keyMapper.OnMoveUpEvent += base.MoveUp;
                keyMapper.OnMoveDownEvent += base.MoveDown;
                keyMapper.OnMoveLeftEvent += base.MoveLeft;
                keyMapper.OnMoveRightEvent += base.MoveRight;
            }
        }

        public override void Move()
        { }
    }
}