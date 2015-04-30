using System.Linq;
using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger
{
    public static class GameConfig
    {
        public static readonly Dimension SCREEN_SIZE = new Dimension(640, 480);
        public static readonly Dimension CAR_DIMENSION = new Dimension(45, 15);
        public static readonly Dimension PLAYER_DIMENSION = new Dimension(15, 45);
        public static readonly Position PLAYER_START_POSITION = new Position(320, 420);
        public static readonly int LEFT_OFFSCREEN_X_POS = (CAR_DIMENSION.Width*-1) - 1;
        public static readonly int RIGHT_OFFSCREEN_X_POS = SCREEN_SIZE.Width + 1;
        public static readonly int[] CARS_LEFT_Y_POS_ARRAY = new[] {50, 100, 150};
        public static readonly int[] CARS_RIGHT_Y_POS_ARRAY = new[] {80, 130, 180};
        public static readonly int CARS_LEFT_Y_POS = CARS_LEFT_Y_POS_ARRAY.Last();
        public static readonly int CARS_RIGHT_Y_POS = CARS_RIGHT_Y_POS_ARRAY.Last();
        public const int FPS = 24;
        public const int DRAW_FRAME_EVERY_MILLISECONDS = 1000/FPS;
        public static readonly int CAR_MIN_DISTANCE = CAR_DIMENSION.Width + 5;
        public static readonly int CAR_MAX_DISTANCE = (int) (CAR_DIMENSION.Width*2.8);
    }
}