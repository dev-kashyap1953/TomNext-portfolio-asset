using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomNextPortfolioAssets.Domain.IRepositories;
using TomNextPortfolioAssets.Domain.Tables;

namespace TomNextPortfolioAssets.EF.Services
{   
    public class AssestsService: IAssetsService
    {

        private readonly IAssetsRepository _assetsRepository;
        private readonly ILogger<AssestsService> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assetsRepository"></param>
        /// <param name="logger"></param>
        public AssestsService(IAssetsRepository assetsRepository, ILogger<AssestsService> logger) 
        {
              _assetsRepository = assetsRepository;
              _logger = logger;
        }

        /// <summary>
        /// Get all assests.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Assets>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Call method GetAllAsync in repository");
                 var Allassests = await _assetsRepository.GetAllAsync();
                _logger.LogInformation("Successfully get all assests in repository");
                return Allassests;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error in GetAllAsync in repository: " + ex.Message);
                throw;
            }
        }
    }
}
