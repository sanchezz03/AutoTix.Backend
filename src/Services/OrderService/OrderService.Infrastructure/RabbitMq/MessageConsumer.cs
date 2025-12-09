using MediatR;
using Microsoft.Extensions.Hosting;
using OrderService.Application.Events;
using OrderService.Infrastructure.RabbitMq.Connection.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.RabbitMq;

public class MessageConsumer : BackgroundService
{
    private readonly IRabbitMQConnection _connection;
    private readonly IMediator _mediator;

    public MessageConsumer(IRabbitMQConnection connection, IMediator mediator)
    {
        _connection = connection;
        _mediator = mediator;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _connection.Connection.CreateModel();

        channel.ExchangeDeclare("payment_exchange", ExchangeType.Direct);

        channel.QueueDeclare("payment_success_queue", true, false, false, null);
        channel.QueueBind("payment_success_queue", "payment_exchange", "payment.success");

        channel.QueueDeclare("payment_failed_queue", true, false, false, null);
        channel.QueueBind("payment_failed_queue", "payment_exchange", "payment.failed");

        Consume(channel, "payment_success_queue", stoppingToken);
        Consume(channel, "payment_failed_queue", stoppingToken);

        return Task.CompletedTask;
    }

    #region Private methods

    private void Consume(IModel channel, string queueName, CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, args) =>
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());
            Console.WriteLine($"[RabbitMQ] Received from {queueName}: {json}");

            if (queueName == "payment_success_queue")
            {
                var evt = JsonSerializer.Deserialize<PaymentSucceededEvent>(json);
                if (evt != null)
                    await _mediator.Publish(evt);
            }
            else if (queueName == "payment_failed_queue")
            {
                var evt = JsonSerializer.Deserialize<PaymentFailedEvent>(json);
                if (evt != null)
                    await _mediator.Publish(evt);
            }

            channel.BasicAck(args.DeliveryTag, false);
        };

        channel.BasicConsume(queueName, false, consumer);
    }

    #endregion
}
