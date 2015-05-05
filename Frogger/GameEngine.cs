using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using GLib;
using Gtk;

namespace ChrisJones.Frogger
{
    public class GameEngine
    {
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

            _frameTimer.Start();
        }

        public bool CycleGame()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            _frameTimer.Restart();

            foreach (var screenObject in _screenObjects.OfType<IMoveable>())
                screenObject.Move();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _screenObjects.OfType<IRenderable>())
                gameObject.Render();
        }

        
        public bool CollisionDetected()
        {
            var queueObjects = (from q in _screenObjects.OfType<GameObjectQueue>()
                                from s in q.ScreenObjects
                                select s).ToArray();

            var players = _screenObjects.OfType<Player>().ToArray();

            foreach (var player in players)
            {
                var collisionObjects = players.Where(p => p != player).Union(queueObjects).ToArray();
                if (_collisionDetector.CheckForCollisions (player, collisionObjects))
                    return true;
            }

            return false;
        }

        public void OnKeyPressed(object o, KeyPressEventArgs args)
        {
            var players = _screenObjects.OfType<Player>().ToArray();
            if (!players.Any())
                return;

            foreach (var player in players)
            {
                var key = args.Event.Key;

                switch (key)
                {
                    case Gdk.Key.KP_8:
                        player.Move(Direction.Up);
                        break;
                    case Gdk.Key.KP_2:
                        player.Move(Direction.Down);
                        break;
                    case Gdk.Key.KP_4:
                        player.Move(Direction.Left);
                        break;
                    case Gdk.Key.KP_6:
                        player.Move(Direction.Right);
                        break;
                } 
            }
        }

        private void CreatePlayers()
        {
            _screenObjects.Add(_gameObjectFactory.CreatePlayer(new Position(GameConfig.PLAYER_START_POSITION.XPos, GameConfig.PLAYER_START_POSITION.YPos), Direction.Up));
        }

        private void CreateCarQueues()
        {
            _screenObjects.Add(_queueFactory.CreateNextQueue());
        }
    }
}