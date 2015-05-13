    using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     The base class of all interactive game objects.
    ///     Handles movement, rendering, win condition checks, position, direction and collisions of game objects.
    ///     Can have children GameObject objects.
    /// </summary>
    public abstract class GameObject
    {
        public Position Position { get; private set; }
        public HitTestArea HitTestArea { get; private set; }
        
        protected List<GameObject> ChildObjects { get; private set; }
        
        private readonly IRenderer _renderer;
        private readonly Direction _direction;
        private readonly IWinCondition[] _winConditions;
        private readonly int _moveSpeed;
        private Position _spawnPosition;


        /// <param name="spawnPosition">The starting position on-screen for this object.</param>
        /// <param name="renderer">Used to render a visual representation of this object to a surface.</param>
        /// <param name="initialDirection">This direction this object or its children face when first created.</param>
        /// <param name="moveSpeed">The distance this object can travel on-screen each game-cycle.</param>
        /// <param name="winConditions">Used to determine if this object has won the game.</param>
        protected GameObject(Position spawnPosition, IRenderer renderer, Direction initialDirection, int moveSpeed, IWinCondition[] winConditions)
        {
            Position = new Position(spawnPosition.XPos, spawnPosition.YPos);
            HitTestArea = new HitTestArea(new Position(0, 0), 0, 0);
            ChildObjects = new List<GameObject>(); 
            
            _renderer = renderer;
            _direction = initialDirection;
            _moveSpeed = moveSpeed;
            _spawnPosition = spawnPosition;
            _winConditions = winConditions;
        }

        protected void MoveRight()
        {
            Position.SetPosition(Position.XPos + _moveSpeed, Position.YPos);
        }

        protected void MoveLeft()
        {
            Position.SetPosition(Position.XPos - _moveSpeed, Position.YPos);
        }

        protected void MoveUp()
        {
            Position.SetPosition(Position.XPos, Position.YPos - _moveSpeed);
        }

        protected void MoveDown()
        {
            Position.SetPosition(Position.XPos, Position.YPos + _moveSpeed);
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

        public bool CollidedWith(GameObject otherObject)
        {
            return HitTestArea.HasCollidedWith(otherObject.HitTestArea) ||
                   ChildObjects.Any(child => child.CollidedWith(otherObject));
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

        public bool HasWon()
        {
            return _winConditions != null && _winConditions.Any(c => c.WonTheGame(this));
        }
    }
}