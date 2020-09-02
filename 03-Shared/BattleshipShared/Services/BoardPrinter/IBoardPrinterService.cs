namespace BattleshipShared.Services.BoardPrinter
{
    /// <summary>
    /// Service that print data for user ui
    /// </summary>
    public interface IBoardPrinterService
    {
        /// <summary>
        /// Generate board information for user
        /// </summary>
        void ShowBoard();

        /// <summary>
        /// Generate user form where user may type location id for game
        /// </summary>
        void ShowForm();

        /// <summary>
        /// Generate ending result after all ship will be destroyed
        /// </summary>
        void ShowEndingResult();
    }
}
