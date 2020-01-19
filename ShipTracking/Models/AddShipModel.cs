using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ShipTracking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public class AddShipModel
    {
        public CoordinateModel Position { get; set; }
        [Required]
        [StringLength(1, ErrorMessage = "Position must be one character - N, E, S or W")]
        public string Orientation { get; set; }
    }
}