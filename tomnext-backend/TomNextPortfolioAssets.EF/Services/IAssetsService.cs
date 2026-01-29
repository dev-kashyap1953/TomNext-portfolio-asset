using System.Collections.Generic;
using System.Threading.Tasks;
using TomNextPortfolioAssets.Domain.Tables;

namespace TomNextPortfolioAssets.EF.Services
{
    public interface IAssetsService
    {
        /// <summary>
        /// Get all assets data.
        /// </summary>
        /// <returns></returns>
        Task<List<Assets>> GetAllAsync();
    }
}
