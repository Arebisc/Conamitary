using RabbitMQ.Client;

namespace Conamitary.Mq.Abstract
{
    public interface IRabbitMqConnectionFactory
    {
        IConnection GetConnection { get; }
    }
}
