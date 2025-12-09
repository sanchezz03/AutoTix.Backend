using PaymentService.Infrastructure.Consumers;

namespace PaymentService.API.BackgroundJobs;

public class PaymentWorker : BackgroundService
{
    private readonly PaymentRequestConsumer _consumer;

    public PaymentWorker(PaymentRequestConsumer consumer)
    {
        _consumer = consumer;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Start();
        return Task.CompletedTask;
    }
}