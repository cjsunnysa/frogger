using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    class PlayerReachesOtherSide : IWinCondition
    {
        public bool WonTheGame(GameObject gameObject)
        {
            return gameObject.Position.YPos < 0;
        }
    }
}