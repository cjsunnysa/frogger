using ChrisJones.Frogger.Input;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.Factories;
using Gtk;
using System;

namespace ChrisJones.Frogger
{
    public class GtkGameEngineController
    {
        private readonly GameEngine _engine;
        private readonly GtkGameObjectFactory _factory;
        private readonly DrawingArea _area;
        private bool _keepLooping = true;

        public GtkGameEngineController(Gtk.Window window)
        {
			if (window == null)
				throw new ArgumentNullException ("window");

			_area = CreateDrawingSurface(window);
            _factory = new GtkGameObjectFactory(_area, CreateKeyMapper (window));
            _engine = new GameEngine(_factory);

			_engine.InitialiseGame();
        }

        public void RunGame()
        {
            GLib.Timeout.Add(1, Loop);
        }

		private DrawingArea CreateDrawingSurface (Gtk.Window window)
		{
			var area = new DrawingArea ();
			area.ExposeEvent += CanvasExposed;

			window.Add(area);

			return area;
		}

		private GdkKeyMovementMapper CreateKeyMapper (Gtk.Window window)
		{
			var keyMapper = new GdkKeyMovementMapper (Gdk.Key.KP_8, Gdk.Key.KP_2, Gdk.Key.KP_4, Gdk.Key.KP_6);

			window.KeyPressEvent += keyMapper.OnKeyPressed;

			return keyMapper;
		}

		private void CanvasExposed(object o, ExposeEventArgs args)
		{
			_engine.RenderFrame();
		}

        private bool Loop()
        {
            if (!_engine.GameCycled())
                return _keepLooping;

            _area.QueueDraw();

            return _keepLooping && _engine.GameIsRunning;
        }

        public void StopGame()
        {
            _keepLooping = false;
        }
    }
}