using Frogger.GameObjects.Interfaces;
using Frogger.Utils;
using Gtk;

namespace Frogger
{
    public class Car : IRendable, IMovable
    {
        private readonly IRenderer _renderer;
        private readonly Position _position;

        public Car(IRenderer renderer, Position position)
        {
            _renderer = renderer;
            _position = position;
        }

        public void Render ()
        {
            _renderer.Render (_position);
        }

        public void MoveTo(Position position)
        {
            _position.SetPosition(position.XPos, position.YPos);
        }
    }
}