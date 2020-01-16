using ShipTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Services
{
    public class ShipTrackingService
    {
        public GridModel GetGrid()
        {
            return new GridModel();
        }
    }
}