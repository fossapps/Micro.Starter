using System;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.InfluxDB;
using Micro.Starter.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Micro.Starter.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database;
                    await MigrateOrFail(db, logger);
                }
                catch (RetryLimitExceededException e)
                {
                    logger.LogCritical(e, "Error connecting to database, application can't start");
                    Environment.ExitCode = 1;
                    return;
                }
            }
            host.Run();
        }

        private static async Task MigrateOrFail(DatabaseFacade db, ILogger logger)
        {
            for (var i = 0; i <= 3; i++)
            {
                var waitTime = new[] {1, 3, 8, 10}[i];
                logger.LogInformation($"Db connection attempt in {waitTime} seconds");
                await Task.Delay(TimeSpan.FromSeconds(waitTime));

                if (await TryMigrate(db))
                {
                    return;
                }
                logger.LogWarning("Connection failed...");
            }
            throw new RetryLimitExceededException("Couldn't connect to database");
        }

        private static async Task<bool> TryMigrate(DatabaseFacade db)
        {
            try
            {
                var canConnect = await db.CanConnectAsync();
                if (!canConnect)
                {
                    return false;
                }
                await db.MigrateAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureMetricsWithDefaults((context, builder) =>
                        {
                            builder.Configuration.ReadFrom(context.Configuration);
                            builder.Report.ToInfluxDb(options =>
                            {
                                options.FlushInterval = TimeSpan.FromSeconds(5);
                                context.Configuration.GetSection("MetricsOptions").Bind(options);
                                options.MetricsOutputFormatter = new MetricsInfluxDbLineProtocolOutputFormatter();
                            });
                        });
                    webBuilder.UseMetrics();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
