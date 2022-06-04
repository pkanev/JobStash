using JobStash.Domain.Events.Technologies;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Technologies.EventHandlers;

public class TechnologyDeletedEventHandler : INotificationHandler<TechnologyDeletedEvent>
{
    private readonly ILogger<TechnologyDeletedEventHandler> logger;

    public TechnologyDeletedEventHandler(ILogger<TechnologyDeletedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(TechnologyDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}