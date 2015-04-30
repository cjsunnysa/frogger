using ChrisJones.Frogger.GameObjects;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger
{
	public partial class MainWindow: Gtk.Window
	{
		private GameEngine _engine;

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			SetDefaultSize (640, 480);
			SetPosition (WindowPosition.CenterOnParent);
			Build ();

			var area = new DrawingArea();
			area.ExposeEvent += area_ExposeEvent;
			this.Add(area);

			var factory = new GdkGameObjectFactory(area);
			_engine = new GameEngine(factory);

			ShowAll ();
		}

		void area_ExposeEvent(object o, ExposeEventArgs args)
		{
			_engine.StartGame();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
}