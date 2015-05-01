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
			var xNotIn = (AreaPosition.XPos + Width < otherArea.AreaPosition.XPos || AreaPosition.XPos > otherArea.AreaPosition.XPos + otherArea.Width);
			var yNotIn = (AreaPosition.YPos + Height < otherArea.AreaPosition.YPos || AreaPosition.YPos > otherArea.AreaPosition.YPos + otherArea.Height);

			var noCollision = xNotIn || yNotIn;

			return noCollision == false;
		}
    }
}
