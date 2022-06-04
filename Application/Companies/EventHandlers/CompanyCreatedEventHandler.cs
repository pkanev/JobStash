using JobStash.Domain.Events.Companies;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobStash.Application.Companies.EventHandlers;

public class CompanyCreatedEventHandler : INotificationHandler<CompanyCreatedEvent>
{
    private readonly ILogger<CompanyCreatedEventHandler> logger;

    public CompanyCreatedEventHandler(ILogger<CompanyCreatedEventHandler> logger)
    {
        this.logger = logger;
    }

    public Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("JobStash Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
