using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Enums;
using BattleshipShared.Extensions;
using BattleshipShared.Models;

namespace BattleshipShared.Services.Ship
{
    public class ShipService : IShipService
    {
        private readonly List<string> _usedLocationsIds;

        public ShipService()
        {
            _usedLocationsIds = new List<string>();
        }

        public bool CheckIfLocationIdWasUsed(LocationModel locationModel)
        {
            if (_usedLocationsIds.Count == 0)
            {
                return false;
            }

            var horizontalIndexName = locationModel.StartHorizontalPositionIndex.GetHorizontalIndexName();

            for (var squareSizeIndex = 0; squareSizeIndex < locationModel.SquareSize; squareSizeIndex++)
            {
                var locationId = GetNextLocationId(locationModel, squareSizeIndex);

                if (_usedLocationsIds.Any(u => u.ToLower() == locationId.ToLower()))
                    return true;
            }

            return false;
        }

        public ShipModel GenerateShipModel(LocationModel locationModel)
        {
            var shipModel = new ShipModel(locationModel.SquareSize);
            
            switch (locationModel.LocationType)
            {
                case LocationType.VerticalToTop:
                case LocationType.VerticalToBottom:

                    foreach (var locationId in GetLocationIdsForVerticalPosition(locationModel))
                    {
                        shipModel.AddLocation(locationId);
                    }
                    
                    break;

                case LocationType.HorizontalToRight:
                case LocationType.HorizontalToLeft:

                    foreach (var locationId in GetLocationIdsForHorizontalPosition(locationModel))
                    {
                        shipModel.AddLocation(locationId);
                    }

                    break;
            }

            return shipModel;
        }

        private string GetNextLocationId(LocationModel locationModel, int squareSizeIndex)
        {
            var verticalIndex = 0;
            var horizontalIndex = 0;

            switch (locationModel.LocationType)
            {
                case LocationType.VerticalToTop:
                    horizontalIndex = locationModel.StartHorizontalPositionIndex;
                    verticalIndex = locationModel.StartVerticalLocationIndex - squareSizeIndex;
                    break;

                case LocationType.VerticalToBottom:
                    horizontalIndex = locationModel.StartHorizontalPositionIndex;
                    verticalIndex = locationModel.StartVerticalLocationIndex + squareSizeIndex;
                    break;

                case LocationType.HorizontalToLeft:
                    verticalIndex = locationModel.StartVerticalLocationIndex;
                    horizontalIndex = locationModel.StartHorizontalPositionIndex - squareSizeIndex;
                    break;

                case LocationType.HorizontalToRight:
                    verticalIndex = locationModel.StartVerticalLocationIndex;
                    horizontalIndex = locationModel.StartHorizontalPositionIndex + squareSizeIndex;
                    break;
            }

            var horizontalIndexName = horizontalIndex.GetHorizontalIndexName();
            return $"{horizontalIndexName}{verticalIndex}";
        }

        private IEnumerable<string> GetLocationIdsForVerticalPosition(LocationModel locationModel)
        {
            for (var squareSizeIndex = 0; squareSizeIndex < locationModel.SquareSize; squareSizeIndex++)
            {
                var locationId = GetNextLocationId(locationModel, squareSizeIndex);

                _usedLocationsIds.Add(locationId);
                yield return locationId;
            }
        }

        private IEnumerable<string> GetLocationIdsForHorizontalPosition(LocationModel locationModel)
        {
            var verticalIndex = locationModel.StartVerticalLocationIndex;

            for (var squareSizeIndex = 0; squareSizeIndex < locationModel.SquareSize; squareSizeIndex++)
            {
                var locationId = GetNextLocationId(locationModel, squareSizeIndex);

                _usedLocationsIds.Add(locationId);

                yield return locationId;
            }
        }
    }
}
