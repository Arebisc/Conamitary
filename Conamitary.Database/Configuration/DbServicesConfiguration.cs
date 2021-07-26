using Conamitary.Database.Abstract;
using Conamitary.Database.Abstract.File;
using Conamitary.Database.Abstract.Receipe;
using Conamitary.Database.Services;
using Conamitary.Database.Services.File;
using Conamitary.Database.Services.Receipe;
using Microsoft.Extensions.DependencyInjection;

namespace Conamitary.Database.Configuration
{
    public static class DbServicesConfiguration
    {
        public static void AddDbServices(this IServiceCollection services)
        {
            services.AddScoped<IDbReceipeAdder, DbReceipeAdder>();
            services.AddScoped<IDbReceipeGetter, DbReceipeGetter>();
            services.AddScoped<IDbReceipeDeleter, DbReceipeDeleter>();
            services.AddScoped<IDbReceipeUpdater, DbReceipeUpdater>();

            services.AddScoped<IDbFileAdder, DbFileAdder>();
            services.AddScoped<IDbFileGetter, DbFileGetter>();
            services.AddScoped<IDbFileDeleter, DbFileDeleter>();
            services.AddScoped<IDbFileUpdater, DbFileUpdater>();

            services.AddScoped<IDbContextSaver, DbContextSaver>();
        }
    }
}
