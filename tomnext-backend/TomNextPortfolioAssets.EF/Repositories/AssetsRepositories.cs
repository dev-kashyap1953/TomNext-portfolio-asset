using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomNextPortfolioAssets.Domain.IRepositories;
using TomNextPortfolioAssets.Domain.Tables;

namespace TomNextPortfolioAssets.EF.Repositories
{
    public class AssetsRepositories: IAssetsRepository
    {
        private readonly TomNextPortfolioAssetsDbContext _tomNextPortfolioAssetsDbContext;
        private readonly ILogger<AssetsRepositories> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tomNextPortfolioAssetsDbContext"></param>
        /// <param name="logger"></param>
        public AssetsRepositories(TomNextPortfolioAssetsDbContext tomNextPortfolioAssetsDbContext, ILogger<AssetsRepositories> logger) 
        {
            _tomNextPortfolioAssetsDbContext = tomNextPortfolioAssetsDbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get all assets from Db.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Assets>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Call method GetAllAsync in assest service");
                var AllAssests = await _tomNextPortfolioAssetsDbContext.Assets.ToListAsync();
                _logger.LogInformation("Sucessfully get all assests from database");
                return AllAssests;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in method GetAllAsync in service: "+ ex.Message);
                throw;
            }
        }
    }
}
