using System;
using System.Collections.Generic;
using System.Text;
using BattleshipShared.Exceptions;
using BattleshipShared.Models;

namespace BattleshipShared.Services.ShipLocationsGenerator
{
    public class ShipLocationsGeneratorService
    {
        private int _gameBoardSize;

        private Dictionary<string, FieldModel> _shipLocations;

        private List<ShipModel> _shipModels;

        private char[] horizontalIndexNames;

        public ShipLocationsGeneratorService()
        {
            horizontalIndexNames = "ABCDEFGHIJ".ToCharArray();
        }

        public void GenerateInitialBoard(int size, List<ShipModel> shipModels)
        {
            _shipModels = shipModels;

            if (shipModels == null || shipModels.Count == 0)
            {
                throw new BadRequestException("Invalid ships data to generate");
            }

            for (var horizontalIndex = 0; horizontalIndex < size; horizontalIndex++)
            {
                for (var verticalIndex = 1; verticalIndex <= size; verticalIndex++)
                {
                    _shipLocations.Add($"{horizontalIndexNames[horizontalIndex]}{verticalIndex}",
                        new FieldModel());
                }
            }
        }
    }
}
