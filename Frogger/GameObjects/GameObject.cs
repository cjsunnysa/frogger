using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public abstract class GameObject
    {
        public Position Position { get; private set; }
        public HitTestArea HitTestArea { get; private set; }
        
        private readonly IRenderer _renderer;
        private readonly int _moveSpeed;
        private Direction _direction;
        private Position _spawnPosition;
        
        protected List<GameObject> ChildObjects { get; private set; }
        
        protected GameObject(Position spawnPosition, IRenderer renderer, Direction initialDirection, int moveSpeed)
        {
            Position = new Position(spawnPosition.XPos, spawnPosition.YPos);
            HitTestArea = new HitTestArea(new Position(0, 0), 0, 0);
            ChildObjects = new List<GameObject>(); 
            
            _renderer = renderer;
            _direction = initialDirection;
            _moveSpeed = moveSpeed;
            _spawnPosition = spawnPosition;
        }

        public void ChangeSpawnPosition(Position position)
        {
            _spawnPosition = new Position(position.XPos, position.YPos);
        }

        public void Respawn()
        {
            Position = new Position(_spawnPosition.XPos, _spawnPosition.YPos);
        }

        public void Render()
        {
            HitTestArea = _renderer.RenderObjectToCanvas(this);

            foreach (var childObject in ChildObjects)
                childObject.Render();
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public bool OrChildrenCollidedWith(GameObject otherObject)
        {
            return HitTestArea.HasCollidedWith(otherObject.HitTestArea) ||
                   ChildObjects.Any(child => child.OrChildrenCollidedWith(otherObject));
        }

        public virtual void AutoMove()
        {
            Move(_direction);
            
            foreach (var childObject in ChildObjects)
                childObject.AutoMove();
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
            Position.SetPosition(Position.XPos + _moveSpeed, Position.YPos);
        }

        public void MoveLeft()
        {
            Position.SetPosition(Position.XPos - _moveSpeed, Position.YPos);
        }

        public void MoveUp()
        {
            Position.SetPosition(Position.XPos, Position.YPos - _moveSpeed);
        }

        public void MoveDown()
        {
            Position.SetPosition(Position.XPos, Position.YPos + _moveSpeed);
        }
    }
}