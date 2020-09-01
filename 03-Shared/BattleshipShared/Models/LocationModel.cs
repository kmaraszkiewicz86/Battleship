using BattleshipShared.Enums;

namespace BattleshipShared.Models
{
    public class LocationModel
    {
        public int StartHorizontalPositionIndex { get; set; }
        public int StartVerticalLocationIndex { get; set; }
        public LocationType LocationType { get; set; }
        public int BoardSize { get; set; }
        public int SquareSize { get; set; }
    }
}