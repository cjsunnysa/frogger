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
        private readonly IGameObjectFactory _gameObjectFactory;
        

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _screenObjects = new List<object>();
            _frameTimer = new Stopwatch();
            _queueFactory= new CarQueueFactory(Direction.Left, gameObjectFactory);
        }

        public void InitialiseGame()
        {
            _screenObjects.Clear();
            
            CreatePlayers();
            
            for(var x = 0; x< GameConfig.CAR_QUEUE_COUNT; x++)
                CreateCarQueues();

            _frameTimer.Start();
        }

        public bool CycleGame()
        {
            if (_frameTimer.ElapsedMilliseconds <= 1000/GameConfig.FPS)
                return false;

            _frameTimer.Restart();

            foreach (var gameObject in _screenObjects.Cast<IMoveable>())
                gameObject.Move();

            return true;
        }

        public void RenderFrame()
        {
            foreach (var gameObject in _screenObjects.Cast<IRenderable>())
                gameObject.Render();
        }

        public void OnKeyPressed(object o, KeyPressEventArgs args)
        {
            var player = _screenObjects.FirstOrDefault(ob => ob is Player) as Player;
            if (player == null)
                return;

            var currentPosition = player.GetPosition();

            var key = args.Event.Key;

            switch (key)
            {
                case Gdk.Key.KP_8:
                    player.SetPosition(new Position(currentPosition.XPos, currentPosition.YPos - GameConfig.PLAYER_SPEED));
                    break;
                case Gdk.Key.KP_2:
                    player.SetPosition(new Position(currentPosition.XPos, currentPosition.YPos + GameConfig.PLAYER_SPEED));
                    break;
                case Gdk.Key.KP_4:
                    player.SetPosition(new Position(currentPosition.XPos-GameConfig.PLAYER_SPEED, currentPosition.YPos));
                    break;
                case Gdk.Key.KP_6:
                    player.SetPosition(new Position(currentPosition.XPos + GameConfig.PLAYER_SPEED, currentPosition.YPos));
                    break;
            }

        }

        private void CreatePlayers()
        {
            _screenObjects.Add(_gameObjectFactory.CreatePlayer(GameConfig.PLAYER_START_POSITION, Direction.Up));
        }

        private void CreateCarQueues()
        {
            _screenObjects.Add(_queueFactory.CreateNextQueue());
        }
    }
}