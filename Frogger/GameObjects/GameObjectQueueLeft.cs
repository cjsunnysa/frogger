using System.Linq;
using ChrisJones.Frogger.Configuration;
using ChrisJones.Frogger.Delegates;
using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     A GameObjectQueue that cycles from right to left.
    /// </summary>
    public class GameObjectQueueLeft : GameObjectQueue
    {
        public GameObjectQueueLeft(int yPos, int moveSpeed, ChildObjectCreateMethod childCreateMethod, int numQueueObjects, IWinCondition[] winConditions)
            : base(new Position(GameConfig.RIGHT_OFFSCREEN_X_POS, yPos), Direction.Right, moveSpeed, childCreateMethod, numQueueObjects, winConditions)
        {
        }

        protected override int GetDistanceToLastObject()
        {
            var lastxpos = ChildObjects.Max(m => m.Position.XPos);

            return (int) (Position.XPos - lastxpos);
        }

        protected override bool ObjectPastEndOfQueue(GameObject gameObject)
        {
            return gameObject.Position.XPos <= GameConfig.LEFT_OFFSCREEN_X_POS;
        }

        protected override bool EnumerateXPosRange(ref int xpos)
        {
            xpos -= GenerateDistance();

            return xpos >= GameConfig.LEFT_OFFSCREEN_X_POS;
        }
    }
}