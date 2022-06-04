using JobStash.Domain.Events.Ads;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Ads.EventHandlers;

public class AdExpiredEventHandler : INotificationHandler<AdExpiredEvent>
{
    private readonly ILogger<AdExpiredEventHandler> logger;

    public AdExpiredEventHandler(ILogger<AdExpiredEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(AdExpiredEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
