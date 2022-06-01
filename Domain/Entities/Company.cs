using Domain.Common;

namespace Domain.Entities;

public class Company : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public Uri? WebPage { get; set; }
    public Uri? CareersPage { get; set; }
    public ICollection<Ad> Ads { get; private set; } = new HashSet<Ad>();
}
