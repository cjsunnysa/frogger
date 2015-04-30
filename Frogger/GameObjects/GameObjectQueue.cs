using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public delegate GameObject FactoryDelegate(Position position);

    public class GameObjectQueue : IMoveable, IRenderable
    {
        internal class OffscreenQueueObject
        {
            public int DistanceToWait { get; set; }
            public GameObject OffscreenGameObject { get; set; }
        }

        
        private readonly int _canvasYPos;
        private readonly List<GameObject> _screenObjects;
        private readonly List<OffscreenQueueObject> _offScreenObjects;
        private readonly Random _numGenerator;
        private readonly Direction _direction;


        public GameObjectQueue(Direction cycleDirection, FactoryDelegate factoryMethod, int canvasYPos, int numQueueObjects)
        {
            _canvasYPos = canvasYPos;
            _screenObjects = new List<GameObject>();
            _offScreenObjects = new List<OffscreenQueueObject>();
            _numGenerator = new Random();
            _direction = cycleDirection;

            switch (cycleDirection)
            {
                case Direction.Left:
                    CreateLeftQueueObjects(factoryMethod, numQueueObjects);
                    break;
                case Direction.Right:
                    CreateRightQueueObjects(factoryMethod, numQueueObjects);
                    break;
                default:
                    throw new InvalidEnumArgumentException("cycleDirection can only be Left or Right.");
            }
            
        }

        public void Cycle()
        {
            foreach (var screenObject in _screenObjects.ToArray())
            {
                screenObject.Move();
                RemoveObjectIfOffscreen(screenObject);
            }

            switch (_direction)
            {
                case Direction.Left:
                    AddOffscreenCarLeft();
                    break;
                case Direction.Right:
                    AddOffscreenCarRight();
                    break;
            }
        }

        public void Move()
        {
            Cycle();
        }

        public void Move(Direction direction)
        {
            Cycle();
        }

        public void Render()
        {
            foreach (var screenObject in _screenObjects)
            {
                screenObject.Render();
            }
        }


        private void CreateRightQueueObjects(FactoryDelegate factoryMethod, int maxCreateCount)
        {
            var createdCount = 0;

            for (var xPos = GameConfig.LEFT_OFFSCREEN_X_POS - GameConfig.CAR_DIMENSION.Width;
                xPos <= GameConfig.RIGHT_OFFSCREEN_X_POS;
                xPos += GenerateDistance())
            {
                CreateQueueObject(factoryMethod, xPos);

                if (++createdCount >= maxCreateCount)
                    return;
            }

            AddRemainderOffscreen(factoryMethod, maxCreateCount, createdCount);
        }

        private void CreateLeftQueueObjects(FactoryDelegate factoryMethod, int maxCreateCount)
        {
            var createdCount = 0;

            for (var xPos = GameConfig.RIGHT_OFFSCREEN_X_POS;
                xPos >= GameConfig.LEFT_OFFSCREEN_X_POS;
                xPos -= GenerateDistance())
            {
                CreateQueueObject(factoryMethod, xPos);

                if (++createdCount >= maxCreateCount)
                    return;
            }

            AddRemainderOffscreen(factoryMethod, maxCreateCount, createdCount);
        }


        private void AddRemainderOffscreen(FactoryDelegate factoryMethod, int maxCreateCount, int createdCount)
        {
            var remainderCount = maxCreateCount - createdCount;
            for (var count = 0; count < remainderCount; count++)
                _offScreenObjects.Add(new OffscreenQueueObject
                {
                    DistanceToWait = GenerateDistance(),
                    OffscreenGameObject = factoryMethod(new Position(0, 0))
                });
        }
        
        private void CreateQueueObject(FactoryDelegate factoryMethod, int xPos)
        {
            var newObject = factoryMethod(new Position(xPos, _canvasYPos));
            _screenObjects.Add(newObject);
        }

        private int GenerateDistance()
        {
            return _numGenerator.Next(GameConfig.CAR_MIN_DISTANCE, GameConfig.CAR_MAX_DISTANCE);
        }

        private void RemoveObjectIfOffscreen(GameObject gameObject)
        {
            var xpos = gameObject.GetPosition().XPos;
            var objectPastScreenLeft = xpos < GameConfig.LEFT_OFFSCREEN_X_POS;
            var objectPastScreenRight = xpos > GameConfig.RIGHT_OFFSCREEN_X_POS;


            if ((gameObject.GetDirection() == Direction.Left && objectPastScreenLeft) ||
                (gameObject.GetDirection() == Direction.Right && objectPastScreenRight))
            {
                _screenObjects.Remove(gameObject);
                _offScreenObjects.Add(new OffscreenQueueObject
                {
                    DistanceToWait = GenerateDistance(),
                    OffscreenGameObject = gameObject
                });
            }
        }
        
        private void AddOffscreenCarLeft()
        {
            var lastxpos = _screenObjects.Where(o => o.GetDirection() == Direction.Left).Max(m => m.GetPosition().XPos);
            var lastQueueObject = _screenObjects.FirstOrDefault(s => s.GetDirection() == Direction.Left && s.GetPosition().XPos.CompareTo(lastxpos) == 0);
            
            if (lastQueueObject == null)
                return;

            
            var carToAdd = _offScreenObjects.FirstOrDefault(o => o.OffscreenGameObject.GetDirection() == Direction.Left);
            if (carToAdd == null)
                return;


            var lastXPos = lastQueueObject.GetPosition().XPos;
            var distanceFromOffScreen = GameConfig.RIGHT_OFFSCREEN_X_POS - lastXPos;


            if (distanceFromOffScreen > carToAdd.DistanceToWait)
            {
                _offScreenObjects.Remove(carToAdd);
                carToAdd.OffscreenGameObject.SetPosition(new Position(GameConfig.RIGHT_OFFSCREEN_X_POS, _canvasYPos));
                _screenObjects.Add(carToAdd.OffscreenGameObject);
            }
        }

        private void AddOffscreenCarRight()
        {
            var lastxpos = _screenObjects.Where(o => o.GetDirection() == Direction.Right).Min(m => m.GetPosition().XPos);
            var lastQueueObject = _screenObjects.FirstOrDefault(s => s.GetDirection() == Direction.Right && s.GetPosition().XPos.CompareTo(lastxpos) == 0);
        
            if (lastQueueObject == null)
                return;


            var carToAdd = _offScreenObjects.FirstOrDefault(o => o.OffscreenGameObject.GetDirection() == Direction.Right);
            if (carToAdd == null)
                return;


            var lastXPos = lastQueueObject.GetPosition().XPos;
            var distanceFromOffScreen = lastXPos - GameConfig.LEFT_OFFSCREEN_X_POS;


            if (distanceFromOffScreen > carToAdd.DistanceToWait)
            {
                _offScreenObjects.Remove(carToAdd);
                carToAdd.OffscreenGameObject.SetPosition(new Position(GameConfig.LEFT_OFFSCREEN_X_POS, _canvasYPos));
                _screenObjects.Add(carToAdd.OffscreenGameObject);
            }
        }
    }
}