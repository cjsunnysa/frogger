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
        public bool GameIsRunning { get; private set; }
        
        private readonly List<GameObject> _gameObjects;
        private readonly Stopwatch _frameTimer;
        private readonly CarQueueFactory _queueFactory;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _gameObjects = new List<GameObject>();
            _frameTimer = new Stopwatch();
            _queueFactory= new CarQueueFactory(Direction.Left, gameObjectFactory);
        }

        public void InitialiseGame()
        {
            _gameObjects.Clear();
            _queueFactory.Initialise();
            
            CreatePlayers();
            CreateCarQueues();

            GameIsRunning = true;

            _frameTimer.Start();
        }

        public bool GameCycled()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            foreach (var screenObject in _gameObjects)
                screenObject.AutoMove();

            if (PlayerWinDetected())
                RespawnPlayers();
			else if (PlayerCollisionDetected())
				GameIsRunning = false;

            _frameTimer.Restart();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _gameObjects)
                gameObject.Render();
        }

        private void RespawnPlayers()
        {
            foreach (var player in _gameObjects.OfType<Player>())
            {
                player.Respawn();
            }
        }

        private bool PlayerCollisionDetected()
        {
            var deadPlayers = (from p in _gameObjects.OfType<Player>().ToArray()
                               from o in _gameObjects.Except(new[] {p})
                               where p.OrChildrenCollidedWith(o) || o.OrChildrenCollidedWith(p)
                               select p).ToArray();

            foreach (var deadPlayer in deadPlayers)
                ReplacePlayerWithStain(deadPlayer);

            return deadPlayers.Any();
        }

        private bool PlayerWinDetected()
        {
            return _gameObjects.OfType<Player>().Any(player => player.HasWon());
        }

        private void CreatePlayers()
        {
            var player =
                _gameObjectFactory.CreatePlayer(
                    new Position(GameConfig.PLAYER_START_POSITION.XPos, GameConfig.PLAYER_START_POSITION.YPos),
                    Direction.Up);
            
            _gameObjects.Add(player);
        }

        private void CreateCarQueues()
        {
            for (var x = 0; x < GameConfig.CAR_QUEUE_COUNT; x++)
                _gameObjects.Add(_queueFactory.CreateNextQueue());
        }

        private void ReplacePlayerWithStain(Player player)
        {
            var stain = _gameObjectFactory.CreateStainFromPlayer(player);
            _gameObjects.Add(stain);
            _gameObjects.Remove(player);
        }
    }
}