using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Ads;

public class AdPublishedEvent : BaseEvent
{
    public Ad Ad { get; }
    public AdPublishedEvent(Ad ad)
    {
        Ad = ad;
    }
}
