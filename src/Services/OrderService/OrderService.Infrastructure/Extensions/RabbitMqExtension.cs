using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.RabbitMq;
using OrderService.Infrastructure.RabbitMq.Connection;
using OrderService.Infrastructure.RabbitMq.Connection.Interface;

namespace OrderService.Infrastructure.Extensions;

public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddHostedService<MessageConsumer>()
            .AddSingleton<IRabbitMQConnection>(new RabbitMQConnection())
            .AddScoped<IMessageProducer, MessageProducer>();
    }
}
