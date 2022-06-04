using JobStash.Domain.Events.Ads;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Ads.EventHandlers;

public class AdCreatedEventHandler : INotificationHandler<AdCreatedEvent>
{
    private readonly ILogger<AdCreatedEventHandler> logger;

    public AdCreatedEventHandler(ILogger<AdCreatedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(AdCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
