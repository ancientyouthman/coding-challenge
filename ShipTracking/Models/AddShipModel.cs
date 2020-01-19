﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShipTracking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public class AddShipModel
    {
        public CoordinateModel Position { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Orientation { get; set; }
    }
}