using System;
using Cairo;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.Renderers.GtkRenderers
{
    public class GtkStainRenderer : GtkRenderer
    {
    
        public GtkStainRenderer(DrawingArea area) : base(area)
        { }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            var x = gameObject.Position.XPos;
            var y = gameObject.Position.YPos;
            var radius = GameConfig.PLAYER_DIMENSION.Width;

            var context = Gdk.CairoHelper.Create(_area.GdkWindow);
            context.SetSourceRGB(0.7, 0.2, 0.0);
            context.LineWidth = 1;


            
            context.Arc(x+5, y+radius, radius, 0, Math.PI * 2);
            context.Fill();
            
            context.Translate(x+8, y+(radius/1.2));
            context.Scale(1, 0.7);
            context.Arc(0, 0, radius*1.2, -1.5, Math.PI/2);
            context.Fill();

            
            return new HitTestArea(new Position(0,0), 0, 0);
        }
    }
}