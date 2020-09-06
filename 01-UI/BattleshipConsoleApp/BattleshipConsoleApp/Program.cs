using BattleshipShared.Helpers;
using BattleshipShared.Services.BattleshipGame;

namespace BattleshipConsoleApp
{
    class Program
    {
        static void Main()
        {
            var injectionOfControlHelper = new InjectionOfControlHelper();
            injectionOfControlHelper.AddRequiredServices();

            IBattleshipGameService battleshipGameService = 
                injectionOfControlHelper.BattleshipGameService;

            battleshipGameService.GenerateDataForGame(new int[] { 5, 4, 4 }, 10);
            battleshipGameService.StartTheGame();
        }
    }
}