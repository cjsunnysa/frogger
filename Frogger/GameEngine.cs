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
        private readonly List<object> _screenObjects;
        private readonly IGameObjectFactory _gameObjectFactory;
        private readonly Stopwatch _frameTimer;
        

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _screenObjects = new List<object>();
            _frameTimer = new Stopwatch();
        }

        public void StartGame()
        {
            _screenObjects.Clear();
            
            CreatePlayers();
            CreateLeftCarQueue();
            CreateRightCarQueue();

            _frameTimer.Start();
        }

        public bool GameCycle()
        {
            if (_frameTimer.ElapsedMilliseconds <= GameConfig.DRAW_FRAME_EVERY_MILLISECONDS)
                return false;


            _frameTimer.Restart();

            foreach (var gameObject in _screenObjects.Cast<IMoveable>())
                gameObject.Move();

            return true;
        }

        public void Render()
        {
            DrawAllObjectsToCanvas();
        }

        
        private void CreatePlayers()
        {
            _screenObjects.Add(_gameObjectFactory.CreatePlayer(GameConfig.PLAYER_START_POSITION, Direction.Up));
        }

        private void CreateLeftCarQueue()
        {
            var queue = new GameObjectQueue(Direction.Left, _gameObjectFactory.CreateCarDrivingLeft,
                GameConfig.CARS_LEFT_Y_POS, 12);

            _screenObjects.Add(queue);
        }

        private void CreateRightCarQueue()
        {
            var queue = new GameObjectQueue(Direction.Right, _gameObjectFactory.CreateCarDrivingRight,
                GameConfig.CARS_RIGHT_Y_POS, 12);

            _screenObjects.Add(queue);
        }
        
        private void DrawAllObjectsToCanvas()
        {
            foreach (var gameObject in _screenObjects.Cast<IRenderable>())
                gameObject.Render();
        }
    }
}