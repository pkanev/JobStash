using JobStash.Domain.Common;

namespace JobStash.Domain.Entities;

public class Technology : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Ad> Ads { get; private set; } = new HashSet<Ad>();
}
