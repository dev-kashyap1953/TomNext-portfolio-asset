using System.Collections.Generic;
using System.Threading.Tasks;
using TomNextPortfolioAssets.Domain.Tables;

namespace TomNextPortfolioAssets.Domain.IRepositories
{
    public interface IAssetsRepository
    {
        /// <summary>
        /// Get all assetes data.
        /// </summary>
        /// <returns></returns>
        Task<List<Assets>> GetAllAsync();
    }
}
