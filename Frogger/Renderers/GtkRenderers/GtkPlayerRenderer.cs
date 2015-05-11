using System;
using Cairo;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.Renderers.GtkRenderers
{
    /// <summary>
    ///     Used to draw a player representation to a Gtk.DrawingArea.
    /// </summary>
    public class GtkPlayerRenderer : GtkRenderer
    {
        public GtkPlayerRenderer(DrawingArea area) : base(area)
        {
        }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            var context = Gdk.CairoHelper.Create(_area.GdkWindow);

            context.LineWidth = 1;
            context.SetSourceRGB(0.7, 0.2, 0.0);

            var x = gameObject.Position.XPos;
            var y = gameObject.Position.YPos;
            var width = GameConfig.PLAYER_DIMENSION.Width; 
            var radius = width / 2;
            var height = width;
            

            context.Arc(x+radius, y+radius, radius, 0, 2 * Math.PI);

            var neckHeight = height/5;
            var bodyY = y + height + neckHeight;
            var bodyHeight = GameConfig.PLAYER_DIMENSION.Height - height - neckHeight;

            context.Rectangle(new Rectangle(x, bodyY, width, bodyHeight));

            var pantsHeight = bodyHeight/2.2;
            var pantsY = y + GameConfig.PLAYER_DIMENSION.Height - pantsHeight;
            context.MoveTo(x, pantsY);
            context.RelLineTo(width, 0);

            context.MoveTo(x + radius, pantsY + pantsHeight/2);
            context.RelLineTo(0, pantsHeight/2);

            context.StrokePreserve();


            (context.GetTarget() as IDisposable).Dispose();
            context.Dispose();

            return new HitTestArea (new Position (gameObject.Position.XPos, gameObject.Position.YPos), GameConfig.PLAYER_DIMENSION.Width, GameConfig.PLAYER_DIMENSION.Height);
        }
    }
}