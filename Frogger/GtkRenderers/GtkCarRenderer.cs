using System;
using System.ComponentModel;
using Cairo;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public abstract class GtkCarRenderer : GtkRenderer
    {
        protected Position _startPosition;
        protected Context _context;

        
        protected GtkCarRenderer(DrawingArea area)
            : base(area)
        {
        }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            _startPosition = gameObject.GetPosition();
            _context = Gdk.CairoHelper.Create(_area.GdkWindow);

            _context.LineWidth = 1;
            _context.SetSourceRGB(0.7, 0.2, 0.0);

            DrawBody();
            DrawDetail();

            _context.ClosePath();

            _context.StrokePreserve();

            (_context.GetTarget() as IDisposable).Dispose();
            _context.Dispose();

            return new HitTestArea (new Position (gameObject.GetPosition().XPos, gameObject.GetPosition().YPos), GameConfig.CAR_DIMENSION.Width, GameConfig.CAR_DIMENSION.Height);
        }

        protected abstract void DrawBody();
        protected abstract void DrawDetail();
    }
}