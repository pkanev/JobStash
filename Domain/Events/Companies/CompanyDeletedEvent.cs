using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Companies;

public class CompanyDeletedEvent : BaseEvent
{
    public Company Company { get; }

    public CompanyDeletedEvent(Company company)
    {
        Company = company;
    }
}
