using System.Text;
using CatalogService.Application.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace CatalogService.Application.RabbitMQ;
public class RabbitMqProducer : IMessageProducer
{
    public async Task SendMessageAsync<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5673,
            UserName = "admin",
            Password = "1234",
            VirtualHost = "/"
        };
        var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        try
        {
            await channel.ExchangeDeclarePassiveAsync("exchangeOrder");
        } 
        catch (OperationInterruptedException ex) when (ex.ShutdownReason.ReplyCode == 404)
        {
            await channel.ExchangeDeclareAsync("exchangeOrder", ExchangeType.Direct);
        }
        await channel.QueueDeclareAsync("orders");
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        await channel.BasicPublishAsync(exchange: "exchangeOrder", routingKey: "orders", body: body);
    }
}