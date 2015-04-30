﻿using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.Interfaces;

namespace ChrisJones.Frogger.GameObjects
{
    public class Car : GameObject
    {
        public Car(Position initialPosition, IRenderer renderer, Direction initialDirection, int speed) : base(initialPosition, renderer, initialDirection, speed)
        {
        }
    }
}