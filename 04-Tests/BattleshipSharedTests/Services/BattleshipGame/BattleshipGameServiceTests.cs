using System;
using System.Collections.Generic;
using BattleshipShared.Exceptions;
using BattleshipShared.Models;
using BattleshipShared.Services.BattleshipGame;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.BoardPrinter;
using BattleshipShared.Services.ShipCollection;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace BattleshipSharedTests.Services.BattleshipGame
{
    [TestFixture]
    public class BattleshipGameServiceTests
    {
        private readonly Mock<IBoardService> _boardServiceMock;

        private readonly BattleshipGameService _battleshipGameService;

        public BattleshipGameServiceTests()
        {
            var shipCollectionService = new Mock<IShipCollectionService>();
            var boardServicePrinter = new Mock<IBoardPrinterService>();

            _boardServiceMock = new Mock<IBoardService>();

            _battleshipGameService = new BattleshipGameService(shipCollectionService.Object,
                _boardServiceMock.Object,
                boardServicePrinter.Object);
        }

        [Test]
        public void StartTheGame_WhenBoardFieldsIsEmpty_ThenThrowException()
        {
            var expectedBoardFields = new Dictionary<string, BoardFieldModel>
            {
            };

            _boardServiceMock.Setup(mock => mock.BoardFields).Returns(expectedBoardFields);

            Action action = () => _battleshipGameService.StartTheGame();

            action.Should().Throw<BadRequestException>("The board service is not initialized...");
        }
    }
}