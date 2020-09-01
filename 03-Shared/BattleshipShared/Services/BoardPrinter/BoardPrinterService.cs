using System;
using System.Linq;
using BattleshipShared.Enums;
using BattleshipShared.Extensions;
using BattleshipShared.Models;
using BattleshipShared.Services.Board;
using BattleshipShared.Stubs.ConsoleImpl;

namespace BattleshipShared.Services.BoardPrinter
{
    public class BoardPrinterService : IBoardPrinterService
    {
        public bool IsFinish => _boardService.RemainingShipModels.Count() == 0;

        private IBoardService _boardService;

        private IConsoleStub _consoleStub;

        public BoardPrinterService(IBoardService boardService, IConsoleStub consoleStub)
        {
            _boardService = boardService;
            _consoleStub = consoleStub;
        }

        public void ShowBoard()
        {
            Console.Clear();

            PrintShipHeader();
            PrintBoardHeader();
            PrintBoard();
        }

        public void ShowForm()
        {
            _consoleStub.SetConsoleColors(ConsoleColorsType.Default);

            _consoleStub.Write("Type location: ");
            var lineString = _consoleStub.ReadLine();

            if (string.IsNullOrWhiteSpace(lineString))
            {
                _consoleStub.WriteLine("The location id is required");
                return;
            }

            _boardService.CheckLocationId(lineString.Trim());
        }

        public void ShowEndingResult()
        {
            _consoleStub.WriteLine($"The game finished after {_boardService.NumberOfHits} hits");
        }

        private void PrintShipHeader()
        {
            _consoleStub.WriteLine("Remaining ships: ");
            foreach (var shipModel in _boardService.RemainingShipModels)
            {
                _consoleStub.WriteLine(shipModel.ToString());
            }
        }

        private void PrintBoardHeader()
        {
            _consoleStub.Write("   ");
            for (var horizontalIndex = 0; horizontalIndex < 10; horizontalIndex++)
            {
                _consoleStub.Write($"{horizontalIndex.GetHorizontalIndexName()} ");
            }
        }

        private void PrintBoard()
        {
            _consoleStub.WriteLine();

            for (var verticalIndex = 1; verticalIndex <= 10; verticalIndex++)
            {
                _consoleStub.SetConsoleColors(ConsoleColorsType.Default);

                PrintVerticalIndex(verticalIndex);

                for (var horizontalIndex = 0; horizontalIndex < 10; horizontalIndex++)
                {
                    var locationId = $"{horizontalIndex.GetHorizontalIndexName()}{verticalIndex}";
                    var boardField = _boardService.BoardFields[locationId];

                    var character = GetBoardFieldCharacter(boardField);

                    _consoleStub.SetConsoleColors(boardField.WasHit);

                    _consoleStub.Write($"{character} ");
                }

                _consoleStub.WriteLine();
            }
        }

        private char GetBoardFieldCharacter(FieldModel boardField)
        {
            var character = '?';

            if (boardField.WasHit)
            {
                character = (boardField.IsNotEmpty) ? 'o' : 'x';
            }

            return character;
        }

        private void PrintVerticalIndex(int verticalIndex)
        {
            if (verticalIndex == 10)
            {
                _consoleStub.Write($"{verticalIndex} ");
            }
            else
            {
                _consoleStub.Write($"{verticalIndex}  ");
            }
        }
    }
}
