using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TomNextPortfolioAssets.EF.Services;

namespace TomNextPortfolioAssets.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssestsController : ControllerBase
    {
        private readonly IAssetsService _assetsService;
        private readonly ILogger<AssestsController> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="assetsService"></param>
        /// <param name="logger"></param>
        public AssestsController(IAssetsService assetsService, ILogger<AssestsController> logger)
        {
            _assetsService = assetsService;
            _logger = logger;
        }

        /// <summary>
        /// Get all assets.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAssets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAssets()
        {
            try
            {
                _logger.LogInformation("Call GetAllAssets");
                var AllAssests = await _assetsService.GetAllAsync();
                _logger.LogInformation("Sucessfully get all assests");
                return Ok(AllAssests);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Failed to get allassests data: " + ex.Message);
                return StatusCode(500, new
                {
                    error = "Failed to get allassests data",
                    message = ex.Message
                });
            }
        }
    }
}
