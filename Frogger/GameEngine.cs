using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using GLib;

namespace ChrisJones.Frogger
{
    public class GameEngine
    {
		private readonly List<Player> _players;
		private readonly List<GameObjectQueue> _queues;
		private readonly IGameObjectFactory _gameObjectFactory;
        private readonly Stopwatch _frameTimer;
		private readonly CollisionDetector _collisionDetector;

		private GameObject[] _gameObjects
		{
			get 
			{
				return (from q in _queues
			        from s in q.ScreenObjects
			        select s).Union (_players).ToArray();
			}
		}

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _players = new List<Player>();
			_queues = new List<GameObjectQueue>();
            _frameTimer = new Stopwatch();
			_collisionDetector = new CollisionDetector ();
        }

        public void StartGame()
        {
            CreatePlayers();
            CreateQueues ();

            _frameTimer.Start();
        }

        public bool GameCycle()
        {
            if (_frameTimer.ElapsedMilliseconds <= GameConfig.DRAW_FRAME_EVERY_MILLISECONDS)
                return false;


            _frameTimer.Restart();

			foreach (var queueObject in _queues)
				queueObject.Cycle ();
			foreach(var player in _players)						 
                player.Move();

			return true;
        }

        public void Render()
        {
            DrawAllObjectsToCanvas();
        }

		public bool CollisionDetected()
		{
			var queueObjects = (from q in _queues
			                    from s in q.ScreenObjects
			                    select s).ToArray();

			foreach (var player in _players) 
			{
				if (_collisionDetector.CheckForCollisions (player, queueObjects))
					return true;
			}

			return false;
		}

        
        private void CreatePlayers()
        {
			_players.Clear();

			_players.Add(_gameObjectFactory.CreatePlayer(GameConfig.PLAYER_START_POSITION, Direction.Up));
        }

		void CreateQueues ()
		{
			_queues.Clear();

			CreateLeftCarQueue ();
			//CreateRightCarQueue ();
		}

        private void CreateLeftCarQueue()
        {
            var queue = new GameObjectQueue(Direction.Left, _gameObjectFactory.CreateCarDrivingLeft,
                GameConfig.CARS_LEFT_Y_POS, 12);

            _queues.Add(queue);
        }

        private void CreateRightCarQueue()
        {
            var queue = new GameObjectQueue(Direction.Right, _gameObjectFactory.CreateCarDrivingRight,
                GameConfig.CARS_RIGHT_Y_POS, 12);

            _queues.Add(queue);
        }
        
        private void DrawAllObjectsToCanvas()
        {
			foreach(var gameObject in _gameObjects)						 
                gameObject.Render();
        }
    }
}