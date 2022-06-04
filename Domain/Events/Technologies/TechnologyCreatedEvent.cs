using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Technologies;

public class TechnologyCreatedEvent : BaseEvent
{
    public Technology Technology { get; }
    public TechnologyCreatedEvent(Technology technology)
    {
        Technology = technology;
    }
}
