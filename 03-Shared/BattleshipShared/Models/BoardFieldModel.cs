namespace BattleshipShared.Models
{
    /// <summary>
    /// Information about each of board field
    /// </summary>
    public class BoardFieldModel
    {
        /// <summary>
        /// The object of ship that is reference with the current of field
        /// </summary>
        public ShipModel ShipModel { get; set; }

        /// <summary>
        /// Check id board is empty
        /// </summary>
        public bool IsNotEmpty => ShipModel != null;

        /// <summary>
        /// Check if field was hit by user
        /// </summary>
        public bool WasHit { get; set; }

        public BoardFieldModel(ShipModel shipModel)
        {
            ShipModel = shipModel;
        }
    }
}