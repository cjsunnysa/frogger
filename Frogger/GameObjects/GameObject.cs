using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public abstract class GameObject : IPositionable, IRenderable, IDirectable, IPathObject, IMoveable
    {
        protected readonly Position _position;
        protected readonly IRenderer _renderer;
        protected Direction _direction;
        protected ShapePath _renderedPath;
        protected int _moveSpeed;

        protected GameObject(Position initialPosition, IRenderer renderer, Direction initialDirection, int moveSpeed)
        {
            _position = initialPosition;
            _renderer = renderer;
            _direction = initialDirection;
            _moveSpeed = moveSpeed;
        }

        public void SetPosition(Position position)
        {
            _position.SetPosition(position.XPos, position.YPos);
        }

        public Position GetPosition()
        {
            return _position;
        }

        public void Render()
        {
            _renderedPath = _renderer.RenderObjectToCanvas(this);
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public ShapePath GetObjectPath()
        {
            return _renderedPath;
        }

        public virtual void Move()
        {
            Move(_direction);
        }

        public void Move(Direction direction)
        {
            switch (_direction)
            {
                case Direction.Left:
                    MoveLeft();
                    break;
                case Direction.Right:
                    MoveRight();
                    break;
                case Direction.Up:
                    MoveUp();
                    break;
                case Direction.Down:
                    MoveDown();
                    break;
            }
        }

        private void MoveRight()
        {
            _position.SetPosition(_position.XPos + _moveSpeed, _position.YPos);
        }

        private void MoveLeft()
        {
            _position.SetPosition(_position.XPos - _moveSpeed, _position.YPos);
        }

        private void MoveUp()
        {
            _position.SetPosition(_position.XPos, _position.YPos - _moveSpeed);
        }

        private void MoveDown()
        {
            _position.SetPosition(_position.XPos, _position.YPos + _moveSpeed);
        }
    }
}