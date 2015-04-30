using System;
using Cairo;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public class GtkPlayerRenderer : GtkRenderer
    {
        private const double PLAYERHEIGHT = 45;
        private const double PLAYERWIDTH = 15;

        public GtkPlayerRenderer(DrawingArea area) : base(area)
        {
        }

        public override ShapePath RenderObjectToCanvas(GameObject gameObject)
        {
            var shapePath = new ShapePath();
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            context.LineWidth = 2;
            context.SetSourceRGB(0.7, 0.2, 0.0);

            context.Rectangle(new Rectangle(gameObject.GetPosition().XPos, gameObject.GetPosition().YPos, PLAYERWIDTH, PLAYERHEIGHT));
            context.StrokePreserve();


            (context.GetTarget() as IDisposable).Dispose();
            context.Dispose();

            return shapePath;
        }
    }
}