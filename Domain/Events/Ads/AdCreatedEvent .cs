using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Ads;

public class AdCreatedEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdCreatedEvent(Ad ad)
    {
        Ad = ad;
    }
}
