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
    }
}