using System.Collections.Generic;
using BattleshipShared.Extensions;
using BattleshipShared.Models;
using BattleshipShared.Services.ShipCollection;

namespace BattleshipShared.Services.Board
{
    /// <summary>
    /// Service that add generated ships by <see cref="ShipCollectionService"/> to board
    /// </summary>
    public class BoardService : IBoardService
    {
        /// <summary>
        /// Number of hits that user do before destroy all of ship
        /// </summary>
        public int NumberOfHits { get; private set; }

        /// <summary>
        /// The data of each board fields of game board <seealso cref="BoardFieldModel"/>
        /// </summary>
        public Dictionary<string, BoardFieldModel> BoardFields { get; private set; }

        /// <summary>
        /// <see cref="IShipCollectionService"/>
        /// </summary>
        private IShipCollectionService _shipCollectionService;

        public BoardService(IShipCollectionService shipCollectionService)
        {
            _shipCollectionService = shipCollectionService;

            BoardFields = new Dictionary<string, BoardFieldModel>();
        }

        /// <summary>
        /// Reference generated ships by <see cref="ShipCollectionService"/> to board
        /// </summary>
        /// <param name="squareSizes">Ship squares size that will be generate for each of ship</param>
        /// <param name="boardSize">The maximum board size to provide valid locations data for each ship</param>
        public void GenerateInitialBoard(int[] squareSizes, int boardSize)
        {
            _shipCollectionService.GenerateCollectionOfShips(squareSizes, boardSize);

            for (var verticalIndex = 1; verticalIndex <= boardSize; verticalIndex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < boardSize; horizontalIndex++)
                {
                    var locationId = $"{horizontalIndex.GetHorizontalIndexName()}{verticalIndex}";

                    var shipModel = _shipCollectionService.GetShipModelByLocationId(locationId);

                    BoardFields.Add(locationId, new BoardFieldModel(shipModel));
                }
            }
        }

        /// <summary>
        /// Check if typed location id by user is match ship that is on that location id
        /// If yes then reduce ship health information and check that location id was hit
        /// Also increase number of hits and store this information in <see cref="NumberOfHits"/> property
        /// </summary>
        /// <param name="locationId">location id that was typed by user</param>
        /// <returns>if on the <paramref name="locationId"/> is ship than true othrewise false</returns>
        public bool CheckLocationId(string locationId)
        {
            NumberOfHits++;

            locationId = locationId.ToUpper();

            if (!BoardFields.ContainsKey(locationId))
                return false;

            var boardField = BoardFields[locationId];
            
            if (!boardField.IsNotEmpty)
            {
                boardField.WasHit = true;
                return false;
            }

            if (!boardField.WasHit)
                boardField.ShipModel.ReduceSquareSize();

            boardField.WasHit = true;

            return true;
        }
    }
}
