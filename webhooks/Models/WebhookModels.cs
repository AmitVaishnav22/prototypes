using System.Text.Json;

namespace Webhooks.Models;

public sealed class WebhookDelivery
{
    public string DeliveryId { get; init; } = string.Empty;

    public string Event { get; init; } = string.Empty;

    public DateTimeOffset ReceivedAt { get; init; }

    public JsonElement Payload { get; init; }
}

public enum EnqueueResult
{
    Success,
    InvalidSignature,
    InvalidPayload
}