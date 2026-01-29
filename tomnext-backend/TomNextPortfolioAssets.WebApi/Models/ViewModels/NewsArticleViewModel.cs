using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.WebApi.Models.ViewModels
{
    /// <summary>
    /// News article view model
    /// </summary>
    public class NewsArticleViewModel
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
        /// Snippet.
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
        /// Rawcount.
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// Created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Matched assests news.
        /// </summary>
        public ICollection<AssetNewsMatchViewModel> Matches { get; set; }
    }
}
