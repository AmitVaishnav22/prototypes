using Microsoft.Extensions.Options;
using Webhooks.Repositories;
using Webhooks.Services;
using Webhooks.Models;

namespace Webhooks.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebhookInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddOptions<RabbitMqOptions>()
            .Bind(configuration.GetSection(RabbitMqOptions.SectionName))
            .ValidateDataAnnotations()
            .Validate(
                options => Uri.TryCreate(options.ConnectionString, UriKind.Absolute, out var uri)
                          && (uri.Scheme == "amqp" || uri.Scheme == "amqps"),
                "RabbitMq:ConnectionString must be an absolute amqp:// or amqps:// URI.")
            .ValidateOnStart();

        services
            .AddOptions<WebhookOptions>()
            .Bind(configuration.GetSection(WebhookOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IMessageQueueRepository, MessageQueueRepository>();
        services.AddScoped<IWebhookService, WebhookService>();

        return services;
    }
}