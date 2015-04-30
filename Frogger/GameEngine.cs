using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger
{
    public class GameEngine
    {
        private readonly List<GameObject> _gameObjects;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameEngine(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _gameObjects = new List<GameObject>();
        }

        public void StartGame()
        {
            _gameObjects.Clear();

            var playerStartPosition = new Position(320, 440);
            const int carsRightPositionYPos = 130;
            const int carsLeftPositionYPos = 190; 
            
            _gameObjects.Add(_gameObjectFactory.CreatePlayer(playerStartPosition, Direction.Up));
            
            for (var xPos = 625; xPos >= 0; xPos-=50)
                _gameObjects.Add(_gameObjectFactory.CreateCarDrivingLeft(new Position(xPos, carsLeftPositionYPos)));

            for (var xPos = 0; xPos >= 625; xPos += 50)
                _gameObjects.Add(_gameObjectFactory.CreateCarDrivingRight(new Position(xPos, carsRightPositionYPos)));

            foreach(var gameObject in _gameObjects)
                gameObject.Render();
        }
    }
}
