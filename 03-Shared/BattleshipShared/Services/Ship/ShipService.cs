using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Enums;
using BattleshipShared.Extensions;
using BattleshipShared.Models;

namespace BattleshipShared.Services.Ship
{
    /// <summary>
    /// Service that create next location identifiers based by provided <see cref="LocationModel"/>
    /// </summary>
    public class ShipService : IShipService
    {
        /// <summary>
        /// Location identifier that was added for previous ship, to prevent situation when 
        /// multiple ships may have conflicts in board field
        /// </summary>
        private readonly List<string> _usedLocationsIds;

        public ShipService()
        {
            _usedLocationsIds = new List<string>();
        }

        /// <summary>
        /// Get location data and try to add location ids to ship
        /// If any location id was used with previous ship then return false
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <param name="shipModel"><see cref="ShipModel"/></param>
        /// <returns></returns>
        public bool TryToGenerateShipModel(LocationModel locationModel,
            ref ShipModel shipModel)
        {
            var horizontalIndexName = locationModel.StartHorizontalPositionIndex.GetHorizontalIndexName();

            foreach (var locationId in GetLocationIdsPositions(locationModel))
            {
                shipModel.AddLocation(locationId);

                if (_usedLocationsIds.Any(u => u.ToLower() == locationId.ToLower()))
                {
                    shipModel.ClearLocationIds();
                    return false;
                }
                    
            }

            shipModel.LocationIds.ForEach(locationId => _usedLocationsIds.Add(locationId));

            return true;
        }

        /// <summary>
        /// Generate location id for next field information
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <returns></returns>
        private IEnumerable<string> GetLocationIdsPositions(LocationModel locationModel)
        {
            for (var squareSizeIndex = 0; squareSizeIndex < locationModel.SquareSize; squareSizeIndex++)
            {
                var locationId = GetNextLocationId(locationModel, squareSizeIndex);

                yield return locationId;
            }
        }

        /// <summary>
        /// Get data for new location id based on which directtion and how many square ship has
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <param name="squareSizeIndex">Current index of field that will get location id</param>
        /// <returns>returns location id that will be use for location id for current field id</returns>
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
    }
}
