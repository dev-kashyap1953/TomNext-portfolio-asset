using System;
using System.Collections.Generic;

namespace TomNextPortfolioAssets.Domain.Tables
{
    public class Assets
    {
     
        /// <summary>
        /// Assests id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Assets fund name.
        /// </summary>
        public string FundName { get; set; }

        /// <summary>
        /// Manager name.
        /// </summary>
        public string Manager { get; set; }

        /// <summary>
        /// Assests class.
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
        /// Matches news.
        /// </summary>
        public ICollection<AssetNewsMatch> Matches { get; set; }
    }
}
