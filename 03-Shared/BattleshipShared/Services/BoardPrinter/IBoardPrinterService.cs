namespace BattleshipShared.Services.BoardPrinter
{
    public interface IBoardPrinterService
    {
        bool IsFinish { get; }
        void ShowBoard();
        void ShowForm();
        void ShowEndingResult();
    }
}
