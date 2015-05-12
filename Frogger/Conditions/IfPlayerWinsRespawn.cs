using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Conditions
{
    public class IfPlayerWinsRespawn : IGameCheckProcedure
    {
        public bool Execute(List<GameObject> gameObjects, IGameObjectFactory factory, GameObjectQueueFactory queueFactory)
        {
            var playerWon = gameObjects.OfType<Player>().Any(p => p.HasWon());
            if (!playerWon)
                return true;
            
            foreach (var player in gameObjects.OfType<Player>())
            {
                player.Respawn();
            }

            return true;
        }
    }
}