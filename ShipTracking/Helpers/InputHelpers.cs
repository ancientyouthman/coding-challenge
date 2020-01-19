using ShipTracking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Helpers
{
    public static class InputHelpers
    {
        private static Dictionary<string, Direction> DirectionInputs => new Dictionary<string, Direction>()
        {
            { "N", Direction.North },
            { "E", Direction.East },
            { "S", Direction.South },
            { "W", Direction.West },

        };

        public static Direction? MapDirectionInput(string direction)
        {
            if (!DirectionInputs.Keys.Contains(direction.ToUpper())) return null;
            return DirectionInputs[direction.ToUpper()];
        }
    }
}