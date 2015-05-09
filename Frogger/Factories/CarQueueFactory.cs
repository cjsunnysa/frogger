using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Factories
{
    public class CarQueueFactory
    {
        private readonly Direction _initialDirection;
		private readonly IGameObjectFactory _factory;
        private Direction _nextQueueDirection;
        private int _totalQueueCount;
        private int _directionQueueCount;

        public CarQueueFactory(Direction firstQueueDirection, IGameObjectFactory factory)
        {
            _initialDirection = firstQueueDirection;
            _nextQueueDirection = firstQueueDirection;
            _factory = factory;
        }

        public void Initialise()
        {
            _nextQueueDirection = _initialDirection;
            _totalQueueCount = 0;
            _directionQueueCount = 0;
        }

        public GameObjectQueue CreateNextQueue()
        {
            var ypos = GameConfig.CAR_QUEUE_START_YPOS + (GameConfig.CAR_QUEUE_YPOS_OFFSET * _totalQueueCount);

			var _thisQueueDirection = _nextQueueDirection; 

            _totalQueueCount++;
            _directionQueueCount++;

            ResolveNextQueueDirection ();

			if (_thisQueueDirection == Direction.Left)
				return new GameObjectQueueLeft (ypos, GameConfig.CAR_SPEED, _factory.CreateCarDrivingLeft, 10);

			return new GameObjectQueueRight(ypos, GameConfig.CAR_SPEED, _factory.CreateCarDrivingRight, 10);
        }

		private void ResolveNextQueueDirection ()
		{
			if (_directionQueueCount >= GameConfig.CHANGE_DIRECTION_EVERY_X_QUEUE) {
				_nextQueueDirection = _nextQueueDirection == Direction.Left ? Direction.Right : Direction.Left;
				_directionQueueCount = 0;
			}
		}
    }
}
