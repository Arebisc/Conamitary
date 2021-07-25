using Conamitary.Database.Configuration;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
using Conamitary.Services.Receipe;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Conamitary.Web.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IReceipeAdder, ReceipeAdder>();
            services.AddScoped<IReceipeGetter, ReceipeGetter>();
            services.AddScoped<IReceipeDeleter, ReceipeDeleter>();
            services.AddScoped<IReceipeUpdater, ReceipeUpdater>();

            services.AddScoped<IMd5Calculator, Md5Calculator>();

            services.AddScoped<IReceipeImageRemover, ReceipeImageRemover>();
            services.AddScoped<IReceipeImageAdder, ReceipeImageAdder>();

            services.AddDbServices();
        }
    }
}
