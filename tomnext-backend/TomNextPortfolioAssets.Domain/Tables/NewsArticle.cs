using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.Domain.Tables
{
    public class NewsArticle
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Published date.
        /// </summary>
        public DateTime PublishedAt { get; set; }

        /// <summary>
        /// Sinppedt.
        /// </summary>
        public string Snippet { get; set; }

        /// <summary>
        /// Sector.
        /// </summary>
        public string Sector { get; set; }

        /// <summary>
        /// Geo.
        /// </summary>
        public string Geo { get; set; }

        /// <summary>
        /// RawContent.
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Matches assets.
        /// </summary>
        public ICollection<AssetNewsMatch> Matches { get; set; }
    }
}
