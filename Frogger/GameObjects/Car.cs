using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    /// <summary>
    ///     Car objects are managed by GameObjectQueues and cycle in a specific direction.
    /// </summary>
    public class Car : GameObject
    {
        /// <param name="spawnPosition">The initial on-screen position.</param>
        /// <param name="renderer">Used to render this object to a drawing surface.</param>
        /// <param name="initialDirection">Which direction this object initially faces.</param>
        /// <param name="speed">The distance travelled per game-cycle.</param>
        public Car(Position spawnPosition, IRenderer renderer, Direction initialDirection, int speed) 
            : base(spawnPosition, renderer, initialDirection, speed)
        {
        }
    }
}