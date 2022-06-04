using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobStash.Application.Ads.Queries.Dtos;
using JobStash.Application.Common.Interfaces;
using JobStash.Application.Common.Mappings;
using JobStash.Application.Common.Models;
using MediatR;

namespace JobStash.Application.Ads.Queries.GetAdsForCompanyWithPagination;

public record GetAdsForCompanyWithPaginationQuery : IRequest<PaginatedList<AdBriefDto>>
{
    public int CompanyId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAdsForCompanyWithPaginationQueryRequestHandler : IRequestHandler<GetAdsForCompanyWithPaginationQuery, PaginatedList<AdBriefDto>>
{
    private readonly IApplicationDbContext context;
    private readonly IMapper mapper;

    public GetAdsForCompanyWithPaginationQueryRequestHandler(IApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<PaginatedList<AdBriefDto>> Handle(
        GetAdsForCompanyWithPaginationQuery request,
        CancellationToken cancellationToken) =>
        await context.Ads
            .Where(a => a.CompanyId == request.CompanyId)
            .ProjectTo<AdBriefDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
}
