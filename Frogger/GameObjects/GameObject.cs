using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public abstract class GameObject : IPositionable, IRenderable, IDirectable, IPathObject
    {
        protected readonly Position _position;
        protected readonly IRenderer _renderer;
        protected Direction _direction;
        protected ShapePath _renderedPath;

        protected GameObject(Position initialPosition, IRenderer renderer, Direction initialDirection)
        {
            _position = initialPosition;
            _renderer = renderer;
            _direction = initialDirection;
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
    }
}