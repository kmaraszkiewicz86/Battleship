namespace BattleshipShared.Services.BattleshipGame
{
    /// <summary>
    /// The main facade that configure all services, generate required data and generate ui
    /// </summary>
    public interface IBattleshipGameService
    {
        /// <summary>
        /// Initialize data for the board and ships that will be put on the board. 
        /// </summary>
        /// <param name="squareSizes">List of squares size for ships that will be generated</param>
        /// <param name="boardSize">The board game size</param>
        void GenerateDataForGame(int[] squareSizes, int boardSize);

        /// <summary>
        /// Generate ui for user with game board and simply form for choose the field id
        /// </summary>
        void StartTheGame();
    }
}