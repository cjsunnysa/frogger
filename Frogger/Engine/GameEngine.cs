using System;
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
    /// <summary>
    ///     Handles the orchestration of the game. Creates the game objects, handles their movement and interaction.
    /// </summary>
    public class GameEngine
    {
        public bool GameIsRunning { get; private set; }
        
        private readonly List<GameObject> _gameObjects;
        private readonly Stopwatch _frameTimer;
        private readonly GameObjectQueueFactory _queueFactory;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IGameCheckProcedure[] _winCheckProcedures;
        private readonly IGameCheckProcedure[] _loseCheckProcedures;

        /// <param name="gameObjectFactory">Creates all the game objects for the game.</param>
        /// <param name="winCheckProcedures">Methods to be executed when Player wins the game.</param>
        /// <param name="loseCheckProcedures">Methods to be executed when Player loses the game.</param>
        public GameEngine(IGameObjectFactory gameObjectFactory, IGameCheckProcedure[] winCheckProcedures, IGameCheckProcedure[] loseCheckProcedures)
        {
            _gameObjectFactory = gameObjectFactory;
            _gameObjects = new List<GameObject>();
            _frameTimer = new Stopwatch();
            _queueFactory= new GameObjectQueueFactory(gameObjectFactory);
            
            if (winCheckProcedures == null || winCheckProcedures.Any() == false)
                throw new ArgumentNullException("winCheckProcedures");

            if (loseCheckProcedures == null || loseCheckProcedures.Any() == false)
                throw new ArgumentNullException("loseCheckProcedures");
            
            _winCheckProcedures = winCheckProcedures;
            _loseCheckProcedures = loseCheckProcedures;
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

            GameIsRunning = PerformWinProcedures() && PerformLoseProcedures();
            
            _frameTimer.Restart();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _gameObjects)
                gameObject.Render();
        }

        #region private methods

        private bool PerformLoseProcedures()
        {
            var continueRunning = true;
            
            foreach (var proc in _loseCheckProcedures)
            {
                var keepRunning = proc.Execute(_gameObjects, _gameObjectFactory, _queueFactory);
                if (continueRunning)
                    continueRunning = keepRunning;
            }

            return continueRunning;
        }

        private bool PerformWinProcedures()
        {
            var continueRunning = true;

            foreach (var proc in _winCheckProcedures)
            {
                var keepRunning = proc.Execute(_gameObjects, _gameObjectFactory, _queueFactory);
                if (continueRunning)
                    continueRunning = keepRunning;
            }

            return continueRunning;
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

        #endregion
    }
}