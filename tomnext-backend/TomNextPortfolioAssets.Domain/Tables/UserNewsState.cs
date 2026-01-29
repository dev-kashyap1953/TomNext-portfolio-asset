using System;

namespace TomNextPortfolioAssets.Domain.Tables
{
    public class UserNewsState
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// NewsArticleId.
        /// </summary>
        public Guid NewsArticleId { get; set; }

        /// <summary>
        /// Isread.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Ispinned.
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Updated date.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
