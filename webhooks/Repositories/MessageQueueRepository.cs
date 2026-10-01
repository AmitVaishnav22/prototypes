using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Webhooks.Models;

namespace Webhooks.Repositories;

public sealed class MessageQueueRepository(
    IOptions<RabbitMqOptions> options,
    ILogger<MessageQueueRepository> logger) : IMessageQueueRepository, IAsyncDisposable
{
    private readonly RabbitMqOptions _options = options.Value;
    private readonly SemaphoreSlim _gate = new(1, 1);

    private IConnection? _connection;
    private IChannel? _channel;

    public async Task EnqueueAsync(WebhookDelivery delivery, CancellationToken cancellationToken)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(delivery);

        await _gate.WaitAsync(cancellationToken);
        try
        {
            var channel = await GetChannelAsync(cancellationToken);

            var properties = new BasicProperties
            {
                ContentType = "application/json",
                DeliveryMode = DeliveryModes.Persistent,
                MessageId = delivery.DeliveryId,
                Type = delivery.Event
            };

            await channel.BasicPublishAsync(
                _options.Exchange,
                _options.RoutingKey,
                mandatory: true,
                properties,
                body,
                cancellationToken);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<IChannel> GetChannelAsync(CancellationToken cancellationToken)
    {
        if (_channel is { IsOpen: true })
        {
            return _channel;
        }

        await DisposeConnectionAsync();

        var factory = new ConnectionFactory
        {
            Uri = new Uri(_options.ConnectionString)
        };

        _connection = await factory.CreateConnectionAsync("webhooks", cancellationToken);

        var channelOptions = new CreateChannelOptions(
            publisherConfirmationsEnabled: true,
            publisherConfirmationTrackingEnabled: true);

        var channel = await _connection.CreateChannelAsync(channelOptions, cancellationToken);

        await channel.ExchangeDeclareAsync(
            _options.Exchange,
            ExchangeType.Direct,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await channel.QueueDeclareAsync(
            _options.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken);

        await channel.QueueBindAsync(
            _options.Queue,
            _options.Exchange,
            _options.RoutingKey,
            cancellationToken: cancellationToken);

        _channel = channel;

        logger.LogInformation(
            "RabbitMQ ready: exchange {Exchange} bound to queue {Queue} with routing key {RoutingKey}",
            _options.Exchange,
            _options.Queue,
            _options.RoutingKey);

        return channel;
    }

    private async Task DisposeConnectionAsync()
    {
        if (_channel is not null)
        {
            await _channel.DisposeAsync();
            _channel = null;
        }

        if (_connection is not null)
        {
            await _connection.DisposeAsync();
            _connection = null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _gate.WaitAsync();
        try
        {
            await DisposeConnectionAsync();
        }
        finally
        {
            _gate.Release();
            _gate.Dispose();
        }
    }
}