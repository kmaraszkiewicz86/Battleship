using System.Collections.Generic;
using BattleshipShared.Models;

namespace BattleshipShared.Services.ShipCollection
{
    /// <summary>
    /// Service creates collection of ships that will be refernce with board fields
    /// </summary>
    public interface IShipCollectionService
    {
        /// <summary>
        /// Created ships that will be reference with board
        /// </summary>
        public List<ShipModel> ShipModels { get; }

        /// <summary>
        /// Get remaining ships that not was destroyed by user
        /// </summary>
        IEnumerable<ShipModel> RemainingShips { get; }

        /// <summary>
        /// Check if all of ship was destroyed
        /// </summary>
        bool IsFinish { get; }

        /// <summary>
        /// Generate ship location data 
        /// </summary>
        /// <param name="locationModel"><see cref="LocationModel"/></param>
        /// <returns><see cref="ShipModel"/></returns>
        void GenerateCollectionOfShips(int[] shipSquareSizes, int boardSize);

        /// <summary>
        /// Get ship by location id
        /// </summary>
        /// <param name="locationId">Location id data base on {horizontal index}{vertical index}</param>
        /// <returns><see cref="ShipModel"/></returns>
        ShipModel GetShipModelByLocationId(string locationId);
    }
}
