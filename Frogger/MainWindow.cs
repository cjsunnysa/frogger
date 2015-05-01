using System;
using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using Gdk;
using Gtk;

namespace ChrisJones.Frogger
{
	public partial class MainWindow: Gtk.Window
	{
		private readonly GameEngine _engine;
		private readonly DrawingArea _area;
		private bool _windowOpen = true;

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();

			_area = new DrawingArea();
			_area.ExposeEvent += OnAreaExposeEvent;
			this.Add(_area);

			var factory = new GdkGameObjectFactory(_area);
			_engine = new GameEngine(factory);
			_engine.StartGame();

			GLib.Timeout.Add(1, Tick);

			ShowAll ();
		}

		private bool Tick()
		{
			if (_engine.GameCycle())
				_area.QueueDraw();

			return _engine.CollisionDetected() == false &&
				   _windowOpen;
		}


		private void OnAreaExposeEvent(object o, ExposeEventArgs args)
		{
			_engine.Render();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			_windowOpen = false;

			Application.Quit ();
			a.RetVal = true;
		}
	}
}