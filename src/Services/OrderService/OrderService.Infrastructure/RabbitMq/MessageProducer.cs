using OrderService.Application.Interfaces;
using OrderService.Infrastructure.RabbitMq.Connection.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.RabbitMq;

public class MessageProducer : IMessageProducer
{
    private readonly IRabbitMQConnection _connection;

    public MessageProducer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public void Publish<T>(T message, string routingKey)
    {
        using var channel = _connection.Connection.CreateModel();

        channel.ExchangeDeclare("order_exchange", ExchangeType.Direct);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: "order_exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body);
    }
}
