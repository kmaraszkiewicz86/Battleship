using System;
using BattleshipShared.Adapters.OutputLogger;
using BattleshipShared.Services.BattleshipGame;
using BattleshipShared.Services.Board;
using BattleshipShared.Services.BoardPrinter;
using BattleshipShared.Services.Ship;
using BattleshipShared.Services.ShipCollection;
using Microsoft.Extensions.DependencyInjection;

namespace BattleshipShared.Helpers
{
    /// <summary>
    /// Helper for creating injection of control container
    /// </summary>
    public class InjectionOfControlHelper
    {
        /// <summary>
        /// Get the main project class <seealso cref="IBattleshipGameService"/>
        /// </summary>
        public IBattleshipGameService BattleshipGameService =>
            _serviceProvider.GetService<IBattleshipGameService>();

        /// <summary>
        /// <see cref="IServiceProvider"/>
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Add all required services for start working with services
        /// </summary>
        public void AddRequiredServices()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IOutputLoggerAdapter, OutputLoggerAdapter>()
                .AddSingleton<IShipService, ShipService>()
                .AddSingleton<IShipCollectionService, ShipCollectionService>()
                .AddScoped<IBoardService, BoardService>()
                .AddScoped<IBoardPrinterService, BoardPrinterService>()
                .AddScoped<IBattleshipGameService, BattleshipGameService>()
                .BuildServiceProvider();
        }
    }
}