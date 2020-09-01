using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Extensions;
using BattleshipShared.Models;
using BattleshipShared.Services.ShipCollection;

namespace BattleshipShared.Services.Board
{
    public class BoardService : IBoardService
    {
        public int NumberOfHits { get; private set; }

        public IEnumerable<ShipModel> RemainingShipModels => 
            _shipCollectionService.ShipModels.Where(s => !s.IsDestroyed);

        public Dictionary<string, FieldModel> BoardFields { get; private set; }

        private IShipCollectionService _shipCollectionService;

        public BoardService(IShipCollectionService shipCollectionService)
        {
            _shipCollectionService = shipCollectionService;

            BoardFields = new Dictionary<string, FieldModel>();
        }

        public void GenerateInitialBoard(int[] squareSizes, int boardSize)
        {
            _shipCollectionService.GenerateCollectionOfShips(squareSizes, boardSize);

            for (var verticalIndex = 1; verticalIndex <= boardSize; verticalIndex++)
            {
                for (var horizontalIndex = 0; horizontalIndex < boardSize; horizontalIndex++)
                {
                    var locationId = $"{horizontalIndex.GetHorizontalIndexName()}{verticalIndex}";

                    var shipModel = _shipCollectionService.GetShipModelByLocationId(locationId);

                    BoardFields.Add(locationId, new FieldModel(shipModel));
                }
            }
        }

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
