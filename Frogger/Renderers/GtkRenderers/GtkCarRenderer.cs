using System;
using Cairo;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.Renderers.GtkRenderers
{
    /// <summary>
    ///     Base class of the renderer for the Gtk platform. Used to draw car objects to a Gtk.DrawingArea.
    /// </summary>
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

            return new HitTestArea (new Position (gameObject.Position.XPos, gameObject.Position.YPos), GameConfig.CAR_DIMENSION.Width, GameConfig.CAR_DIMENSION.Height);
        }

        protected abstract void DrawBody(Context context, GameObject gameObject);
        protected abstract void DrawDetail(Context context, GameObject gameObject);
    }
}