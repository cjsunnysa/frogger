using System;
using Cairo;
using Frogger.GameObjects.Interfaces;
using Frogger.Utils;
using Gtk;

namespace Frogger.GtkRenderers
{
    public class GtkPlayerRenderer : IRenderer
    {
        private DrawingArea _area;
        private const double PlayerHeadHeight = 5;
        private const double PlayerBodyHeight = 15;

        public GtkPlayerRenderer(DrawingArea area)
        {
            _area = area;
        }

        public void Render(Position position)
        {
            var _context = Gdk.CairoHelper.Create (_area.GdkWindow);

            _context.LineWidth = 2;
            _context.SetSourceRGB (0.7, 0.2, 0.0);

            _context.Arc(position.XPos, position.YPos, PlayerHeadHeight, 0, 2*Math.PI);
            _context.StrokePreserve ();

            _context.Rectangle(new Rectangle(position.XPos-PlayerHeadHeight/1.5, position.YPos+PlayerHeadHeight+1, PlayerHeadHeight*1.5, PlayerBodyHeight));
            _context.StrokePreserve ();				

            (_context.GetTarget () as IDisposable).Dispose ();
            _context.Dispose ();
        }
    }
}