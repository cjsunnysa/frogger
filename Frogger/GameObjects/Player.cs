using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
	public class Player : GameObject
	{
		public Player(Position initialPosition, IRenderer renderer, Direction initialDirection, int speed) : base(initialPosition, renderer, initialDirection, speed)
		{
		}

		public override void Move()
		{ }
	}
}