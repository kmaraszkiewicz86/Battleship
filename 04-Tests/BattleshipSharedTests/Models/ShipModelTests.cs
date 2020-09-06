using BattleshipShared.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipSharedTests.Models
{
    [TestFixture]
    public class ShipModelTests
    {
        [Test]
        public void IsDestroyed_WhenShipHasSizeGreatherThenZero_ThenReturnFalse()
        {
            CheckIfIsDestroyedStateIsValid(1, false);
        }

        [Test]
        public void IsDestroyed_WhenShipHasSizeEqualsZero_ThenReturnTrue()
        {
            CheckIfIsDestroyedStateIsValid(3, true);
        }

        [TestCase(4, "Destroyer")]
        [TestCase(5, "Battleship")]
        public void ToString_WhenShipSizeHasXSize_ThenReturnValidInoformation(int size, string expectedName)
        {
            var shipModel = new ShipModel(size);

            var expectedShipInformation = $"{expectedName}: Remain square to hit is {size}";

            shipModel.ToString().Should().Be(expectedShipInformation);
        }

        private void CheckIfIsDestroyedStateIsValid(int reduceTimeAmount, bool isTrue)
        {
            var shipModel = new ShipModel(2);

            for (var index = 0; index < reduceTimeAmount; index++)
            {
                shipModel.ReduceSquareSize();
            }

            shipModel.IsDestroyed.Should().Be(isTrue);
        }
    }
}