using Domain.Common;
using Domain.Entities;

namespace Domain.Events.Ads;

public class AdDeletedEvent : BaseEvent
{
    public Ad Ad { get; }

    public AdDeletedEvent(Ad ad)
    {
        Ad = ad;
    }
}
