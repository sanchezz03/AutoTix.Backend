using PaymentService.API.BackgroundJobs;

namespace PaymentService.API.Extensions;

public static class BackgoundJob
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        return services
            .AddHostedService<PaymentWorker>();
    }
}
