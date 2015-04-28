using Frogger.Utils;

namespace Frogger.GameObjects.Interfaces
{
    public interface ISceneRenderer
    {
        double GetCanvasWidth();
        double GetCanvasHeight();
        IMovable AddPlayer(Position initialPosition);
        IMovable AddCar(Position initialPosition);
        void RenderScene();
    }
}