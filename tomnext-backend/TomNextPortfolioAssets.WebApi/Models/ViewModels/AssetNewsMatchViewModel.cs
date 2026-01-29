using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.WebApi.Models.ViewModels
{
    /// <summary>
    /// Assets news match view model
    /// </summary>
    public class AssetNewsMatchViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Assests id.
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Assets.
        /// </summary>
        public AssetsViewModel Asset { get; set; }


        /// <summary>
        /// New artiale id.
        /// </summary>
        public Guid NewsArticleId { get; set; }

        /// <summary>
        /// News article
        /// </summary>
        public NewsArticleViewModel NewsArticle { get; set; }

        /// <summary>
        /// Relevance
        /// </summary>
        public int RelevanceScore { get; set; }

        /// <summary>
        /// Matchfields.
        /// </summary>
        public List<string> MatchedFields { get; set; }

        /// <summary>
        /// Created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
