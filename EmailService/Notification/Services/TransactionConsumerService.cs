using System.Text;
using System.Text.Json;
using Notification.Dtos;
using Notification.Models;
using Notification.Utilities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.Services;

public class TransactionConsumerService : BackgroundService
{
    private ConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;
    private IEmailService _emailService;
    
    public TransactionConsumerService(IEmailService emailService)
    {
        _emailService = emailService;
        _connectionFactory = new ConnectionFactory(){ HostName = "localhost"};
        InitConsumer();
    }

    private void InitConsumer()
    {
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: GlobalVariables.TRANSACTION_QUEUE, 
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var transactionNotificationData = JsonSerializer.Deserialize<TransactionNotification>(contentString);
            await _emailService.HandleEmailNotification(transactionNotificationData);
        };
        
        _channel.BasicConsume(queue: GlobalVariables.TRANSACTION_QUEUE,
            autoAck: true,
            consumer: consumer);
    }
}