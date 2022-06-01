using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Ads;

public class AdExpiredEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdExpiredEvent(Ad ad)
    {
        Ad = ad;
    }
}
