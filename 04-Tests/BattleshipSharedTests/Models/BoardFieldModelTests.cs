using BattleshipShared.Models;
using FluentAssertions;
using NUnit.Framework;

namespace BattleshipSharedTests.Models
{
    [TestFixture]
    public class BoardFieldModelTests
    {
        [Test]
        public void IsNotEmpty_WhenShipModelIsNull_ThenIsNotEmptyIsFalse()
        {
            CheckIfIsNotEmptyIsInCorrectState(null, false);
        }

        [Test]
        public void IsNotEmpty_WhenShipModelIsNull_ThenIsNotEmptyIsTrue()
        {
            CheckIfIsNotEmptyIsInCorrectState(new ShipModel(4), true);
        }

        private void CheckIfIsNotEmptyIsInCorrectState(ShipModel shipModel, bool isTrue)
        {
            BoardFieldModel boardFieldModel = new BoardFieldModel(shipModel);

            boardFieldModel.IsNotEmpty.Should().Be(isTrue);
        }
    }
}