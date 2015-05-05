using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Factories
{
    public class CarQueueFactory
    {
        private Direction _initialDirection;
        private Direction _nextQueueDirection;
        private readonly IGameObjectFactory _factory;
        private int _totalQueueCount;
        private int _directionQueueCount;

        public CarQueueFactory(Direction firstQueueDirection, IGameObjectFactory factory)
        {
            _initialDirection = firstQueueDirection;
            _nextQueueDirection = firstQueueDirection;
            _factory = factory;
        }

        public void Reset()
        {
            _nextQueueDirection = _initialDirection;
            _totalQueueCount = 0;
            _directionQueueCount = 0;
        }

        public GameObjectQueue CreateNextQueue()
        {
            var ypos = GameConfig.CAR_QUEUE_START_YPOS + (GameConfig.CAR_QUEUE_YPOS_OFFSET * _totalQueueCount);


            GameObjectQueue queue = null;

            switch (_nextQueueDirection)
            {
                case Direction.Left:
                    queue = new GameObjectQueue(Direction.Left, _factory.CreateCarDrivingLeft, ypos, 10);
                    break;
                case Direction.Right:
                    queue = new GameObjectQueue(Direction.Right, _factory.CreateCarDrivingRight, ypos, 10);
                    break;
            }

            _totalQueueCount++;
            _directionQueueCount++;

            if (_directionQueueCount >= GameConfig.CHANGE_DIRECTION_EVERY_X_QUEUE)
            {
                _directionQueueCount = 0;
                _nextQueueDirection = _nextQueueDirection == Direction.Left ? Direction.Right : Direction.Left;
            }

            return queue;
        }
    }
}
