using ChrisJones.Frogger.Delegates;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IKeyMapper
    {
        event OnMove OnMoveUpEvent;
        event OnMove OnMoveDownEvent;
        event OnMove OnMoveLeftEvent;
        event OnMove OnMoveRightEvent;
    }
}