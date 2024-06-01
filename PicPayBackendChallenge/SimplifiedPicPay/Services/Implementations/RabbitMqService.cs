using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace SimplifiedPicPay.Services;

public class RabbitMqService : IWalletConfirmartionEmailService
{
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqService(string hostName)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = hostName
        };
    }
    
    public void Publish(object data, string routingKey, string queue)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(
            queue: queue, 
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var payload = JsonConvert.SerializeObject(data);
        var body = Encoding.UTF8.GetBytes(payload);

        Console.WriteLine($"{data.GetType().Name} Published");
        channel.BasicPublish(exchange: "",
            routingKey: routingKey,
            basicProperties: null,
            body:body);
    }
}