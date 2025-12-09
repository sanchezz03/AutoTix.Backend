using RabbitMQ.Client;

namespace PaymentService.Infrastructure.RabbitMq.Interface;

public interface IRabbitMQConnection
{
    IConnection Connection { get; }
}
