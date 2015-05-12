using System.Collections.Generic;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Factories
{
    public class CreateTwoWayTrafficObjects : IGameObjectCreationProcedure
    {
        private IGameObjectFactory _factory;
        private List<GameObject> _gameObjects;

        public List<GameObject> CreateGameObjects(IGameObjectFactory factory)
        {
            _factory = factory;
            _gameObjects = new List<GameObject>();

            CreatePlayers();
            CreateCarQueues();

            return _gameObjects;
        }

        private void CreatePlayers()
        {
            var player =
                _factory.CreatePlayer(
                    new Position(GameConfig.PLAYER_START_POSITION.XPos, GameConfig.PLAYER_START_POSITION.YPos),
                    Direction.Up);

            _gameObjects.Add(player);
        }

        private void CreateCarQueues()
        {
            for (var x = 0; x < GameConfig.CAR_QUEUE_COUNT; x++)
                _gameObjects.Add(_factory.CreateNextQueue());
        }
    }
}