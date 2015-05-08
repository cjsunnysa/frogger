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
        
        private readonly List<GameObject> _screenObjects;
        private readonly Stopwatch _frameTimer;
        private readonly CarQueueFactory _queueFactory;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _screenObjects = new List<GameObject>();
            _frameTimer = new Stopwatch();
            _queueFactory= new CarQueueFactory(Direction.Left, gameObjectFactory);
        }

        public void InitialiseGame()
        {
            _screenObjects.Clear();
            _queueFactory.Reset();
            
            CreatePlayers();
            CreateCarQueues();

            GameRunning = true;
            _frameTimer.Start();
        }

        public bool CycleGame()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            foreach (var screenObject in _screenObjects)
                screenObject.AutoMove();

            if (PlayerCollisionDetected())
                GameRunning = false;

            if (PlayerWinDetected())
                RespawnPlayers();

            _frameTimer.Restart();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _screenObjects)
                gameObject.Render();
        }


        private void RespawnPlayers()
        {
            foreach (var player in _screenObjects.OfType<Player>())
            {
                player.Respawn();
            }
        }

        private bool PlayerCollisionDetected()
        {
            var deadPlayers = (from p in _screenObjects.OfType<Player>().ToArray()
                               from o in _screenObjects.Except(new[] {p})
                               where p.OrChildrenCollidedWith(o) || o.OrChildrenCollidedWith(p)
                               select p).ToArray();

            foreach (var deadPlayer in deadPlayers)
                ReplacePlayerWithStain(deadPlayer);

            return deadPlayers.Any();
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
            for (var x = 0; x < GameConfig.CAR_QUEUE_COUNT; x++)
                _screenObjects.Add(_queueFactory.CreateNextQueue());
        }

        private void ReplacePlayerWithStain(Player player)
        {
            var stain = _gameObjectFactory.CreateStainFromPlayer(player);
            _screenObjects.Add(stain);
            _screenObjects.Remove(player);
        }
    }
}