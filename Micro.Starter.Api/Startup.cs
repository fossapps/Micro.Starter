using System;
using Micro.Starter.Api.Configs;
using Micro.Starter.Api.Models;
using Micro.Starter.Api.Repository;
using Micro.Starter.Api.Uuid;
using Micro.Starter.Api.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Slack;
using Microsoft.Extensions.Options;

namespace Micro.Starter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddConfiguration(services, Configuration);
            services.AddMetrics();
            ConfigureDependencies(services);
            services.AddControllers();
            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.RegisterMiddleware = true;
                x.ReportApiVersions = true;
                x.AssumeDefaultVersionWhenUnspecified = true;
            });
            RegisterWorker(services);
        }

        private static void ConfigureDependencies(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddSingleton<IUuidService, UuidService>();
        }

        private static void AddConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection("DatabaseConfig"));
            services.Configure<SlackLoggingConfig>(configuration.GetSection("Logging").GetSection("Slack"));
        }

        private static void RegisterWorker(IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<SlackLoggingConfig> slackConfig)
        {
            ConfigureSlack(loggerFactory, slackConfig.Value, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static void ConfigureSlack(ILoggerFactory loggerFactory, SlackLoggingConfig slackConfig, IWebHostEnvironment env)
        {
            if (string.IsNullOrEmpty(slackConfig.WebhookUrl))
            {
                return;
            }
            loggerFactory.AddSlack(new SlackConfiguration
            {
                MinLevel = slackConfig.MinLogLevel,
                WebhookUrl = new Uri(slackConfig.WebhookUrl)
            }, env.ApplicationName, env.EnvironmentName);
        }
    }
}
