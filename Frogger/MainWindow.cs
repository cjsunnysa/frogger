using Frogger.GtkRenderers;
using Gtk;

namespace Frogger
{
	public partial class MainWindow: Gtk.Window
	{
		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			SetDefaultSize (640, 480);
			SetPosition (WindowPosition.CenterOnParent);
			Build ();


			var sceneRenderer = new GtkSceneRenderer(this);
			var gameEngine = new GameEngine (sceneRenderer);
			
			gameEngine.Run();

			ShowAll ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}
}