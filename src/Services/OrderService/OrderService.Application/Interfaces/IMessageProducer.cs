namespace OrderService.Application.Interfaces;

public interface IMessageProducer
{
    void Publish<T>(T message, string routingKey);
}
