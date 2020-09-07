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
                
                for (var horizontalIndex = 2; horizontalIndex < 7; horizontalIndex++)
                {
                    var indexName = horizontalIndex.GetHorizontalIndexName();
                    battleshipShipModel.AddLocation($"{indexName}1");
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
                .Returns(ShipModelTestData);
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

        [TestCase("G1")]
        [TestCase("E2")]
        [TestCase("F5")]
        [TestCase("G2")]
        [TestCase("G5")]
        public void GenerateInitialBoard_WhenShipWasGenerated_ThenCheckRandomFieldHaveEmptyFlag(string locationId)
        {
            CheckIfBoardFieldIsEmpty(locationId);
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
