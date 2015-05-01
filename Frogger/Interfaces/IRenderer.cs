using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IRenderer
    {
        HitTestArea RenderObjectToCanvas(GameObject gameObject);
    }
}
