
// This file has been generated by the GUI designer. Do not modify.
namespace ChrisJones.Frogger
{
	public partial class MainWindow
	{
		protected virtual void Build ()
		{
			global::ChrisJones.Frogger.Gui.Initialize (this);
			// Widget Frogger.MainWindow
			this.Name = "Frogger.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
			this.WindowPosition = ((global::Gtk.WindowPosition)(3));
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 640;
			this.DefaultHeight = 480;
			this.Show ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		}
	}
}