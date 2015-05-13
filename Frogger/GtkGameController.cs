using ChrisJones.Frogger.Input;
using ChrisJones.Frogger.Engine;
using ChrisJones.Frogger.Factories;
using Gtk;
using System;
using ChrisJones.Frogger.Conditions;
using ChrisJones.Frogger.GameCycle;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger
{
    /// <summary>
    ///     A wrapper for the GameEngine class that is specific to and dependent on the Gtk# framework.
    ///     Automates the controlling of the GameEngine class.
    /// </summary>
    public class GtkGameController
    {
        private readonly GameEngine _engine;
        private readonly DrawingArea _area;
        private bool _keepLooping = true;

        /// <param name="window">A Gtk.Window which will hold a DrawingArea onto which the game will be drawn.</param>
        public GtkGameController(Gtk.Window window)
        {
            if (window == null)
                throw new ArgumentNullException ("window");

            _area = CreateDrawingSurface(window);

            var createProcedure = new CreateTwoWayTrafficObjects();
            var factory = new GtkGameObjectFactory(_area, CreateKeyMapper(window));
            var gameProcedures = new IGameCycleProcedure[] { new MoveAutomatedObjects(), new IfPlayerWinsThenRespawn(), new IfPlayerLosesThenStain() };
            
            _engine = new GameEngine(createProcedure, factory, gameProcedures);

            _engine.InitialiseGame();
        }

        public void RunGame()
        {
            GLib.Timeout.Add(1, Loop);
        }

        public void StopGame()
        {
            _keepLooping = false;
        }

        #region private methods

        private DrawingArea CreateDrawingSurface(Gtk.Window window)
        {
            var area = new DrawingArea();
            area.ExposeEvent += CanvasExposed;

            window.Add(area);

            return area;
        }

        private GdkKeyMapper CreateKeyMapper(Gtk.Window window)
        {
            var keyMapper = new GdkKeyMapper(Gdk.Key.KP_8, Gdk.Key.KP_2, Gdk.Key.KP_4, Gdk.Key.KP_6);

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

        #endregion
    }
}