using System;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.BoardPrinter;
using BattleshipShared.Services.Ship;
using BattleshipShared.Services.ShipCollection;
using BattleshipShared.Stubs.ConsoleImpl;

namespace BattleshipConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConsoleStub consoleStub = new ConsoleStub();

            IShipService shipService = new ShipService();
            IShipCollectionService shipCollectionService = new ShipCollectionService(shipService);

            IBoardService boardService = new BoardService(shipCollectionService);
            boardService.GenerateInitialBoard(new int[] { 2 }, 10);

            IBoardPrinterService boardServicePrinter = new BoardPrinterService(boardService, consoleStub);

            while (true)
            {
                boardServicePrinter.ShowBoard();
                boardServicePrinter.ShowForm();

                if (boardServicePrinter.IsFinish)
                {
                    boardServicePrinter.ShowEndingResult();
                    break;
                }
            }

            Console.WriteLine("Press any key to close application");
            Console.ReadKey();
        }
    }
}