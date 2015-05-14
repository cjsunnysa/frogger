using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameCycle
{
    public class IfPlayerWinsThenRespawn : IGameCycleProcedure
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