using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Factories
{
    /// <summary>
    ///     Creates game object queues which cycle object in a left or right direction on-screen.
    /// </summary>
    public class GameObjectQueueFactory
    {
        private readonly IGameObjectFactory _factory;
        private Direction _nextQueueDirection;
        private int _totalQueueCount;
        private int _directionQueueCount;

        
        /// <param name="factory">Creates game objects for the queue to manage.</param>
        public GameObjectQueueFactory(IGameObjectFactory factory)
        {
            _factory = factory;
        }

        public void Initialise()
        {
            _nextQueueDirection = GameConfig.DIRECTION_FIRST_QUEUE;
            _totalQueueCount = 0;
            _directionQueueCount = 0;
        }

        public GameObjectQueue CreateNextQueue()
        {
            var ypos = GameConfig.CAR_QUEUE_START_YPOS + (GameConfig.CAR_QUEUE_YPOS_OFFSET * _totalQueueCount);

            var thisQueueDirection = _nextQueueDirection; 

            _totalQueueCount++;
            _directionQueueCount++;

            ResolveNextQueueDirection ();

            if (thisQueueDirection == Direction.Left)
                return new GameObjectQueueLeft (ypos, GameConfig.CAR_SPEED, _factory.CreateCarDrivingLeft, 10);

            return new GameObjectQueueRight(ypos, GameConfig.CAR_SPEED, _factory.CreateCarDrivingRight, 10);
        }

        #region private methods
        private void ResolveNextQueueDirection()
        {
            if (_directionQueueCount >= GameConfig.CHANGE_DIRECTION_EVERY_X_QUEUE)
            {
                _nextQueueDirection = _nextQueueDirection == Direction.Left ? Direction.Right : Direction.Left;
                _directionQueueCount = 0;
            }
        } 
        #endregion
    }
}
