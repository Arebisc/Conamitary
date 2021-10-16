using Conamitary.Mq.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.Microservices.FileConverter
{
    class Program
    {
        public static IConfiguration Configuration;

        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                await new HostBuilder()
                    .ConfigureServices(ConfigureServices)
                    .RunConsoleAsync();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        static void ConfigureServices(HostBuilderContext hostBuilderContext, 
            IServiceCollection services)
        {
            ConfigureOptions(services);

            services.AddLogging(loggingBuilder =>
             {
                 // configure Logging with NLog
                 loggingBuilder.ClearProviders();
                 loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 loggingBuilder.AddNLog(Configuration);
             });

            services.AddHostedService<ThumbnailGeneratorService>();
        }

        static void ConfigureOptions(IServiceCollection services)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(Configuration);

            services.Configure<RabbitMq>(
                Configuration.GetSection(nameof(RabbitMq)));
        }
    }
}
