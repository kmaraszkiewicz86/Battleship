using System.Collections.Generic;
using BattleshipShared.Models;

namespace BattleshipShared.Services.ShipCollection
{
    public interface IShipCollectionService
    {
        public List<ShipModel> ShipModels { get; }

        void GenerateCollectionOfShips(int[] shipSquareSizes, int boardSize);

        ShipModel GetShipModelByLocationId(string locationId);
    }
}
