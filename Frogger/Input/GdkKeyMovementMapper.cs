using ChrisJones.Frogger.Delegates;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.Input
{
    public class GdkKeyMovementMapper : IKeyMapper
    {
        private readonly Gdk.Key _upKey;
        private readonly Gdk.Key _downKey;
        private readonly Gdk.Key _leftKey;
        private readonly Gdk.Key _rightKey;

        public GdkKeyMovementMapper(Gdk.Key upKey, Gdk.Key downKey, Gdk.Key leftKey, Gdk.Key rightKey)
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