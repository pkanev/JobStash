using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Companies;

public class CompanyCreatedEvent : BaseEvent
{
    public Company Company { get; }
    public CompanyCreatedEvent(Company company)
    {
        Company = company;
    }
}
