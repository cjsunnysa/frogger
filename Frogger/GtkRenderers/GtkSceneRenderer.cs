using System.Collections.Generic;
using Frogger.GameObjects.Interfaces;
using Frogger.Utils;
using Gtk;

namespace Frogger.GtkRenderers
{
    public class GtkSceneRenderer : ISceneRenderer
    {
        private Gtk.Window _window;
        private DrawingArea _area;
        private List<IRendable> _sceneObjects;

        public GtkSceneRenderer(Gtk.Window window)
        {
            _window = window;
            _area = new DrawingArea();
            _area.ExposeEvent += _area_ExposeEvent;;
            _window.Add(_area);

            _sceneObjects = new List<IRendable>();
        }

        void _area_ExposeEvent (object o, ExposeEventArgs args)
        {
            RenderScene ();
        }

        #region ISceneRenderer implementation

        public double GetCanvasWidth ()
        {
            return _window.Allocation.Width;
        }

        public double GetCanvasHeight ()
        {
            return _window.Allocation.Height;
        }

        public void ClearScene()
        {
            _sceneObjects = new List<IRendable> ();
        }

        public IMovable AddPlayer (Position initialPosition)
        {
            var player = new Player(new GtkPlayerRenderer(_area), initialPosition);
            _sceneObjects.Add (player);

            return player;
        }

        public IMovable AddCar (Position initialPosition)
        {
            var car = new Car(new GtkCarRenderer(_area), initialPosition);
            _sceneObjects.Add (car);

            return car;
        }

        public void RenderScene()
        {
            _area.GdkWindow.Clear();
            
            foreach (var sceneObject in _sceneObjects) 
            {
                sceneObject.Render ();
            }
        }

        #endregion
    }
}