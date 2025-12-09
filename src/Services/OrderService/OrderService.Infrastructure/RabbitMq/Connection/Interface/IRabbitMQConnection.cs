using RabbitMQ.Client;

namespace OrderService.Infrastructure.RabbitMq.Connection.Interface;

public interface IRabbitMQConnection
{
    IConnection Connection { get; }
}

