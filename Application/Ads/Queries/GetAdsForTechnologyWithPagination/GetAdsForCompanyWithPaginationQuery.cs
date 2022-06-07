using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Common.Mappings;
using JobStash.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobStash.Application.Ads.Queries.GetAdsForTechnologyWithPagination;

public record GetAdsForTechnologyWithPaginationQuery : IRequest<PaginatedList<AdBriefDto>>
{
    public int TechnologyId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAdsForTechnologyWithPaginationQueryHandler : IRequestHandler<GetAdsForTechnologyWithPaginationQuery, PaginatedList<AdBriefDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAdsForTechnologyWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<AdBriefDto>> Handle(
        GetAdsForTechnologyWithPaginationQuery request,
        CancellationToken cancellationToken) =>
        await context.Ads
            .Include(a => a.Technologies)
            .Where(a => a.Technologies.Any(t => t.Id == request.TechnologyId))
            .ProjectTo<AdBriefDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
}
