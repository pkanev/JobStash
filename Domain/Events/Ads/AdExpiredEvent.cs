using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Ads;

public class AdExpiredEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdExpiredEvent(Ad ad)
    {
        Ad = ad;
    }
}
