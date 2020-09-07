using System.Collections.Generic;
using System.Linq;
using BattleshipShared.Extensions;
using BattleshipShared.Models;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.ShipCollection;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BattleshipSharedTests.Services.Board
{
    [TestFixture]
    public class BoardServiceTests
    {
        private readonly Mock<IShipCollectionService> _shipCollectionService;

        private BoardService _boardService;

        private ShipModel ShipModelTestData
        {
            get
            {
                var battleshipShipModel = new ShipModel(5);
                
                for (var verticalIndex = 2; verticalIndex < 6; verticalIndex++)
                {
                    var indexName = verticalIndex.GetHorizontalIndexName();
                    battleshipShipModel.AddLocation($"A{verticalIndex}");
                }

                return battleshipShipModel;
            }
        }

        public BoardServiceTests()
        {
            _shipCollectionService = new Mock<IShipCollectionService>();
        }

        [SetUp]
        public void SetUp()
        {
            _boardService = new BoardService(_shipCollectionService.Object);
            _shipCollectionService.Setup(mock => mock.GetShipModelByLocationId(It.IsAny<string>()))
                .Returns((string locationId) => ShipModelTestData.LocationIds.Any(l => l == locationId) ? ShipModelTestData : null);
            _boardService.GenerateInitialBoard(new int[] { 5 }, 10);
        }

        [Test]
        public void GenerateInitialBoard_WhenShipWasGenerated_ThenCheckIfAllBoardIsReferenceOnBoardGame()
        {
            foreach (var locationId in ShipModelTestData.LocationIds)
            {
                var boardField = GetBoardFieldModelByLocationId(locationId);
                boardField.IsNotEmpty.Should().BeTrue();
            }
        }

        [TestCase("A1")]
        [TestCase("B1")]
        [TestCase("B2")]
        [TestCase("B3")]
        [TestCase("B4")]
        [TestCase("B5")]
        [TestCase("B6")]
        [TestCase("A7")]
        [TestCase("B7")]
        public void GenerateInitialBoard_WhenShipWasGenerated_ThenCheckRandomFieldHaveEmptyFlag(string locationId)
        {
            CheckIfBoardFieldIsEmpty(locationId);
        }

        [Test]
        public void CheckLocationId_WhenUserTypeNotEmptyFieldLocationId_ThenReturnTrueAndSetFieldAsHit()
        {
            CheckIfCheckLocationIdIsWorkingGood(ShipModelTestData.LocationIds.First(), true);
        }

        [Test]
        public void CheckLocationId_WhenUserTypeEmptyFieldLocationId_ThenReturnFalseAndSetFieldAsHit()
        {
            CheckIfCheckLocationIdIsWorkingGood("A1", false);
        }

        [Test]
        public void CheckLocationId_WhenUserTypeInvalidLocationId_ThenReturnFalseAndSetFieldAsHit()
        {
            var result = _boardService.CheckLocationId("bad_location_id");
            result.Should().BeFalse();
        }

        private void CheckIfCheckLocationIdIsWorkingGood(string locationId, bool exceptedResult)
        {
            var result = _boardService.CheckLocationId(locationId);

            result.Should().Be(exceptedResult);
            _boardService.BoardFields[locationId].WasHit.Should().BeTrue();
        }

        private BoardFieldModel GetBoardFieldModelByLocationId(string locationId)
        {
            return _boardService.BoardFields.Single(b => b.Key == locationId).Value;
        }

        private void CheckIfBoardFieldIsEmpty(string locationId)
        {
            var boardField = GetBoardFieldModelByLocationId(locationId);
            boardField.IsNotEmpty.Should().BeFalse();
        }
    }
}
