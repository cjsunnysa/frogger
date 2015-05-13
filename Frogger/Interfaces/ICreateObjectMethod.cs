using System.Collections.Generic;
using ChrisJones.Frogger.GameObjects;

namespace ChrisJones.Frogger.Interfaces
{
    public interface ICreateObjectMethod
    {
        List<GameObject> CreateGameObjects(IGameObjectFactory factory);
    }
}