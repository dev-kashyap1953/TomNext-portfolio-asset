using TomNextPortfolioAssets.Domain.Tables;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TomNextPortfolioAssets.EF
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class TomNextPortfolioAssetsDbContext : DbContext
    {
        /// <summary>
        /// My tables.
        /// </summary>
        //public DbSet<MyTable> MyTables { get; set; }

        public DbSet<Assets> Assets { get; set; }
        public DbSet<AssetNewsMatch> AssetsNewMatch { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<UserNewsState> UserNewsStates { get; set; }


        /// <summary>
        /// Creates context.
        /// </summary>
        /// <param name="options">Context options.</param>
        public TomNextPortfolioAssetsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(TomNextPortfolioAssetsDbContext)));
        }
    }
}
