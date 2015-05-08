using ChrisJones.Frogger.Delegates;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IKeyMapper
    {
        event OnMoveEvent OnMoveUpEvent;
        event OnMoveEvent OnMoveDownEvent;
        event OnMoveEvent OnMoveLeftEvent;
        event OnMoveEvent OnMoveRightEvent;
    }
}