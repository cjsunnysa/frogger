using System;
using Cairo;
using Frogger.GameObjects.Interfaces;
using Frogger.Utils;
using Gtk;

namespace Frogger.GtkRenderers
{
    public class GtkCarRenderer : IRenderer
    {
        private DrawingArea _area;
        private const double CarWidth = 40;
        private const double CarHeight = 20;

        public GtkCarRenderer(DrawingArea area)
        {
            _area = area;
        }

        public void Render(Position position)
        {
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            context.LineWidth = 2;
            context.SetSourceRGB(0.7, 0.2, 0.0);

            context.Rectangle(new Rectangle(position.XPos, position.YPos, CarWidth, CarHeight));
            context.StrokePreserve();

            
            (context.GetTarget() as IDisposable).Dispose();
            context.Dispose();
        }
    }
}