using System.Collections.Generic;
using System.Text;

namespace BattleshipShared.Models
{
    /// <summary>
    /// Data of generated ship model
    /// </summary>
    public class ShipModel
    {
        /// <summary>
        /// Information if the ship is shunk
        /// </summary>
        public bool IsDestroyed => CurrentSquaresSize == 0;

        /// <summary>
        /// The size of ship
        /// </summary>
        public int CurrentSquaresSize { get; private set; }

        /// <summary>
        /// The list of location ids that inform user where ship was placed on board
        /// </summary>
        public List<string> LocationIds { get; }

        /// <summary>
        /// The name of ship that based of length of ship squares
        /// </summary>
        public string Name
        {
            get
            {
                return CurrentSquaresSize == 5 ? "Battleship" : "Destroyer";
            }
        }

        public ShipModel(int size)
        {
            CurrentSquaresSize = size;
            LocationIds = new List<string>();
        }

        /// <summary>
        /// Add location id to <see cref="LocationIds"/> field
        /// </summary>
        /// <param name="locationId"></param>
        public void AddLocation(string locationId)
        {
            LocationIds.Add(locationId);
        }

        /// <summary>
        /// Clear location ids of ship
        /// </summary>
        public void ClearLocationIds()
        {
            LocationIds.Clear();
        }

        /// <summary>
        /// Reduce healft of ship
        /// </summary>
        public void ReduceSquareSize()
        {
            if (CurrentSquaresSize > 0)
                CurrentSquaresSize--;
        }

        /// <summary>
        /// Print information aboud ship
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var locationIdsStringBuilder = new StringBuilder();

            foreach (var locationId in LocationIds)
            {
                locationIdsStringBuilder.Append($"{locationId}, ");
            }

            var condition = $"Remain square to hit: {CurrentSquaresSize}";

            if (CurrentSquaresSize == 0)
            {
                condition = "Ship destroyed";
            }

            return $"{Name} : {condition} -> {locationIdsStringBuilder}";
        }
    }
}
