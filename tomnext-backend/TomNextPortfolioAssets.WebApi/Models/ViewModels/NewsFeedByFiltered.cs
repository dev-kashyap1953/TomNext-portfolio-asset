using System.ComponentModel;

namespace TomNextPortfolioAssets.WebApi.Models.ViewModels
{
    /// <summary>
    /// News feed by filtered model.
    /// </summary>
    public class NewsFeedByFiltered
    {
        /// <summary>
        /// Filter by assests.
        /// </summary>
        [DefaultValue("")]
        public string Assets {get; set;} = string.Empty;

        /// <summary>
        ///Filter by geo
        /// </summary>
        [DefaultValue("")]
        public string Geo { get; set; } = string.Empty;

        /// <summary>
        /// Filter by scetors.
        /// </summary>
        [DefaultValue("")]
        public string Sectors { get; set; } = string.Empty;
    }
}
