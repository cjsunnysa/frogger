using System.Collections.Generic;
using ChrisJones.Frogger.Factories;
using ChrisJones.Frogger.GameObjects;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IGameCycleProcedure
    {
        bool Execute(List<GameObject> gameObjects, IGameObjectFactory factory);
    }
}