using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.Domain.Tables
{
    public class AssetNewsMatch
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Assstsid.
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Assets.
        /// </summary>
        public Assets Asset { get; set; }

        /// <summary>
        /// Newsarticalid.
        /// </summary>
        public Guid NewsArticleId { get; set; }

        /// <summary>
        /// Newsarticle
        /// </summary>
        public NewsArticle NewsArticle { get; set; }

        /// <summary>
        /// Relevancescore.
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
