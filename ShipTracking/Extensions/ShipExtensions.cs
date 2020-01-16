using ShipTracking.Enums;
using ShipTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Extensions
{
    public static class ShipExtensions
    {
        public static void Rotate(this ShipModel ship, Rotation rotation)
        {
            var orientation = (int)ship.Orientation;
            // safe to do this i think as there will only ever be left and right.
            // re-think if ships can ever move diagonally or rotate 180 degrees
            orientation = rotation == Rotation.Left ? orientation - 90 : orientation + 90;
            if (orientation < 0) orientation = 270;
            if (orientation > 270) orientation = 0;

            ship.Orientation = (Direction)orientation;

        }
    }
}