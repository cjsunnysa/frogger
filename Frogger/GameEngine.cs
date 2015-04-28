using System;
using Frogger.GameObjects.Interfaces;
using Frogger.Utils;

namespace Frogger
{
    public class GameEngine
    {
        private readonly ISceneRenderer _sceneRenderer;
        private readonly RandomGenerator _positionGenerator;
        private Position _playerStartPosition;

        public GameEngine(ISceneRenderer sceneRenderer)
        {
            _sceneRenderer = sceneRenderer;
            _playerStartPosition = new Position(_sceneRenderer.GetCanvasWidth()/2, _sceneRenderer.GetCanvasHeight()/2);
            _positionGenerator = new RandomGenerator ();
        }

        private void Initialise()
        {
            _sceneRenderer.ClearScene ();
            _sceneRenderer.AddPlayer (_playerStartPosition);
            _sceneRenderer.RenderScene ();

            GLib.Timeout.Add(2000, ExecuteGameCycle);
        }

        private bool ExecuteGameCycle()
        {
            var xpos = _positionGenerator.GetRandomPosition(Convert.ToInt32(_sceneRenderer.GetCanvasWidth()));
            var ypos = _positionGenerator.GetRandomPosition(Convert.ToInt32(_sceneRenderer.GetCanvasHeight()));
            
            _sceneRenderer.AddCar(new Position(xpos, ypos));
            _sceneRenderer.RenderScene ();

            return true;
        }

        public void Run()
        {
            Initialise ();
        }
    }
}