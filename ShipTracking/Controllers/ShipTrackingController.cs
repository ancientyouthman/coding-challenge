using ShipTracking.Models;
using ShipTracking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipTracking.Controllers
{
    public class ShipTrackingController : Controller
    {
        private readonly ShipTrackingService _shipTrackingService;
        private readonly  string _allowedCommands = "FLR";

        public ShipTrackingController()
        {
            _shipTrackingService = new ShipTrackingService();
        }

        [HttpGet]
        public ActionResult RenderGrid()
        {
            var grid = _shipTrackingService.GetGrid();
            return PartialView("_Grid", grid);
        }

        [HttpGet]
        public ActionResult RenderControlPanel()
        {
            var ships = new List<ShipModel>();
            var grid = _shipTrackingService.GetGrid();
            if (grid != null && grid.Ships != null) ships = grid.Ships.ToList();
            return PartialView("_ControlPanel", ships);
        }

        [HttpPost]
        public ActionResult MoveShips(IEnumerable<InstructionModel> instructions )
        {
            if (instructions != null && instructions.Any()) return null;

                foreach(var instruction in instructions)
                {
                    foreach (char c in instruction.InstructionString)
                    {
                        if (!_allowedCommands.Contains(c))
                        {
                            ModelState.AddModelError("", $"Invalid command for Ship ID {instruction.ShipId}");
                        }
                    }
                }            

            if (!ModelState.IsValid) return null; // TODO: hmm

            _shipTrackingService.MoveShips(instructions);
            return null;

        }
    }
}