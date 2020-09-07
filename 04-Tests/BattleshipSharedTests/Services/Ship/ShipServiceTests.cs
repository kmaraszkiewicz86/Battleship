using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Models;
using BattleshipShared.Services.Ship;
using FluentAssertions;
using NUnit.Framework;
using static BattleshipSharedTests.TestData.LocationModelTestData;

namespace BattleshipSharedTests.Services.Ship
{
    [TestFixture]
    public class ShipServiceTests
    {
        private ShipService _shipService;

        private List<string> _expectedLocationIds 
            => new List<string> { "B1", "B2", "B3", "B4", "B5" };

        [SetUp]
        public void SetUp()
        {
            _shipService = new ShipService();
        }

        [Test]
        public void TryToGenerateShipModel_WhenLocationModelNotEntersOnPreviousShipModel_ThenReturnTrue()
        {
            var shipModel = new ShipModel(LocationModels.First().SquareSize);
            bool result = _shipService.TryToGenerateShipModel(LocationModels.First(), ref shipModel);

            result.Should().BeTrue();
            shipModel.LocationIds.Should().BeEquivalentTo(_expectedLocationIds);
        }

        [Test]
        public void TryToGenerateShipModel_WhenSecondLocationModelEntersOnPreviousShipModel_ThenReturnFalse()
        {
            var shipModel = new ShipModel(LocationModels.First().SquareSize);
            _shipService.TryToGenerateShipModel(LocationModels.First(), ref shipModel);

            shipModel = new ShipModel(LocationModels.Last().SquareSize);
            var result = _shipService.TryToGenerateShipModel(LocationModels.Last(), ref shipModel);
            result.Should().BeFalse();
        }
    }
}