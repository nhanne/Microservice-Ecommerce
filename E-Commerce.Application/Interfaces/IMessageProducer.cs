namespace E_Commerce.Interfaces;

public interface IMessageProducer
{
    Task SendMessageAsync<T> (T message);
}