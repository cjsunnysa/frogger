using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Renderers.GtkRenderers
{
    public abstract class GtkRenderer : IRenderer
    {
        protected readonly DrawingArea _area;

        protected GtkRenderer(DrawingArea area)
        {
            _area = area;
        }

        public abstract HitTestArea RenderObjectToCanvas(GameObject gameObject);
    }
}