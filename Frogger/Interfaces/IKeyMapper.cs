namespace ChrisJones.Frogger.Interfaces
{
    public delegate void OnMove();

    public interface IKeyMapper
    {
        event OnMove OnMoveUpEvent;
        event OnMove OnMoveDownEvent;
        event OnMove OnMoveLeftEvent;
        event OnMove OnMoveRightEvent;
    }
}