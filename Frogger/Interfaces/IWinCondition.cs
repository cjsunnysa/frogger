using ChrisJones.Frogger.GameObjects;

namespace ChrisJones.Frogger.Interfaces
{
    public interface IWinCondition
    {
        bool WonTheGame(GameObject gameObject);
    }
}