using System;
using Cairo;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public class GtkPlayerRenderer : GtkRenderer
    {

        public GtkPlayerRenderer(DrawingArea area) : base(area)
        {
        }

        public override ShapePath RenderObjectToCanvas(GameObject gameObject)
        {
            var shapePath = new ShapePath();
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            context.LineWidth = 2;
            context.SetSourceRGB(0.7, 0.2, 0.0);

            context.Rectangle(new Rectangle(gameObject.GetPosition().XPos, gameObject.GetPosition().YPos, GameConfig.PLAYER_DIMENSION.Width, GameConfig.PLAYER_DIMENSION.Height));
            context.StrokePreserve();


            (context.GetTarget() as IDisposable).Dispose();
            context.Dispose();

            return shapePath;
        }
    }
}