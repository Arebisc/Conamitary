using Conamitary.Database;
using Conamitary.Database.Configuration;
using Conamitary.Microservices.FileApi.StartupFilters;
using Conamitary.Services.Abstract.Commons;
using Conamitary.Services.Abstract.PhysicalFiles;
using Conamitary.Services.Abstract.Receipe;
using Conamitary.Services.Commons;
using Conamitary.Services.PhysicalFiles;
using Conamitary.Services.Receipe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Conamitary.Microservices.FileApi
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
            services.AddDbContext<ConamitaryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPhysicalFileSaver, LocalDiskFileSaver>();
            services.AddScoped<IPhysicalFileGetter, LocalDiskFileGetter>();
            services.AddScoped<IPhysicalFileRemover, LocalDiskFileRemover>();
            services.AddScoped<IPhysicalPathCreator, LocalDiskPathCreator>();

            services.AddScoped<IReceipeImageAdder, ReceipeImageAdder>();
            services.AddScoped<IReceipeImageRemover, ReceipeImageRemover>();
            services.AddScoped<IReceipeImageGetter, ReceipeImageGetter>();

            services.AddScoped<IMd5Calculator, Md5Calculator>();

            services.AddTransient<IStartupFilter, PhysialFilesPathStartupFilter>();

            services.AddDbServices();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if(Configuration.GetSection("SslRedirection").Exists() && 
                Convert.ToBoolean(Configuration.GetSection("SslRedirection").Value) == true)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
