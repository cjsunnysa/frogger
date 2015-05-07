using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.GtkRenderers;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Factories
{
    public class GtkGameObjectFactory : IGameObjectFactory
    {
        private readonly DrawingArea _area;
        private readonly IKeyMapper _keyMapper;

        public GtkGameObjectFactory(DrawingArea area, IKeyMapper playerKeyMapper)
        {
            _keyMapper = playerKeyMapper;
            _area = area;
        }

        public Player CreatePlayer(Position startPosition, Direction initialDirection)
        {
            return new Player(startPosition, new GtkPlayerRenderer(_area), initialDirection, GameConfig.PLAYER_SPEED, _keyMapper);
        }

        public Car CreateCarDrivingLeft(Position startPosition)
        {
            return new Car(startPosition, new GtkLeftCarRenderer(_area), Direction.Left, GameConfig.CAR_SPEED);
        }

        public Car CreateCarDrivingRight(Position startPosition)
        {
            return new Car(startPosition, new GtkRightCarRenderer(_area), Direction.Right, GameConfig.CAR_SPEED);
        }

        public Stain CreateStainFromPlayer(Player player)
        {
            var position = player.GetPosition();
            return new Stain(new Position(position.XPos, position.YPos), new GtkStainRenderer(_area));
        }
    }
}