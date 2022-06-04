using JobStash.Domain.Common;
using JobStash.Domain.Entities;

namespace JobStash.Domain.Events.Ads;

public class AdPublishedEvent : BaseEvent
{
    public Ad Ad { get; }
    public AdPublishedEvent(Ad ad)
    {
        Ad = ad;
    }
}
