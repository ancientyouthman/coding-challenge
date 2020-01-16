using Newtonsoft.Json;
using ShipTracking.Enums;
using ShipTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShipTracking.Extensions;

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

        public UpdateAttempt MoveShips(IEnumerable<InstructionModel> instructions)
        {
            var result = new UpdateAttempt { Success = false };
            var grid = this.GetGrid();
            var ships = grid.Ships;
            if (ships == null || !ships.Any())
            {
                result.Message = "No ships on grid!";
                return result;
            }

            ships = ships.Where(ship => !ship.Lost);
            foreach (var instruction in instructions)
            {
                var shipToMove = ships.Where(ship => ship.Id == instruction.ShipId).FirstOrDefault();
                if (shipToMove == null) continue;
                var currentPosition = shipToMove.Position;
                var finalPosition = new CoordinateModel();

                foreach (char command in instruction.InstructionString)
                {
                    switch (command)
                    {
                        case 'L':
                            shipToMove.Rotate(Rotation.Left);
                            break;
                        case 'F':
                       //     shipToMove.Advance();
                            break;
                        case 'R':
                            shipToMove.Rotate(Rotation.Right);
                            break;
                    }
                }

            }


            result.Success = true;
            return result;

        }
    }
}