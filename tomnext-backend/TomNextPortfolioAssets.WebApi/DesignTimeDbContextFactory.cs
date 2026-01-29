using System;
using System.IO;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TomNextPortfolioAssets.EF;
using TomNextPortfolioAssets.WebApi.EnvironmentConfiguration;

namespace TomNextPortfolioAssets.WebApi
{
    /// <summary>
    /// Design time db context factory.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TomNextPortfolioAssetsDbContext>
    {
        /// <summary>
        /// Create db context.
        /// </summary>
        /// <param name="args">Args.</param>
        /// <returns>TomNextPortfolioAssets db context.</returns>
        public TomNextPortfolioAssetsDbContext CreateDbContext(string[] args)
        {
            // Load launchSettings.json
            var launchSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "launchSettings.json");
            LoadEnvironmentVariablesFromJson(launchSettingsPath, "profiles['IIS Express'].environmentVariables");

            // Load environment for default configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Add(new CustomEnvironmentVariableConfigurationSource())
                .Build();

            string connectionString = null;
            if (IsNeedToConnectToDB(configuration.GetValue<string>("ConnectionStrings"), configuration.GetValue<bool>("SkipDbConnectIfNoConnectionString")))
            {
                connectionString = GetConnectString(configuration);
            }
            var optionsBuilder = new DbContextOptionsBuilder<TomNextPortfolioAssetsDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return new TomNextPortfolioAssetsDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Is need to connect to db.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="skipDbConnectIfNoConnectionString">Skip db connect if no connection string.</param>
        /// <returns>True/False</returns>
        public static bool IsNeedToConnectToDB(string connectionString, bool skipDbConnectIfNoConnectionString)
        {
            return !(skipDbConnectIfNoConnectionString && string.IsNullOrWhiteSpace(connectionString));
        }

        /// <summary>
        /// Get connect string.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="isForSqlServer">Is for sql server.</param>
        /// <returns>String</returns>
        public static string GetConnectString(IConfiguration configuration, bool isForSqlServer = false)
        {
            var connectionString = configuration.GetValue<string>("ConnectionStrings");

            if (configuration.GetValue<bool>("InjectDBCredentialFromEnvironment"))
            {
                if (isForSqlServer)
                {
                    connectionString +=
                        $";User Id={configuration.GetValue<string>("AspNetCoreDbUserName")};Password='{configuration.GetValue<string>("AspNetCoreDbPassword")}'";
                }
                else
                {
                    connectionString +=
                        $";Username={configuration.GetValue<string>("AspNetCoreDbUserName")};Password='{configuration.GetValue<string>("AspNetCoreDbPassword")}'";
                }
            }
            return connectionString;
        }

        /// <summary>
        /// Load environment variable from json (Use only for local development and test project)
        /// </summary>
        /// <param name="path">Path of json file.</param>
        /// <param name="environmentVariablesJsonPath">Environment variables json path like profiles['IIS Express'].environmentVariables.</param>
        public static void LoadEnvironmentVariablesFromJson(string path, string environmentVariablesJsonPath = "")
        {
            if (!File.Exists(path)) return;

            string jsonContent;
            try
            {
                jsonContent = File.ReadAllText(path);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }

            JObject jsonObject;
            try
            {
                jsonObject = JObject.Parse(jsonContent);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return;
            }

            // Use a dynamic path to extract environment variables from the JSON object
            var environmentVariables = string.IsNullOrWhiteSpace(environmentVariablesJsonPath) ? jsonObject.Root : jsonObject.SelectToken(environmentVariablesJsonPath);

            if (environmentVariables == null)
            {
                Console.WriteLine("No environment variables found at the specified path.");
                return;
            }

            foreach (JProperty variable in environmentVariables)
            {
                try
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error setting environment variable {variable.Name}: {ex.Message}");
                }
            }
        }
    }
}

