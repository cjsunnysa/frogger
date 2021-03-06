﻿using Gtk;

namespace ChrisJones.Frogger
{
    public partial class MainWindow: Gtk.Window
    {
        private readonly GtkGameController _gameController;

        public MainWindow () : base (Gtk.WindowType.Toplevel)
        {
            Build ();

            _gameController = new GtkGameController(this);
            _gameController.RunGame();
            
            ShowAll ();
        }

        protected void OnDeleteEvent (object sender, DeleteEventArgs a)
        {
            _gameController.StopGame();

            Application.Quit ();
            a.RetVal = true;
        }
    }
}