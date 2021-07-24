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
            services.AddScoped<IReceipeAdder, ReceipeAdder>();
            services.AddScoped<IReceipeGetter, ReceipeGetter>();
            services.AddScoped<IReceipeDeleter, ReceipeDeleter>();
            services.AddScoped<IReceipeUpdater, ReceipeUpdater>();

            services.AddScoped<IFileAdder, FileAdder>();
            services.AddScoped<IFileGetter, FileGetter>();
            services.AddScoped<IFileDeleter, FileDeleter>();
            services.AddScoped<IFileUpdater, FileUpdater>();

            services.AddScoped<IDbContextSaver, DbContextSaver>();
        }
    }
}
