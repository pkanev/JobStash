using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Ads;

public class AdDeletedEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdDeletedEvent(Ad ad)
    {
        Ad = ad;
    }
}
