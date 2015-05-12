using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Conditions
{
    public class IfPlayerLosesStain : IGameCycleProcedure
    {
        public bool Execute(List<GameObject> gameObjects, IGameObjectFactory factory)
        {
            return NoCollisionDetected(gameObjects, factory);
        }

        private bool NoCollisionDetected(List<GameObject> gameObjects, IGameObjectFactory factory)
        {
            var deadPlayers = (from p in gameObjects.OfType<Player>().ToArray()
                               from o in gameObjects.Except(new[] { p })
                               where p.CollidedWith(o) || o.CollidedWith(p)
                               select p).ToArray();

            foreach (var deadPlayer in deadPlayers)
                ReplacePlayerWithStain(deadPlayer, gameObjects, factory);

            return (!deadPlayers.Any());
        }

        private void ReplacePlayerWithStain(Player player, List<GameObject> gameObjects, IGameObjectFactory factory)
        {
            var stain = factory.CreateStainFromPlayer(player);
            gameObjects.Add(stain);
            gameObjects.Remove(player);
        } 

    }
}