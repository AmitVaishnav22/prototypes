using System.Text;
using Microsoft.AspNetCore.Mvc;
using Webhooks.Models;
using Webhooks.Services;

namespace Webhooks.Controllers;

[ApiController]
[Route("newsletter")]
public sealed class WebhookController(IWebhookService webhookService) : ControllerBase
{
    [HttpPost("github-release")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GitHubRelease(
        [FromHeader(Name = "X-Hub-Signature-256")] string? signature,
        [FromHeader(Name = "X-GitHub-Event")] string? eventName,
        [FromHeader(Name = "X-GitHub-Delivery")] string? deliveryId,
        CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var rawBody = await reader.ReadToEndAsync(cancellationToken);

        var result = await webhookService.EnqueueAsync(
            rawBody,
            signature,
            eventName,
            deliveryId,
            cancellationToken);

        return result switch
        {
            EnqueueResult.Success => StatusCode(
                StatusCodes.Status201Created,
                new { deliveryId, Event = eventName }),
            EnqueueResult.InvalidSignature => Unauthorized(),
            _ => BadRequest()
        };
    }
}