using ChrisJones.Frogger.Engine;
using Gtk;

namespace ChrisJones.Frogger.GtkRenderers
{
    public class GtkLeftCarRenderer : GtkCarRenderer
    {
        public GtkLeftCarRenderer(DrawingArea area) : base(area)
        {
        }

        protected override void DrawDetail()
        {
            var startx = _startPosition.XPos;
            var starty = _startPosition.YPos;
            var boty = _startPosition.YPos + GameConfig.CAR_DIMENSION.Height;

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
            _context.MoveTo(startx, starty + yoffset);
            _context.LineTo(startx + xoffset, doorTopY);
            //top door line.
            _context.RelLineTo(xoffset, 0);

            //bottom bonnet line.
            _context.MoveTo(startx, boty - yoffset);
            _context.LineTo(startx + xoffset, doorBotY);
            //bottom door line.
            _context.RelLineTo(xoffset, 0);


            //windscreen large curve.
            _context.MoveTo(wind1X, doorTopY);
            _context.RelCurveTo(-6, yoffset, -6, GameConfig.CAR_DIMENSION.Height - yoffset, 0, doorBotY - doorTopY);


            //windscreen small curve.
            _context.MoveTo(wind2X, starty + shieldOffset);
            _context.RelCurveTo(-6, 1, -6, shieldHeight - 1, 0, shieldHeight);

            //roof bottom line.
            _context.RelLineTo(xoffset - shieldWidth, 0);


            //roof top line.
            _context.MoveTo(wind2X, starty + shieldOffset);
            _context.RelLineTo(xoffset - shieldWidth, 0);


            //windscreen top corner.
            _context.MoveTo(wind2X, starty + shieldOffset);
            _context.LineTo(wind1X, doorTopY);

            //windscreen bottom corner.
            _context.MoveTo(wind2X, wind2Y);
            _context.LineTo(wind1X, doorBotY);

            //rear top strut.
            _context.MoveTo(strutX, roofTopY);
            _context.LineTo(strutX + strutWidth, doorTopY);
            _context.RelLineTo(-strutWidth, 0);

            //rear bottom strut.
            _context.MoveTo(strutX, roofBotY);
            _context.LineTo(strutX + strutWidth, doorBotY);
            _context.RelLineTo(-strutWidth, 0);

            //rear screen top.
            _context.MoveTo(strutX, roofTopY);
            _context.RelLineTo(0, shieldHeight);


            //rear screen bottom.
            _context.MoveTo(strutX + strutWidth, doorTopY);
            _context.RelLineTo(0, doorBotY - doorTopY);



            var bootX = strutX + strutWidth;
            var bootWidth = startx + GameConfig.CAR_DIMENSION.Width - bootX;
            var adjustWidth = bootWidth / 4;
            bootX += adjustWidth;
            bootWidth -= adjustWidth;


            //boot top to bot line.
            _context.MoveTo(bootX, roofTopY);
            _context.LineTo(bootX, roofBotY);

            //boot width line top.
            _context.MoveTo(bootX, roofTopY);
            _context.LineTo(bootX + bootWidth, roofTopY);

            //boot width line bottom.
            _context.MoveTo(bootX, roofBotY);
            _context.LineTo(bootX + bootWidth, roofBotY);
        }

        protected override void DrawBody()
        {
            _context.MoveTo(_startPosition.XPos, _startPosition.YPos);
            _context.RelLineTo(GameConfig.CAR_DIMENSION.Width, 0);
            _context.RelLineTo(0, GameConfig.CAR_DIMENSION.Height);
            _context.RelLineTo(GameConfig.CAR_DIMENSION.Width * -1, 0);
            _context.RelCurveTo(-4, -4, -4, GameConfig.CAR_DIMENSION.Height * -1 + 4, 0, GameConfig.CAR_DIMENSION.Height * -1);
        }
    }
}