using System;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.InfluxDB;
using Micro.Starter.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Micro.Starter.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<ApplicationContext>().Database.Migrate();
            }
            host.Run();
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
