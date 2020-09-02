using System.Collections.Generic;
using BattleshipShared.Models;

namespace BattleshipShared.Services.Board
{
    /// <summary>
    /// Service that add generated ships by <see cref="ShipCollectionService"/> to board
    /// </summary>
    public interface IBoardService
    {
        /// <summary>
        /// Number of hits that user do before destroy all of ship
        /// </summary>
        int NumberOfHits { get; }

        /// <summary>
        /// The data of each board fields of game board <seealso cref="BoardFieldModel"/>
        /// </summary>
        Dictionary<string, BoardFieldModel> BoardFields { get; }

        /// <summary>
        /// Reference generated ships by <see cref="ShipCollectionService"/> to board
        /// </summary>
        /// <param name="squareSizes">Ship squares size that will be generate for each of ship</param>
        /// <param name="boardSize">The maximum board size to provide valid locations data for each ship</param>
        void GenerateInitialBoard(int[] squareSizes, int boardSize);

        /// <summary>
        /// Check if typed location id by user is match ship that is on that location id
        /// If yes then reduce ship health information and check that location id was hit
        /// Also increase number of hits and store this information in <see cref="NumberOfHits"/> property
        /// </summary>
        /// <param name="locationId">location id that was typed by user</param>
        /// <returns>if on the <paramref name="locationId"/> is ship than true othrewise false</returns>
        bool CheckLocationId(string locationId);
    }
}
