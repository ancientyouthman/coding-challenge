using Newtonsoft.Json;
using ShipTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipTracking.Services
{
    public class ShipTrackingService
    {
        private readonly DataService _dataService;

        public ShipTrackingService()
        {
            _dataService = new DataService();
        }

        public GridModel GetGrid()
        {
            var gridData = _dataService.ReadGridData();
            var grid = JsonConvert.DeserializeObject<GridModel>(gridData);
            return grid;
        }
    }
}