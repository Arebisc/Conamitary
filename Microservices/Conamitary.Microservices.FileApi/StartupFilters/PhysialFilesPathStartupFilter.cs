using Conamitary.Services.Abstract.PhysicalFiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Conamitary.Microservices.FileApi.StartupFilters
{
    public class PhysialFilesPathStartupFilter : IStartupFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public PhysialFilesPathStartupFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<IPhysicalPathCreator>();

                service.CreateFilesSavePath()
                    .GetAwaiter()
                    .GetResult();
            }

            return next;
        }
    }
}
