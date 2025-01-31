
using Catalog.API.Context;

namespace Catalog.API.HostingService
{
    public class AppHostedService : IHostedService
    {
        /// <summary>
        ///This method run once when application start and seed the database
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            CatalogDbContextSeed.Seed();
            return Task.CompletedTask;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
