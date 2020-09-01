using System.Collections.Generic;
using System.Text;

namespace BattleshipShared.Models
{
    public class ShipModel
    {
        public bool IsDestroyed => CurrentSquaresSize == 0;

        public int SquaresSize => _size;

        public int CurrentSquaresSize { get; private set; }

        public List<string> LocationPositions { get; }

        public string Name
        {
            get
            {
                return _size == 5 ? "Battleship" : "Destroyer";
            }
        }

        private int _size;

        public ShipModel(int size)
        {
            _size = size;
            CurrentSquaresSize = _size;
            LocationPositions = new List<string>();
        }

        public void AddLocation(string locationId)
        {
            LocationPositions.Add(locationId);
        }

        public void ReduceSquareSize()
        {
            if (CurrentSquaresSize > 0)
                CurrentSquaresSize--;
        }

        public override string ToString()
        {
            var locationIdsStringBuilder = new StringBuilder();

            foreach (var locationId in LocationPositions)
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
