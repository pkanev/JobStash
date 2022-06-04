using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Ads;

public class AdCreatedEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdCreatedEvent(Ad ad)
    {
        Ad = ad;
    }
}
