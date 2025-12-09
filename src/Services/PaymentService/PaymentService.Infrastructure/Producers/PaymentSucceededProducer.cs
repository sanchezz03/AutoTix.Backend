using PaymentService.Application.Events;
using PaymentService.Application.Interfaces;
using PaymentService.Infrastructure.RabbitMq.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PaymentService.Infrastructure.Producers;

public class PaymentSucceededProducer : IPaymentSucceededProducer
{
    private readonly IModel _channel;

    public PaymentSucceededProducer(IRabbitMQConnection connection)
    {
        _channel = connection.Connection.CreateModel();
        _channel.ExchangeDeclare("payment.exchange", ExchangeType.Direct);
    }

    public void Publish(PaymentSucceededEvent evt)
    {
        var json = JsonSerializer.Serialize(evt);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish(
            exchange: "payment.exchange",
            routingKey: "payment.succeeded",
            basicProperties: null,
            body: body
        );
    }
}
