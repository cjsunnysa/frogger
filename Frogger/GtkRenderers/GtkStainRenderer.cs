using System;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public class GtkStainRenderer : GtkRenderer
    {
    
        public GtkStainRenderer(DrawingArea area) : base(area)
        { }

        public override HitTestArea RenderObjectToCanvas(GameObject gameObject)
        {
            var x = gameObject.GetPosition().XPos;
            var y = gameObject.GetPosition().YPos;
            var radius = GameConfig.PLAYER_DIMENSION.Width/1.1;

            var context = Gdk.CairoHelper.Create(_area.GdkWindow);
            context.SetSourceRGB(0.7, 0.2, 0.0);
            context.LineWidth = 1;


            
            context.Arc(x, y+radius, radius, 0, Math.PI * 2);
            context.Fill();
            
            context.Translate(x, y+radius/2.1);
            context.Scale(1, 0.5);
            context.Arc(0, 0, radius*1.2, -1.5, Math.PI/2);
            context.Fill();

            
            return new HitTestArea(new Position(0,0), 0, 0);
        }
    }
}