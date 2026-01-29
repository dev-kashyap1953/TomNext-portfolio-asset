using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using TomNextPortfolioAssets.Domain.Tables;

namespace TomNextPortfolioAssets.EF.DataConfigurations
{
    public class AssetsDataConfiguration : IEntityTypeConfiguration<Assets>
    {
        public void Configure(EntityTypeBuilder<Assets> builder)
        {
            builder.HasData(
                Create(
                    Guid.Parse("8051e63c-04c2-49d0-830e-968e19ee594d"),
                    "MMC EIS Fund",
                    "Parimal",
                    "1",
                    "GB",
                    new List<string> { "Technology", "Healthcare" },
                    new List<string> { "global stocks", "equity growth" },
                    new DateTime(2026, 01, 27, 12, 45, 48, DateTimeKind.Utc)
                ),

                Create(
                    Guid.Parse("6c3e0d72-ee26-4e29-8283-9964f01d5658"),
                    "Emerging Markets Growth Fund",
                    "Rahul",
                    "3",
                    "EMEA",
                    new List<string> { "Financials", "Energy", "Industrials" },
                    new List<string> { "emerging economies", "market growth", "developing nations" },
                    new DateTime(2026, 01, 27, 12, 50, 59, DateTimeKind.Utc)
                ),

                Create(
                    Guid.Parse("7071195d-7354-43b0-a38d-584b12ae5cec"),
                    "Global Corporate Bond Fund",
                    "Parimal",
                    "4",
                    "Global",
                    new List<string> { "Corporate Bonds", "Finance", "Utilities" },
                    new List<string> { "bond yields", "interest rates", "fixed income investing" },
                    new DateTime(2026, 01, 27, 12, 50, 59, DateTimeKind.Utc)
                ),

                Create(
                    Guid.Parse("12e62a7a-9ae9-4f75-93e4-e456987c13a7"),
                    "Sustainable Energy Fund",
                    "Darshan",
                    "5",
                    "Global",
                    new List<string> { "Renewable Energy", "Clean Tech", "Utilities" },
                    new List<string> { "green energy", "ESG investing", "sustainability" },
                    new DateTime(2026, 01, 27, 12, 50, 59, DateTimeKind.Utc)
                ),

                Create(
                    Guid.Parse("8d3c9203-7754-428e-b54f-20757bef855c"),
                    "Global Tech Innovation Fund",
                    "BlackRock",
                    "2",
                    "US",
                    new List<string> { "Technology", "AI", "Semiconductors" },
                    new List<string> { "artificial intelligence", "tech growth", "chip makers" },
                    new DateTime(2026, 01, 27, 12, 48, 58, DateTimeKind.Utc)
                )
            );
        }

        private static Assets Create(
            Guid id,
            string fundName,
            string manager,
            string assetClass,
            string geography,
            List<string> sectors,
            List<string> keywords,
            DateTime createdAt)
        {
            return new Assets
            {
                Id = id,
                FundName = fundName,
                Manager = manager,
                AssetClass = assetClass,
                Geography = geography,
                Sectors = sectors,
                Keywords = keywords,
                CreatedAt = createdAt
            };
        }
    }
}
