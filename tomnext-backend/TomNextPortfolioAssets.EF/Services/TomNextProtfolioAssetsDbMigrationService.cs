using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace TomNextPortfolioAssets.EF.Services
{
    /// <summary>
    /// Database migration service.
    /// </summary>
    public class TomNextPortfolioAssetsDbMigrationService : IDbMigrationService
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly TomNextPortfolioAssetsDbContext _context;

        /// <summary>
        /// Creates service.
        /// </summary>
        /// <param name="configuration">Configuration root.</param>
        /// <param name="context">Database context.</param>
        public TomNextPortfolioAssetsDbMigrationService(
            IConfiguration configuration,
            TomNextPortfolioAssetsDbContext context)
        {
            _configurationRoot = (IConfigurationRoot)configuration;
            _context = context;
        }

        /// <summary>
        /// Migrates database.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();

            _configurationRoot.Reload();
        }
    }
}
