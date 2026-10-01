using Webhooks.Models;

namespace Webhooks.Repositories;

public interface IMessageQueueRepository
{
    Task EnqueueAsync(WebhookDelivery delivery, CancellationToken cancellationToken);
}