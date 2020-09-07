using BattleshipShared.Helpers;
using BattleshipShared.Services.BattleshipGame;

namespace BattleshipConsoleApp
{
    class Program
    {
        /// <summary>
        /// Main method to initialize and run game
        /// To activate cheat that shows ships location ids that above of game board
        /// Change debugMode setting to true in appsettings.json file
        /// </summary>
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