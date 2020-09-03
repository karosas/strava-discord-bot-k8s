﻿using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace StravaDiscordBot.Shared.Extensions
{
    public static class WebhostBuilderExtensions
    {
        public static IWebHostBuilder UseSerilog(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder
                .UseSerilog((builderContext, loggerConfig) =>
                {
                    loggerConfig.Enrich.FromLogContext();
                    loggerConfig.Enrich.WithProperty("Environment", builderContext.HostingEnvironment.EnvironmentName);
                    loggerConfig.Enrich.WithProperty("Type", "DiscordStravaBot");
                    loggerConfig.MinimumLevel.Debug();

                    loggerConfig.WriteTo.Console();

                    /*var options = new AppOptions();
                    builderContext.Configuration.Bind(options);

                    if (options.Humio != null && !string.IsNullOrEmpty(options.Humio.Token))
                    {
                        loggerConfig.WriteTo.Logger(lc =>
                        {
                            lc.MinimumLevel.Information();
                            lc.MinimumLevel.Override("System", LogEventLevel.Information);
                            lc.MinimumLevel.Override("Microsoft", LogEventLevel.Information);

                            lc.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://cloud.humio.com:443/api/v1/dataspaces/sandbox/ingest/elasticsearch"))
                            {
                                MinimumLogEventLevel = LogEventLevel.Information,
                                ModifyConnectionSettings = c => c.BasicAuthentication(options.Humio.Token, ""),
                                Period = TimeSpan.FromMilliseconds(500)
                            });
                        });
                    }*/
                    //if(options.LogToConsole)
                });
        }
    }
}