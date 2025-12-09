using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Services;
using PaymentService.Application.Services.Interfaces;

namespace PaymentService.Application.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IPaymentProcessor, MockPaymentProcessor>();
    }
}
