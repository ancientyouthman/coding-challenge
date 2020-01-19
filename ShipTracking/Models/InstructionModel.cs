using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShipTracking.Models
{
    public class InstructionModel
    {
        [StringLength(100, ErrorMessage = "Instruction string must be less than 100 characters")]
        public string InstructionString { get; set; }
        [Required]
        public int ShipId { get; set; }
    }
}