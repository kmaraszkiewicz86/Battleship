using Microsoft.Extensions.Configuration;

namespace BattleshipShared.Settings
{
    /// <summary>
    /// Helper for fetch data from configuration json file using <see cref="IConfiguration"/> class
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Get is application under debug mode
        /// </summary>
        public static bool IsDebugMode
        {
            get
            {
                if (bool.TryParse(Configuration["debugMode"], out var isDebugMode))
                {
                    return isDebugMode;
                }

                return false;
            }
        }

        /// <summary>
        /// Get <see cref="IConfiguration"/> for fetch settings from configuration json file
        /// </summary>
        private static IConfiguration Configuration
        {
            get
            {
                return new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }
    }
}