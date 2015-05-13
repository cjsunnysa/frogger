using ChrisJones.Frogger.Delegates;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Input
{
    /// <summary>
    ///     This subscribes to the Gtk.Window OnKeyPressed event and maps the key to a movement event that a GameObject can subscribe to.
    /// </summary>
    public class GdkKeyMapper : IKeyMapper
    {
        private readonly Gdk.Key _upKey;
        private readonly Gdk.Key _downKey;
        private readonly Gdk.Key _leftKey;
        private readonly Gdk.Key _rightKey;

        /// <param name="upKey">The key to move the GameObject upward.</param>
        /// <param name="downKey">The key to move the GameObject downward.</param>
        /// <param name="leftKey">The key to move the GameObject left.</param>
        /// <param name="rightKey">The key to move the GameObject right.</param>
        public GdkKeyMapper(Gdk.Key upKey, Gdk.Key downKey, Gdk.Key leftKey, Gdk.Key rightKey)
        {
            _upKey = upKey;
            _downKey = downKey;
            _leftKey = leftKey;
            _rightKey = rightKey;
        }

        public void OnKeyPressed(object o, KeyPressEventArgs args)
        {
            var key = args.Event.Key;

            OnMoveEvent eventToFire = null;
            
            if (key == _upKey)
                eventToFire = OnMoveUpEvent;
            else if (key == _downKey)
                eventToFire = OnMoveDownEvent;
            else if (key ==_leftKey)
                eventToFire = OnMoveLeftEvent;
            else if (key == _rightKey)
                eventToFire = OnMoveRightEvent;
            
            if (eventToFire == null)
                return;

            eventToFire();
        }

        public event OnMoveEvent OnMoveUpEvent;
        public event OnMoveEvent OnMoveDownEvent;
        public event OnMoveEvent OnMoveLeftEvent;
        public event OnMoveEvent OnMoveRightEvent;
    }
}