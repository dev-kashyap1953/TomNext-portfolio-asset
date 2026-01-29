using System;

namespace TomNextPortfolioAssets.WebApi.Models.ViewModels
{
    /// <summary>
    /// User news state view model.
    /// </summary>
    public class UserNewsStateViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid NewsArticleId { get; set; }

        /// <summary>
        /// Is read or not.
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Is pin or not.
        /// </summary>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Updated date.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
