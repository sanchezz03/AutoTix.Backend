using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Events;
using PaymentService.Application.Services.Interfaces;
using PaymentService.Infrastructure.RabbitMq.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace PaymentService.Infrastructure.Consumers;

public class PaymentRequestConsumer
{
    private readonly IRabbitMQConnection _connection;
    private readonly IServiceScopeFactory _scopeFactory;
    private IModel _channel;

    public PaymentRequestConsumer(IRabbitMQConnection connection, IServiceScopeFactory scopeFactory)
    {
        _connection = connection;
        _scopeFactory = scopeFactory;

        _channel = _connection.Connection.CreateModel();
        _channel.QueueDeclare("payment.request.queue", durable: true, exclusive: false, autoDelete: false);
    }

    public void Start()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (_, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IPaymentProcessor>();

            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var message = JsonSerializer.Deserialize<PaymentRequestEvent>(json);

            if (message != null)
            {
                await processor.ProcessAsync(message);
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("payment.request.queue", autoAck: false, consumer);
    }
}
