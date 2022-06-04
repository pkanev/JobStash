using JobStash.Application.Common.Mappings;
using JobStash.Domain.Entities;

namespace JobStash.Application.Companies.Queries.GetCompaniesWithPagination;

public record CompanyBriefDto : IMapFrom<Company>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
