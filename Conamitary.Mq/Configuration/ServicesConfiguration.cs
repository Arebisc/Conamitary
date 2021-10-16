using Conamitary.Mq.Abstract;
using Conamitary.Mq.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Conamitary.Mq
{
    public static class ServicesConfiguration
    {
        public static void ConfigureRabbitMqServices(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnectionFactory, RabbitMqConnectionFactory>();
        }
    }
}
