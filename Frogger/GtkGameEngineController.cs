using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.Factories;
using Gtk;

namespace ChrisJones.Frogger
{
    public class GtkGameEngineController
    {
        private readonly GameEngine _engine;
        private readonly GtkGameObjectFactory _factory;
        private readonly DrawingArea _area;
        private bool _fireAgain = true;

        public GtkGameEngineController(Gtk.Window window)
        {
            var area = new DrawingArea();
            _area = area;
            _area.ExposeEvent += CanvasExposed;
            
            window.Add(area);
            
            var keyMapper = new GdkKeyMovementMapper(Gdk.Key.KP_8, Gdk.Key.KP_2, Gdk.Key.KP_4, Gdk.Key.KP_6);
            window.KeyPressEvent += keyMapper.OnKeyPressed;

            _factory = new GtkGameObjectFactory(_area, keyMapper);
            _engine = new GameEngine(_factory);
            _engine.InitialiseGame();
        }

        public void CanvasExposed(object o, ExposeEventArgs args)
        {
            _engine.RenderFrame();
        }

        public void OnKeyPressed(object o, KeyPressEventArgs args)
        {
            
        }

        public void Run()
        {
            GLib.Timeout.Add(1, Loop);
        }

        private bool Loop()
        {
            if (!_engine.CycleGame())
                return _fireAgain;

            _area.QueueDraw();

            return _fireAgain && _engine.GameRunning;
        }

        public void Stop()
        {
            _fireAgain = false;
        }
    }
}