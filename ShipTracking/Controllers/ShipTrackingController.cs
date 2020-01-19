using ShipTracking.Models;
using ShipTracking.Services;
using ShipTracking.Extensions;
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
        private readonly string _allowedCommands = "FLR";

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
        public ActionResult RenderMoveShips()
        {
            var ships = new List<ShipModel>();
            var grid = _shipTrackingService.GetGrid();
            if (grid != null && grid.Ships != null) ships = grid.Ships.ToList();
            return PartialView("_MoveShips", ships);
        }

        [HttpPost]
        public ActionResult MoveShips(List<InstructionModel> instructions)
        {
            if (instructions == null || !instructions.Any()) return null;
            instructions = instructions.Where(instruction => !string.IsNullOrEmpty(instruction.InstructionString)).ToList();
            foreach (var instruction in instructions)
            {
                foreach (char c in instruction.InstructionString)
                {
                    if (!_allowedCommands.Contains(Char.ToUpper(c)))
                    {
                        ModelState.AddModelError(instruction.ShipId.ToString(), $"Invalid command for Ship ID {instruction.ShipId}");
                        break;
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return Errors();
            }

            var result = _shipTrackingService.MoveShips(instructions);
            if (result.Success) return this.RenderGrid();
            ModelState.AddModelError("", result.Message);
            return Errors();

        }

        [HttpGet]
        public ActionResult RenderResizeGrid()
        {
            return PartialView("_RenderResizeGrid");
        }

        [HttpPost]
        public ActionResult ResizeGrid(CoordinateModel coords)
        {
            return null;
        }

        [HttpGet]
        public ActionResult RenderAddShip()
        {
            return PartialView("AddShip");
        }

        [HttpPost]
        public ActionResult AddShip(AddShipModel ship)
        {
            return null;
        }


        private ActionResult Errors()
        {
            Response.StatusCode = 400;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return Json(new { errors });
        }
    }
}