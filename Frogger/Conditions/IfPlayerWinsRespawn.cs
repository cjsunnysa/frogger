using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Conditions
{
    public class IfPlayerWinsRespawn : IGameCycleProcedure
    {
        public bool Execute(List<GameObject> gameObjects, IGameObjectFactory factory)
        {
            var playerWon = gameObjects.Any(p => p.HasWon());
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