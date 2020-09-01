using System.Collections.Generic;
using BattleshipShared.Enums;
using BattleshipShared.Extensions;

namespace BattleshipShared.Models
{
    public class ShipModel
    {
        public int Size => _locationPositions.Count;

        public List<string> LocationPositions => _locationPositions;

        private int _size;

        private List<string> _locationPositions;

        public ShipModel(int size)
        {
            _size = size;
        }

        public void AddLocation(int startHorizontalPositionIndex, int startVerticalLocationIndex,
            LocationType locationType, int maxBoardSize)
        {
            switch (locationType)
            {
                case LocationType.HorizontalLeft:

                    if (startHorizontalPositionIndex < _size)
                    {
                        startHorizontalPositionIndex = _size;
                    }

                    break;

                case LocationType.HorizontalRight:

                    if ((startHorizontalPositionIndex + _size) > maxBoardSize)
                    {
                        startHorizontalPositionIndex = maxBoardSize - _size;
                    }

                    break;

                case LocationType.VerticalToTop:

                    if (startVerticalLocationIndex < _size)
                    {
                        startVerticalLocationIndex = _size;
                    }

                    break;

                case LocationType.VerticalToBottom:

                    if ((startVerticalLocationIndex + _size) > maxBoardSize)
                    {
                        startVerticalLocationIndex = maxBoardSize - _size;
                    }

                    break;
            }

            switch (locationType)
            {
                case LocationType.VerticalToTop:
                case LocationType.VerticalToBottom:

                    var horizontalIndexName = startHorizontalPositionIndex.GetHorizontalIndexName();

                    for (var verticalIndex = 0; verticalIndex < _size; verticalIndex++)
                    {
                        var locationPostionIndex = startVerticalLocationIndex + verticalIndex;

                        _locationPositions.Add($"{horizontalIndexName}{locationPostionIndex}");
                    }
                    break;

                case LocationType.HorizontalRight:
                case LocationType.HorizontalLeft:

                    break;
            }
        }
    }
}
