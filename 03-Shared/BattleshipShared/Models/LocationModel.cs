using BattleshipShared.Enums;

namespace BattleshipShared.Models
{
    /// <summary>
    /// Data required for generate ship location ids
    /// </summary>
    public class LocationModel
    {
        /// <summary>
        /// Start postion index of ship horizontaly
        /// </summary>
        public int StartHorizontalPositionIndex { get; set; }

        /// <summary>
        /// Start postion index of ship verticaly
        /// </summary>
        public int StartVerticalLocationIndex { get; set; }

        /// <summary>
        /// <see cref="LocationType"/>
        /// </summary>
        public LocationType LocationType { get; set; }

        /// <summary>
        /// The maximum board squars in horizontaly and verticaly orientation eg. 10x10
        /// </summary>
        public int BoardSize { get; set; }

        /// <summary>
        /// The square length of ship that will be generated
        /// </summary>
        public int SquareSize { get; set; }
    }
}