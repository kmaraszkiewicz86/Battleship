using System;
using System.Linq;
using BattleshipShared.Adapters.OutputLogger;
using BattleshipShared.Enums;
using BattleshipShared.Extensions;
using BattleshipShared.Models;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.ShipCollection;

namespace BattleshipShared.Services.BoardPrinter
{
    /// <summary>
    /// Service that print data for user ui
    /// </summary>
    public class BoardPrinterService : IBoardPrinterService
    {
        /// <summary>
        /// <see cref="IBoardService"/>
        /// </summary>
        private IBoardService _boardService;

        /// <summary>
        /// <see cref="IShipCollectionService"/>
        /// </summary>
        private IShipCollectionService _shipCollectionService;

        /// <summary>
        /// <see cref="IOutputLoggerAdapter"/>
        /// </summary>
        private IOutputLoggerAdapter _outputLoggerAdapter;

        public BoardPrinterService(IBoardService boardService,
            IShipCollectionService shipCollectionService, IOutputLoggerAdapter outputLoggerAdapter)
        {
            _boardService = boardService;
            _shipCollectionService = shipCollectionService;
            _outputLoggerAdapter = outputLoggerAdapter;
        }

        /// <summary>
        /// Generate board information for user
        /// </summary>
        public void ShowBoard()
        {
            Console.Clear();

            PrintRemainingShipsInformationHeader();
            PrintHorizontalIndexesNames();
            PrintBoard();
        }

        /// <summary>
        /// Generate user form where user may type location id for game
        /// </summary>
        public void ShowForm()
        {
            _outputLoggerAdapter.SetConsoleColors(ConsoleColorsType.Default);

            _outputLoggerAdapter.WriteLine("========================================================");
            _outputLoggerAdapter.Write("Type location eg. (A1) or (a1): ");
            var lineString = _outputLoggerAdapter.ReadLine();

            if (string.IsNullOrWhiteSpace(lineString))
            {
                _outputLoggerAdapter.WriteLine("The location id is required");
                return;
            }

            _boardService.CheckLocationId(lineString.Trim());
        }

        /// <summary>
        /// Generate ending result after all ship will be destroyed
        /// </summary>
        public void ShowEndingResult()
        {
            _outputLoggerAdapter.WriteLine("========================================================");
            _outputLoggerAdapter.WriteLine("======================GAME IS OFER======================");
            _outputLoggerAdapter.WriteLine("========================================================");
            _outputLoggerAdapter.WriteLine($"The game finished after {_boardService.NumberOfHits} hits");
            _outputLoggerAdapter.WriteLine("========================================================");
        }

        /// <summary>
        /// Print ship that was not destroyed before board for user ui
        /// </summary>
        private void PrintRemainingShipsInformationHeader()
        {
            _outputLoggerAdapter.WriteLine("Remaining ships: ");
            _outputLoggerAdapter.WriteLine("========================================================");

            if (_shipCollectionService.RemainingShips.Count() == 0)
            {
                _outputLoggerAdapter.WriteLine("All ships was destroyed");
            }
            else
            {
                foreach (var shipModel in _shipCollectionService.RemainingShips)
                {
                    _outputLoggerAdapter.WriteLine(shipModel.ToString());
                }
            }

            _outputLoggerAdapter.WriteLine("========================================================");
            _outputLoggerAdapter.WriteLine();
        }

        /// <summary>
        /// Print horizontal names that was generate above of game board
        /// </summary>
        private void PrintHorizontalIndexesNames()
        {
            _outputLoggerAdapter.Write("   ");
            for (var horizontalIndex = 0; horizontalIndex < 10; horizontalIndex++)
            {
                _outputLoggerAdapter.Write($"{horizontalIndex.GetHorizontalIndexName()} ");
            }

            _outputLoggerAdapter.WriteLine();
        }

        /// <summary>
        /// Print board that show which board fields was hit
        /// </summary>
        private void PrintBoard()
        {
            for (var verticalIndex = 1; verticalIndex <= 10; verticalIndex++)
            {
                _outputLoggerAdapter.SetConsoleColors(ConsoleColorsType.Default);

                PrintVerticalIndex(verticalIndex);

                for (var horizontalIndex = 0; horizontalIndex < 10; horizontalIndex++)
                {
                    var locationId = $"{horizontalIndex.GetHorizontalIndexName()}{verticalIndex}";
                    var boardField = _boardService.BoardFields[locationId];

                    var character = GetBoardFieldCharacter(boardField);

                    _outputLoggerAdapter.SetConsoleColors(boardField.WasHit);

                    _outputLoggerAdapter.Write($"{character} ");
                }

                _outputLoggerAdapter.WriteLine();
            }
        }

        /// <summary>
        /// Get character for each board fields based by information if user hits that field
        /// and if on the field was one of ship squares that was hit by user
        /// </summary>
        /// <param name="boardField"><see cref="BoardFieldModel"/></param>
        /// <returns></returns>
        private char GetBoardFieldCharacter(BoardFieldModel boardField)
        {
            var character = '?';

            if (boardField.WasHit)
            {
                character = (boardField.IsNotEmpty) ? 'o' : 'x';
            }

            return character;
        }

        /// <summary>
        /// Print verticaly index is generated
        /// left of the game board
        /// </summary>
        /// <param name="verticalIndex"></param>
        private void PrintVerticalIndex(int verticalIndex)
        {
            if (verticalIndex == 10)
            {
                _outputLoggerAdapter.Write($"{verticalIndex} ");
            }
            else
            {
                _outputLoggerAdapter.Write($"{verticalIndex}  ");
            }
        }
    }
}