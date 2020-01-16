using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public class GridModel
    {
        public CoordinateModel Dimensions { get; set; }
        public IEnumerable<ShipModel> Ships { get; set; }
        public IEnumerable<CoordinateModel> PointsOfNoReturn { get; set; }

    }
}