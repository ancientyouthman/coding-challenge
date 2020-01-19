using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public interface IPositionableOnGrid
    {
        CoordinateModel Position { get; set; }
    }
}