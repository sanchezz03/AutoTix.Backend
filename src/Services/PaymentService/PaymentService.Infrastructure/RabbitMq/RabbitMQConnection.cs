using PaymentService.Infrastructure.RabbitMq.Interface;
using RabbitMQ.Client;

namespace PaymentService.Infrastructure.RabbitMq;

public class RabbitMQConnection : IRabbitMQConnection, IDisposable
{
    private IConnection? _connection;

    public IConnection Connection => _connection!;

    public RabbitMQConnection()
    {
        InitializeConnection();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }

    #region Private method

    private void InitializeConnection()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "rabbitmq",
            UserName = "guest",
            Password = "guest",
            DispatchConsumersAsync = true
        };

        int retryCount = 0;
        while (retryCount < 5)
        {
            try
            {
                _connection = factory.CreateConnection();
                break;
            }
            catch
            {
                retryCount++;
                Thread.Sleep(3000);
            }
        }

        if (_connection == null)
            throw new Exception("Failed to connect to RabbitMQ after 5 attempts.");
    }

    #endregion
}
