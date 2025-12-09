using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.RabbitMq;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<IMessageProducer, MessageProducer>();
    }
}
