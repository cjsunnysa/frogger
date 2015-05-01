using System;
using Cairo;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public class GtkCarRenderer : GtkRenderer
    {
        public GtkCarRenderer(DrawingArea area)
            : base(area)
        {
        }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            context.LineWidth = 2;
            context.SetSourceRGB(0.7, 0.2, 0.0);

			context.Rectangle(new Rectangle(gameObject.GetPosition().XPos, gameObject.GetPosition().YPos, GameConfig.CAR_DIMENSION.Width, GameConfig.CAR_DIMENSION.Height));
            context.StrokePreserve();

			(context.GetTarget() as IDisposable).Dispose();
            context.Dispose();

			return new HitTestArea (new Position (gameObject.GetPosition ().XPos, gameObject.GetPosition ().YPos), GameConfig.CAR_DIMENSION.Width, GameConfig.CAR_DIMENSION.Height);
        }
    }
}