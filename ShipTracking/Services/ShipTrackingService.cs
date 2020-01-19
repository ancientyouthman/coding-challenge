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
            var result = new UpdateAttempt();
            var grid = this.GetGrid();
            var ships = grid.Ships;
            ships = ships.Where(ship => !ship.Lost).OrderBy(ship => ship.Id);

            if (ships == null || !ships.Any())
            {
                result.Message = "No ships on grid!";
                return result;
            }

            var xBoundary = grid.Dimensions.X;
            var yBoundary = grid.Dimensions.Y;
            var pointsOfNoReturn = new List<CoordinateModel>();

            foreach (var instruction in instructions)
            {
                var shipToMove = ships.Where(ship => ship.Id == instruction.ShipId).FirstOrDefault();
                if (shipToMove == null) continue;
                var currentPosition = shipToMove.Position;
                foreach (char command in instruction.InstructionString)
                {
                    switch (Char.ToUpper(command))
                    {
                        case 'L':
                            shipToMove.Rotate(Rotation.Left);
                            break;
                        case 'F':
                            shipToMove.Advance();
                            if (shipToMove.Position.X > xBoundary
                                || shipToMove.Position.Y > yBoundary
                                || shipToMove.Position.X < 0
                                || shipToMove.Position.Y < 0)
                            {
                                shipToMove.Lost = true;
                                pointsOfNoReturn.Add(new CoordinateModel
                                {
                                    X = shipToMove.Position.X,
                                    Y = shipToMove.Position.Y
                                });
                                continue;
                            }

                            break;
                        case 'R':
                            shipToMove.Rotate(Rotation.Right);
                            break;
                    }
                }
            }

            grid.PointsOfNoReturn = grid.PointsOfNoReturn.Concat(pointsOfNoReturn);
            var gridJson = JsonConvert.SerializeObject(grid);
            _dataService.UpdateGrid(gridJson);

            result.Success = true;
            return result;

        }

        public UpdateAttempt ResizeGrid(ResizeGridModel model)
        {
            var grid = this.GetGrid();
            grid.Dimensions = new CoordinateModel
            {
                X = model.X,
                Y = model.Y
            };
            // query: should we be "discovering" ships previously marked as lost? spec says "lost forever"
            // also: can we abstract this away (creating and updating the grid json string)
            var gridJson = JsonConvert.SerializeObject(grid);
            var result = _dataService.UpdateGrid(gridJson);
            return result;
        }

        public UpdateAttempt AddShip(AddShipModel model)
        {
            var grid = this.GetGrid();
            var shipId = 1;

            if (grid.Ships != null && grid.Ships.Any()) shipId = grid.Ships.Select(ship => ship.Id).Max() + 1;

            var shipToAdd = new ShipModel
            {
                Id = shipId,
                Lost = false,
                Orientation = model.Orientation,
                Position = new CoordinateModel {
                    X = model.Position.X,
                    Y = model.Position.Y,
                }
            };

            grid.Ships = grid.Ships.Concat(new[] { shipToAdd });
            var gridJson = JsonConvert.SerializeObject(grid);
            var result = _dataService.UpdateGrid(gridJson);
            return result;
        }
    }
}