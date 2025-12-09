using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Interfaces;
using PaymentService.Infrastructure.Consumers;
using PaymentService.Infrastructure.Producers;
using PaymentService.Infrastructure.RabbitMq;
using PaymentService.Infrastructure.RabbitMq.Interface;

namespace PaymentService.Infrastructure.Extensions;

public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddSingleton<IRabbitMQConnection>(new RabbitMQConnection())
            .AddSingleton<IPaymentSucceededProducer, PaymentSucceededProducer>()
            .AddSingleton<PaymentRequestConsumer>();
    }
}
