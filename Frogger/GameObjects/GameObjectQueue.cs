using System;
using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Delegates;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;
using ChrisJones.Frogger.Renderers;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     The base class for GameObjectQueues. Queues for a specific direction inherit from this class.
    ///     Creates a queue of GameObjects and moves them in a direction. When an object reaches the end of the queue it is removed from the screen and waits to be added to the start of the queue.
    /// </summary>
    public abstract class GameObjectQueue : GameObject
    {
        /// <summary>
        ///     Used to hold offscreeen GameObjects and the distance they will be added from the last object in the queue.
        /// </summary>
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

        /// <param name="intitialPosition">Only the YPos of the Position is used. The vertical position of the queue on the screeen.</param>
        /// <param name="initialDirection">The direction all objects in the queue are traveling.</param>
        /// <param name="moveSpeed">The horizontal distance all object of the queue will travel each frame that is rendered.</param>
        /// <param name="childCreateMethod">The factory method that will create the objects managed by this queue.</param>
        /// <param name="numQueueObjects">The maximum number of objects the queue can create and manage.</param>
        /// <param name="winConditions">Used to determine if this object has won the game.</param>
        protected GameObjectQueue(Position intitialPosition, Direction initialDirection, int moveSpeed, ChildObjectCreateMethod childCreateMethod, int numQueueObjects, IWinCondition[] winConditions)
            : base(intitialPosition, new NullRenderer(), initialDirection, moveSpeed, winConditions)
        {
            _offScreenObjects = new List<OffscreenQueueObject>();
            _numGenerator = new Random();

            CreateQueueObjects(childCreateMethod, numQueueObjects);
        }

        public override void AutoMove()
        {
            CycleQueue();
        }

        protected int GenerateDistance()
        {
            return _numGenerator.Next(GameConfig.CAR_MIN_DISTANCE, GameConfig.CAR_MAX_DISTANCE);
        }

        #region private methods
        private void CreateQueueObjects(ChildObjectCreateMethod factoryMethod, int maxCreateCount)
        {
            var createdCount = 0;

            var xpos = (int)Position.XPos;
            do
            {
                CreateQueueObject(factoryMethod, xpos);

                if (++createdCount >= maxCreateCount)
                    return;

            } while (EnumerateXPosRange(ref xpos));

            AddRemainderOffscreen(factoryMethod, maxCreateCount, createdCount);
        }

        private void AddRemainderOffscreen(ChildObjectCreateMethod factoryMethod, int maxCreateCount, int createdCount)
        {
            var remainderCount = maxCreateCount - createdCount;
            for (var count = 0; count < remainderCount; count++)
                _offScreenObjects.Add(new OffscreenQueueObject
                {
                    DistanceToWait = GenerateDistance(),
                    Object = factoryMethod(new Position(0, 0))
                });
        }

        private void CreateQueueObject(ChildObjectCreateMethod factoryMethod, int xPos)
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
        #endregion

    }
}