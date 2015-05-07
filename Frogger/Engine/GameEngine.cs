using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Engine
{
    public class GameEngine
    {
        public bool GameRunning { get; private set; }
        
        private readonly List<object> _screenObjects;
        private readonly Stopwatch _frameTimer;
        private readonly CarQueueFactory _queueFactory;
        private readonly CollisionDetector _collisionDetector;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _screenObjects = new List<object>();
            _frameTimer = new Stopwatch();
            _queueFactory= new CarQueueFactory(Direction.Left, gameObjectFactory);
            _collisionDetector = new CollisionDetector();
        }

        public void InitialiseGame()
        {
            _screenObjects.Clear();
            _queueFactory.Reset();
            
            CreatePlayers();
            
            for(var x = 0; x < GameConfig.CAR_QUEUE_COUNT; x++)
                CreateCarQueues();

            GameRunning = true;
            _frameTimer.Start();
        }

        public bool CycleGame()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            foreach (var screenObject in _screenObjects.OfType<IMoveable>())
                screenObject.Move();

            if (CollisionDetected())
                GameRunning = false;

            if (PlayerWinDetected())
                InitialiseGame();

            _frameTimer.Restart();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _screenObjects.OfType<IRenderable>())
                gameObject.Render();
        }

        
        private bool CollisionDetected()
        {
            var queueObjects = (from q in _screenObjects.OfType<GameObjectQueue>()
                                from s in q.ScreenObjects
                                select s).ToArray();

            var players = _screenObjects.OfType<Player>().ToArray();

            foreach (var player in players)
            {
                var collisionObjects = players.Where(p => p != player).Union(queueObjects).ToArray();
                if (_collisionDetector.CheckForCollisions(player, collisionObjects))
                {
                    _screenObjects.Remove(player);
                    CreateStainForPlayer(player);
                    return true;
                }
            }

            return false;
        }

        private void CreateStainForPlayer(Player player)
        {
            var stain = _gameObjectFactory.CreateStainFromPlayer(player);
            _screenObjects.Add(stain);
        }

        private bool PlayerWinDetected()
        {
            return _screenObjects.OfType<Player>().Any(player => player.HasWon());
        }

        private void CreatePlayers()
        {
            var player =
                _gameObjectFactory.CreatePlayer(
                    new Position(GameConfig.PLAYER_START_POSITION.XPos, GameConfig.PLAYER_START_POSITION.YPos),
                    Direction.Up);
            
            _screenObjects.Add(player);
        }

        private void CreateCarQueues()
        {
            _screenObjects.Add(_queueFactory.CreateNextQueue());
        }
    }
}