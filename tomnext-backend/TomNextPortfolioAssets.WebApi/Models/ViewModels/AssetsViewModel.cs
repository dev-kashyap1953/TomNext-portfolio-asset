using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.WebApi.Models.ViewModels
{
    /// <summary>
    /// Assests view model
    /// </summary>
    public class AssetsViewModel
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Fund name.
        /// </summary>
        public string FundName { get; set; }

        /// <summary>
        /// Manager name.
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// Assest class.
        /// </summary>
        public string AssetClass { get; set; }

        /// <summary>
        /// Geography.
        /// </summary>
        public string Geography { get; set; }

        /// <summary>
        /// Sectors.
        /// </summary>
        public List<string> Sectors { get; set; }

        /// <summary>
        /// Keywords.
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// Created date.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Matches assets.
        /// </summary>
        public List<AssetNewsMatchViewModel> Matches { get; set; }
    }
}
