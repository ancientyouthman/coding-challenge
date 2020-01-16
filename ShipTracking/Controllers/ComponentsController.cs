using ShipTracking.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShipTracking.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ShipTrackingService _shipTrackingService;

        public ComponentsController()
        {
            _shipTrackingService = new ShipTrackingService();
        }

        [HttpGet]
        public ActionResult RenderGrid()
        {
            var grid = _shipTrackingService.GetGrid();
            return PartialView("_Grid", grid);
        }
    }
}