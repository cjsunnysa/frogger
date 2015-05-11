using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using ChrisJones.Frogger.Renderers.GtkRenderers;
using Gtk;

namespace ChrisJones.Frogger.Factories
{
    /// <summary>
    ///     Creates the game objects used to represent the game on-screen.
    ///     Creates the game objects with renderers for the Gtk# platform.
    /// </summary>
    public class GtkGameObjectFactory : IGameObjectFactory
    {
        private readonly DrawingArea _area;
        private readonly IKeyMapper _playerKeyMapper;

        public GtkGameObjectFactory(DrawingArea area, IKeyMapper playerKeyMapper)
        {
            _playerKeyMapper = playerKeyMapper;
            _area = area;
        }

        public Player CreatePlayer(Position startPosition, Direction initialDirection)
        {
            return new Player(startPosition, new GtkPlayerRenderer(_area), initialDirection, GameConfig.PLAYER_SPEED, _playerKeyMapper);
        }

        public Car CreateCarDrivingLeft(Position startPosition)
        {
            return new Car(startPosition, new GtkCarRendererLeft(_area), Direction.Left, GameConfig.CAR_SPEED);
        }

        public Car CreateCarDrivingRight(Position startPosition)
        {
            return new Car(startPosition, new GtkCarRendererRight(_area), Direction.Right, GameConfig.CAR_SPEED);
        }

        public Stain CreateStainFromPlayer(Player player)
        {
            var position = player.Position;
            return new Stain(new Position(position.XPos, position.YPos), new GtkStainRenderer(_area));
        }
    }
}