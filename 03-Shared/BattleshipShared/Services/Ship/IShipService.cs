using BattleshipShared.Models;

namespace BattleshipShared.Services.Ship
{
    /// <summary>
    /// Service that create next location identifiers based by provided <see cref="LocationModel"/>
    /// </summary>
    public interface IShipService
    {
        /// <summary>
        /// Get location data and try to add location ids to ship
        /// If any location id was used with previous ship then return false
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <param name="shipModel"><see cref="ShipModel"/></param>
        /// <returns></returns>
        bool TryToGenerateShipModel(LocationModel locationModel,
            ref ShipModel shipModel);
    }
}
