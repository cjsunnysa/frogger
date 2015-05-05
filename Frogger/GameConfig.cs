using System.Linq;
using ChrisJones.Frogger.Drawing2D;
using Gtk;

namespace ChrisJones.Frogger
{
    public static class GameConfig
    {
        public static readonly Dimension SCREEN_SIZE = new Dimension(640, 480);

        
        public static readonly Dimension PLAYER_DIMENSION = new Dimension(10, 25);
        public static readonly Position PLAYER_START_POSITION = new Position(320, 420);
        public static readonly int PLAYER_SPEED = 35;

        public static readonly Dimension CAR_DIMENSION = new Dimension(40, 15);
        public static readonly int CAR_QUEUE_COUNT = 2;
        public static readonly int CAR_QUEUE_START_YPOS = 150;
        public static readonly int CAR_QUEUE_YPOS_OFFSET = 60;
        public static readonly int CHANGE_DIRECTION_EVERY_X_QUEUE = 1;
        public static readonly int CAR_MIN_DISTANCE = (int)(CAR_DIMENSION.Width * 1) + 5;
        public static readonly int CAR_MAX_DISTANCE = (int)(CAR_DIMENSION.Width * 3);
        public static readonly int CAR_SPEED = 10;
        public static readonly int LEFT_OFFSCREEN_X_POS = (CAR_DIMENSION.Width * -1) - 1;
        public static readonly int RIGHT_OFFSCREEN_X_POS = SCREEN_SIZE.Width + 1;

        public const int FPS = 24;
    }
}