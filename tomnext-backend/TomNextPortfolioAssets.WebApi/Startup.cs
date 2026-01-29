using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TomNextPortfolioAssets.Domain.IRepositories;
using TomNextPortfolioAssets.EF;
using TomNextPortfolioAssets.EF.Repositories;
using TomNextPortfolioAssets.EF.Services;
using TomNextPortfolioAssets.WebApi.Extensions;

namespace TomNextPortfolioAssets.WebApi
{
    /// <summary>
    /// Startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Creates startup instance.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures application services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            ConfigureSqlServerContext(services);

            services.AddAutoMapper(typeof(TomNextPortfolioAssetsAutoMapperProfile));

            services.AddHttpClient();


            AddVersioning(services);

            services.AddSwagger(Configuration);

            AddServices(services);
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <param name="env">Environment.</param>
        /// <param name="apiVersionDescriptionProvider">API version description provider.</param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseCors(options =>
            {
                options
                    .WithOrigins(Configuration.GetValue<string>("AllowedHosts"))
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseDeveloperExceptionPage();
            Console.WriteLine($"We are running in debug: UseDeveloperExceptionPage");
            
            UseDatabaseMigrations(app);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger(apiVersionDescriptionProvider, Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSqlServerContext(IServiceCollection services)
        {
            if (DesignTimeDbContextFactory.IsNeedToConnectToDB(Configuration.GetValue<string>("ConnectionStrings"), Configuration.GetValue<bool>("SkipDbConnectIfNoConnectionString")))
            {
                string connectionString = DesignTimeDbContextFactory.GetConnectString(Configuration);

                services.AddDbContext<TomNextPortfolioAssetsDbContext>(options =>
                    options.UseNpgsql(
                        connectionString,
                        o => o.EnableRetryOnFailure(3)
                    ));
            }
            else
                services.AddDbContext<TomNextPortfolioAssetsDbContext>();
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddTransient<IDbMigrationService, TomNextPortfolioAssetsDbMigrationService>();
            services.AddTransient<IAssetsRepository, AssetsRepositories>();
            services.AddTransient<IAssetsService, AssestsService>();
        }

        private static void AddVersioning(IServiceCollection services)
        {
            var apiVersioningBuilder = services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            // Add versioned API explorer
            apiVersioningBuilder.AddApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });

        }

        private void UseDatabaseMigrations(IApplicationBuilder app)
        {
            if (DesignTimeDbContextFactory.IsNeedToConnectToDB(Configuration.GetValue<string>("ConnectionStrings"), Configuration.GetValue<bool>("SkipDbConnectIfNoConnectionString")))
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider.GetRequiredService<IDbMigrationService>().MigrateAsync().Wait();
                }
            }
        }
    }
}
