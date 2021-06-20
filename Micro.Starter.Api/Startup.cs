using Micro.Auth.Sdk;
using Micro.Starter.Api.GraphQL.Extensions;
using Micro.Starter.Api.Internal.Configs;
using Micro.Starter.Api.Internal.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddConfiguration(Configuration);
            services.AddMetrics();
            services.ConfigureRequiredDependencies();
            services.ConfigureHealthChecks();
            services.AddControllers();
            services.ConfigureSwagger();
            services.RegisterWorker();
            services.ConfigureGraphql();
            services.ConfigureAuthServices(new Config
            {
                KeyStoreUrl = Configuration.GetSection("Services").Get<Services>().KeyStore.Url
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<SlackLoggingConfig> slackConfig)
        {
            loggerFactory.ConfigureLoggerWithSlack(slackConfig.Value, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.SetupAuth();
            app.SetupGraphQl();
            app.AddSwaggerWithUi();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.ConfigureHealthCheckEndpoint();
            });
        }
    }
}
