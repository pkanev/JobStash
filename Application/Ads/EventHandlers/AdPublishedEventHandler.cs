using JobStash.Domain.Events.Ads;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Ads.EventHandlers;

public class AdPublishedEventHandler : INotificationHandler<AdPublishedEvent>
{
    private readonly ILogger<AdPublishedEventHandler> logger;

    public AdPublishedEventHandler(ILogger<AdPublishedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(AdPublishedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
