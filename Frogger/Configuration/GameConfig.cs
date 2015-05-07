﻿using ChrisJones.Frogger.Drawing2D;

namespace ChrisJones.Frogger.Configuration
{
    public static class GameConfig
    {
        public static readonly Dimension SCREEN_SIZE = new Dimension(640, 480);

        
        public static readonly Dimension PLAYER_DIMENSION = new Dimension(10, 30);
        public static readonly Position PLAYER_START_POSITION = new Position(320, 420);
        public static readonly int PLAYER_SPEED = 30;

        public static readonly Dimension CAR_DIMENSION = new Dimension(55, 23);
        public static readonly int CAR_QUEUE_COUNT = 2;
        public static readonly int CAR_QUEUE_START_YPOS = 152;
        public static readonly int CAR_QUEUE_YPOS_OFFSET = 62;
        public static readonly int CHANGE_DIRECTION_EVERY_X_QUEUE = 1;
        public static readonly int CAR_MIN_DISTANCE = (int)(CAR_DIMENSION.Width * 1) + 5;
        public static readonly int CAR_MAX_DISTANCE = (int)(CAR_DIMENSION.Width * 3);
        public static readonly int CAR_SPEED = 10;
        public static readonly int LEFT_OFFSCREEN_X_POS = (CAR_DIMENSION.Width * -1) - 1;
        public static readonly int RIGHT_OFFSCREEN_X_POS = SCREEN_SIZE.Width + 1;

        public const int FPS = 24;
    }
}