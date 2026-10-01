using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Webhooks.Models;
using Webhooks.Repositories;

namespace Webhooks.Services;

public sealed class WebhookService(
    IMessageQueueRepository messageQueue,
    IOptions<WebhookOptions> options,
    ILogger<WebhookService> logger) : IWebhookService
{
    private const string SignaturePrefix = "sha256=";
    private const int DigestBytes = 32;

    private readonly byte[] _secret = Encoding.UTF8.GetBytes(options.Value.Secret);

    public async Task<EnqueueResult> EnqueueAsync(
        string rawBody,
        string? signature,
        string? eventName,
        string? deliveryId,
        CancellationToken cancellationToken)
    {
        if (!IsSignatureValid(rawBody, signature))
        {
            logger.LogWarning("Rejected delivery: missing or invalid X-Hub-Signature-256");

            return EnqueueResult.InvalidSignature;
        }

        JsonElement payload;
        try
        {
            payload = JsonSerializer.Deserialize<JsonElement>(rawBody);
        }
        catch (JsonException)
        {
            return EnqueueResult.InvalidPayload;
        }

        if (payload.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined)
        {
            return EnqueueResult.InvalidPayload;
        }

        var delivery = new WebhookDelivery
        {
            DeliveryId = string.IsNullOrWhiteSpace(deliveryId) ? Guid.NewGuid().ToString("D") : deliveryId,
            Event = string.IsNullOrWhiteSpace(eventName) ? "unknown" : eventName,
            ReceivedAt = DateTimeOffset.UtcNow,
            Payload = payload
        };

        await messageQueue.EnqueueAsync(delivery, cancellationToken);

        logger.LogInformation("Enqueued delivery {DeliveryId} ({Event})", delivery.DeliveryId, delivery.Event);

        return EnqueueResult.Success;
    }

    private bool IsSignatureValid(string rawBody, string? signature)
    {
        if (string.IsNullOrEmpty(signature) || !signature.StartsWith(SignaturePrefix, StringComparison.Ordinal))
        {
            return false;
        }

        Span<byte> provided = stackalloc byte[DigestBytes];
        if (!TryParseHex(signature.AsSpan(SignaturePrefix.Length), provided))
        {
            return false;
        }

        Span<byte> expected = stackalloc byte[DigestBytes];
        HMACSHA256.HashData(_secret, Encoding.UTF8.GetBytes(rawBody), expected);

        return CryptographicOperations.FixedTimeEquals(expected, provided);
    }

    private static bool TryParseHex(ReadOnlySpan<char> source, Span<byte> destination)
    {
        if (source.Length != destination.Length * 2)
        {
            return false;
        }

        for (var index = 0; index < destination.Length; index++)
        {
            var high = HexValue(source[index * 2]);
            var low = HexValue(source[index * 2 + 1]);

            if (high < 0 || low < 0)
            {
                return false;
            }

            destination[index] = (byte)((high << 4) | low);
        }

        return true;
    }

    private static int HexValue(char character) => character switch
    {
        >= '0' and <= '9' => character - '0',
        >= 'a' and <= 'f' => character - 'a' + 10,
        >= 'A' and <= 'F' => character - 'A' + 10,
        _ => -1
    };
}