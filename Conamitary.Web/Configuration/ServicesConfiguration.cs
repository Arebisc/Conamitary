using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.Files;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
using Conamitary.Services.Files;
using Conamitary.Services.Receipe;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Conamitary.Web.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServices(
            this IServiceCollection services,
            string fileApiBaseUrl)
        {
            services.AddScoped<IReceipeAdder, ReceipeAdder>();
            services.AddScoped<IReceipeGetter, ReceipeGetter>();
            services.AddScoped<IReceipeRemover, ReceipeRemover>();
            services.AddScoped<IReceipeUpdater, ReceipeUpdater>();

            services.AddScoped<IMd5Calculator, Md5Calculator>();
            services.AddHttpClient(ApiTypes.FileApi, x =>
            {
                x.BaseAddress = new Uri(fileApiBaseUrl);
            });

            services.AddScoped<IFileGetter, FileGetter>();
            services.AddScoped<IReceipeFileRemover, ReceipeFileRemover>();
            services.AddScoped<IReceipeFileAdder, ReceipeFileAdder>();
        }
    }
}
