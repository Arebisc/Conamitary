using Conamitary.Mq.Abstract;
using Conamitary.Mq.Configuration.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;

namespace Conamitary.Mq.Services
{
    public class RabbitMqConnectionFactory: IRabbitMqConnectionFactory
    {
        private readonly IConnection _connection;
        private readonly RabbitMq _options;
        private readonly ILogger<RabbitMqConnectionFactory> _logger;

        public RabbitMqConnectionFactory(IOptions<RabbitMq> options,
            ILogger<RabbitMqConnectionFactory> logger)
        {
            _options = options.Value;
            _logger = logger;

            CreateConnection();
        }

        public IConnection GetConnection
        { 
            get
            {
                return _connection;
            }
        }

        private void CreateConnection()
        {
            _logger.LogDebug("Creating rabbitmq connection...");
            _logger.LogTrace("Create connection for data: ", JsonSerializer.Serialize(_options));

            var factory = new ConnectionFactory()
            {
                // This is my configuration. Just change it to my own use
                HostName = _options.Host,
                UserName = _options.Username,
                Password = _options.Password,
                Port = _options.Port,
            };
            factory.CreateConnection();
        }
    }
}
