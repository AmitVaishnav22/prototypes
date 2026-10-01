using Webhooks.Models;

namespace Webhooks.Services;

public interface IWebhookService
{
    Task<EnqueueResult> EnqueueAsync(
        string rawBody,
        string? signature,
        string? eventName,
        string? deliveryId,
        CancellationToken cancellationToken);
}