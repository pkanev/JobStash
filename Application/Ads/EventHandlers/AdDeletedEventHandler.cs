using JobStash.Domain.Events.Ads;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Ads.EventHandlers;

public class AdDeletedEventHandler : INotificationHandler<AdDeletedEvent>
{
    private readonly ILogger<AdDeletedEventHandler> logger;

    public AdDeletedEventHandler(ILogger<AdDeletedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(AdDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
