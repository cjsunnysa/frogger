using System.Collections.Generic;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameCycle
{
    public class MoveAutomatedObjects : IGameCycleProcedure
    {
        public bool Execute(List<GameObject> gameObjects, IGameObjectFactory factory)
        {
            foreach (var screenObject in gameObjects)
                screenObject.AutoMove();

            return true;
        }
    }
}