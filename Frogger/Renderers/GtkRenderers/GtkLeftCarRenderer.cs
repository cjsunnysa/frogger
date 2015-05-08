using Cairo;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.GameObjects;
using Gtk;

namespace ChrisJones.Frogger.Renderers.GtkRenderers
{
    public class GtkLeftCarRenderer : GtkCarRenderer
    {
        public GtkLeftCarRenderer(DrawingArea area) : base(area)
        {
        }

        protected override void DrawDetail(Context context, GameObject gameObject)
        {
            var startPosition = gameObject.Position;
            
            var startx = startPosition.XPos;
            var starty = startPosition.YPos;
            var boty = startPosition.YPos + GameConfig.CAR_DIMENSION.Height;

            var yoffset = GameConfig.CAR_DIMENSION.Height / 4;
            var xoffset = GameConfig.CAR_DIMENSION.Width / 2.8;

            var doorheight = GameConfig.CAR_DIMENSION.Height / 10;
            var doorTopY = starty + doorheight;
            var doorBotY = boty - doorheight;

            var wind1X = startx + xoffset;

            var shieldWidth = xoffset / 3.5;
            var shieldOffset = yoffset / 1.2;
            var roofTopY = starty + shieldOffset;
            var roofBotY = starty + GameConfig.CAR_DIMENSION.Height - shieldOffset;
            var shieldHeight = GameConfig.CAR_DIMENSION.Height - (shieldOffset * 2);

            var wind2X = wind1X + shieldWidth;
            var wind2Y = starty + GameConfig.CAR_DIMENSION.Height - shieldOffset;

            var strutX = wind2X + xoffset - shieldWidth;
            var strutWidth = xoffset / 4;

            //top bonnet line.
            context.MoveTo(startx, starty + yoffset);
            context.LineTo(startx + xoffset, doorTopY);
            //top door line.
            context.RelLineTo(xoffset, 0);

            //bottom bonnet line.
            context.MoveTo(startx, boty - yoffset);
            context.LineTo(startx + xoffset, doorBotY);
            //bottom door line.
            context.RelLineTo(xoffset, 0);


            //windscreen large curve.
            context.MoveTo(wind1X, doorTopY);
            context.RelCurveTo(-6, yoffset, -6, GameConfig.CAR_DIMENSION.Height - yoffset, 0, doorBotY - doorTopY);


            //windscreen small curve.
            context.MoveTo(wind2X, starty + shieldOffset);
            context.RelCurveTo(-6, 1, -6, shieldHeight - 1, 0, shieldHeight);

            //roof bottom line.
            context.RelLineTo(xoffset - shieldWidth, 0);


            //roof top line.
            context.MoveTo(wind2X, starty + shieldOffset);
            context.RelLineTo(xoffset - shieldWidth, 0);


            //windscreen top corner.
            context.MoveTo(wind2X, starty + shieldOffset);
            context.LineTo(wind1X, doorTopY);

            //windscreen bottom corner.
            context.MoveTo(wind2X, wind2Y);
            context.LineTo(wind1X, doorBotY);

            //rear top strut.
            context.MoveTo(strutX, roofTopY);
            context.LineTo(strutX + strutWidth, doorTopY);
            context.RelLineTo(-strutWidth, 0);

            //rear bottom strut.
            context.MoveTo(strutX, roofBotY);
            context.LineTo(strutX + strutWidth, doorBotY);
            context.RelLineTo(-strutWidth, 0);

            //rear screen top.
            context.MoveTo(strutX, roofTopY);
            context.RelLineTo(0, shieldHeight);


            //rear screen bottom.
            context.MoveTo(strutX + strutWidth, doorTopY);
            context.RelLineTo(0, doorBotY - doorTopY);



            var bootX = strutX + strutWidth;
            var bootWidth = startx + GameConfig.CAR_DIMENSION.Width - bootX;
            var adjustWidth = bootWidth / 4;
            bootX += adjustWidth;
            bootWidth -= adjustWidth;


            //boot top to bot line.
            context.MoveTo(bootX, roofTopY);
            context.LineTo(bootX, roofBotY);

            //boot width line top.
            context.MoveTo(bootX, roofTopY);
            context.LineTo(bootX + bootWidth, roofTopY);

            //boot width line bottom.
            context.MoveTo(bootX, roofBotY);
            context.LineTo(bootX + bootWidth, roofBotY);
        }

        protected override void DrawBody(Context context, GameObject gameObject)
        {
            context.MoveTo(gameObject.Position.XPos, gameObject.Position.YPos);
            context.RelLineTo(GameConfig.CAR_DIMENSION.Width, 0);
            context.RelLineTo(0, GameConfig.CAR_DIMENSION.Height);
            context.RelLineTo(GameConfig.CAR_DIMENSION.Width * -1, 0);
            context.RelCurveTo(-4, -4, -4, GameConfig.CAR_DIMENSION.Height * -1 + 4, 0, GameConfig.CAR_DIMENSION.Height * -1);
        }
    }
}