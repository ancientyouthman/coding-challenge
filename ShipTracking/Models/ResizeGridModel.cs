using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public class ResizeGridModel
    {
        [Required(ErrorMessage = "Please enter a value for the X axis")]
        [Range(0, 50, ErrorMessage = "X axis value must be a positive number less than 50")]
        public int X { get; set; }
        [Required(ErrorMessage = "Please enter a value for the Y axis")]
        [Range(0, 50, ErrorMessage = "Y axis value must be a positive number less than 50")]
        public int Y { get; set; }
    }
}