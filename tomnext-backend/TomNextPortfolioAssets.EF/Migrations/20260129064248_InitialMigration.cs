using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TomNextPortfolioAssets.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FundName = table.Column<string>(type: "text", nullable: true),
                    Manager = table.Column<string>(type: "text", nullable: true),
                    AssetClass = table.Column<string>(type: "text", nullable: true),
                    Geography = table.Column<string>(type: "text", nullable: true),
                    Sectors = table.Column<List<string>>(type: "text[]", nullable: true),
                    Keywords = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Snippet = table.Column<string>(type: "text", nullable: true),
                    Sector = table.Column<string>(type: "text", nullable: true),
                    Geo = table.Column<string>(type: "text", nullable: true),
                    RawContent = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNewsStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NewsArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNewsStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetsNewMatch",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewsArticleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelevanceScore = table.Column<int>(type: "integer", nullable: false),
                    MatchedFields = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsNewMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetsNewMatch_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetsNewMatch_NewsArticles_NewsArticleId",
                        column: x => x.NewsArticleId,
                        principalTable: "NewsArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetClass", "CreatedAt", "FundName", "Geography", "Keywords", "Manager", "Sectors" },
                values: new object[,]
                {
                    { new Guid("12e62a7a-9ae9-4f75-93e4-e456987c13a7"), "5", new DateTime(2026, 1, 27, 12, 50, 59, 0, DateTimeKind.Utc), "Sustainable Energy Fund", "Global", new List<string> { "green energy", "ESG investing", "sustainability" }, "Darshan", new List<string> { "Renewable Energy", "Clean Tech", "Utilities" } },
                    { new Guid("6c3e0d72-ee26-4e29-8283-9964f01d5658"), "3", new DateTime(2026, 1, 27, 12, 50, 59, 0, DateTimeKind.Utc), "Emerging Markets Growth Fund", "EMEA", new List<string> { "emerging economies", "market growth", "developing nations" }, "Rahul", new List<string> { "Financials", "Energy", "Industrials" } },
                    { new Guid("7071195d-7354-43b0-a38d-584b12ae5cec"), "4", new DateTime(2026, 1, 27, 12, 50, 59, 0, DateTimeKind.Utc), "Global Corporate Bond Fund", "Global", new List<string> { "bond yields", "interest rates", "fixed income investing" }, "Parimal", new List<string> { "Corporate Bonds", "Finance", "Utilities" } },
                    { new Guid("8051e63c-04c2-49d0-830e-968e19ee594d"), "1", new DateTime(2026, 1, 27, 12, 45, 48, 0, DateTimeKind.Utc), "MMC EIS Fund", "GB", new List<string> { "global stocks", "equity growth" }, "Parimal", new List<string> { "Technology", "Healthcare" } },
                    { new Guid("8d3c9203-7754-428e-b54f-20757bef855c"), "2", new DateTime(2026, 1, 27, 12, 48, 58, 0, DateTimeKind.Utc), "Global Tech Innovation Fund", "US", new List<string> { "artificial intelligence", "tech growth", "chip makers" }, "BlackRock", new List<string> { "Technology", "AI", "Semiconductors" } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetsNewMatch_AssetId",
                table: "AssetsNewMatch",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsNewMatch_NewsArticleId",
                table: "AssetsNewMatch",
                column: "NewsArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsNewMatch");

            migrationBuilder.DropTable(
                name: "UserNewsStates");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "NewsArticles");
        }
    }
}
