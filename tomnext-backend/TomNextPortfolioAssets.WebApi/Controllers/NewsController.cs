using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomNextPortfolioAssets.EF;
using TomNextPortfolioAssets.EF.Services;
using TomNextPortfolioAssets.WebApi.Models.ViewModels;

namespace TomNextPortfolioAssets.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly TomNextPortfolioAssetsDbContext _tomNextPortfolioAssetsDbContext;
        private readonly IAssetsService _assetsService;
        private readonly ILogger<NewsController> _logger;
        private const string InitialSearch = "business";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tomNextPortfolioAssetsDbContext"></param>
        /// <param name="assetsService"></param>
        public NewsController(TomNextPortfolioAssetsDbContext tomNextPortfolioAssetsDbContext, IAssetsService assetsService, ILogger<NewsController> logger)
        {
            _tomNextPortfolioAssetsDbContext = tomNextPortfolioAssetsDbContext;
            _assetsService = assetsService;
            _logger = logger;
        }

        /// <summary>
        /// Get news from rss.
        /// </summary>
        /// <param name="newsFeedByFiltered"></param>
        /// <returns></returns>
        [HttpPost("GetNewsFromRss")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewsFromRss([FromBody] NewsFeedByFiltered newsFeedByFiltered)
        {
            try
            {
                _logger.LogInformation("Call endpoint GetNewsFromRss to get all news from rss");
                var baseUrl = $"https://news.google.com/rss/search?q={Uri.EscapeDataString($"\"{InitialSearch}\"")}";

                var queryParts = new List<string>();
                var parameters = new List<string>();

                if (!string.IsNullOrWhiteSpace(newsFeedByFiltered?.Assets))
                    queryParts.Add(newsFeedByFiltered.Assets);

                if (!string.IsNullOrWhiteSpace(newsFeedByFiltered?.Sectors))
                    queryParts.Add(newsFeedByFiltered.Sectors);

                if (queryParts.Count > 0)
                {
                    var q = Uri.EscapeDataString(string.Join(" ", queryParts));
                    parameters.Add($"q={q}");
                }

                if (!string.IsNullOrWhiteSpace(newsFeedByFiltered?.Geo))
                    parameters.Add($"gl={Uri.EscapeDataString(newsFeedByFiltered.Geo)}");

                var rssUrl = parameters.Count > 0
                    ? $"{baseUrl}/search?{string.Join("&", parameters)}"
                    : baseUrl;

                var feed = await FeedReader.ReadAsync(rssUrl);

                var articles = feed.Items
                    .Take(10)
                    .Select(item => new
                    {
                        title = item.Title,
                        description = item.Description,
                        link = item.Link,
                        publishedAt = item.PublishingDate?.ToString("yyyy-MM-dd HH:mm"),
                        source = ((MediaRssFeedItem)(item.SpecificItem)).Source.Value,
                        sourceUrl = ((MediaRssFeedItem)(item.SpecificItem)).Source.Url,

                    })
                    .ToList();

                _logger.LogInformation("Successfully get all news from rss");

                return Ok(new
                {
                    total = articles.Count,
                    articles,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch RSS news: " + ex.Message);
                return StatusCode(500, new
                {
                    error = "Failed to fetch RSS news",
                    message = ex.Message
                });
            }
        }
        /// <summary>
        /// Set the read basd on id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/read")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Read(Guid id)
        {
            try
            {
                _logger.LogInformation("Call read endpoint to set read data for news");
                var state = await _tomNextPortfolioAssetsDbContext.UserNewsStates.FindAsync(id);
                if (state == null)
                {
                    _logger.LogInformation($"Unable to find {id} for read");
                    return NotFound();
                }

                state.IsRead = true;
                _tomNextPortfolioAssetsDbContext.UserNewsStates.Update(state);
                await _tomNextPortfolioAssetsDbContext.SaveChangesAsync();
                _logger.LogInformation("Successfully set the read for news");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to set read for news");
                return StatusCode(500, new
                {
                    error = "Failed to set read for news",
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Pin the news based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Pin(Guid id)
        {
            try
            {
                _logger.LogInformation("Call pin endpoint to set pin for news");
                var state = await _tomNextPortfolioAssetsDbContext.UserNewsStates.FindAsync(id);
                if (state == null)
                {
                    _logger.LogError($"Unable to find {id} for pin");
                    return NotFound();
                }
                state.IsPinned = true;
                _tomNextPortfolioAssetsDbContext.UserNewsStates.Update(state);
                await _tomNextPortfolioAssetsDbContext.SaveChangesAsync();
                _logger.LogInformation("Successfully set the pin for news");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to set pin for news");
                return StatusCode(500, new
                {
                    error = "Failed to set pin for news",
                    message = ex.Message
                }); ;
            }
        }

        /// <summary>
        /// Get topindicators data.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTopIndicators")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopIndicators()
        {
            try
            {
                _logger.LogInformation("Call GetTopIndicators method");
                _logger.LogInformation("Call assests for get all assests");
                var AllAssets = await _assetsService.GetAllAsync();
                _logger.LogInformation("Successfully get all assests data");

                //const string BaseUrl = "https://news.google.com/rss/search?q=business when:7d";
                var BaseUrl = $"https://news.google.com/rss/search?q={Uri.EscapeDataString($"\"{InitialSearch}\"")} when:7d";


                _logger.LogInformation("Call rss URL to get all news");
                var feed = await FeedReader.ReadAsync(BaseUrl);

                var articles = feed.Items
                    .Select(item => new
                    {
                        title = item.Title,
                        description = item.Description, // fixed
                        link = item.Link,
                        publishedAt = item.PublishingDate?.ToString("yyyy-MM-dd HH:mm")
                    })
                    .ToList();
                _logger.LogInformation("Sucessfully get all news from rss");

                int highRelevanceCount = 0;

                foreach (var article in articles)
                {
                    int score = 0;
                    var text = $"{article.title} {article.description}".ToLower();

                    foreach (var asset in AllAssets)
                    {
                        if (text.Contains(asset.FundName.ToLower()))
                            score += 2;

                        foreach (var sector in asset.Sectors)
                            if (text.Contains(sector.ToLower()))
                                score += 1;

                        foreach (var keyword in asset.Keywords)
                            if (text.Contains(keyword.ToLower()))
                                score += 1;
                    }

                    if (score >= 2)
                        highRelevanceCount++;
                }

                int assetsWithNews = 0;

                foreach (var asset in AllAssets)
                {
                    bool hasNews = articles.Any(article =>
                    {
                        var text = $"{article.title} {article.description}".ToLower();
                        return text.Contains(asset.FundName.ToLower()) ||
                               asset.Sectors.Any(s => text.Contains(s.ToLower())) ||
                               asset.Keywords.Any(k => text.Contains(k.ToLower()));
                    });

                    if (hasNews)
                        assetsWithNews++;
                }

                var coverageCount = 0;

                if (assetsWithNews > 0 && AllAssets.Count > 0)
                {
                    coverageCount = assetsWithNews / AllAssets.Count * 100;
                }

                _logger.LogInformation("Sucessfully count highRelevanceCount, coverageCount, {highRelevanceCount}, {coverageCount}", highRelevanceCount, coverageCount);
                return Ok(new
                {
                    total = articles.Count,
                    highRelevanceCount,
                    topSecotrs = 0,
                    coverageCount,
                    articles
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to fetch RSS news: " + ex.Message);
                return StatusCode(500, new
                {
                    error = "Failed to fetch RSS news",
                    message = ex.Message
                });
            }
        }
    }
}
