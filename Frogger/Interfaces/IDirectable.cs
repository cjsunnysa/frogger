using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IDirectable
    {
        void SetDirection(Direction direction);
        Direction GetDirection();
    }
}