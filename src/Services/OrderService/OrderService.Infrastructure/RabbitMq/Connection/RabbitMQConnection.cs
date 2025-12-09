using OrderService.Infrastructure.RabbitMq.Connection.Interface;
using RabbitMQ.Client;

namespace OrderService.Infrastructure.RabbitMq.Connection;

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
            Password = "guest"
        };

        _connection = factory.CreateConnection();
    }

    #endregion
}

