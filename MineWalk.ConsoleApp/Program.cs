using MineWalk.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MineWalk.ConsoleApp
{
    public class Program
    {
        /// <summary>
        /// Standard cli enrty point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            
            serviceProvider.GetService<Game>().Run();
        }

        /// <summary>
        /// Service configuration following typical pattern.
        /// </summary>
        /// <returns>Collection of configured services.</returns>
        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<Game>()
                    .AddTransient<GameBoardService>()
                    .AddTransient<ILogger, ConsoleLogger>();

            return services;
        }
    }
}
