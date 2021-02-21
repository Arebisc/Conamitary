using Conamitary.Services.Abstract.File;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.File;
using Conamitary.Services.Receipe;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conamitary.Web.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IReceipeAdder, ReceipeAdder>();
            services.AddScoped<IReceipeGetter, ReceipeGetter>();
            services.AddScoped<IReceipeRemover, ReceipeRemover>();
            services.AddScoped<IReceipeUpdater, ReceipeUpdater>();

            services.AddScoped<IMd5Calculator, Md5Calculator>();
            services.AddHttpClient();
        }
    }
}
