using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Configuration
{
    /// <summary>
    ///     Holds global configuration values for the Screen, Player, Car and Animation behaviour.
    /// </summary>
    public static class GameConfig
    {
        // Screen.
        public static readonly Dimension SCREEN_SIZE = new Dimension(640, 480);
        
        
        // Player.
        public static readonly Dimension PLAYER_DIMENSION = new Dimension(10, 30);
        public static readonly Position PLAYER_START_POSITION = new Position(320, 420);
        public const int PLAYER_SPEED = 30;

        
        // Car.
        public static readonly Dimension CAR_DIMENSION = new Dimension(55, 23);
            // How many queues to render.
        public const int CAR_QUEUE_COUNT = 2;
            // From where should the queues be rendered.
        public const int CAR_QUEUE_START_YPOS = 152;
            // Each queue should be rendered this value below the last.
        public const int CAR_QUEUE_YPOS_OFFSET = 62;
        public const Direction DIRECTION_FIRST_QUEUE = Direction.Right;
        public const int CHANGE_DIRECTION_EVERY_X_QUEUE = 1;
        public const int CAR_SPEED = 10;
            // The min gap distance between cars.
        public static readonly int CAR_MIN_DISTANCE = (int)(CAR_DIMENSION.Width * 1) + 5;
            // The max gap distance between cars.
        public static readonly int CAR_MAX_DISTANCE = (int)(CAR_DIMENSION.Width * 3);
            // The left position at which the queue ends/begins.
        public static readonly int LEFT_OFFSCREEN_X_POS = (CAR_DIMENSION.Width * -1) - 1;
            // The right position at which the queue ends/begins.
        public static readonly int RIGHT_OFFSCREEN_X_POS = SCREEN_SIZE.Width + 1;


        // Animation.
            // How many times per second a frame should be rendered.
        public const int FPS = 24;
    }
}