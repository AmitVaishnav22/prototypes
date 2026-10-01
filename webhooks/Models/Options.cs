using System.ComponentModel.DataAnnotations;

namespace Webhooks.Models;

public sealed class RabbitMqOptions
{
    public const string SectionName = "RabbitMq";

    [Required(AllowEmptyStrings = false, ErrorMessage = "RabbitMq:ConnectionString is required. Use amqp:// or amqps://, set via the RabbitMq__ConnectionString environment variable or user secrets.")]
    public string ConnectionString { get; init; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Exchange { get; init; } = "newsletter";

    [Required(AllowEmptyStrings = false)]
    public string Queue { get; init; } = "newsletter.github-release";

    [Required(AllowEmptyStrings = false)]
    public string RoutingKey { get; init; } = "github-release";
}

public sealed class WebhookOptions
{
    public const string SectionName = "Webhook";

    [Required(AllowEmptyStrings = false, ErrorMessage = "Webhook:Secret is required. Set via the Webhook__Secret environment variable or user secrets. It must match the secret used by the caller.")]
    [MinLength(32, ErrorMessage = "Webhook:Secret must be at least 32 characters.")]
    public string Secret { get; init; } = string.Empty;
}