using Domain.Common;
using Domain.Events.Ads;

namespace Domain.Entities;

public class Ad : BaseAuditableEntity
{
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public Uri WebAddress { get; set; } = null!;

    public ICollection<Technology> Technologies { get; private set; } = new HashSet<Technology>();

    private bool published;
    public bool Published
    {
        get => published;
        set
        {
            if (value == true && published == false)
                AddDomainEvent(new AdPublishedEvent(this));

            published = value;
        }
    }

    private bool expired;
    public bool Expired
    {
        get => expired;
        set
        {
            if (value == true && expired == false)
                AddDomainEvent(new AdExpiredEvent(this));

            expired = value;
        }
    }
}
