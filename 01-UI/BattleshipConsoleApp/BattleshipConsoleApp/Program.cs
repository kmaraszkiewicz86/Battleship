using System;
using BattleshipShared.Adapters.OutputLogger;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.BoardPrinter;
using BattleshipShared.Services.Ship;
using BattleshipShared.Services.ShipCollection;

namespace BattleshipConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IOutputLoggerAdapter outputLoggerAdapter = new OutputLoggerAdapter();

            IShipService shipService = new ShipService();
            IShipCollectionService shipCollectionService = new ShipCollectionService(shipService);

            IBoardService boardService = new BoardService(shipCollectionService);
            boardService.GenerateInitialBoard(new int[] { 2 }, 10);

            IBoardPrinterService boardServicePrinter = new BoardPrinterService(
                boardService, shipCollectionService, outputLoggerAdapter);

            while (true)
            {
                boardServicePrinter.ShowBoard();
                boardServicePrinter.ShowForm();

                if (shipCollectionService.IsFinish)
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