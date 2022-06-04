using JobStash.Domain.Events.Technologies;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Technologies.EventHandlers;

public class TechnologyCreatedEventHandler : INotificationHandler<TechnologyCreatedEvent>
{
    private readonly ILogger<TechnologyCreatedEventHandler> logger;

    public TechnologyCreatedEventHandler(ILogger<TechnologyCreatedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(TechnologyCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
