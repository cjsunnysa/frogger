using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisJones.Frogger.Drawing2D
{
    public class HitTestArea
    {
        public Position AreaPosition { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public HitTestArea(Position areaPosition, int width, int height)
        {
            AreaPosition = areaPosition;
            Width = width;
            Height = height;
        }

        public bool HasCollidedWith(HitTestArea otherArea)
        {
            if (AreaPosition.XPos == 0 && AreaPosition.YPos == 0 && Width == 0 && Height == 0)
                return false;
            
            var xNotIn = (AreaPosition.XPos + Width < otherArea.AreaPosition.XPos || AreaPosition.XPos > otherArea.AreaPosition.XPos + otherArea.Width);
            var yNotIn = (AreaPosition.YPos + Height < otherArea.AreaPosition.YPos || AreaPosition.YPos > otherArea.AreaPosition.YPos + otherArea.Height);

            var noCollision = xNotIn || yNotIn;

            return noCollision == false;
        }

        public override string ToString()
        {
            var rightX = AreaPosition.XPos + Width;
            var rightY = AreaPosition.YPos + Height;
            var sb = new StringBuilder();
            
            sb.AppendLine(string.Format("\ttopleft  x: {0} y: {1}", AreaPosition.XPos, AreaPosition.YPos));
            sb.AppendLine(string.Format("\tbotRight x: {0} y: {1}", rightX, rightY));

            return sb.ToString();
        }
    }
}
