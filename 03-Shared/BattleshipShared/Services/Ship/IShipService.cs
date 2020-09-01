using BattleshipShared.Models;

namespace BattleshipShared.Services.Ship
{
    public interface IShipService
    {
        bool CheckIfLocationIdWasUsed(LocationModel locationModel);

        ShipModel GenerateShipModel(LocationModel locationModel);
    }
}
