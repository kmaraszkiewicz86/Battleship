using System;
using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Enums;
using BattleshipShared.Models;
using BattleshipShared.Services.Ship;

namespace BattleshipShared.Services.ShipCollection
{
    public class ShipCollectionService : IShipCollectionService
    {
        public List<ShipModel> ShipModels { get; private set; }

        private IShipService _shipService;

        private Random _random;

        public ShipCollectionService(IShipService shipService)
        {
            ShipModels = new List<ShipModel>();
            _shipService = shipService;

            _random = new Random();
        }

        public void GenerateCollectionOfShips(int[] squareSizes, int boardSize)
        {
            var locationModel = new LocationModel();
            locationModel.BoardSize = boardSize;

            var locationTypeLength = Enum.GetValues(typeof(LocationType)).Length - 1;

            foreach (var squareSize in squareSizes)
            {
                var randomPosition = _random.Next(1, locationTypeLength);
                
                locationModel.LocationType = (LocationType)randomPosition;
                locationModel.SquareSize = squareSize;

                locationModel = GetRandomPostionIndexes(locationModel);
                
                ShipModels.Add(_shipService.GenerateShipModel(locationModel));
            }
        }

        private LocationModel GetRandomPostionIndexes(LocationModel locationModel)
        {
            do
            {
                locationModel.StartHorizontalPositionIndex = _random.Next(0, locationModel.BoardSize - 1);
                locationModel.StartVerticalLocationIndex = _random.Next(1, locationModel.BoardSize);

                locationModel = GetStartPosition(locationModel);
            }
            while (_shipService.CheckIfLocationIdWasUsed(locationModel));

            return locationModel;
        }

        private LocationModel GetStartPosition(LocationModel locationModel)
        {
            switch (locationModel.LocationType)
            {
                case LocationType.HorizontalToLeft:

                    if (locationModel.StartHorizontalPositionIndex < locationModel.SquareSize)
                    {
                        locationModel.StartHorizontalPositionIndex = locationModel.SquareSize;
                    }

                    break;

                case LocationType.HorizontalToRight:

                    if ((locationModel.StartHorizontalPositionIndex + locationModel.SquareSize) > locationModel.BoardSize)
                    {
                        locationModel.StartHorizontalPositionIndex = locationModel.BoardSize - locationModel.SquareSize;
                    }

                    break;

                case LocationType.VerticalToTop:

                    if (locationModel.StartVerticalLocationIndex < locationModel.SquareSize)
                    {
                        locationModel.StartVerticalLocationIndex = locationModel.SquareSize;
                    }

                    break;

                case LocationType.VerticalToBottom:

                    if ((locationModel.StartVerticalLocationIndex + locationModel.SquareSize) > locationModel.BoardSize)
                    {
                        locationModel.StartVerticalLocationIndex = locationModel.BoardSize - locationModel.SquareSize;
                    }

                    break;
            }

            return locationModel;
        }

        public ShipModel GetShipModelByLocationId(string locationId)
        {
            return ShipModels.FirstOrDefault(
                s => s.LocationPositions.Any(l => l.ToLower() == locationId.ToLower()));
        }
    }
}
