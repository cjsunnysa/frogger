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
        
        private List<GameObject> _gameObjects;
        private readonly Stopwatch _frameTimer;
        private readonly ICreateObjectMethod _method;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly IGameCycleProcedure[] _gameProcedures;

        /// <param name="method">Returns a list of all game objects for the game.</param>
        /// <param name="gameObjectFactory">Creates game objects.</param>
        /// <param name="gameProcedures">Performs win/loss condition checks and responses.</param>
        public GameEngine(ICreateObjectMethod method, IGameObjectFactory gameObjectFactory, IGameCycleProcedure[] gameProcedures)
        {
            _method = method;
            _gameObjectFactory = gameObjectFactory;
            _frameTimer = new Stopwatch();
            
            if (gameProcedures == null || gameProcedures.Any() == false)
                throw new ArgumentNullException("gameProcedures");

            _gameProcedures = gameProcedures;
        }

        public void InitialiseGame()
        {
            _gameObjectFactory.Initialise();
            _gameObjects = _method.CreateGameObjects(_gameObjectFactory);
            
            GameIsRunning = true;

            _frameTimer.Start();
        }

        public bool GameCycled()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            GameIsRunning = PerformGameProcedures();
            
            _frameTimer.Restart();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _gameObjects)
                gameObject.Render();
        }

        #region private methods
        private bool PerformGameProcedures()
        {
            var continueRunning = true;
            
            foreach (var proc in _gameProcedures)
            {
                var keepRunning = proc.Execute(_gameObjects, _gameObjectFactory);
                if (continueRunning)
                    continueRunning = keepRunning;
            }

            return continueRunning;
        }
        #endregion
    }
}