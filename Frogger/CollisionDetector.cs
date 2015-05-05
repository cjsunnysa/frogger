using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using GLib;

namespace ChrisJones.Frogger
{
    public class CollisionDetector
    {
        public bool CheckForCollisions(IHitTestable testObject, IEnumerable<IHitTestable> collidableObjects)
        {
            foreach (var obj in collidableObjects)
            {
                if (testObject.GetHitTestArea().HasCollidedWith(obj.GetHitTestArea()))
                {
                    var t1 = testObject.GetHitTestArea();
                    var t2 = testObject.GetHitTestArea();
                    
                    Console.WriteLine("player:");
                    Console.WriteLine("{0}", t1);
                    Console.WriteLine("object:");
                    Console.WriteLine("{0}", t2);
                    
                    return true;
                }
            }

            return false;
        }
    }
}