using System;
using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Enums;
using BattleshipShared.Models;
using BattleshipShared.Services.Ship;

namespace BattleshipShared.Services.ShipCollection
{
    /// <summary>
    /// Service creates collection of ships that will be refernce with board fields
    /// </summary>
    public class ShipCollectionService : IShipCollectionService
    {
        /// <summary>
        /// Created ships that will be reference with board
        /// </summary>
        public List<ShipModel> ShipModels { get; private set; }

        /// <summary>
        /// Get remaining ships that not was destroyed by user
        /// </summary>
        public IEnumerable<ShipModel> RemainingShips =>
            ShipModels.Where(s => !s.IsDestroyed);

        /// <summary>
        /// Check if all of ship was destroyed
        /// </summary>
        public bool IsFinish => RemainingShips.Count() == 0;

        /// <summary>
        /// <see cref="IShipService"/>
        /// </summary>
        private IShipService _shipService;

        private Random _random;

        public ShipCollectionService(IShipService shipService)
        {
            ShipModels = new List<ShipModel>();
            _shipService = shipService;

            _random = new Random();
        }

        /// <summary>
        /// Generate list of ships with randomize location positions
        /// </summary>
        /// <param name="squareSizes">Ship squares size that will be generate for each of ship</param>
        /// <param name="boardSize">The maximum board size to provide valid locations data for each ship</param>
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

                ShipModels.Add(GenerateShipModel(locationModel));
            }
        }

        /// <summary>
        /// Generate ship location data 
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <returns><see cref="ShipModel"/></returns>
        private ShipModel GenerateShipModel(LocationModel locationModel)
        {
            var shipModel = new ShipModel(locationModel.SquareSize);

            do
            {
                locationModel.StartHorizontalPositionIndex = _random.Next(0, locationModel.BoardSize - 1);
                locationModel.StartVerticalLocationIndex = _random.Next(1, locationModel.BoardSize);

                locationModel = GetStartPosition(locationModel);
            }
            while (!_shipService.TryToGenerateShipModel(locationModel, ref shipModel));

            return shipModel;
        }

        /// <summary>
        /// Update <see cref="LocationModel"/> of staring position for horizontal and vertical position
        /// The data will be starting point ot generate next location ids for all ship squares
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <returns>returns updated data of starting location ids for ship model</returns>
        private LocationModel GetStartPosition(LocationModel locationModel)
        {
            switch (locationModel.LocationType)
            {
                case LocationType.HorizontalToLeft:

                    locationModel.StartHorizontalPositionIndex = GetStartPositionIndexOrSquareSize(
                        locationModel.StartHorizontalPositionIndex,
                        locationModel.SquareSize);

                    break;

                case LocationType.HorizontalToRight:

                    locationModel.StartHorizontalPositionIndex = GetStartPositionIndexDontExceedSizeScope(
                        locationModel.StartHorizontalPositionIndex,
                        locationModel.SquareSize,
                        locationModel.BoardSize);

                    break;

                case LocationType.VerticalToTop:

                    locationModel.StartVerticalLocationIndex = GetStartPositionIndexOrSquareSize(
                        locationModel.StartVerticalLocationIndex,
                        locationModel.SquareSize);

                    break;

                case LocationType.VerticalToBottom:

                    locationModel.StartVerticalLocationIndex = GetStartPositionIndexDontExceedSizeScope(
                        locationModel.StartVerticalLocationIndex,
                        locationModel.SquareSize,
                        locationModel.BoardSize);

                    break;
            }

            return locationModel;
        }

        /// <summary>
        /// Check if Size of ship is smallest then remaining squars to end of board
        /// If lenght of squares is greather then remaining squares to end of board then return as 
        /// started position index square length of ship
        /// </summary>
        /// <param name="startPostionIndex">started positions for check if is valid position to add to ship</param>
        /// <param name="squareSize">ship square size</param>
        /// <returns>return starting position index for ship</returns>
        private int GetStartPositionIndexOrSquareSize(int startPostionIndex, int squareSize)
        {
            if (startPostionIndex < squareSize)
            {
                return squareSize;
            }

            return startPostionIndex;
        }

        /// <summary>
        /// Check if start position is not too near of end of board, if yes then return the maximum index
        /// position that dont exceed end of board base of ship square size
        /// </summary>
        /// <param name="startPostionIndex">started positions for check if is valid position to add to ship</param>
        /// <param name="squareSize">ship square size</param>
        /// <param name="boardSize">the lenght of board</param>
        /// <returns>return starting position index for ship</returns>
        private int GetStartPositionIndexDontExceedSizeScope(int startPostionIndex, int squareSize, int boardSize)
        {
            if ((startPostionIndex + squareSize) > boardSize)
            {
                return boardSize - squareSize;
            }

            return startPostionIndex;
        }

        /// <summary>
        /// Get ship by location id
        /// </summary>
        /// <param name="locationId">Location id data base on {horizontal index}{vertical index}</param>
        /// <returns><see cref="ShipModel"/></returns>
        public ShipModel GetShipModelByLocationId(string locationId)
        {
            return ShipModels.FirstOrDefault(
                s => s.LocationIds.Any(l => l.ToLower() == locationId.ToLower()));
        }
    }
}
