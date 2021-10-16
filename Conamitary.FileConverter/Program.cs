using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Conamitary.FileConverter
{
    class Program
    {
        public static IConfiguration configuration;

        static async Task Main(string[] args)
        {
            await new HostBuilder()
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        static void ConfigureServices(HostBuilderContext hostBuilderContext, 
            IServiceCollection services)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(configuration);

            services.AddHostedService<MqListener>();
        }
    }
}
