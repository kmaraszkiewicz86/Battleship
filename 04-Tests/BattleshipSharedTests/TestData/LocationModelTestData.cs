using System.Collections.Generic;
using BattleshipShared.Enums;
using BattleshipShared.Models;

namespace BattleshipSharedTests.TestData
{
    public static class LocationModelTestData
    {
        public static List<LocationModel> LocationModels
        {
            get
            {
                return new List<LocationModel>
                {
                    new LocationModel
                    {
                        BoardSize = 10,
                        LocationType = LocationType.VerticalToBottom,
                        SquareSize = 5,
                        StartHorizontalPositionIndex = 1,
                        StartVerticalLocationIndex = 1,
                    },
                    new LocationModel
                    {
                        BoardSize = 10,
                        LocationType = LocationType.HorizontalToRight,
                        SquareSize = 5,
                        StartHorizontalPositionIndex = 0,
                        StartVerticalLocationIndex = 1,
                    }
                };
            }
        }
    }
}