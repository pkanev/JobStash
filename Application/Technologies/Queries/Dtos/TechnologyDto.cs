using JobStash.Application.Common.Mappings;
using JobStash.Domain.Entities;

namespace JobStash.Application.Technologies.Queries.Dtos;

public record TechnologyDto : IMapFrom<Technology>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}
