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
        protected GtkCarRenderer(DrawingArea area)
            : base(area)
        {
        }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            
            context.LineWidth = 1;
            context.SetSourceRGB(0.7, 0.2, 0.0);

            DrawBody(context, gameObject);
            DrawDetail(context, gameObject);

            context.ClosePath();

            context.StrokePreserve();

            (context.GetTarget() as IDisposable).Dispose();
            context.Dispose();

            return new HitTestArea (new Position (gameObject.GetPosition().XPos, gameObject.GetPosition().YPos), GameConfig.CAR_DIMENSION.Width, GameConfig.CAR_DIMENSION.Height);
        }

        protected abstract void DrawBody(Context context, GameObject gameObject);
        protected abstract void DrawDetail(Context context, GameObject gameObject);
    }
}