using JobStash.Domain.Events.Companies;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Companies.EventHandlers;

public class CompanyDeletedEventHandler : INotificationHandler<CompanyDeletedEvent>
{
    private readonly ILogger<CompanyDeletedEventHandler> logger;

    public CompanyDeletedEventHandler(ILogger<CompanyDeletedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(CompanyDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
