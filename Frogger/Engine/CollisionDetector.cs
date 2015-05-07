using System;
using System.Collections.Generic;
using System.Linq;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Engine
{
    public class CollisionDetector
    {
        public bool CheckForCollisions(IHitTestable testObject, IEnumerable<IHitTestable> collidableObjects)
        {
            return collidableObjects.Any(obj => testObject.GetHitTestArea().HasCollidedWith(obj.GetHitTestArea()));
        }
    }
}