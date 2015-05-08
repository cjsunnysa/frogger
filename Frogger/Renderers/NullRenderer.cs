using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Renderers
{
    public class NullRenderer : IRenderer
    {
        public HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            return new HitTestArea(new Position(0, 0), 0, 0);
        }
    }
}