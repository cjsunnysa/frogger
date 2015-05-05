using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.GtkRenderers;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Factories
{
    public class GdkGameObjectFactory : IGameObjectFactory
    {
        private readonly DrawingArea _area;
        
        public GdkGameObjectFactory(DrawingArea area)
        {
            _area = area;
        }

        public Player CreatePlayer(Position startPosition, Direction initialDirection)
        {
            return new Player(startPosition, new GtkPlayerRenderer(_area), initialDirection, GameConfig.PLAYER_SPEED);
        }

        public Car CreateCarDrivingLeft(Position startPosition)
        {
            //ToDo: create GtkCarRendererLeft and Right for facing different directions.
            return new Car(startPosition, new GtkCarRenderer(_area), Direction.Left, GameConfig.CAR_SPEED);
        }

        public Car CreateCarDrivingRight(Position startPosition)
        {
            return new Car(startPosition, new GtkCarRenderer(_area), Direction.Right, GameConfig.CAR_SPEED);
        }
    }
}