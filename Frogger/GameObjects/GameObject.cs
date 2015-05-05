using System.Text;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
	public abstract class GameObject : IPositionable, IRenderable, IDirectable, IHitTestable, IMoveable
	{
		protected readonly Position _position;
		protected readonly IRenderer _renderer;
		protected Direction _direction;
		protected HitTestArea _hitTestArea;
		protected int _moveSpeed;

		protected GameObject(Position initialPosition, IRenderer renderer, Direction initialDirection, int moveSpeed)
		{
			_position = initialPosition;
			_renderer = renderer;
			_direction = initialDirection;
			_moveSpeed = moveSpeed;
			_hitTestArea = new HitTestArea (new Position(0,0), 0, 0);
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
			_hitTestArea = _renderer.RenderObjectToCanvas(this);
		}

		public void SetDirection(Direction direction)
		{
			_direction = direction;
		}

		public Direction GetDirection()
		{
			return _direction;
		}

		public HitTestArea GetHitTestArea()
		{
			return _hitTestArea;
		}

		public virtual void Move()
		{
			Move(_direction);
		}

		public void Move(Direction direction)
		{
			switch (direction)
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

	    public void MoveRight()
		{
			_position.SetPosition(_position.XPos + _moveSpeed, _position.YPos);
		}

		public void MoveLeft()
		{
			_position.SetPosition(_position.XPos - _moveSpeed, _position.YPos);
		}

		public void MoveUp()
		{
			_position.SetPosition(_position.XPos, _position.YPos - _moveSpeed);
		}

		public void MoveDown()
		{
			_position.SetPosition(_position.XPos, _position.YPos + _moveSpeed);
		}
	}
}