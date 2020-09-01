using System;
using System.Collections.Generic;
using System.Text;
using BattleshipShared.Models;

namespace BattleshipShared.Services.Board
{
    public interface IBoardService
    {
        int NumberOfHits { get; }

        IEnumerable<ShipModel> RemainingShipModels { get; }

        Dictionary<string, FieldModel> BoardFields { get; }

        void GenerateInitialBoard(int[] squareSizes, int boardSize);

        bool CheckLocationId(string locationId);
    }
}
