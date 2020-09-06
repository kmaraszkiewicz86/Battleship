using System;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.BoardPrinter;
using BattleshipShared.Services.ShipCollection;

namespace BattleshipShared.Services.BattleshipGame
{
    /// <summary>
    /// The main facade that configure all services, generate required data and generate ui
    /// </summary>
    public class BattleshipGameService : IBattleshipGameService
    {
        /// <summary>
        /// <see cref="IShipCollectionService"/>
        /// </summary>
        private readonly IShipCollectionService _shipCollectionService;

        /// <summary>
        /// <see cref="IBoardService"/>
        /// </summary>
        private readonly IBoardService _boardService;

        /// <summary>
        /// <see cref="IBoardPrinterService"/>
        /// </summary>
        private readonly IBoardPrinterService _boardServicePrinter;

        public BattleshipGameService(IShipCollectionService shipCollectionService, IBoardService boardService,
            IBoardPrinterService boardServicePrinter)
        {
            _shipCollectionService = shipCollectionService;
            _boardService = boardService;
            _boardServicePrinter = boardServicePrinter;
        }

        /// <summary>
        /// Initialize data for the board and ships that will be put on the board. 
        /// </summary>
        /// <param name="squareSizes">List of squares size for ships that will be generated</param>
        /// <param name="boardSize">The board game size</param>
        public void GenerateDataForGame(int[] squareSizes, int boardSize)
        {
            _boardService.GenerateInitialBoard(squareSizes, boardSize);
        }

        /// <summary>
        /// Generate ui for user with game board and simply form for choose the field id
        /// </summary>
        public void StartTheGame()
        {
            if (_boardService.BoardFields.Count == 0)
            {
                throw new Exception("The board service is not initialized...");
            }

            while (true)
            {
                _boardServicePrinter.ShowBoard();

                if (_shipCollectionService.IsFinish)
                {
                    _boardServicePrinter.ShowEndingResult();
                    break;
                }

                _boardServicePrinter.ShowForm();
            }

            Console.WriteLine("Press any key to close application");
            Console.ReadKey();
        }

    }
}