using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.Conditions
{
    class PlayerReachesOtherSide : IWinCondition
    {
        public bool WonTheGame(GameObject gameObject)
        {
            return gameObject.Position.YPos < 0;
        }
    }
}