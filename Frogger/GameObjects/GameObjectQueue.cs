using System;
using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Delegates;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Renderers;

namespace ChrisJones.Frogger.GameObjects
{
    public abstract class GameObjectQueue : GameObject
    {
        internal class OffscreenQueueObject
        {
            public int DistanceToWait { get; set; }
            public GameObject Object { get; set; }
        }

        private readonly List<OffscreenQueueObject> _offScreenObjects;
        private readonly Random _numGenerator;

        protected abstract int GetDistanceToLastObject();
        protected abstract bool ObjectPastEndOfQueue(GameObject gameObject);
        protected abstract bool EnumerateXPosRange(ref int xpos);
    
        protected GameObjectQueue(Position intitialPosition, Direction initialDirection, int moveSpeed, FactoryDelegate factoryMethod, int numQueueObjects)
            : base(intitialPosition, new NullRenderer(), initialDirection, moveSpeed)
        {
            _offScreenObjects = new List<OffscreenQueueObject>();
            _numGenerator = new Random();

            CreateQueueObjects(factoryMethod, numQueueObjects);
        }

        private void CreateQueueObjects(FactoryDelegate factoryMethod, int maxCreateCount)
        {
            var createdCount = 0;

            var xpos = (int) Position.XPos;
            do
            {
                CreateQueueObject(factoryMethod, xpos);

                if (++createdCount >= maxCreateCount)
                    return;

            } while (EnumerateXPosRange(ref xpos));

            AddRemainderOffscreen(factoryMethod, maxCreateCount, createdCount);
        }
        
        private void AddRemainderOffscreen(FactoryDelegate factoryMethod, int maxCreateCount, int createdCount)
        {
            var remainderCount = maxCreateCount - createdCount;
            for (var count = 0; count < remainderCount; count++)
                _offScreenObjects.Add(new OffscreenQueueObject
                {
                    DistanceToWait = GenerateDistance(),
                    Object = factoryMethod(new Position(0, 0))
                });
        }
        
        private void CreateQueueObject(FactoryDelegate factoryMethod, int xPos)
        {
            var newObject = factoryMethod(new Position(xPos, Position.YPos));
            ChildObjects.Add(newObject);
        }

        private void RemoveObjectIfOffscreen(GameObject gameObject)
        {
            if (!ObjectPastEndOfQueue(gameObject))
                return;

            ChildObjects.Remove(gameObject);
            _offScreenObjects.Add(new OffscreenQueueObject
            {
                DistanceToWait = GenerateDistance(),
                Object = gameObject
            });
        }
        
        private void AddOffscreenObjectToOnscreen()
        {
            var offscreenObjectToAdd = _offScreenObjects.FirstOrDefault();
            if (offscreenObjectToAdd == null)
                return;

            var distanceToLastObject = GetDistanceToLastObject();

            if (distanceToLastObject < offscreenObjectToAdd.DistanceToWait) 
                return;

            _offScreenObjects.Remove(offscreenObjectToAdd);
            offscreenObjectToAdd.Object.ChangeSpawnPosition(Position);
            offscreenObjectToAdd.Object.Respawn();
            ChildObjects.Add(offscreenObjectToAdd.Object);
        }

        private void CycleQueue()
        {
            foreach (var screenObject in ChildObjects.ToArray())
            {
                screenObject.AutoMove();
                RemoveObjectIfOffscreen(screenObject);
            }

            AddOffscreenObjectToOnscreen();
        }

        protected int GenerateDistance()
        {
            return _numGenerator.Next(GameConfig.CAR_MIN_DISTANCE, GameConfig.CAR_MAX_DISTANCE);
        }

        public override void AutoMove()
        {
            CycleQueue();
        }
    }
}