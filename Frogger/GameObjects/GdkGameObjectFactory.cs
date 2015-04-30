﻿using ChrisJones.Frogger.Drawing2D;
using ChrisJones.Frogger.GtkRenderers;
using ChrisJones.Frogger.Interfaces;
using Gtk;

namespace ChrisJones.Frogger.GameObjects
{
    public class GdkGameObjectFactory : IGameObjectFactory
    {
        private readonly DrawingArea _area;
        private readonly int _moveSpeed = 10;

        public GdkGameObjectFactory(DrawingArea area)
        {
            _area = area;
        }

        public Player CreatePlayer(Position startPosition, Direction initialDirection)
        {
            return new Player(startPosition, new GtkPlayerRenderer(_area), initialDirection, _moveSpeed);
        }

        public Car CreateCarDrivingLeft(Position startPosition)
        {
            //ToDo: create GtkCarRendererLeft and Right for facing different directions.
            return new Car(startPosition, new GtkCarRenderer(_area), Direction.Left, _moveSpeed);
        }

        public Car CreateCarDrivingRight(Position startPosition)
        {
            return new Car(startPosition, new GtkCarRenderer(_area), Direction.Right, _moveSpeed);
        }
    }
}